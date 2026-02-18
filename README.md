# Hello World API

A minimal ASP.NET Core Minimal APIs demonstration project showcasing specification-first development and industry-standard documentation practices.

## Quick Start

Get the server running in 5 steps:

1. **Install .NET 8 SDK** (if not already installed)
   - Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)
   - macOS: Verify ARM64 native with `dotnet --info`

2. **Clone or navigate to project**
   ```bash
   cd hello-world-api
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the server**
   ```bash
   dotnet run
   ```
   Server listens on `http://localhost:5000`

5. **Test the endpoint**
   ```bash
   curl -X POST http://localhost:5000/test/hello
   ```
   Expected response: `{"message":"hello world"}`

## Prerequisites

- **OS**: macOS (ARM64) or Linux/Windows with .NET 8 SDK
- **Software**:
  - .NET 8 LTS SDK ([download](https://dotnet.microsoft.com/download))
  - Visual Studio Code (recommended) with C# Dev Kit extension
  - Git (for version control)

- **Verification**:
  ```bash
  dotnet --version  # Should show 8.0.x
  dotnet --info     # Should show "Architecture: arm64" on macOS
  ```

## Testing the API

### Using curl
```bash
# Test POST /test/hello
curl -X POST http://localhost:5000/test/hello \
  -H "Content-Type: application/json"

# Expected response
{"message":"hello world"}
```

### Using Postman
1. Create a new POST request
2. URL: `http://localhost:5000/test/hello`
3. Headers: `Content-Type: application/json`
4. Send request
5. Response body: `{"message":"hello world"}`

## Project Structure

```
hello-world-api/
├── CLAUDE.md                    # Agent guidance (see for development workflow)
├── README.md                    # This file
├── global.json                  # .NET SDK version specification
├── docs/                        # All specifications and guides
│   ├── specs/                   # API and system specifications
│   │   ├── hello-endpoint.md    # POST /test/hello contract
│   │   ├── api-design.md        # API design principles
│   │   ├── error-handling.md    # Error response format
│   │   └── technology-choices.md # Framework decisions
│   ├── guides/                  # How-to guides
│   │   ├── getting-started.md   # Detailed setup guide
│   │   └── adding-endpoints.md  # Workflow for new endpoints
│   └── adr/                     # Architecture decision records
│       └── adr-001-minimal-apis.md
├── src/                         # Source code
│   └── Program.cs               # Application entry point and routes
└── tests/                       # Test code
    └── HelloEndpointTests.cs    # xUnit tests for POST /test/hello
```

See `CLAUDE.md` for detailed development workflow and coding standards.

## Documentation

All project specifications are documented in markdown for clarity and AI comprehension:

- **[CLAUDE.md](./CLAUDE.md)** — Agent guidance for development workflow
- **[API Design](./docs/specs/api-design.md)** — API principles and endpoint index
- **[Hello Endpoint Spec](./docs/specs/hello-endpoint.md)** — Complete POST /test/hello contract
- **[Error Handling](./docs/specs/error-handling.md)** — Error response format and codes
- **[Technology Choices](./docs/specs/technology-choices.md)** — Why ASP.NET Core, .NET 8, xUnit
- **[Getting Started Guide](./docs/guides/getting-started.md)** — Detailed setup instructions
- **[Adding Endpoints Guide](./docs/guides/adding-endpoints.md)** — Workflow for new features
- **[Architecture Decisions](./docs/adr/adr-001-minimal-apis.md)** — Why Minimal APIs

## Running Tests

Execute all tests:
```bash
dotnet test
```

Run specific test file:
```bash
dotnet test --filter "HelloEndpointTests"
```

Run with verbose output:
```bash
dotnet test --verbosity normal
```

## Contributing

Development follows specification-first methodology:

1. Create specification in `/docs/specs/` before implementation
2. Reference spec in `/docs/specs/api-design.md` endpoint index
3. Implement feature in `/src` following patterns in existing code
4. Write tests in `/tests` using xUnit patterns
5. See [CLAUDE.md](./CLAUDE.md) for detailed workflow

## License

This is a demonstration project. Use as needed for learning and development.

## Support

For detailed development guidance, see [CLAUDE.md](./CLAUDE.md). For questions about API contracts, see the relevant specification file in `/docs/specs/`.
