# Proposal

**Goal**  
Validate the NewGlobe Feature Toggle platform with a layered automation approach that provides fast feedback on correctness, reliability, performance, and basic security:

- **Unit** (tiny helpers/builders)
- **API** (contract-leaning, happy-path smoke of read endpoints.)
- **UI** (Playwright login + Projects smoke)
- **Performance** (k6 smoke of `GET /projects`)
- **Security** (OWASP ZAP baseline scan.)

**Why these tools**

- **xUnit** + **FluentAssertions**: de-facto .NET testing stack, fast, readable.
- **Playwright (C# / NUnit runner)**: reliable cross-browser, Azure-friendly, reliable auth flow,riche traces, easy CI integration.
- **k6**: code-as-load-test, tiny & scriptable; good for smoke/perf budgets.
- **ZAP**: industry standard dynamic baseline; quick feedback on common issues.
- **GitHub Actions**: simple CI that anyone can run by forking.

**Strategy**

1. Keep tests data-independent by discovering a project via `GET /projects`.
2. Assert structure (status codes & minimal fields) rather than brittle values.
3. Fail fast in CI, publish artifacts (TRX, traces, ZAP report).
4. Docker Compose allows local k6/ZAP without polluting the host.

**Deliverables**  
This repo includes: solution skeleton, unit & API tests, one Playwright test, a k6 script, ZAP config, Docker Compose, and CI YAML, plus a README with run instructions and explanations.

**Test sequencing (when each runs)**
Stage What runs Purpose

---

## PR Unit, API smoke, UI smoke (Chromium headless), SCA/SAST Gate regressions quickly

## Nightly API smoke, UI smoke (browser matrix if desired), k6 smoke, ZAP baseline Deeper signal without blocking PRs

## Pre-release Targeted perf ramp + ZAP baseline (stricter fail) Final confidence on prod-like env

**Data & state management**

- No mutations in shared envs; tests are read-only.

- Project keys are discovered at runtime from /projects.

- If deterministic data becomes necessary, plan to introduce a seeded test project or a fixture endpoint (documented but out-of-scope for now).

**Negative & edge checks (API)**

Even in a smoke suite, include a few quick non-happy checks:

- /projects/{unknown} → 404.

- /features/status without required params → 200/400 depending on API contract (assert defined behavior).

- Basic retry/backoff policy left to clients (documented assumption).

**UI scope & reliability**

- Flow: navigate to app → AAD login (email, password, “Stay signed in?”) → Projects dashboard.

- Selectors: resilient role/label regexes; avoid brittle CSS. Prefer getByRole, visible-text fallbacks.

- Stability: single Chromium run in PRs; optional browser matrix nightly (Chromium/Firefox/WebKit).

- Accessibility smoke (optional): snapshot or axe-core quick run on Projects to catch obvious issues.

- Flakiness policy: Retry=2 on UI smoke; mark/quarantine persistent flakes and ticket.

**Performance (k6 smoke) – explicit thresholds**

Target (for /projects in test env):

- http_req_failed < 1%

- p(95) < 800ms

- p(99) < 1500ms

- avg < 400ms
  Publish k6 summary as CI artifact; fail the job if thresholds are exceeded.

**Security posture**

ZAP Baseline against UI base URL, with a short max-duration; produce HTML report.

Findings policy: fail on High nightly; warn on others.

Add SAST/SCA: CodeQL (C#/JS) + dependency audit (NuGet/npm) to catch supply chain issues.

**CI & artifacts**

- Separate jobs for API, UI (C# Playwright), k6, ZAP.

- Cache NuGet/npm where possible.

- Required secrets (from .env.example):
  API_BASE_URL, BASIC_AUTH_USERNAME, BASIC_AUTH_PASSWORD, OAUTH_TOKEN (optional), UI_BASE_URL, UI_USERNAME, UI_PASSWORD.

**Artifacts:**

- API: TRX + coverage (Cobertura).

- UI: Playwright traces, screenshots, HTML report.

- k6: JSON/text summary.

- ZAP: HTML report.

**Observability & post-merge (future)**

- Lightweight synthetic check (GET /projects every 5m) with alert on sustained failures.

- Error budget for availability of read endpoints; link CI evidence in incidents.

**Deliverables**

This repo includes:

- API & Unit tests (xUnit + FluentAssertions).

- UI smoke (Playwright C# with NUnit runner) handling MS AAD login.

- k6 smoke for /projects with thresholds.

- ZAP baseline config and rules.

- GitHub Actions CI (jobs per layer) with artifact upload.

- README + .env.example + basic Docker Compose helpers.

**Out-of-scope / Future work**

- Write endpoints (create/update/targeting rules), deep rule engines.

- Full security assessment (DAST+SAST hardening, authz fuzzing).

- Consumer-driven contract tests (e.g., Pact) for /projects and /features/status.

- Broader browser/device matrix, visual regression, and accessibility depth.
