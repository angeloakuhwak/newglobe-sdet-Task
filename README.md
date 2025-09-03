# NewGlobe SDET Solution

End-to-end validation of the Feature Toggle service:

- .NET **API tests** with xUnit
- **Playwright (C#)** UI smoke (optional TS/JS UI smoke if that folder exists)
- **k6** performance smoke
- **OWASP ZAP** baseline scan
- **GitHub Actions** CI

---

**Repo Layout**
newglobe-sdet-solution/
├─ docs/
│ ├─ Proposal.md
│ └─ TestPlan.md
├─ tests/
│ └─ FeatureToggle.ApiTests/
│ ├─ ApiClient.cs
│ ├─ Builders.cs
│ ├─ ProjectTests.cs
│ ├─ FeatureToggleTests.cs
│ ├─ SearchTests.cs
│ └─ UnitTests.cs
├─ ui-tests-csharp/
│ ├─ UiTests.csproj
│ ├─ Support/Env.cs, MsLogin.cs
│ └─ Tests/PlaywrightLoginFixture.cs, ProjectsSmoke.cs
├─ k6/
│ └─ smoke.js
├─ .zap/
│ ├─ baseline.yaml
│ └─ rules.tsv
├─ .github/workflows/
│ └─ ci.yml
├─ docker-compose.yml
├─ .env.example
└─ README.md

---

**Prerequisites**

- .NET 8 SDK

- Node.js 20+ (only if running the TS/JS UI suite)

- Docker Desktop (for ZAP / k6 via containers)

- k6 (optional if not using Docker)

  1. Windows: install from https://grafana.com/k6 (MSI)

  2. macOS: brew install k6

  3. Linux: follow k6 docs

## Quick Start

1. **Clone** and create your `.env`:

copy .env.example .env # PowerShell (or: Copy-Item .env.example .env)

### Linux/macOS: cp .env

Fill these values in .env:

# API auth (use either BASIC or OAUTH)

API*BASE_URL=https://softwaredeveloperintesttechnicaltestapi-test.bridgeinternationalacademies.com
BASIC_AUTH_USERNAME=bridge_api_user
BASIC_AUTH_PASSWORD=\*\*\_redacted*\*\*

# or:

# OAUTH_TOKEN=eyJ...

# UI (Azure AD login used by the UI smoke)

UI_BASE_URL=https://softwaredeveloperintesttechnicaltest-test.bridgeinternationalacademies.com
UI_USERNAME=candidate-2@bridgeintacademiestest.onmicrosoft.com
UI_PASSWORD=**value**

### Run Locally

.NET API tests

```bash
dotnet restore
dotnet test

```

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

Add these Repository secrets:

API_BASE_URL,
BASIC_AUTH_USERNAME,
BASIC_AUTH_PASSWORD (or OAUTH_TOKEN)

UI_BASE_URL,
UI_USERNAME,
UI_PASSWORD

On every push/PR, the pipeline will:

Run .NET tests (artifacts: TRX + coverage)

Run Playwright (artifacts: traces/screenshots)

Run k6 smoke

Run ZAP baseline (artifact: HTML report)

### Notes / Assumptions

Tests discover a project key dynamically to avoid brittle data.

API assertions validate status codes and minimal shape for stability.

UI suite is a single smoke to keep it reliable and fast.

### Why these tools?

See docs/Proposal.md.

(Optional) Solution file at root
If you haven’t already, include a solution that references the test project.

Create NewGlobe.SDET.sln:

powershell

```
dotnet new sln -n NewGlobe.SDET
dotnet sln add .\tests\FeatureToggle.ApiTests\FeatureToggle.ApiTests.csproj
```

### How to run everything locally (step-by-step)

Install prerequisites

.NET 8 SDK

Node 20+

(Optional) k6

Docker Desktop (for ZAP/k6 via compose)

Env vars

Copy .env.example → .env

Populate with the provided credentials and URLs.

- API tests

```bash
dotnet restore
dotnet test
```

- UI smoke

````bash
# Build first so Playwright scripts are generated
dotnet build ui-tests-csharp/UiTests.csproj -c Debug

# Install browsers for C# Playwright (Windows PowerShell):
powershell -ExecutionPolicy Bypass -File ui-tests-csharp\bin\Debug\net8.0\playwright.ps1 install

# Run tests
dotnet test ui-tests-csharp/UiTests.csproj -c Debug
```
### k6 smoke With k6 locally:

- k6 smoke

```bash
k6 run k6\smoke.js
```

# or:

```bash
docker compose up k6 --abort-on-container-exit
```

- ZAP baseline

```bash
docker compose up zap --abort-on-container-exit
```

Push to GitHub

Commit and push; CI will run and produce artifacts in the Actions tab.
````
