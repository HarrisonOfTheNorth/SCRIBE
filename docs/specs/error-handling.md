# Error Handling Specification

## Error Response Envelope

All error responses use a consistent envelope format for predictable client handling.

### Standard Error Response
```json
{
  "error": {
    "code": "ERROR_CODE",
    "message": "Human-readable error description"
  }
}
```

### Response Fields
- **error.code** (string, required): Machine-readable error identifier in UPPER_CASE
- **error.message** (string, required): Human-readable description of what went wrong
- **error.details** (array, optional): Additional context for certain error types (validation errors)

### With Optional Details
```json
{
  "error": {
    "code": "VALIDATION_ERROR",
    "message": "Request validation failed",
    "details": [
      {
        "field": "email",
        "issue": "invalid email format"
      },
      {
        "field": "age",
        "issue": "must be 18 or older"
      }
    ]
  }
}
```

---

## HTTP Status Codes

### 2xx Success
| Code | Name | Usage |
|------|------|-------|
| 200 | OK | Request successful, response body included |
| 201 | Created | Resource successfully created (POST) |
| 204 | No Content | Success with no response body (DELETE) |

### 4xx Client Error
| Code | Name | Usage | Error Code |
|------|------|-------|-----------|
| 400 | Bad Request | Invalid request format or parameters | INVALID_REQUEST |
| 401 | Unauthorized | Authentication required | AUTHENTICATION_REQUIRED |
| 403 | Forbidden | Authenticated but lacks permission | FORBIDDEN |
| 404 | Not Found | Resource doesn't exist | NOT_FOUND |
| 409 | Conflict | Request conflicts with current state | CONFLICT |

### 5xx Server Error
| Code | Name | Usage | Error Code |
|------|------|-------|-----------|
| 500 | Internal Server Error | Unhandled exception or server error | INTERNAL_SERVER_ERROR |
| 503 | Service Unavailable | Service temporarily unavailable | SERVICE_UNAVAILABLE |

---

## Error Codes Registry

### INTERNAL_SERVER_ERROR
**HTTP Status**: 500
**Cause**: Unhandled exception, database error, or unexpected server failure
**Message**: "An unexpected error occurred"
**Details**: Typically omitted; client shouldn't rely on details for 5xx errors

**Example**:
```json
{
  "error": {
    "code": "INTERNAL_SERVER_ERROR",
    "message": "An unexpected error occurred"
  }
}
```

### INVALID_REQUEST
**HTTP Status**: 400
**Cause**: Malformed request body, missing required fields, invalid data types
**Message**: "Request validation failed" or specific field error
**Details**: Optional; may include field-level issues

**Example - Generic**:
```json
{
  "error": {
    "code": "INVALID_REQUEST",
    "message": "Request validation failed"
  }
}
```

**Example - With Details**:
```json
{
  "error": {
    "code": "INVALID_REQUEST",
    "message": "Request validation failed",
    "details": [
      { "field": "email", "issue": "must be a valid email address" }
    ]
  }
}
```

### AUTHENTICATION_REQUIRED
**HTTP Status**: 401
**Cause**: Request lacks valid authentication credentials
**Message**: "Authentication required"

**Example**:
```json
{
  "error": {
    "code": "AUTHENTICATION_REQUIRED",
    "message": "Authentication required"
  }
}
```

### FORBIDDEN
**HTTP Status**: 403
**Cause**: User is authenticated but lacks permission for the resource
**Message**: "Access denied"

**Example**:
```json
{
  "error": {
    "code": "FORBIDDEN",
    "message": "Access denied"
  }
}
```

### NOT_FOUND
**HTTP Status**: 404
**Cause**: Requested resource doesn't exist
**Message**: "Resource not found"

**Example**:
```json
{
  "error": {
    "code": "NOT_FOUND",
    "message": "Resource not found"
  }
}
```

### CONFLICT
**HTTP Status**: 409
**Cause**: Request conflicts with current state (e.g., duplicate resource creation, version mismatch)
**Message**: "Request conflicts with current state"

**Example**:
```json
{
  "error": {
    "code": "CONFLICT",
    "message": "Request conflicts with current state"
  }
}
```

### SERVICE_UNAVAILABLE
**HTTP Status**: 503
**Cause**: Service is temporarily unavailable (maintenance, overload)
**Message**: "Service temporarily unavailable"

**Example**:
```json
{
  "error": {
    "code": "SERVICE_UNAVAILABLE",
    "message": "Service temporarily unavailable"
  }
}
```

---

## Implementation Guidelines

### In Code
1. **Throw domain exceptions** with error code and message
2. **Catch exceptions** in middleware or endpoint handler
3. **Map to HTTP status code** and return error envelope
4. **Log original exception** for debugging (don't expose in response)

### Example C# Pattern
```csharp
try
{
    // Business logic
    return Results.Ok(new { message = "hello world" });
}
catch (ValidationException ex)
{
    return Results.BadRequest(new {
        error = new {
            code = "INVALID_REQUEST",
            message = ex.Message
        }
    });
}
catch (Exception ex)
{
    // Log the exception
    logger.LogError(ex, "Unhandled exception in endpoint");

    return Results.InternalServerError(new {
        error = new {
            code = "INTERNAL_SERVER_ERROR",
            message = "An unexpected error occurred"
        }
    });
}
```

### Testing Error Cases
- Test each HTTP status code response
- Verify error envelope structure
- Test that sensitive information is not leaked in error messages
- Verify error codes match specification

---

## Security Considerations

### Information Disclosure
- **DO**: Return generic error messages in production (e.g., "An unexpected error occurred")
- **DON'T**: Expose stack traces, SQL queries, or internal details to clients
- **DO**: Log full details internally for debugging

### Error Messages
- Keep messages user-friendly and actionable
- For 5xx errors, use generic messages only
- For 4xx errors, provide helpful guidance on fixing the request

### Rate Limiting (Future)
When implementing rate limiting, use:
- HTTP Status: 429 Too Many Requests
- Error Code: RATE_LIMIT_EXCEEDED
- Include `Retry-After` header with seconds to wait

---

## Related Specifications

- [Hello Endpoint](./hello-endpoint.md) — Example endpoint with error handling
- [API Design](./api-design.md) — Overall API design principles
