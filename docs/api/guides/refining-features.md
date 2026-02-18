# Refining Existing Features Guide

Workflow for changing the behaviour of something that already exists in the Hello World API, following the SCRIBE specification-first methodology.

## The SCRIBE Principle

Under SCRIBE, **you never hand-write code changes directly**. You update the specification that describes the current behaviour, then re-execute the implementation prompt. Claude reads all spec files and regenerates the complete codebase from them.

## When to Use This Workflow

Use this workflow (not the Adding Endpoints workflow) when:
- A stub in generated code needs replacing with a real implementation
- A response contract needs changing (new fields, different structure)
- A hardcoded value needs replacing with a configurable one (environment variable, secret, external config)
- A design decision needs evolving (different SDK, different pattern, different storage mechanism)
- An error code or HTTP status needs updating

> **Stubs in generated code are a signal that the corresponding spec is incomplete.** Refinement = completing the spec, then regenerating.

---

## Step-by-Step Workflow

### Step 1: Identify the Affected Spec File(s)

Find the spec file(s) that currently describe the behaviour you want to change:

| If you're changing... | Update this spec |
|---|---|
| Request or response contract | `docs/api/specs/{feature}-endpoint.md` |
| A library, SDK, or platform mechanism | `docs/api/specs/technology-choices.md` |
| A cross-cutting design principle | `docs/api/specs/api-design.md` |
| Error codes or HTTP statuses | `docs/api/specs/error-handling.md` |

---

### Step 2: Provision External Dependencies First

If the refinement requires resources that must exist before the code can work — environment variables, secrets, cloud configuration, third-party credentials — **provision those first**, outside the SCRIBE cycle.

Then note the exact names (variable names, key names, config references) that the generated code will need to reference. You will document these in the spec in Step 3.

**Example**: If replacing a hardcoded JWT secret with one read from an environment variable, first decide the variable name (e.g., `JWT_SECRET`), then document that name in the spec before running the implementation prompt.

---

### Step 3: Update the Spec File(s)

Edit the identified spec files to describe the **intended final behaviour** — not the stub, not the interim state.

Be specific:
- Name environment variables exactly as they will appear in configuration
- Name SDK classes, interfaces, or configuration patterns the implementation should use
- Describe the new behaviour completely enough that the implementation prompt can generate correct code without ambiguity

---

### Step 4: Update Error Codes (if changed)

If the refinement introduces new error conditions or changes existing error codes or HTTP statuses, update `docs/api/specs/error-handling.md`.

---

### Step 5: Execute the Implementation Prompt

Run `API-IMPLEMENTATION-PROMPT.md` as a Claude Code prompt. It reads all spec files — including your updated ones — and regenerates the complete codebase from scratch.

> **Restoration is not required before refinement.** The implementation prompt always regenerates from specs regardless. Restoration is only needed if you want to review specs in isolation before regenerating, or to start fresh from a clean state.

---

## Checklist

- [ ] Affected spec file(s) identified
- [ ] External dependencies provisioned (if any) and their names documented
- [ ] Spec file(s) updated to describe intended final behaviour
- [ ] Error codes updated in `error-handling.md` (if changed)
- [ ] `API-IMPLEMENTATION-PROMPT.md` executed and all tests pass

---

## Related Documentation

- [API Design](../specs/api-design.md) — Design principles and endpoint index
- [Error Handling](../specs/error-handling.md) — Error code registry
- [Technology Choices](../specs/technology-choices.md) — SDK and platform decisions
- [Adding Endpoints](./adding-endpoints.md) — Workflow for new endpoints (not refinements)
- [API-IMPLEMENTATION-PROMPT.md](../../API-IMPLEMENTATION-PROMPT.md) — The implementation prompt to execute
