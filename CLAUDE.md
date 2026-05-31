# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this project is

FormulaForge is a proof-of-concept spreadsheet computation engine with a custom DSL (FormulaScript). Users connect data sources (REST API, database, CSV/XLS upload, or a push endpoint), write FormulaScript code in a Monaco editor, and inspect the resulting scalars and time series.

The UI design target is in `mockups/` (HTML files + screenshots) — dark orange/black theme, French language, four main views: Projets, Éditeur, Connecteurs, Time series.

## Running the project

Prerequisites: set user secrets in `FormulaForge.AppHost` (see below), and run `npm install` once in `FormulaForge.Web` (Monaco editor is copied from node_modules to wwwroot at build time).

```bash
# One-time setup
cd src/FormulaForge/FormulaForge.Web && npm install && cd ../../..

# Start everything (Aspire orchestrates API + Web + PostgreSQL)
dotnet run --project src/FormulaForge/FormulaForge.AppHost
```

The Aspire dashboard shows URLs for the Web frontend and API service.

### Required user secrets (set in FormulaForge.AppHost)

```bash
cd src/FormulaForge/FormulaForge.AppHost
dotnet user-secrets set "Parameters:encryption-key" "<key from GenerateEncryptionKey.fsx>"
dotnet user-secrets set "Parameters:pocket-id-client-id" "<oidc-client-id>"
dotnet user-secrets set "Parameters:pocket-id-client-secret" "<oidc-client-secret>"
```

Authentication uses OpenID Connect via Pocket ID.

## Running tests

```bash
# All engine tests
dotnet test src/FormulaForge/FormulaForge.Engine.Tests

# Single test class
dotnet test src/FormulaForge/FormulaForge.Engine.Tests --filter "FullyQualifiedName~ScriptInterpreterTests"
```

Tests use xUnit. There are no integration tests — only engine unit tests.

## EF Core migrations (run from the Postgres persistence project)

```bash
cd src/FormulaForge/FormulaForge.ApiService.Persistence.Postgres
dotnet ef migrations add <MigrationName>
```

Migrations are applied automatically at API startup.

## Architecture

```
FormulaForge.AppHost          .NET Aspire host — orchestrates all services + PostgreSQL
FormulaForge.Web              Blazor Server (InteractiveServer render mode)
FormulaForge.ApiService       ASP.NET Core Minimal API (CRUD for projects, sample data endpoint)
FormulaForge.Domain           Entity models (Project, scalar/timeseries value entities)
FormulaForge.Domain.Services  IProjectManagementService — thin service layer over the repository
FormulaForge.ApiService.Persistence          IProjectRepository interface
FormulaForge.ApiService.Persistence.Postgres EF Core + PostgreSQL implementation
FormulaForge.Engine           FormulaScript parser + interpreter + TimeSeries primitives
FormulaForge.Engine.Tests     xUnit tests for the engine
externals/EasyParsing          Parser combinator library (local submodule, used by Engine)
```

The Web project does **not** call the ApiService over HTTP — it injects `IProjectManagementService` directly (both projects share the Postgres persistence layer via DI wired in `FormulaForge.Web`'s `Program.cs`... actually wait, let me re-check). Actually the Web project references `FormulaForge.ApiService.Persistence.Postgres` directly and registers `PostgresProjectRepository` and `ProjectManagementService` in its own DI container.

## FormulaScript engine

**Parser** (`FormulaForge.Engine/DomainSpecificLanguage/`): built with EasyParsing (monadic parser combinators). Entry point is `FormulaProgramScriptParser.ProgramParser`, which returns `AstNode[]`. Lines that fail to parse become `InvalidLine` nodes rather than hard errors.

**AST nodes** (`Ast/AstNode.cs`): `StatementNode.VariableAssignmentExpressionNode`, `StatementNode.FunctionDeclarationNode`, `ComputedExpressionNode` (arithmetic tree via EasyParsing `BinaryOperationOperand<T>`), `FunctionCallExpressionNode`, `LiteralExpressionNode` (constant or variable reference).

**Interpreter** (`Runtime/ScriptInterpreter.cs`): walks the AST and writes results into a `Scope`. Supports `+`, `-`, `*`, `/` on scalars and time series (including scalar×series broadcasting). Functions are first-class: declared with `name(a, b) = expression`, called normally.

**Runtime values** (`Runtime/RuntimeVariableValue.cs`):
- `RuntimeScalarValue` wraps `ScalarValueNode` (Integer, Decimal, Boolean)
- `RuntimeTimeSeriesValue` wraps `TimeSeries<ScalarValueNode>`

**Context** (`Contexts/IDslContext.cs` / `DslContextBase`): consumers subclass `DslContextBase` and override `CreateGlobalScope()` to inject pre-loaded variables (input scalars and time series) into `Scope.BuiltInVariables`. Built-in variables cannot be overwritten by user code.

**TimeSeries** (`Time/TimeSeries.cs`): `SortedList<Period, T>` with overlap-aware `Add`. `Period` is [InclusiveStart, ExclusiveEnd). Helper methods: `GetDays()`, `GetMonths()`, `GetYears()`. Arithmetic extensions (`Add`, `Subtract`, `Multiply`, `Divide`) align by period.

## Blazor Web UI

- **Render mode**: `InteractiveServer` (declared globally on `<Routes>` in `App.razor`, and also locally on `ComputationGrid`).
- **UI library**: Microsoft Fluent UI (`Microsoft.FluentUI.AspNetCore.Components`). Use `<Fluent*>` components.
- **Monaco editor**: `MonacoEditor.razor` is a Blazor wrapper around Monaco. The JS interop is in `wwwroot/monacoInterop.js`. The `formulaforge` language is registered with syntax highlighting and `#` line-comment support.
- **ComputationGrid.razor**: spreadsheet-style editable grid for displaying input/output time series (columns = months, rows = series names) plus a scalar card strip.

## Data model

`Project` owns typed scalar and time series collections stored as separate EF tables (`DecimalScalarValueEntity`, `IntegerTimeSeriesEntity`, etc.). Each time series entry stores `Start` and `End` (`DateTimeOffset`, forced to UTC by a global value converter).

`DataSourceSpec` is an abstract record with one concrete type so far: `RestApiDataSourceSpec`. Connector types shown in the mockups (CSV, DB, PUSH) are not yet implemented in domain entities.

## Design target

The mockups in `mockups/` define the target UI. Key conventions visible there:
- French language throughout
- Dark background (`#1a1a1a`-ish) with orange accent (`#e85d04` / `#f97316`)
- Source type badges: `CSV` (green), `API` (blue), `DB` (purple), `PUSH` (orange)
- Editor page layout: Inputs panel → Code panel (Monaco) → Console/Problems/Variables tabs → Outputs panel
- Projects list is a full table with source badges, run status, and timing
