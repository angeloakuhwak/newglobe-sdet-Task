# Test Plan — NewGlobe Feature Toggle Platform

> **Purpose:** Provide fast, layered feedback on correctness, reliability, performance, and baseline security with minimal brittleness and clear, automatable gates.

---

## 1) Objectives & Quality Goals

- **Correctness/Contract:** key read endpoints behave and response shape is stable.
- **User-flow reliability:** user can sign in and see **Projects**.
- **Performance (smoke):** baseline latency/availability is acceptable.
- **Security (baseline):** obvious dynamic web risks are surfaced early.
- _(Aligned to Proposal “Goal / Strategy / Sequencing”.)_

---

## 2) Scope (In)

### API (read-only)

- `GET /projects`
- `GET /projects/{key}`
- `GET /projects/{key}/features`
- `GET /projects/{key}/features/status`
- _(Global)_ `GET /features/status` _(presence/behavior check)_

### UI

- AAD login flow and Projects dashboard smoke _(Playwright C#)_.

### Non-functional

- **k6** smoke of `GET /projects` _(latency & error rate only)_.
- **OWASP ZAP** baseline scan against the UI base URL.

_*(Scope mirrors the “Scope” in your previous plan; expanded with explicit endpoints and flows.)*_

---

## 3) Out of Scope

- Write operations _(POST/PATCH/DELETE)_, targeting rules, analytics.
- Full security assessment _(active scan, authenticated DAST, pen-testing)_.
- Broad browser/device matrix, visual regression, deep accessibility.
- Contract tests with consumer-driven tooling _(e.g., Pact)_ — **future**.

_*(As per Proposal/Plan “Out-of-scope”.)*_

---

## 4) Test Approach

### 4.1 Unit (Helpers/Builders)

- **Tech:** xUnit + FluentAssertions _(C#)_.
- **Targets:** object builders and tiny utilities used by API tests.
- **Pass criteria:** assertions on fields, defaults, simple invariants.

### 4.2 API (Contract-leaning Smoke)

- **Tech:** xUnit + FluentAssertions; `ApiClient` supports **Basic** or **Bearer** from env.
- **Data strategy:** discover a valid `projectKey` via `GET /projects` at runtime to avoid brittle fixtures.
- **Happy-path checks:**
  - Status **200**
  - Body is a JSON array/object with minimal required fields _(e.g., `key`, `name`)_.
- **Negative/boundary checks:**
  - `GET /projects/{unknown}` ⇒ **404**.
  - `GET /features/status` without required params ⇒ stable, documented behavior _(200/400)_ — assert as observed.
  - No-auth call to `/projects` ⇒ **401/403**.
- **Non-goals:** deep schema diffing, pagination/range, data mutation.

### 4.3 UI (Playwright C# – Login + Projects Smoke)

- **Tech:** Playwright for .NET _(C#)_, **NUnit** runner for UI tests.
- **Flow:** navigate to app → AAD _(email/password/“Stay signed in?”)_ → Projects dashboard signal _(flexible role/text selectors)_.
- **Stability:** resilient locators (`getByRole`, visible text, regex fallbacks); helper handles AAD; retries enabled `[Retry(2)]`.
- **Pass signal (any):** Projects heading/link visible **or** “PROJECT KEY” label visible **and** app origin reached.
- **Artifacts:** screenshots/traces on failure; Playwright HTML report.

_*(Exactly as described in Proposal “UI scope & reliability”.)*_

### 4.4 Performance (k6 Smoke)

- **Scenario:** `GET /projects` using discovered `projectKey`s _(setup step calls `/projects` once)_.
- **Stages:** tiny 3-stage ramp _(e.g., 1→3 VUs over ~1 min)_.
- **Thresholds (gate):**

**_http_req_failed < 1%_**
**_http_req_duration{endpoint:projects}: p(95) < 800ms, p(99) < 1500ms, avg < 400ms_**

- **Outputs:** console summary + JSON summary uploaded in CI.

_*(Thresholds per Proposal “Performance (k6 smoke) – explicit thresholds”.)*_

### 4.5 Security (ZAP Baseline)

- **Mode:** unauthenticated baseline _(passive scan)_; short max duration _(e.g., `-m 5`)_.
- **Report:** HTML + JSON artifacts.
- **Policy:**
- **PRs:** informational only _(do not fail the PR)_.
- **Nightly:** fail on **High**; warn on **Medium/Low** _(use `rules.tsv`/allowlist where justified)_.
- **Notes:** Some demo assets _(Swagger, CSP)_ may raise expected warnings; document waivers in `.zap/rules.tsv`.

_*(As in Proposal "Security posture / findings policy".)*_

---

## 5) Environments & Access

- **Endpoints & creds from `.env`** _(also mirrored as GitHub Actions secrets)_:
- `API_BASE_URL`
- `BASIC_AUTH_USERNAME`
- `BASIC_AUTH_PASSWORD` _(or `OAUTH_TOKEN`)_
- `UI_BASE_URL`
- `UI_USERNAME`
- `UI_PASSWORD`
- **Auth:** Basic or OAuth _(API)_; **AAD interactive** _(UI)_.
- **State:** tests are read-only; no data mutations.

_*(Per Proposal “Data & state management / Required secrets”.)*_

---

## 6) Risks & Mitigations

- **Volatile data** → dynamic discovery of `projectKey`.
- **Auth variability** → `ApiClient` supports Basic/Bearer; UI AAD helper handles common flows.
- **UI fragility** → role-based selectors and multiple “ready” signals.
- **Flakes** → retries _(UI)_, network-idle waits, screenshots/traces.

_*(As already captured in Proposal & prior Test Plan.)*_

---

## 7) Entry & Exit Criteria

### Entry

- Test env reachable.
- Secrets present _(local `.env` or CI secrets)_.
- Playwright browsers installed _(CI: `npx playwright install --with-deps`)_.

### Exit (PR Gate)

- **API:** all green; key endpoints return **200** with minimal fields.
- **UI:** smoke passes _(Projects visible after login)_.
- **k6:** thresholds met.
- **ZAP (PR):** report produced _(no gate)_.
- **Artifacts uploaded:** TRX + coverage, Playwright report/traces, k6 summary, ZAP HTML.

### Exit (Nightly)

- All above **plus** ZAP: **no High alerts** _(job fails on High)_.

_*(Exit criteria align with Proposal/Nightly policy.)*_

---

## 8) CI/CD & Sequencing

### On every PR/push

1. **Unit + API** → `dotnet test`, upload TRX/coverage.
2. **UI smoke** → Chromium headless; upload report/traces.
3. **k6 smoke** → upload JSON summary.
4. **ZAP baseline** → generate HTML/JSON; informational.

### Nightly

- Same, optionally add **browser matrix** _(Chromium/Firefox/WebKit)_ for UI, and enforce ZAP **fail on High**.

_*(Sequencing mirrors Proposal “Test sequencing”.)*_

---

## 9) Reporting & Triage

- **API:** TRX + Cobertura coverage _(Artifacts)_.
- **UI:** Playwright HTML report, screenshots, traces _(Artifacts)_.
- **k6:** JSON/Text summary _(Artifacts)_.
- **ZAP:** HTML + JSON _(Artifacts)_.
- **Defect filing:** include failing job link + artifacts; attach ZAP evidence snippet and k6 percentile rows.

---

## 10) Tools & Versions _(at time of submission)_

- .NET 8 SDK, xUnit + FluentAssertions
- Playwright for .NET **1.45** _(NUnit runner for UI tests)_
- Node **20+** _(for Playwright install step)_
- **k6** CLI _(or Docker)_
- **OWASP ZAP** _(Docker image `ghcr.io/zaproxy/zaproxy:stable`)_
- **GitHub Actions** CI

---

## 11) Local Execution (summary)

````bash
# 1) Setup
cp .env.example .env  # then fill values


.NET API tests

```bash
dotnet restore
dotnet test

````

Results: \*_/TestResults/_.trx and coverage.cobertura.xml (when run in CI).

### UI smoke (Playwright)

````bash
# Build first so Playwright scripts are generated
dotnet build ui-tests-csharp/UiTests.csproj -c Debug

# Install browsers for C# Playwright (Windows PowerShell):
powershell -ExecutionPolicy Bypass -File ui-tests-csharp\bin\Debug\net8.0\playwright.ps1 install

# Run tests
dotnet test ui-tests-csharp/UiTests.csproj -c Debug
```
### k6 smoke With k6 locally:

```bash
k6 run k6/smoke.js | tee k6-results.txt
```

Or via Docker:

```bash
docker compose up k6 --abort-on-container-exit
```

### ZAP baseline Via Docker:

```bash
powershell docker pull ghcr.io/zaproxy/zaproxy:stable
```

```bash
powershell $Env:UI_BASE_URL = "https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com"
```

```bash
powershell

docker run --rm -v "${wrk}:/zap/wrk" ghcr.io/zaproxy/zaproxy:stable `
zap-baseline.py `
-t "$Env:UI_BASE_URL" `
-m 5 `
-a `
-r zap-baseline.html `
-J zap-baseline.json `
-w rules.tsv
````

Or

```bash
docker compose up zap --abort-on-container-exit
```

Report: zap-baseline-report.html (generated in the container working dir).

### CI (GitHub Actions)

**Add these Repository secrets:**

- API_BASE_URL,
- BASIC_AUTH_USERNAME,
- BASIC_AUTH_PASSWORD (or OAUTH_TOKEN)

- UI_BASE_URL,
- UI_USERNAME,
  -UI_PASSWORD

**On every push/PR, the pipeline will:**

- Run .NET tests (artifacts: TRX + coverage)

- Run Playwright (artifacts: traces/screenshots)

- Run k6 smoke

-Run ZAP baseline (artifact: HTML report)

### Future Work

- Broader negative tests (rate limits, invalid filters), pagination.

- Contract tests (e.g., Pact) for /projects and /features/status.

- Deeper perf (ramp patterns, backoff), authenticated ZAP, SAST/SCA (CodeQL + npm/NuGet audit).

("Out-of-scope / Future work" per Proposal.)
