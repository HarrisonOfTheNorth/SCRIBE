# API Design Specification

## API Design Principles

All endpoints in this API follow these design principles:

### 1. JSON-First
- All endpoints return JSON (application/json)
- No plain text, HTML, or binary responses
- Consistent structure across all endpoints

### 2. Standard HTTP Methods
- **POST**: For actions or submissions (creates state, may have side effects)
- **GET**: For retrieving data (idempotent, no side effects)
- **PUT**: For full resource replacement
- **PATCH**: For partial updates
- **DELETE**: For resource removal

### 3. Consistent Response Envelopes
- **Success**: Direct JSON object or data structure
- **Error**: Wrapped in `{ "error": { "code": "...", "message": "..." } }`
- See [Error Handling Specification](./error-handling.md) for details

### 4. Predictable URL Paths
- Lowercase with hyphens (e.g., `/test-hello`, NOT `/TestHello`)
- Resource-oriented paths: `/resource/{id}/sub-resource`
- Avoid verbs in paths; use HTTP methods instead
- Example: POST `/items` (create), GET `/items/{id}` (retrieve)

### 5. Meaningful HTTP Status Codes
- **2xx**: Success
  - 200 OK: Request successful, response body included
  - 201 Created: Resource created (typically POST)
  - 204 No Content: Success with no response body

- **4xx**: Client Error
  - 400 Bad Request: Invalid input or request format
  - 401 Unauthorized: Authentication required
  - 403 Forbidden: Authenticated but lacks permission
  - 404 Not Found: Resource doesn't exist
  - 409 Conflict: Request conflicts with current state

- **5xx**: Server Error
  - 500 Internal Server Error: Unhandled exception
  - 503 Service Unavailable: Service temporarily down

---

## Endpoint Index

| Method | Path | Purpose | Spec | Status |
|--------|------|---------|------|--------|
| POST | `/test/hello` | Return hello world message | [hello-endpoint.md](./hello-endpoint.md) | Active |

### Status Definitions
- **Active**: Implemented and production-ready
- **Planned**: Specified but not yet implemented
- **Deprecated**: No longer recommended; will be removed

---

## Naming Conventions

### Path Naming
```
/resource                     # Collection endpoint
/resource/{id}               # Single resource endpoint
/resource/{id}/sub-resource  # Sub-resource endpoint
```

- Use **lowercase letters and hyphens**: `/test-hello`, `/user-profile`, `/api-keys`
- Avoid **CamelCase**: ❌ `/TestHello`, ❌ `/UserProfile`
- Avoid **underscores**: ❌ `/test_hello`, ❌ `/user_profile`
- Avoid **verbs in paths**: ❌ `/getUser`, ❌ `/createItem`
- Use HTTP methods for actions: POST /items (create), DELETE /items/{id} (delete)

### Query Parameter Naming
```
/items?sort=name&filter=active&page=1
```

- Use **lowercase camelCase**: `?sortBy=name`, `?filterStatus=active`
- Be **descriptive**: `?search=query` not `?q=query`
- Use **standard pagination**: `?page=1&limit=50`

### JSON Field Naming
```json
{
  "message": "hello world",
  "userId": 123,
  "createdAt": "2026-02-17T10:30:00Z"
}
```

- Use **camelCase**: `message`, `userId`, `createdAt`
- Be **consistent**: Don't mix `user_id` and `userId`
- Use **ISO 8601 for timestamps**: `2026-02-17T10:30:00Z`

---

## Response Format Standards

### Success Response - Single Object
```json
{
  "message": "hello world"
}
```

### Success Response - Collection
```json
{
  "items": [
    { "id": 1, "name": "Item 1" },
    { "id": 2, "name": "Item 2" }
  ],
  "count": 2
}
```

### Success Response - Wrapped Data
```json
{
  "data": {
    "id": 123,
    "name": "Resource Name"
  }
}
```

Use wrapping sparingly; prefer direct objects when possible.

### Error Response - Standard Format
```json
{
  "error": {
    "code": "ERROR_CODE",
    "message": "Human-readable error description"
  }
}
```

### Error Response - With Details
```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Request validation failed",
    "details": [
      { "field": "email", "issue": "invalid format" },
      { "field": "age", "issue": "must be 18 or older" }
    ]
  }
}
```

Use details for validation errors; omit for generic errors.

---

## Pagination (Future Reference)

When implementing endpoints that return collections, use:

```
GET /items?page=1&limit=50
```

Response:
```json
{
  "items": [...],
  "pagination": {
    "page": 1,
    "limit": 50,
    "total": 150,
    "hasMore": true
  }
}
```

---

## Versioning (Future Reference)

If API versioning becomes necessary, use URL path versioning:

```
/v1/resource
/v2/resource
```

Avoid header-based versioning for simplicity.

---

## Related Specifications

- [Hello Endpoint Specification](./hello-endpoint.md) — Phase 1 endpoint contract
- [Error Handling Specification](./error-handling.md) — Error codes and response format
- [Technology Choices](./technology-choices.md) — Why these principles and frameworks
