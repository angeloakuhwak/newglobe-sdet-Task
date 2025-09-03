import http from "k6/http";
import encoding from "k6/encoding";
import { check, sleep, fail } from "k6";
import { Trend } from "k6/metrics";
import { textSummary } from "https://jslib.k6.io/k6-summary/0.0.4/index.js";
import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

function parseEnvFile(text) {
  const out = {};
  for (const line of text.split(/\r?\n/)) {
    const m = line.match(/^\s*([A-Za-z_][A-Za-z0-9_]*)\s*=\s*(.*)\s*$/);
    if (!m) continue;
    out[m[1]] = m[2].replace(/^['"]|['"]$/g, "");
  }
  return out;
}
const local = (() => {
  try {
    return parseEnvFile(open("../.env.local"));
  } catch {
    return {};
  }
})();

const base = (__ENV.API_BASE_URL || local.API_BASE_URL || "").replace(
  /\/$/,
  ""
);
const user = __ENV.BASIC_AUTH_USERNAME || local.BASIC_AUTH_USERNAME || "";
const pass = __ENV.BASIC_AUTH_PASSWORD || local.BASIC_AUTH_PASSWORD || "";
const token = __ENV.OAUTH_TOKEN || local.OAUTH_TOKEN || "";

if (!base) fail("API_BASE_URL is not set (env or ../.env.local).");

//  headers to match the API requests
function guid() {
  // RFC-4122-ish v4
  return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(/[xy]/g, (c) => {
    const r = (Math.random() * 16) | 0,
      v = c === "x" ? r : (r & 0x3) | 0x8;
    return v.toString(16);
  });
}
function authHeader() {
  if (token) return { Authorization: `Bearer ${token}` };
  if (user || pass) {
    const b64 = encoding.b64encode(`${user}:${pass}`);
    return { Authorization: `Basic ${b64}` };
  }
  return {};
}
function requestHeaders() {
  return {
    ...authHeader(),
    Accept: "application/json",
    "User-Agent": "NewGlobeSdetTests/1.0 (Windows)",
    "X-Request-ID": guid(),
  };
}

// k6 options 
export const options = {
  scenarios: {
    smoke: {
      executor: "ramping-vus",
      startVUs: 0,
      stages: [
        { duration: "10s", target: 1 },
        { duration: "30s", target: 3 },
        { duration: "20s", target: 0 },
      ],
      gracefulStop: "5s",
    },
  },
  thresholds: {
    checks: ["rate>0.99"],
    "http_req_failed{endpoint:projects}": ["rate<0.01"],
    "http_req_duration{endpoint:projects}": ["p(95)<800"],
    "http_req_duration{endpoint:features}": ["p(95)<1000"],
  },
};

// metrics
const t_projects = new Trend("projects_duration");
const t_features = new Trend("features_duration");

// ---------- setup: get a usable projectKey ----------
export function setup() {
  const res = http.get(`${base}/projects`, {
    headers: requestHeaders(),
    tags: { endpoint: "projects" },
  });

  check(res, {
    "setup: /projects 200": (r) => r.status === 200,
    "setup: projects not empty": (r) =>
      Array.isArray(r.json()) && r.json().length > 0,
  });

  const list = res.json() || [];
  return { projectKey: (list[0] && list[0].key) || "" };
}

// main iteration
export default function (data) {
  // 1) /projects
  const r1 = http.get(`${base}/projects`, {
    headers: requestHeaders(),
    tags: { endpoint: "projects" },
  });
  t_projects.add(r1.timings.duration);

  check(r1, {
    "GET /projects -> 200": (r) => r.status === 200,
    "projects has items": (r) => Array.isArray(r.json()) && r.json().length > 0,
  });

  // 2) /projects/{key}/features
  if (data.projectKey) {
    const r2 = http.get(`${base}/projects/${data.projectKey}/features`, {
      headers: requestHeaders(),
      tags: { endpoint: "features" },
    });
    t_features.add(r2.timings.duration);

    check(r2, {
      "GET /projects/{key}/features -> 200": (r) => r.status === 200,
      "features is array": (r) => Array.isArray(r.json()),
    });
  }

  sleep(1);
}

export function handleSummary(data) {
  return {
    stdout: textSummary(data, { indent: " ", enableColors: true }),
    "k6/summary.json": JSON.stringify(data, null, 2),
    "k6/summary.html": htmlReport(data),
  };
}
