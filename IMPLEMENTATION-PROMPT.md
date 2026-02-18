# Implementation Prompt

Generate a complete, production-ready Hello World API project according to the specifications in this documentation directory.

## Part 1: Implement the API

The API should:
- Implement the endpoint specified in [Hello Endpoint Specification](./docs/specs/hello-endpoint.md)
- Follow all design principles in [API Design Specification](./docs/specs/api-design.md)
- Handle errors according to [Error Handling Specification](./docs/specs/error-handling.md)
- Use the technology stack defined in [Technology Choices Specification](./docs/specs/technology-choices.md)
- Follow all coding standards and conventions in [CLAUDE.md](./CLAUDE.md)
- Include a complete test suite following testing patterns in [CLAUDE.md](./CLAUDE.md)
- Be structured to scale following the workflow in [Adding Endpoints Guide](./docs/guides/adding-endpoints.md)

## Part 2: Verify the Implementation

Verify the implementation by:
- Building the project successfully with `dotnet build`
- Running all tests and confirming they pass with `dotnet test`
- Testing the endpoint manually and confirming it returns the expected response

## Part 3: Generate Getting Started Guide

After the API is implemented and all tests pass:

1. Review [Getting Started Guide Design Specification](./docs/specs/getting-started-design.md) for the abstract structure
2. Generate `/docs/guides/getting-started.md` with:
   - All sections defined in the design specification
   - Actual, project-specific setup steps and commands
   - Real expected output from this project's commands
   - Concrete prerequisite verification steps
   - Actual testing commands for this project's endpoints
   - Real troubleshooting relevant to the technology stack

The generated guide should follow the structure and principles in the design spec, but with actual content specific to this project.

## Part 4: Create Restoration Prompt

After getting-started.md is generated:

1. Review [Restoration Prompt Design Specification](./docs/specs/restore-prompt-design.md) for the abstract structure
2. Generate `/restoration-prompt.md` with:
   - Clear purpose: reversing the implementation to pre-implementation state
   - Verification steps to confirm all specification files remain
   - Safe deletion commands for all generated artifacts (/src, /tests, global.json, hello-world-api.sln, .gitignore, docs/guides/getting-started.md, API-CLAUDE.md)
   - Confirmation that all generated files are removed
   - Post-restoration verification checklist
   - **CRITICAL**: Self-deletion instruction as final step (remove restoration-prompt.md itself)

The generated restoration prompt allows users to cleanly remove all generated project artifacts and return the package to its pre-implementation state, ready for another implementation run. It deletes itself as the final step to complete the restoration cycle.

## Part 5: Generate API-CLAUDE.md (Final Step)

After restoration-prompt.md is generated and all code has been compiled and tested successfully:

1. Generate `/API-CLAUDE.md` — the Claude Code agent guidance file specific to this generated project
2. This file is the implementation-scoped equivalent of the repository-level `CLAUDE.md`
3. It must contain:
   - **Project Overview**: What the generated project is, why it exists, tech stack, and current phase scope
   - **Getting Started for Claude**: Quick context on the spec-first approach, all key terminal commands (build, run, test), configuration files, and links to documentation
   - **Coding Standards**: Language/framework-specific conventions, testing patterns, error handling rules, and git workflow
   - **Key Workflow**: Step-by-step process for adding new endpoints/features, adapted to this project's technology
   - **Important Gotchas**: Platform-specific notes, framework-specific issues, and any project-specific constraints discovered during implementation
   - **Links to Detailed Documentation**: All `/docs/specs/`, `/docs/guides/`, and `/docs/adr/` files with descriptions
   - **Document Metadata**: Version, date, audience, and cross-reference to `CLAUDE.md` (the methodology-level guidance)

4. The content must be populated with **actual, project-specific details** — real build commands, real test commands, real endpoint names, real file paths — not generic templates
5. The audience is a Claude Code agent that will work on this project in future sessions; it should be able to read `API-CLAUDE.md` and immediately understand how to build, test, and extend the project correctly

**Note**: `API-CLAUDE.md` is a generated artefact. It will be removed by `restoration-prompt.md` if the project is restored to specification-only state, and regenerated when `IMPLEMENTATION-PROMPT.md` is executed again.

---

**All decisions, requirements, and constraints are documented in the markdown files in this directory.**
