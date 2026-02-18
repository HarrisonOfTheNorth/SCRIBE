# Adding Endpoints Guide

Workflow for adding new endpoints to the Hello World API following the SCRIBE specification-first methodology.

## The SCRIBE Principle

Under SCRIBE, **you never hand-write code to add a feature**. You write or update specifications, then re-execute the implementation prompt. Claude reads all spec files and regenerates the complete codebase from them.

---

## Step-by-Step Workflow

### Step 1: Write the Endpoint Specification

Create `docs/api/specs/{path-slug}-endpoint.md` following the pattern of existing endpoint specs (e.g., `hello-endpoint.md`):

- Path and HTTP method
- Authentication requirement (if any)
- Request body (JSON structure with example)
- Success response (HTTP status + JSON structure)
- All error responses (HTTP status + error codes)
- Implementation notes

**File naming**: lowercase with hyphens — `get-user.md`, `user-login.md`, `item-search.md`

---

### Step 2: Update the Endpoint Index

Add the new path to the endpoint table in `docs/api/specs/api-design.md`.

---

### Step 3: Add Error Codes (if new)

If the new endpoint introduces error codes not already in the registry, add them to `docs/api/specs/error-handling.md`.

---

### Step 4: Execute the Implementation Prompt

Run `API-IMPLEMENTATION-PROMPT.md` as a Claude Code prompt. It will read all spec files and generate the updated codebase — source code, tests, and guides.

---

## Checklist

- [ ] Endpoint spec written in `docs/api/specs/{name}.md`
- [ ] Request and response contracts documented
- [ ] curl examples included in spec
- [ ] Endpoint added to `api-design.md` index table
- [ ] New error codes (if any) added to `error-handling.md`
- [ ] `API-IMPLEMENTATION-PROMPT.md` executed and all tests pass

---

## Related Documentation

- [API Design](../specs/api-design.md) — Endpoint index and design principles
- [Error Handling](../specs/error-handling.md) — Error code registry
- [API-IMPLEMENTATION-PROMPT.md](../../API-IMPLEMENTATION-PROMPT.md) — The implementation prompt to execute
- [API-CLAUDE.md](../../API-CLAUDE.md) — Generated agent guidance for the codebase
