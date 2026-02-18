# Adding Endpoints Guide

Workflow for adding new endpoints to the Hello World API following specification-first development.

## Process Overview

The API grows through this iterative cycle:

1. **Specification**: Document the endpoint contract first
2. **Design**: Define request/response structure, error cases
3. **Reference**: Add to API design endpoint index
4. **Implementation**: Write code following existing patterns
5. **Testing**: Write xUnit tests for success and error cases
6. **Documentation**: Update guides if needed (e.g., special deployment requirements)

This specification-first approach ensures clarity, consistency, and maintains excellent documentation.

---

## Step-by-Step: Adding a New Endpoint

### Step 1: Create Endpoint Specification

Create a new file in `/docs/api/specs/{endpoint-name}.md`:

**File Naming**:
- Use lowercase with hyphens: `user-login.md`, `item-search.md`
- Match the endpoint path or feature name
- One file per endpoint (or logical group of related endpoints)

**Template Content** (copy from `hello-endpoint.md` and adapt):
```markdown
# {Feature Name} Endpoint Specification

## Endpoint

**{METHOD}** `/{path}/{name}`

## Purpose

One-line description of what this endpoint does.

## Request Contract

### Method
{GET|POST|PUT|PATCH|DELETE}

### URL
```
http://localhost:5000/{path}/{name}
```

### Headers
```
Content-Type: application/json
```

### Body
Describe the request JSON structure or note if empty.

### Example Request
```bash
curl -X {METHOD} http://localhost:5000/{path}/{name} \
  -H "Content-Type: application/json" \
  -d '{"field": "value"}'
```

---

## Response Contract - Success (200 OK)

### Status Code
200

### JSON Structure
```json
{
  "field": "value"
}
```

### Example Response
```json
{"field":"value"}
```

---

## Response Contract - Error (4xx or 5xx)

### Status Code
{400|401|403|404|409|500}

### JSON Structure
```json
{
  "error": {
    "code": "ERROR_CODE",
    "message": "Error description"
  }
}
```

### Example Response
```json
{
  "error": {
    "code": "ERROR_CODE",
    "message": "Error description"
  }
}
```

---

## Implementation Notes

Describe how the endpoint should be implemented (patterns to follow, special considerations).

---

## Related Specifications

- [API Design Principles](./api-design.md)
- [Error Handling](./error-handling.md)
```

**Example: Adding a GET /users/{id} endpoint**

Create `/docs/api/specs/get-user.md`:
```markdown
# Get User Endpoint Specification

## Endpoint

**GET** `/users/{id}`

## Purpose

Retrieve a single user by ID.

## Request Contract

### Method
GET

### URL
```
http://localhost:5000/users/123
```

### Headers
```
Content-Type: application/json
```

### Path Parameters
- **id** (integer): Numeric user ID

### Example Request
```bash
curl -X GET http://localhost:5000/users/123 \
  -H "Content-Type: application/json"
```

---

## Response Contract - Success (200 OK)

### Status Code
200

### JSON Structure
```json
{
  "id": 123,
  "name": "John Doe",
  "email": "john@example.com"
}
```

### Example Response
```json
{"id":123,"name":"John Doe","email":"john@example.com"}
```

---

## Response Contract - Error (404 Not Found)

### Status Code
404

### JSON Structure
```json
{
  "error": {
    "code": "NOT_FOUND",
    "message": "User not found"
  }
}
```

## Implementation Notes

- Route parameter: `{id}` extracts the numeric ID
- Query database or in-memory store for user by ID
- Return 404 if user doesn't exist
- No authentication required for Phase 1
```

---

### Step 2: Update API Design Index

Edit `/docs/api/specs/api-design.md` and add the new endpoint to the **Endpoint Index** table:

Before:
```markdown
| Method | Path | Purpose | Spec | Status |
|--------|------|---------|------|--------|
| POST | `/test/hello` | Return hello world message | [hello-endpoint.md](./hello-endpoint.md) | Active |
```

After:
```markdown
| Method | Path | Purpose | Spec | Status |
|--------|------|---------|------|--------|
| POST | `/test/hello` | Return hello world message | [hello-endpoint.md](./hello-endpoint.md) | Active |
| GET | `/users/{id}` | Retrieve a single user by ID | [get-user.md](./get-user.md) | Active |
```

---

### Step 3: Implement in Code

Create handler code in `/src` following existing patterns.

**File Naming**:
- Use PascalCase: `UserHandler.cs`, `ItemHandler.cs`
- One feature per file (or related features in one file)
- Keep feature code separate from main Program.cs if possible

**Example: Minimal Implementation**

Create `/src/Features/UserHandler.cs`:
```csharp
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Features
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapGet("/users/{id}", GetUser)
                .WithName("GetUser")
                .WithOpenApi();
        }

        public static async Task<IResult> GetUser(int id)
        {
            // Example: Mock data store
            var users = new Dictionary<int, User>
            {
                { 1, new User { Id = 1, Name = "John Doe", Email = "john@example.com" } },
                { 2, new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com" } }
            };

            if (!users.ContainsKey(id))
            {
                return Results.NotFound(new
                {
                    error = new
                    {
                        code = "NOT_FOUND",
                        message = "User not found"
                    }
                });
            }

            return Results.Ok(users[id]);
        }
    }
}
```

Update `Program.cs` to register the new endpoints:
```csharp
var app = builder.Build();

// Configure middleware
app.UseCors();

// Map endpoints
app.MapPost("/test/hello", () =>
{
    return Results.Ok(new { message = "hello world" });
})
.WithName("Hello")
.WithOpenApi();

app.MapUserEndpoints();  // NEW LINE

app.Run();
```

---

### Step 4: Write Tests

Create test class in `/tests/{FeatureName}Tests.cs`:

Create `/tests/UserEndpointTests.cs`:
```csharp
using Xunit;
using System.Net;
using System.Net.Http.Json;

namespace HelloWorldApi.Tests
{
    public class UserEndpointTests : IAsyncLifetime
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        public async Task InitializeAsync()
        {
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        public async Task DisposeAsync()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }

        [Theory]
        [InlineData(1, "John Doe", "john@example.com")]
        [InlineData(2, "Jane Smith", "jane@example.com")]
        public async Task GetUser_WithValidId_ReturnsOkWithUser(
            int id, string expectedName, string expectedEmail)
        {
            // Act
            var response = await _client.GetAsync($"/users/{id}");
            var json = await response.Content.ReadAsAsync<dynamic>();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(id, (int)json.id);
            Assert.Equal(expectedName, (string)json.name);
            Assert.Equal(expectedEmail, (string)json.email);
        }

        [Fact]
        public async Task GetUser_WithNonexistentId_ReturnsNotFound()
        {
            // Act
            var response = await _client.GetAsync("/users/999");
            var json = await response.Content.ReadAsAsync<dynamic>();

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("NOT_FOUND", (string)json.error.code);
            Assert.Equal("User not found", (string)json.error.message);
        }
    }
}
```

**Test Naming Convention**:
- `{MethodName}_{Scenario}_{ExpectedOutcome}`
- Example: `GetUser_WithValidId_ReturnsOkWithUser`
- Example: `GetUser_WithNonexistentId_ReturnsNotFound`

**Parameterized Tests** (use [Theory] and [InlineData]):
```csharp
[Theory]
[InlineData(1)]
[InlineData(2)]
[InlineData(3)]
public async Task GetUser_WithValidIds_ReturnsOk(int id)
{
    var response = await _client.GetAsync($"/users/{id}");
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
}
```

---

### Step 5: Build and Test

Build the project:
```bash
dotnet build
```

Run all tests:
```bash
dotnet test
```

Run tests with verbose output to see which tests passed:
```bash
dotnet test --verbosity normal
```

---

### Step 6: Manual Testing

Start the server:
```bash
dotnet run --project src
```

Test the new endpoint manually:
```bash
curl -X GET http://localhost:5000/users/1 \
  -H "Content-Type: application/json"

# Expected response
{"id":1,"name":"John Doe","email":"john@example.com"}
```

---

### Step 7: Update Guides (Optional)

If the endpoint requires special setup, deployment, or configuration:

1. Update project documentation if it affects getting-started steps
2. Create/update a specialized guide if the feature is complex
3. Document any environment variables or configuration needed

---

## File Naming Conventions

### Specification Files (in `/docs/api/specs/`)
- **Naming**: lowercase with hyphens: `get-user.md`, `user-login.md`, `item-search.md`
- **Pattern**: `{http-method}-{resource-name}.md` or `{feature-name}.md`
- **Rationale**: Predictable naming makes files easy to find

### Implementation Files (in `/src`)
- **Naming**: PascalCase: `UserHandler.cs`, `ItemService.cs`, `AuthService.cs`
- **Pattern**: `{FeatureName}Handler.cs` or `{FeatureName}Service.cs`
- **Rationale**: Follow C# naming conventions

### Test Files (in `/tests`)
- **Naming**: PascalCase: `UserEndpointTests.cs`, `ItemServiceTests.cs`
- **Pattern**: `{FeatureName}Tests.cs`
- **Rationale**: Clear which feature the tests cover

---

## Checklist for Adding a New Endpoint

- [ ] Specification written in `/docs/api/specs/{endpoint-name}.md`
- [ ] Request contract documented (method, path, headers, body)
- [ ] Response contract documented (200 and error responses)
- [ ] Examples provided (curl command and JSON)
- [ ] Endpoint added to API Design index
- [ ] Implementation code written in `/src/`
- [ ] Handler registered in `Program.cs`
- [ ] Tests written in `/tests/`
- [ ] All tests pass with `dotnet test`
- [ ] Manual testing confirms endpoint works
- [ ] Code follows C# conventions from [CLAUDE.md](../../CLAUDE.md)
- [ ] Error responses follow error handling spec

---

## Related Documentation

- [CLAUDE.md](../../CLAUDE.md) — Coding standards and conventions
- [API Design](../api/specs/api-design.md) — API design principles
- [Error Handling](../api/specs/error-handling.md) — Error response format
- [Technology Choices](../api/specs/technology-choices.md) — Framework and platform decisions
