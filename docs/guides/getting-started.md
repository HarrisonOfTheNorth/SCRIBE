# Getting Started with Hello World API

This guide will help you set up and run the Hello World API project on your local machine. Follow these steps to get from zero to running.

---

## Prerequisites

Before you begin, ensure you have the following installed:

### Required Software

- **macOS** with Apple Silicon (ARM64) or Intel processor
- **.NET 8 SDK** or later (LTS version)
- **Git** for version control
- **curl** or **Postman** for testing API endpoints

### Verify Prerequisites

Check if you have .NET 8 installed:

```bash
dotnet --info
```

Expected output (note the SDK version):
```
.NET SDK: 8.0.405
Architecture: arm64 (or x86_64 on Intel)
OS Version: Darwin (macOS)
```

If you see version 6.0.x, 7.x.x, or 9.x.x, you need to install .NET 8 LTS.

**Download .NET 8 from**: https://dotnet.microsoft.com/download/dotnet/8.0

### Install .NET 8 (macOS)

If not already installed:

1. Visit https://dotnet.microsoft.com/download/dotnet/8.0
2. Download the **macOS ARM64** installer (for Apple Silicon Macs)
3. Run the installer
4. Verify installation with `dotnet --info`

---

## Quick Start

Get the project running in 5 minutes:

### Step 1: Clone or Download the Project

```bash
cd /path/to/your/workspace
git clone <repository-url> hello-world-api
cd hello-world-api
```

Or if you have the code already:

```bash
cd hello-world-api
```

### Step 2: Verify Project Structure

The project should have this structure:

```bash
ls -la
```

Expected directories and files:
```
global.json                    # .NET 8 SDK version lock
hello-world-api.sln          # Solution file
src/                          # Source code
  ├── Program.cs              # Main application entry point
  └── HelloWorldApi.csproj     # Source project file
tests/                        # Test suite
  ├── HelloEndpointTests.cs    # Endpoint tests
  └── HelloWorldApi.Tests.csproj # Test project file
docs/                         # Documentation
  └── specs/                  # API specifications
```

### Step 3: Build the Project

```bash
dotnet build
```

Expected output:
```
Build succeeded.
    2 Warning(s)
    0 Error(s)
```

### Step 4: Run All Tests

```bash
dotnet test
```

Expected output:
```
Passed!  - Failed:     0, Passed:    13, Skipped:     0, Total:    13
```

### Step 5: Start the API Server

```bash
dotnet run --project src
```

Expected output:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
```

The server is now running and ready to accept requests. Leave this terminal open.

---

## Project Setup / Installation

### Step 1: Create Project Directory

```bash
mkdir -p ~/projects/hello-world-api
cd ~/projects/hello-world-api
```

### Step 2: Obtain the Code

**Option A: Clone from Git**
```bash
git clone <repository-url> .
```

**Option B: Download ZIP**
Download the project as ZIP and extract to `~/projects/hello-world-api`

### Step 3: Verify Directory Structure

Check that all required files exist:

```bash
# Verify essential files
test -f global.json && echo "✓ global.json found"
test -f hello-world-api.sln && echo "✓ Solution file found"
test -d src && echo "✓ src directory found"
test -d tests && echo "✓ tests directory found"
```

Expected output:
```
✓ global.json found
✓ Solution file found
✓ src directory found
✓ tests directory found
```

### Step 4: Restore NuGet Packages

```bash
dotnet restore
```

This downloads all required NuGet packages. Expected output:
```
Restored /path/to/HelloWorldApi.csproj
Restored /path/to/HelloWorldApi.Tests.csproj
```

### Step 5: Build the Project

```bash
dotnet build
```

This compiles the C# code. Expected output:
```
Build succeeded.
    2 Warning(s)
    0 Error(s)

Time Elapsed 00:00:35.49
```

---

## Running the Application

### Start the API Server

```bash
dotnet run --project src
```

### Expected Output

When the server starts successfully, you'll see:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://localhost:5000
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to exit
```

### Where the API Listens

The API is accessible at:
- **Base URL**: `http://localhost:5000`
- **Hello Endpoint**: `http://localhost:5000/test/hello`
- **Swagger UI** (if enabled): `http://localhost:5000/swagger/index.html`

### Stop the Server

To stop the running server, press **Ctrl+C** in the terminal.

### Common Startup Issues

**Issue**: Port 5000 already in use
```
Address already in use
```

**Solution**: Either stop the other process using port 5000, or specify a different port:
```bash
dotnet run --project src -- --urls "http://localhost:5001"
```

---

## Testing the Application

### Test the Hello Endpoint

The Hello World API provides a single endpoint for Phase 1: **POST /test/hello**

#### Using curl

Open a new terminal (keep the server running) and test:

```bash
curl -X POST http://localhost:5000/test/hello \
  -H "Content-Type: application/json"
```

**Expected response**:
```json
{"message":"hello world"}
```

**Status code**: 200 OK

#### Using curl (silent mode with pretty output)

```bash
curl -s -X POST http://localhost:5000/test/hello | jq
```

**Expected output**:
```json
{
  "message": "hello world"
}
```

#### Using Postman

1. Open Postman
2. Click **New Request**
3. Set:
   - Method: **POST**
   - URL: `http://localhost:5000/test/hello`
   - Header: `Content-Type: application/json`
4. Click **Send**
5. Verify response shows `{"message":"hello world"}` with status 200

### Verify Response Format

Check that the response:
- Has HTTP status code **200 OK**
- Has `Content-Type: application/json` header
- Contains JSON: `{"message":"hello world"}`
- No error field in response

### Endpoint Specification

For complete details about the endpoint, see [Hello Endpoint Specification](../specs/hello-endpoint.md)

---

## Running Tests

### Run All Tests

```bash
dotnet test
```

**Expected output**:
```
Test run for /path/to/HelloWorldApi.Tests.dll (.NETCoreApp,Version=v8.0)
VSTest version 17.11.1 (arm64)

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:    13, Skipped:     0, Total:    13
```

### Run Tests with Verbose Output

To see which tests passed:

```bash
dotnet test --verbosity normal
```

This will list all 13 passing tests:
- PostHello_WithValidRequest_ReturnsOkWithMessage
- PostHello_WithValidRequest_ReturnsCorrectJsonStructure
- PostHello_WithEmptyBody_ReturnsOkWithMessage
- PostHello_MultipleConsecutiveCalls_AllReturnSameMessage (x3)
- PostHello_ResponseHasCorrectContentType
- PostHello_ResponseBodyIsValidJson
- PostHello_RequestHeaders_ContentTypeNotRequired
- PostHello_VerifyNoErrorInResponse
- PostHello_ResponseMessageFieldIsCorrect
- PostHello_EmptyRequest_ReturnsOk
- PostHello_ResponseIsConsistent

### Interpret Test Output

- **Passed**: All tests passed successfully (green indicator)
- **Failed**: One or more tests failed (red indicator)
- **Skipped**: Tests were skipped (yellow indicator)

### Run Specific Tests

To run only tests matching a pattern:

```bash
dotnet test --filter "PostHello_WithValidRequest"
```

### Debug Failing Tests

If tests fail:

1. Check build output for compilation errors:
   ```bash
   dotnet build
   ```

2. Verify .NET SDK version:
   ```bash
   dotnet --info
   ```

3. Run tests with additional output:
   ```bash
   dotnet test --verbosity detailed
   ```

---

## Troubleshooting

### Problem: "Command not found: dotnet"

**Why it occurs**: .NET SDK is not installed or not in your PATH.

**Solution**:
1. Install .NET 8 SDK from https://dotnet.microsoft.com/download/dotnet/8.0
2. Restart your terminal
3. Verify: `dotnet --info`

### Problem: "SDK version 8.0.405 not found"

**Why it occurs**: global.json specifies a version that isn't installed.

**Solution**:
1. Check installed SDK versions: `dotnet --info`
2. Update `global.json` to match an installed version:
   ```json
   {
     "sdk": {
       "version": "8.0.405"
       }
   }
   ```
3. Try again: `dotnet build`

### Problem: "Address already in use" when running the server

**Why it occurs**: Port 5000 is already in use by another process.

**Solution**:
1. Find the process using port 5000:
   ```bash
   lsof -i :5000
   ```
2. Kill that process or use a different port:
   ```bash
   dotnet run --project src -- --urls "http://localhost:5001"
   ```

### Problem: Build fails with "NU1603: ... was not found"

**Why it occurs**: NuGet package version mismatch (usually harmless).

**Solution**:
1. This is typically just a warning
2. If build fails anyway, restore packages:
   ```bash
   dotnet restore
   dotnet build
   ```

### Problem: Tests fail locally

**Why it occurs**: Environment issues, port conflicts, or missing dependencies.

**Solution**:
1. Ensure no other process is running on port 5000
2. Clean build artifacts:
   ```bash
   dotnet clean
   dotnet build
   dotnet test
   ```
3. Check .NET version: `dotnet --info`

### Problem: "Could not find project or directory"

**Why it occurs**: Working directory is incorrect.

**Solution**:
1. Verify you're in the correct directory:
   ```bash
   pwd  # Should show path to hello-world-api
   ls   # Should show src/, tests/, global.json, etc.
   ```
2. Navigate to correct directory:
   ```bash
   cd /path/to/hello-world-api
   ```

---

## Next Steps

### 1. Read the API Specifications

- [API Design Principles](../specs/api-design.md) — How the API is designed
- [Hello Endpoint Specification](../specs/hello-endpoint.md) — Details about the /test/hello endpoint
- [Error Handling](../specs/error-handling.md) — How errors are handled

### 2. Make Modifications

To modify the endpoint or add features:
1. See [Adding Endpoints Guide](./adding-endpoints.md) for the workflow
2. Follow the specification-first approach: write spec before code

### 3. Run in Different Modes

**Debug mode** (default with full symbols):
```bash
dotnet run --project src
```

**Release mode** (optimized):
```bash
dotnet run --project src --configuration Release
```

### 4. View API Documentation

Visit Swagger UI while server is running:
```
http://localhost:5000/swagger/index.html
```

### 5. Explore Architecture Decisions

- [ADR-001: Minimal APIs](../adr/adr-001-minimal-apis.md) — Why we chose Minimal APIs

### 6. Learn More

- [Technology Choices Specification](../specs/technology-choices.md) — Framework and platform decisions
- [CLAUDE.md](../../CLAUDE.md) — Coding standards and conventions

---

## Common Commands Reference

```bash
# Build the project
dotnet build

# Run all tests
dotnet test

# Start the API server
dotnet run --project src

# Clean build artifacts
dotnet clean

# Restore NuGet packages
dotnet restore

# Run specific test
dotnet test --filter "TestNamePattern"

# Test with verbose output
dotnet test --verbosity normal
```

---

## Support and Documentation

For detailed documentation, see:
- Project documentation: `/docs/`
- API specifications: `/docs/specs/`
- Architecture decisions: `/docs/adr/`
- Implementation guides: `/docs/guides/`

---

**You're all set!** The API should now be running and responding to requests. Start with the [Hello Endpoint Specification](../specs/hello-endpoint.md) to understand the API better.
