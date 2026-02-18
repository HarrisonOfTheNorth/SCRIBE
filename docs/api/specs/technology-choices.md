# Technology Choices Specification

## Framework: ASP.NET Core Minimal APIs

### Decision
Use **ASP.NET Core Minimal APIs** for building the REST API.

### Why Minimal APIs
- **Minimal Ceremony**: Define routes and handlers with minimal boilerplate
- **Lightweight**: Perfect for APIs that don't need MVC's view layer
- **Performance**: Fast startup and execution, ideal for cloud functions
- **Modern**: Native async/await support throughout
- **Scalable**: Can grow from simple endpoints to complex feature sets
- **Type-Safe**: Full C# type system and compiler support

### Trade-offs

#### Minimal APIs Advantages
✓ Simple and direct for API development
✓ Minimal boilerplate code
✓ Native middleware integration
✓ Excellent for microservices
✓ Perfect for demonstration projects

#### Minimal APIs Limitations
✗ Less suitable for large projects with 100+ endpoints (MVC might be cleaner)
✗ No built-in view rendering (not needed for API-only projects)

### Alternatives Considered

#### ASP.NET Core MVC
- **Why Rejected**: More ceremony (controllers, action methods) than needed for a simple API
- **When Better**: Large projects with 50+ endpoints benefiting from structured organization
- **Verdict**: Overkill for Phase 1

#### gRPC
- **Why Rejected**: Binary protocol, not REST, requires Protocol Buffer definitions
- **When Better**: High-performance, language-agnostic RPC between services
- **Verdict**: Wrong tool for a simple REST API demonstration

#### Express.js (Node.js)
- **Why Rejected**: Different runtime (.NET 8 specified); different ecosystem
- **When Better**: JavaScript/TypeScript shops; existing Node.js infrastructure
- **Verdict**: Out of scope for .NET specification

### Conclusion
Minimal APIs provide the best fit for this project: lightweight, modern, and perfectly suited for a simple API that can scale to enterprise complexity.

---

## Runtime: .NET 8 LTS

### Decision
Use **.NET 8 Long-Term Support (LTS)** as the target runtime.

### Why .NET 8
- **Long-Term Support**: Guaranteed support until **November 10, 2026** (minimum 3 years)
- **Stability**: LTS releases are production-grade with extensive testing
- **Performance**: 30-50% faster on Apple Silicon (ARM64) compared to x64
- **Modern Language Features**: C# 12 with nullable reference types, pattern matching, LINQ improvements
- **Security**: Regular security patches throughout support period

### Version Specification
- **Exact Version**: .NET 8 LTS (currently 8.0.101)
- **Configuration**: Specified in `global.json` to ensure consistency across team and CI/CD
- **Update Policy**: Keep within 8.0.x series; update patch versions for security

### .NET 8 Release Timeline
| Version | Release Date | Support Until |
|---------|-------------|---------------|
| 8.0.0   | November 14, 2023 | November 10, 2026 |
| Current | Latest patch in 8.0.x | November 10, 2026 |

### Supported Platforms
- Windows (x64, ARM64)
- Linux (x64, ARM64)
- macOS (x64, ARM64)

### Alternatives Considered

#### .NET 7
- **Why Rejected**: Support ends May 14, 2024 (already ended)
- **Verdict**: Too short support window for production use

#### .NET 9 (Current Release)
- **Why Rejected**: Not LTS; support ends November 2025 (only ~1 year remaining)
- **Verdict**: Shorter support than .NET 8 LTS; not suitable for long-lived projects

#### .NET Framework (Classic)
- **Why Rejected**: End of support; .NET 5+ is the modern platform
- **Verdict**: Legacy; not recommended for new projects

### Conclusion
.NET 8 LTS offers the best balance of modernity, performance, and long-term stability. The 3-year support window ensures the application remains secure and supported throughout its lifecycle.

---

## Testing Framework: xUnit

### Decision
Use **xUnit** for unit and integration testing.

### Why xUnit
- **Modern**: Built specifically for .NET 5+ (not legacy MSTest or NUnit)
- **Lightweight**: Minimal magic; explicit and readable test code
- **Parameterized Tests**: [Theory] and [InlineData] for testing multiple scenarios elegantly
- **Parallel Execution**: Fast test runs by default (with proper test isolation)
- **Async Support**: Native async/await support in tests
- **Community**: Popular in the .NET ecosystem; excellent documentation

### Key Features
- **[Fact]**: Single test case with expected behavior
- **[Theory]**: Parameterized test with multiple data sets
- **[InlineData]**: Inline test data (no separate data files)
- **Assert**: Clear, fluent assertions

### Example Test Pattern
```csharp
[Fact]
public async Task PostHello_ReturnsOkWithMessage()
{
    // Arrange & Act
    var response = await client.PostAsync("/test/hello", null);

    // Assert
    Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
}

[Theory]
[InlineData(1, 2, 3)]
[InlineData(2, 3, 5)]
public void Add_WithValidNumbers_ReturnsCorrectSum(int a, int b, int expected)
{
    Assert.Equal(expected, a + b);
}
```

### Alternatives Considered

#### MSTest (Microsoft Test)
- **Why Rejected**: Heavier framework with more conventions and magic
- **When Better**: Enterprise projects with existing MSTest infrastructure
- **Verdict**: Overkill for this project

#### NUnit
- **Why Rejected**: Older framework; xUnit is more modern for .NET
- **When Better**: Legacy projects with existing NUnit tests
- **Verdict**: xUnit is the modern choice

### Conclusion
xUnit provides a modern, lightweight testing experience with excellent support for parameterized tests. The [Theory]/[InlineData] pattern is perfect for testing multiple scenarios without code duplication.

---

## Platform: macOS with ARM64 Native

### Decision
Develop and deploy on **macOS with ARM64 native .NET SDK** (not x64 emulation).

### Why ARM64 Native
- **Performance**: 30-50% better performance compared to x64 emulation via Rosetta 2
- **Native Compilation**: Leverages Apple Silicon capabilities
- **Battery Life**: Lower power consumption on Apple Silicon Macs
- **Future-Proof**: ARM architecture is the future for mobile and desktop

### macOS Verification
Verify ARM64 native installation:
```bash
dotnet --info
# Look for: Architecture: arm64 (NOT x86_64 or x86)
```

If seeing x86_64:
1. Uninstall current .NET SDK
2. Download ARM64 installer from [dotnet.microsoft.com](https://dotnet.microsoft.com)
3. Verify with `dotnet --info` again

### Performance Comparison
| Metric | ARM64 Native | x64 Emulation |
|--------|-------------|----------------|
| Startup Time | ~500ms | ~700ms |
| Throughput | Baseline | 40-60% slower |
| Memory Usage | Lower | Higher (emulation overhead) |

### Cross-Platform Compatibility
- Code compiles on Windows, Linux, and macOS
- No platform-specific code needed for Phase 1
- Use `global.json` to ensure consistent SDK across platforms

### Alternatives Considered

#### x64 Emulation (Rosetta 2)
- **Simpler**: Works without reinstalling .NET
- **Slower**: 30-50% performance penalty
- **Not Recommended**: Unnecessary overhead on Apple Silicon

#### Windows or Linux Only
- **Why Rejected**: Project explicitly targets macOS
- **Verdict**: Out of scope

### Conclusion
ARM64 native .NET on macOS provides optimal performance and aligns with modern Apple Silicon hardware. The installation and verification process is straightforward with clear guidance.

---

## API Response Format: JSON

### Decision
All API responses return **JSON (application/json)**.

### Why JSON
- **Standard**: De facto standard for REST APIs
- **Human-Readable**: Easy to debug and inspect
- **Language-Agnostic**: Supported in all programming languages
- **Parser Availability**: Excellent JSON parsing libraries across platforms
- **Schema Tools**: JSON Schema for documentation and validation

### Content-Type Header
```
Content-Type: application/json
```

Always set this header on responses. ASP.NET Core Minimal APIs do this automatically.

### Example Response
```json
{
  "message": "hello world"
}
```

### Alternatives Considered

#### Plain Text
- **Why Rejected**: No structure; harder to parse and validate
- **Verdict**: Insufficient for structured data

#### XML
- **Why Rejected**: Verbose; less commonly used in modern APIs
- **Verdict**: Dated compared to JSON

#### Protocol Buffers
- **Why Rejected**: Binary format; adds complexity
- **Verdict**: Overkill for this project

### Conclusion
JSON is the industry standard for REST APIs. All endpoints will return JSON responses with consistent envelope format (see Error Handling Specification).

---

## Project Structure and Configuration

### Directory Organization
```
hello-world-api/
├── global.json              # .NET SDK version lock file
├── src/                     # Source code
├── tests/                   # Test code
└── docs/                    # Documentation
    ├── specs/               # API specifications
    ├── guides/              # How-to guides
    └── adr/                 # Architecture decision records
```

### SDK Version Locking
**File**: `global.json`

The project uses `global.json` to specify the exact .NET 8 SDK version, ensuring consistency across:
- Local developer machines
- CI/CD pipelines
- Team members
- Docker containers (if used)

**Example Format**:
```json
{
  "sdk": {
    "version": "8.0.101"
  }
}
```

This ensures all builds use the same patch version, preventing subtle compatibility issues.

### Project File Organization

**Source Project** (`src/`):
- Contains the ASP.NET Core API application
- Main entry point: `Program.cs`
- Organized by feature as complexity grows
- Target framework: .NET 8

**Test Project** (`tests/`):
- Contains xUnit test suite
- Follows project structure mirrors source (e.g., `/tests/HelloEndpointTests.cs` for `/src` features)
- Target framework: .NET 8
- Dependencies: xUnit, xUnit.Runner.VisualStudio, Microsoft.NET.Test.Sdk

### NuGet Dependencies

**Minimum Required Packages** (for Source):
- ASP.NET Core runtime (included with framework)

**Required Packages** (for Tests):
- xunit
- xunit.runner.visualstudio
- Microsoft.NET.Test.Sdk

**Optional Packages** (recommended):
- Swashbuckle.AspNetCore (for Swagger/OpenAPI documentation)

---

## Summary

| Choice | Decision | Rationale |
|--------|----------|-----------|
| **Framework** | ASP.NET Core Minimal APIs | Lightweight, modern, perfect for APIs |
| **Runtime** | .NET 8 LTS | Stable, long support, high performance |
| **Testing** | xUnit | Modern, lightweight, parameterized tests |
| **Platform** | macOS ARM64 Native | Best performance on Apple Silicon |
| **Response Format** | JSON | Industry standard for REST APIs |
| **SDK Locking** | global.json | Ensures consistency across team and CI/CD |

These choices create a modern, performant, and maintainable API platform suitable for both demonstration and production use.

---

## Related Specifications

- [API Design](./api-design.md) — API design principles
- [Error Handling](./error-handling.md) — Error response format
- [Hello Endpoint](./hello-endpoint.md) — Example endpoint implementation
