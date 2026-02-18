# ADR-001: Use ASP.NET Core Minimal APIs

## Status

**Accepted**

## Context

The project needs a lightweight REST API framework for a Hello World demonstration that will scale from a single endpoint to 10+ endpoints. The decision is between:

1. **ASP.NET Core Minimal APIs** — Lightweight, direct endpoint definition
2. **ASP.NET Core MVC** — Full-featured, controller-based architecture
3. **gRPC** — High-performance RPC protocol (binary, Protocol Buffers)

Key constraints:
- API-only project (no view rendering needed)
- Target audience: demonstration and learning
- Must support modern async/await patterns
- Must allow easy testing
- Should be scalable from simple to enterprise

## Decision

**Adopt ASP.NET Core Minimal APIs** as the HTTP framework for this project.

### Key Reasons

1. **Minimal Ceremony**: Endpoints are defined directly in `Program.cs` or handler classes with minimal boilerplate
2. **Modern**: Built for .NET 5+ with native async/await support throughout
3. **Perfect for APIs**: No MVC controller overhead; direct routing and handler definition
4. **Performance**: Fast startup and execution; ideal for cloud workloads
5. **Testability**: Straightforward to test with standard HTTP client
6. **Scalability**: Can grow from simple endpoints to complex feature sets without refactoring

## Consequences

### Positive
✓ Simple and direct for API development
✓ Minimal boilerplate code
✓ Native middleware integration (CORS, authentication, logging)
✓ Excellent for microservices and cloud deployments
✓ Modern C# patterns (async/await, pattern matching, records)
✓ Perfect for demonstrations and learning projects
✓ Excellent testing experience with WebApplicationFactory
✓ Industry adoption: 60,000+ projects use AGENTS.md/CLAUDE.md patterns

### Negative/Tradeoffs
✗ Less structured than MVC for large projects (100+ endpoints)
✗ No built-in view rendering (not needed for API-only projects)
✗ Handler organization is team-dependent (no enforced structure)
✗ May require more planning for large, complex projects

### Mitigation Strategies
- **For Large Projects**: Organize handlers into feature folders (`/src/Features/{Feature}/`)
- **For Consistency**: Follow established patterns documented in [Adding Endpoints Guide](../../guides/adding-endpoints.md)
- **For Clarity**: Maintain comprehensive API documentation in `/docs/specs/`

## Alternatives Considered

### ASP.NET Core MVC
**Status**: Rejected

MVC provides a structured, controller-based approach with separation of concerns:
```csharp
// MVC approach
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        // Implementation
    }
}
```

**Why Rejected**:
- More ceremony and boilerplate than needed for Phase 1
- Controller/Action pattern adds structure not required for simple APIs
- Better for large projects with 50+ endpoints; overkill for this project
- Additional concepts (routing attributes, model binding decorators) increase cognitive load

**When Better**:
- Projects with 50+ endpoints benefiting from strict organization
- Teams wanting enforced architectural patterns
- Large organizations with standardized MVC practices

### gRPC
**Status**: Rejected

gRPC provides high-performance RPC using Protocol Buffers:
```protobuf
// gRPC approach
service Users {
  rpc GetUser(GetUserRequest) returns (User) {}
}
```

**Why Rejected**:
- Binary protocol, not REST/HTTP semantics
- Requires Protocol Buffer definitions (.proto files)
- Increases complexity for a simple API
- Less suitable for demonstration/learning (steeper learning curve)
- Browser clients require additional complexity (gRPC-Web)

**When Better**:
- High-performance, language-agnostic RPC between services
- Performance-critical systems (e.g., microservices with high throughput)
- Teams already experienced with Protocol Buffers

### Azure Functions
**Status**: Not Selected (out of scope)

Azure Functions provide serverless execution in Azure cloud:

**Why Not Selected**:
- Project targets local macOS development
- Azure Functions suitable for cloud deployment, not local development
- Different deployment model (event-driven)
- Unnecessary overhead for demonstration project

**When Better**:
- Deploying to Azure cloud
- Event-driven architecture requirements
- Serverless workload model

## Implementation

The implementation uses:

**File Structure**:
```
src/
├── Program.cs           # Route definitions and middleware setup
└── Features/            # Optional: organize handlers by feature
    └── HelloHandler.cs  # Endpoint handlers
```

**Minimal APIs Pattern**:
```csharp
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Direct endpoint definition
app.MapPost("/test/hello", async () =>
{
    return Results.Ok(new { message = "hello world" });
});

app.Run();
```

**For Organization** (as project grows):
```csharp
// In Program.cs
app.MapHelloEndpoints();
app.MapUserEndpoints();
app.MapItemEndpoints();

// In Features/HelloHandler.cs
public static void MapHelloEndpoints(this WebApplication app)
{
    app.MapPost("/test/hello", async () =>
    {
        return Results.Ok(new { message = "hello world" });
    });
}
```

## References

- [Microsoft Docs: ASP.NET Core Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
- [Minimal APIs Tutorial](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api)
- [Architecture Comparison: MVC vs Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/apis)

## Related Decisions

- [Technology Choices Specification](../specs/technology-choices.md) — Complete technology stack
- [API Design Specification](../specs/api-design.md) — API design principles

## Decision Log

| Date | Status | Notes |
|------|--------|-------|
| 2026-02-17 | Accepted | Chosen for Phase 1 demonstration project |
