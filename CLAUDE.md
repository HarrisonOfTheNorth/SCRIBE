# Claude Guidance: Hello World API

## Project Overview

**What**: Hello World API demonstrating blue-chip markdown documentation practices for Claude Code development.

**Why**: Showcase how specification-first development scales from a simple 1-endpoint API to an enterprise system using industry-standard AGENTS.md/CLAUDE.md documentation patterns.

**Tech Stack**:
- Framework: ASP.NET Core Minimal APIs
- Runtime: .NET 8 LTS (support until November 2026)
- Testing: xUnit with parameterized tests
- Platform: macOS with ARM64 native SDK (NOT x64 emulation)

**Scope**: Phase 1 implements single POST `/test/hello` endpoint returning JSON response. Grows to 10+ endpoints in Phase 3.

---

## Getting Started for Claude

### Quick Context
This project uses specification-first development: all requirements are documented in markdown before code generation. The `/docs` directory contains detailed specifications and guides.

### Key Terminal Commands
```bash
dotnet build              # Compile the project
dotnet run               # Start the API server (listens on http://localhost:5000)
dotnet test              # Run all xUnit tests
dotnet test --filter "TestClass" --verbosity normal  # Run specific test with output
```

### Global .NET Configuration
- **File**: `global.json`
- **Purpose**: Specifies exact .NET 8 SDK version
- **Importance**: Ensures consistency across team and CI/CD
- **macOS Consideration**: Verify ARM64 native with `dotnet --info` (do NOT use x64 emulation)

### Documentation Navigation
- See [API Design Principles](./docs/specs/api-design.md) for endpoint contracts
- See [Getting Started Guide](./docs/guides/getting-started.md) for detailed setup
- See [Adding Endpoints Workflow](./docs/guides/adding-endpoints.md) for development process

---

## Coding Standards

### C# and .NET Conventions
- **Naming**: PascalCase for classes, interfaces, public members; camelCase for local variables
- **Async/Await**: Always use `async Task` for Azure Functions and HTTP handlers; use `Task<T>` for methods returning values
- **File Organization**: One public class per file; related internal classes may share a file
- **Using Statements**: Alphabetically ordered at top of file

### Testing with xUnit
- **Test Class Naming**: `{FeatureName}Tests` or `{ClassName}Tests`
- **Test Method Naming**: `{MethodName}_{Scenario}_{Expected}` (e.g., `PostHello_WithValidRequest_ReturnsOkWithMessage`)
- **Parameterized Tests**: Use `[Theory]` with `[InlineData]` for multiple test cases
- **Assertions**: Use xUnit's `Assert` class for clear, fluent assertions
- **Setup/Teardown**: Use constructor for setup (IDisposable pattern for teardown)

### Error Handling
- Follow error response format defined in [Error Handling Specification](./docs/specs/error-handling.md)
- All unhandled exceptions should return 500 with `INTERNAL_SERVER_ERROR` code
- Validation failures should return 400 with `INVALID_REQUEST` code

### Git Workflow
- Commit messages are descriptive: "Add POST /test/hello endpoint" not "Update code"
- One logical change per commit
- Push to main branch directly for Phase 1 (no PR ceremony for single-endpoint demo)

---

## Key Workflow: Adding New Endpoints

### Step 1: Write Specification
Create new markdown file in `/docs/specs/{endpoint-name}.md` following the pattern in `hello-endpoint.md`:
- **Endpoint**: HTTP method + path
- **Purpose**: One-line description
- **Request Contract**: Method, URL, headers, body structure
- **Response Contract - Success**: HTTP status, JSON structure with example
- **Response Contract - Error**: HTTP status, error response format
- **Examples**: Real curl/Postman commands with expected output

### Step 2: Reference in Documentation
- Add entry to [API Design - Endpoint Index](./docs/specs/api-design.md)
- Link to new spec file
- Mark status: Active, Planned, or Deprecated

### Step 3: Implement Feature
- Create handler class in `/src` following existing patterns
- Add route handler in Program.cs or dedicated feature file
- Use async/await throughout
- Follow C# naming conventions above

### Step 4: Write Tests
- Create test file in `/tests/{FeatureName}Tests.cs`
- Use `[Theory]` with `[InlineData]` for multiple scenarios
- Test success case (200), error cases (400, 500), and edge cases
- Run `dotnet test` to verify all tests pass

### Step 5: Update Guides (Optional)
- If the endpoint requires special deployment or configuration, add to [Guides](./docs/guides/)
- If architectural decision was needed, add ADR to `/docs/adr/`

---

## Important Gotchas

### macOS ARM64
- Always verify `dotnet --info` shows ARM64 architecture, NOT x86_64
- x64 emulation causes 30-50% performance degradation
- If emulation detected, reinstall .NET 8 SDK for Apple Silicon

### .NET 8 SDK Version
- `global.json` specifies exact SDK version (e.g., 8.0.101)
- Running on different SDK version may cause subtle compatibility issues
- Keep SDK regularly updated within 8.0.x series

### JSON Response Format
- All endpoints must return JSON (not plain text or HTML)
- Use consistent error envelope across all endpoints (see [Error Handling Spec](./docs/specs/error-handling.md))
- Test with `curl` or Postman to verify JSON structure before deployment

### xUnit Async Tests
- Always mark async test methods as `public async Task`, NOT `public async void`
- Exceptions in `async void` methods are uncatchable; use `async Task` instead
- Framework will automatically await `Task` return types

---

## Links to Detailed Documentation

### Specifications
- [Hello Endpoint Contract](./docs/specs/hello-endpoint.md) — POST /test/hello specification
- [API Design Principles](./docs/specs/api-design.md) — Endpoint design standards
- [Error Handling Specification](./docs/specs/error-handling.md) — Error response format and codes
- [Technology Choices](./docs/specs/technology-choices.md) — Framework and platform decisions

### Guides
- [Getting Started](./docs/guides/getting-started.md) — Setup and first run
- [Adding Endpoints](./docs/guides/adding-endpoints.md) — Workflow for new endpoints

### Architecture Decisions
- [ADR-001: Minimal APIs](./docs/adr/adr-001-minimal-apis.md) — Why Minimal APIs over MVC

---

## Document Metadata

- **Version**: 1.0 (Phase 1)
- **Last Updated**: 2026-02-17
- **Audience**: Claude Code agents implementing this specification
- **Related**: See `README.md` for human-oriented quick start
