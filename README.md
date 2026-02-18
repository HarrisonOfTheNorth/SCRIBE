# SCRIBE Methodology: Specification, Capture, Refinement, Implementation, Build, Execution

## What is SCRIBE?

**SCRIBE** is a specification-first development methodology that transforms natural language project descriptions into complete, tested, production-ready systems. Rather than jumping directly to coding, SCRIBE guides teams through a structured approach: capture requirements, refine specifications, generate implementation code, build and validate, and establish reversible iteration cycles.

SCRIBE is not a new theoretical framework—it's a practical synthesis of five well-established industry patterns applied specifically to AI-assisted software development. By following the SCRIBE methodology, teams can move from a project idea to a fully implemented, tested system in hours instead of weeks, while maintaining high-quality specifications as the single source of truth.

**Value Proposition**: From natural language project description to implemented, tested, production-ready system with reversible specifications.

**What SCRIBE Delivers**:
- Complete specification packages (11+ markdown files) ready for implementation
- Multiple implementation prompts (one for code generation, one for restoration)
- Reversible code generation enabling true specification-first development cycles
- Automated testing and quality validation built into the workflow
- Enterprise-scalable architecture growing from 8 → 20 → 35+ specification files

**Who Should Use SCRIBE**: Developers and teams using AI-assisted development; organizations adopting specification-first practices; projects requiring maintainable, iteration-friendly documentation; any project built with Claude Code or similar AI development assistants.

---

## The SCRIBE Acronym: What Each Letter Represents

SCRIBE is an acronym where each letter represents a critical phase in the development lifecycle:

### S — Specification (Choose or Create a Technology-Specific Speculator)

**What**: The Specification phase is about selecting or creating the appropriate SCRIBE Speculator for your technology type. A **Speculator** is a technology-specific specification generator prompt (a markdown file) that guides specification generation for a particular technology.

**Definition — Speculator**: A Speculator is a specification generator that asks structured, technology-appropriate questions and generates complete specification packages for a particular technology type. Speculators embody both "specification generation" and "architectural thinking" — they help architects and developers *speculate* on how their project should be structured.

**Purpose**: Speculators ask structured questions and generate complete specification packages. Different technologies (APIs, applications, databases, services, etc.) have different specification requirements and different Speculators. The choice of Speculator determines HOW specifications will be created for that technology.

**Speculators are Technology-Specific, NOT Project-Specific**:
- **API Speculator** works for ALL API projects (REST, GraphQL, gRPC, etc.)
- **Application Speculator** works for ALL application projects (web, desktop, mobile)
- **Database Speculator** works for ALL database projects
- **Service Speculator** works for ALL microservices
- Different Speculators ask different discovery questions
- But ALL Speculators follow the same SCRIBE methodology and doctrine

**How It Works**:
1. Identify your technology type (API, Application, Database, Service, etc.)
2. Look for existing SCRIBE Speculator for that technology
3. If a Speculator exists: use it
4. If no Speculator exists: create one based on the generic Speculator pattern
5. Execute the Speculator to begin the Capture phase (Phase 1)

**The Specification Artifact** (created by the Speculator):
After a Speculator is executed and specifications are generated and refined, you'll have:
- **CLAUDE.md** — Agent guidance with project context, workflow, and gotchas (300-400 lines)
- **README.md** — Human-oriented quick start guide
- **/docs/specs/** — Detailed specifications (design principles, error handling, technology choices, feature specifications)
- **/docs/guides/** — Procedural workflows and how-to guides
- **/docs/adr/** — Architecture decision records explaining why key decisions were made
- **Total**: 11+ markdown files that completely describe the project before any code exists

**Why Specification Phase is First**: Everything flows from the specification. Implementation generates code from it. Build validates against it. Execution proves it works correctly. Specifications remain authoritative—code is generated from them, not vice versa.

**SCRIBE Methodology is Agnostic to Technology**: All Speculators follow the same SCRIBE doctrine and principles. Whether you choose an API Speculator, Application Speculator, or Database Speculator, you follow the same workflow. All users "sing off the same hymn sheet" regarding SCRIBE principles.

---

### C — Capture (Interactive Discovery through AI Questioning)

**What**: The AI asks structured questions to discover and clarify all project requirements needed to define the specification.

**How**: TECHNICAL-SPECIFICATION-SPECULATOR.md (e.g., [API-SPECULATOR.md](./API-SPECULATOR.md)) runs and systematically questions the developer:
1. AI asks targeted questions organized into 5-7 categories:
   - Project Basics (name, purpose, vision, problem solved)
   - Technology Decisions (language, framework, testing framework)
   - Project Scope (main entities, endpoints, Phase 1 features)
   - API Specifics (response format, authentication, error handling)
   - Special Requirements (performance, security, integrations)
2. Each category includes 3-4 questions, totaling 15-25 questions
3. Developer answers each question with specific, detailed information
4. Answers clarify requirements that become part of the specification

**Why Capture is Critical**: Tacit project knowledge exists only in the developer's mind. Capture systematically extracts this knowledge through interactive questioning. The questions are exhaustive—capturing all necessary information before specification generation.

**Output**: Developer's answers to all questionnaire questions, ready for specification generation

**Duration**: 30-45 minutes for thorough answers

**Success Criteria**:
- All questions answered or marked "not applicable"
- Answers are clear and specific (not vague)
- Answers provide sufficient detail for generating specifications
- Developer is satisfied all key information has been captured

---

### R — Refinement (Developer & AI Collaborative Improvement of Specifications)

**What**: Developer and AI work together in an iterative loop to review generated specifications and improve them until they are correct and project-specific.

**How**:
1. Developer reads all 11+ specification files carefully (30-60 minutes typical)
2. Developer identifies gaps, differences, additions, or corrections needed:
   - Sections that don't match project vision
   - Missing requirements or edge cases
   - Technology choices that need adjustment
   - Feature details that need clarification
3. Developer chats with AI discussing those gaps, differences, additions, or corrections
4. AI suggests refinements to the specification files with detailed reasoning
5. Developer reviews AI suggestions and provides approval/authority to proceed
6. AI modifies the markdown files directly based on developer approval
7. Developer reads the updated specification files to verify changes
8. Iteration continues: steps 2-7 repeat until developer is satisfied

**Why Refinement is Essential**: TECHNICAL-SPECIFICATION-SPECULATOR.md creates initial specifications based on captured information, but the developer has the complete project knowledge. Refinement is the collaborative process where developer and AI work together to improve, customize, and finalize the specifications. This is where human judgment shapes the technical direction while AI handles the implementation of changes.

**Note on Capture + Refinement**: Capture extracts requirements through questioning. Refinement continues that process—the developer further clarifies and improves the specifications based on their full understanding of the project, with AI actively suggesting and implementing improvements.

**Key Distinction**: This is NOT manual file editing. It's AI-assisted refinement where:
- Developer identifies what needs to change
- AI proposes specific markdown changes
- Developer approves changes
- AI executes the changes
- Cycle repeats until specifications are finalized

**Output**: Finalized specification package with:
- All information accurate and project-specific
- All gaps filled through AI-developer collaboration
- All sections customized for this project
- Cross-references validated
- Ready for implementation with high confidence

**Duration**: 30-60 minutes for thorough refinement (iterative process)

**Success Criteria**:
- Specifications accurately reflect project requirements
- All gaps filled from developer's complete knowledge
- Developer and AI aligned on all changes
- Cross-references verified after all edits
- Developer confident specifications are complete and correct

---

### I — Implementation (Code Generation from Specifications)

**What**: AI reads the refined specification files and generates all project code including production implementation and test cases.

**How**: Developer executes IMPLEMENTATION-PROMPT.md:
1. IMPLEMENTATION-PROMPT.md reads all specification markdown files
2. AI analyzes specifications and understands the complete project definition
3. AI generates /src directory with complete implementation code
4. AI generates /tests directory with comprehensive test cases
5. Code generation completes (passive—code sits in directories)

**Why Implementation Comes After Specifications**: By the time implementation runs, the specification is complete and refined. AI has clear definition of what to build. Code is generated directly from the specification, not subject to interpretation or guesswork.

**Implementation is Passive**: Once code is generated by IMPLEMENTATION-PROMPT.md, it's complete. The developer does not manually write code. The AI generates everything based on the specifications.

**Output**:
- Complete /src directory with all implementation code
- Complete /tests directory with all test cases
- Code follows specification contracts exactly
- All code is generated (no manual coding)

**Duration**: 15-30 minutes for generation

**Note**: Additional outputs (getting-started.md, restoration-prompt.md) are generated as part of IMPLEMENTATION-PROMPT.md's 4-part system, not separate phases.

---

### B — Build (Automated Compilation and Testing)

**What**: AI runs the appropriate compilation and test commands for the generated code to verify the implementation is correct and matches the specifications.

**How**:
- AI runs the compilation command — Ensures all generated code compiles cleanly without errors (e.g., `dotnet build`, `npm run build`, `cargo build`, etc.)
- AI runs the test command — Runs all test cases, verifying they pass (e.g., `dotnet test`, `npm test`, `cargo test`, etc.; typically 15+ tests for Phase 1)
- Automated validation — Confirms implementation matches specification contracts

**Why Build is Essential**: Build phase proves that:
1. The specifications were clear enough for implementation
2. The generated code is correct and complete
3. The system works as specified
4. There are no gaps or ambiguities in the specification

**If Tests Fail**: Test failures indicate gaps in the specification (not code quality issues). Refinement phase can then improve the specification and regenerate.

**Output**:
- Successful compilation with no errors
- All tests passing (green test suite)
- Implementation validated against specifications
- High confidence that system works as specified

**Duration**: 5-15 minutes for compilation and testing

**What Build Does NOT Include**: Build only handles compilation and testing. It does not include running or consuming the application—that's the Execution phase.

---

### E — Execution (Developer Consumption and Use of Software)

**What**: Developer consumes and uses the generated software to verify it works and to validate the specifications were correct.

**How**: Developer interacts with the generated system:
- **For APIs**: Make requests to endpoints (POST, GET, etc.), verify responses match specification contracts
- **For applications**: Run the application, test user workflows, verify functionality
- **For services**: Call service methods, test behavior against specification
- Verify the system behaves exactly as specified

**Why Execution Validates Everything**: Execution is where the developer actually uses the software. This proves:
1. The specifications were correct and complete
2. The implementation matches the specifications
3. The system works as intended in real use
4. All specified features function properly

**Execution Enables Iteration**: After execution, the developer can:
- Identify any gaps or improvements needed in specifications
- Refine specifications based on learnings
- Run restoration-prompt.md to return to specification-only state
- Regenerate code with improved specifications
- Execute again to validate improvements

**Output**:
- Working software the developer can use
- Verification that system works as specified
- Knowledge of any gaps or improvements needed
- Foundation for next iteration cycle

**Duration**: Varies by project (typically 10-30 minutes for Phase 1)

**Restoration & Iteration**: After execution, if refinements are needed:
1. Run restoration-prompt.md (removes /src, /tests, generated guides)
2. Refine the specifications
3. Regenerate code using IMPLEMENTATION-PROMPT.md
4. Execute again
5. Repeat as needed

---

## The SCRIBE Workflow: Complete Lifecycle

The SCRIBE methodology begins with selecting the right technology-specific Speculator, then moves through six distinct phases working with the core Specification artifact:

### Specification Phase (S) — Choose or Create a Technology-Specific Speculator

**Goal**: Select or create the appropriate SCRIBE Speculator for your technology type

**What Happens**:
1. Developer identifies their technology type (API, Application, Database, Service, etc.)
2. Developer looks for existing SCRIBE Speculator for that technology
3. If a Speculator exists for that technology: use it
4. If no Speculator exists: create one based on the generic Speculator pattern
5. Once selected/created, execute the Speculator to begin Capture phase (Phase 1)

**Why Specification Phase is First**: The choice of Speculator determines HOW specifications will be created. Different technologies have different discovery questions and specification requirements. But all Speculators follow the same SCRIBE methodology and doctrine.

**Speculators are Technology-Specific, Not Project-Specific**:
- **API Speculator** works for ALL API projects (REST, GraphQL, gRPC, etc.)
- **Application Speculator** works for ALL application projects (web, desktop, mobile, console)
- **Database Speculator** works for ALL database projects (SQL, NoSQL, etc.)
- **Service Speculator** works for ALL microservices and distributed systems
- Different Speculators ask different discovery questions appropriate to the technology
- But all follow the same SCRIBE methodology and principles

**Output**: The appropriate SCRIBE Speculator (technology-specific specification generator prompt) ready to execute

**Duration**: 5-15 minutes to identify or select an existing Speculator

**Success Criteria**:
- Correct Speculator selected/created for your technology
- Speculator ready to execute
- Developer understands the Speculator will guide specification creation

---

### Phase 1: Capture (C) — AI Asks Questions to Discover Requirements

**Goal**: AI exhaustively questions the developer to capture all requirements needed for the specification

**What Happens**:
1. Developer executes the selected Speculator (e.g., TECHNICAL-SPECIFICATION-SPECULATOR.md (e.g., [API-SPECULATOR.md](./API-SPECULATOR.md)) for API Speculator)
2. AI systematically asks structured questions across 5-7 categories:
   - Project Basics (name, purpose, problem it solves)
   - Technology Decisions (language, framework, testing framework)
   - Project Scope (main entities, endpoints, Phase 1 features)
   - API Specifics (response format, authentication, error handling)
   - Special Requirements (performance, security, integrations)
3. Developer answers each question with specific, detailed information
4. Questions continue until all are exhausted
5. AI then proceeds to generate initial specification files based on answers

**Why Capture is First**: All requirements knowledge is tacit—it exists only in the developer's mind. Capture systematically extracts this through exhaustive questioning. The questionnaire is designed to be comprehensive, ensuring nothing important is missed.

**Output**: Developer's answers to all questionnaire questions, ready to feed into specification generation

**Duration**: 30-45 minutes

**Success Criteria**:
- All questions answered or marked "not applicable"
- Answers are clear and specific (not vague)
- Answers provide sufficient detail for generating specifications

---

### Phase 2: Specification Generation — AI Creates Initial Specification Files

**Goal**: AI generates the initial 11+ markdown specification files based on captured requirements

**What Happens**:
1. After capture phase completes, AI reads all developer answers
2. AI generates complete specification package in two passes:
   - **Pass 1**: Core files (CLAUDE.md, README.md, core specification files)
   - **Pass 2**: Auxiliary files (guides, ADRs, detailed specifications)
3. Output: 11+ markdown files defining the complete project
   - CLAUDE.md (agent guidance, 300-400 lines)
   - README.md (human-oriented quick start)
   - /docs/specs/ (API design, error handling, technology choices, feature specs)
   - /docs/guides/ (getting-started-design, adding-features)
   - /docs/adr/ (architecture decision records)

**Why Two-Pass Generation**: Pass 1 creates core/architecture files. User reviews Pass 1 while AI generates Pass 2. This prevents compound errors.

**Output**: Complete initial specification package (11+ markdown files)

**Duration**: 20-30 minutes for generation

**Success Criteria**:
- All files follow naming conventions
- Cross-references are correct
- Specifications are abstract (no code)
- Specifications are complete

---

### Phase 3: Refinement (R) — Developer & AI Collaboratively Improve Specifications

**Goal**: Developer and AI work together iteratively to review and improve specifications until they are correct and project-specific

**What Happens**:
1. Developer reads all 11+ specification files carefully (30-60 minutes typical)
2. Developer identifies gaps, differences, or areas needing improvement:
   - Sections that don't match project vision
   - Missing requirements or edge cases
   - Technology choices that need adjustment
   - Feature details that need clarification
3. Developer chats with AI about identified gaps and differences
4. AI suggests specific refinements to specification files
5. Developer approves refinements, granting AI authority to modify files
6. AI modifies the specification files directly
7. Developer reads updated files to verify changes are correct
8. Iteration continues until all gaps are addressed and developer is satisfied

**Why Refinement Continues Capture**: Capture exhausts questionnaire-based discovery. Refinement continues the discovery process through developer-AI conversation. The developer knows their project deeply—refinement lets developer and AI work together to improve specifications accordingly.

**AI-Developer Collaboration**: Unlike traditional development where developers manually edit files, SCRIBE Refinement is:
- Developer identifies needs and provides direction
- AI proposes specific changes to specification files
- Developer reviews and approves changes
- AI implements approved changes
- Process repeats iteratively

**Output**: Finalized, project-specific specification package with:
- All information accurate for this project
- All gaps filled through collaborative refinement
- All sections customized appropriately
- Cross-references validated
- Ready for implementation with high confidence

**Duration**: 30-60 minutes for thorough refinement (typically 3-5 iteration cycles)

**Success Criteria**:
- Specifications accurately reflect project requirements
- All gaps filled from developer's complete knowledge
- Developer and AI have consensus on all specifications
- Cross-references verified after all edits
- Developer confident specifications are correct and complete

---

### Phase 4: Implementation (I) — AI Generates Code from Specifications

**Goal**: AI reads the refined specifications and generates all project code

**What Happens**:
1. Developer executes IMPLEMENTATION-PROMPT.md
2. AI reads all refined specification markdown files
3. AI analyzes the complete specification
4. AI generates /src directory with all implementation code
5. AI generates /tests directory with comprehensive test cases
6. Code generation completes (passive—code sits in directories)

**Why Implementation Comes After Refinement**: By this point, the specification is complete and refined. AI has clear definition of what to build. Implementation is direct and unambiguous.

**Implementation is Passive**: Once IMPLEMENTATION-PROMPT.md generates code, it's complete. The developer doesn't manually write code. AI generates everything from the specification.

**Output**:
- Complete /src directory with all implementation code
- Complete /tests directory with all test cases
- Code follows specification contracts exactly
- All code is generated (no manual coding)

**Duration**: 15-30 minutes for code generation

**Success Criteria**:
- Code generation completes without errors
- All specified code is present
- Implementation matches specifications exactly

---

### Phase 5: Build (B) — Automated Compilation and Testing

**Goal**: Compile code and run all tests to validate implementation matches specifications

**What Happens**:
1. AI runs the appropriate compilation command — ensures all code compiles cleanly (e.g., `dotnet build`, `npm run build`, `cargo build`, etc.)
2. AI runs the appropriate test command — verifies all test cases pass (e.g., `dotnet test`, `npm test`, `cargo test`, etc.; typically 15+ tests)
3. AI validates implementation against specification contracts
4. AI generates getting-started.md guide (IMPLEMENTATION-PROMPT.md Part 3)
5. AI generates restoration-prompt.md (IMPLEMENTATION-PROMPT.md Part 4)

**Why Build is Critical**: Build proves that:
- The specifications were clear enough for implementation
- The generated code is correct and complete
- The system works as specified
- All tests pass

**If Tests Fail**: Test failures indicate gaps in the specification (not code quality issues). Refinement can improve specifications and regenerate.

**Output**:
- Successful compilation with no errors
- All tests passing (green test suite)
- Getting-started guide with specific setup instructions
- Restoration script ready for iteration
- Implementation validated against specifications

**Duration**: 5-15 minutes for compilation and testing

**Success Criteria**:
- `dotnet build` succeeds with no errors
- `dotnet test` shows all tests passing
- All implementation matches specification contracts
- Getting-started and restoration files generated

---

### Phase 6: Execution (E) — Developer Consumes and Uses Software

**Goal**: Developer uses the generated software to validate it works as specified

**What Happens**:
1. Developer runs/consumes the generated application:
   - **For APIs**: Makes requests to endpoints (POST, GET, etc.), verifies responses
   - **For applications**: Runs the app, tests workflows
   - **For services**: Calls service methods, tests behavior
2. Developer verifies the system behaves exactly as specified
3. Developer identifies any gaps or improvements needed

**Why Execution is Essential**: Execution proves:
- The specifications were correct and complete
- The implementation matches the specifications
- The system works as intended in real use
- All specified features function properly

**Execution Enables Iteration**: After using the system, developer can:
- Identify any gaps or improvements needed in specifications
- Refine specifications based on learnings
- Run restoration-prompt.md to return to specification-only state
- Regenerate code with improved specifications
- Execute again to validate improvements

**Output**:
- Working software the developer can use
- Verification that system works as specified
- Knowledge of any gaps or improvements needed
- Foundation for next iteration cycle (if needed)

**Duration**: Varies by project (typically 10-30 minutes for Phase 1)

**Iteration Cycle** (repeat as needed):
1. Use the system (Execution phase)
2. Refine specifications based on learnings
3. Run restoration-prompt.md (returns to specification-only state)
4. Run IMPLEMENTATION-PROMPT.md again (regenerates code)
5. Run Build phase again (verify compilation and tests)
6. Use the system again (Execution phase)
7. Repeat as needed

**Success Criteria**:
- Developer successfully uses the system
- System behaves as specified
- Any gaps identified for next iteration
- Specifications remain authoritative
- Code can be regenerated from specifications

---

## Key Design Principles

SCRIBE is built on six foundational design principles that guide all decisions:

### 1. Specification-First

**Principle**: All requirements are documented in abstract markdown specifications before any code is written.

**Implementation**:
- Specifications define "what" the system should do (contracts, behaviors, constraints)
- Specifications are abstract, not concrete (no paths, no implementation details)
- Code implements "how" the system achieves those specifications
- Specifications remain authoritative; code is always secondary

**Benefit**:
- Clear requirements before expensive coding
- Multiple implementation languages/frameworks possible from same specs
- Easy to communicate requirements to stakeholders
- Specifications become living documentation

---

### 2. Reversible Systems

**Principle**: Complete ability to restore to specification-only state enables true iteration.

**Implementation**:
- Specifications stored in version control (permanent)
- Generated code files marked as temporary/regeneratable
- Restoration prompts remove generated artifacts cleanly
- Specifications preserved through entire iteration cycle

**Benefit**:
- Refine specifications without code accumulation
- Eliminate technical debt from iteration
- Regenerate with different technology if needed
- True specification-first development enabled

---

### 3. Two-Pass Generation

**Principle**: Structural generation happens in two passes to reduce compound errors.

**Implementation**:
- **Pass 1**: Core files that define architecture (CLAUDE.md, README.md, core specs)
- **Pass 2**: Auxiliary files that depend on core (guides, ADRs, detailed specs)
- User reviews between passes and can request refinement
- Pass 2 builds on validated Pass 1 outputs

**Benefit**:
- Errors in Pass 1 don't cascade into Pass 2
- User can refine core architecture before details
- Reduces overall error rate significantly
- Clear decision points for course correction

---

### 4. Progressive Disclosure

**Principle**: CLAUDE.md is lean; detailed information is in /docs/ subdirectories.

**Implementation**:
- CLAUDE.md: 300-400 lines of agent guidance (persistent rules, workflows, gotchas)
- Links to /docs/specs/ for detailed requirements
- Links to /docs/guides/ for procedural knowledge
- Links to /docs/adr/ for architectural decisions

**Benefit**:
- Agents read only what they need, when they need it
- CLAUDE.md stays stable while specs evolve
- Knowledge organized by type and detail level
- Follows AGENTS.md industry standard

---

### 5. Validation-Built-In

**Principle**: Errors are caught early through explicit validation before implementation.

**Implementation**:
- Output Validation Checklist validates before user sees artifacts
- Constraint Specification prevents entire error classes upfront
- Cross-reference validation ensures consistency
- AGENTS.md standard compliance verified automatically

**Benefit**:
- Errors caught before implementation (when expensive)
- User never sees invalid specifications
- Higher first-pass quality
- Reduces iteration cycles

---

### 6. Composable & Scalable

**Principle**: System grows from 8 → 20 → 35+ files without architectural refactoring.

**Implementation**:
- Flat directory structure (no prescriptive subdirectories)
- Supports nested generators (/src/GENERATOR-FEATURE.md in future)
- Template inheritance pattern for customization
- Meta-generator creates generators (infinite scaling)

**Benefit**:
- Phase 1 project can grow to Phase 3 without redesign
- Teams can create domain-specific generators
- Patterns scale to any project size
- Supports microservices and modular architectures

---

## Foundational Industry Standards

SCRIBE is not a new theoretical framework—it's a synthesis of five proven industry patterns:

### 1. Meta-Prompting (Academic Standard)

**What**: Prompts that generate other prompts, enabling self-scaling systems.

**How SCRIBE Uses It**: TECHNICAL-SPECIFICATION-SPECULATOR.md (e.g., [API-SPECULATOR.md](./API-SPECULATOR.md)) is a meta-prompt that generates specification packages. The specification generator itself can be customized or regenerated for different domains.

**Reference**: Meta Prompting arxiv papers, Prompt Engineering guides

---

### 2. Prompt Pattern Catalog (Vanderbilt University)

**What**: 16 documented reusable prompt patterns for common AI tasks.

**How SCRIBE Uses It**:
- Discovery Before Generation pattern (Specification Capture phase)
- Template-Based Generation pattern (Specification Refinement phase)
- Verification Steps pattern (Review & Validation phase)

**Reference**: "A Prompt Pattern Catalog to Enhance Prompt Engineering with ChatGPT" (arxiv.org/abs/2302.11382)

---

### 3. OpenAI Meta-Prompt API

**What**: Formalized meta-schemas for prompt generation and management.

**How SCRIBE Uses It**:
- Two-pass generation strategy
- Template inheritance model
- Constraint specification before generation
- Decision rationale documentation

**Reference**: OpenAI Prompt Generation API documentation

---

### 4. Spec-Driven Development Methodology

**What**: Transforms natural language requirements into complete specifications before implementation.

**How SCRIBE Uses It**:
- All development is specification-first
- Code is generated from specifications
- Specifications are source of truth
- Tests verify specification clarity

**Reference**: "Mastering Spec-Driven Development with Prompted AI Workflows"

---

### 5. AI Project Scaffolding (Emerging Pattern)

**What**: Semantic scaffolding using plain English input to maintain consistent project structure.

**How SCRIBE Uses It**:
- Questionnaire captures project in plain English
- Scaffolding generates consistent file structures
- Specifications follow AGENTS.md standard
- Generated code follows project conventions

**Reference**: Claude Code Templates (aitmpl.com), MetaGPT

---

### AGENTS.md/CLAUDE.md Industry Standard

**What**: Industry standard adopted by 60,000+ projects, endorsed by OpenAI/Google/Sourcegraph.

**How SCRIBE Builds on It**:
- CLAUDE.md is primary agent guidance file
- Progressive Disclosure pattern (main file links to details)
- Flat structure with flexibility (no prescriptive directories)
- SCRIBE extends for specification-first development

**Key Characteristics**:
- Lean CLAUDE.md (300-400 lines, not comprehensive)
- Links to /docs/ for detailed requirements
- Nested CLAUDE.md support for monorepos
- Markdown as universal format

---

### Markdown as Universal Format

**Why Markdown**:
- Tool-agnostic (works with any editor, IDE, version control)
- Human and machine readable
- Version control friendly (diffs, merges work well)
- Supports linking, embedding examples, code blocks
- Universal adoption across development tools

**SCRIBE Markdown Practices**:
- All files: lowercase-with-hyphens naming
- H1 once per file (primary heading at top)
- Consistent bullet styles (- for all lists)
- Links use relative paths: `[Link](./other-file.md)`
- Code blocks for examples and specifications

---

## Intended Outcomes: What Teams Achieve with SCRIBE

Teams using SCRIBE achieve:

### Faster Project Setup
- From project idea to implemented system: 2-4 hours (vs. 2-4 weeks manually)
- From specification to code: 30-45 minutes
- Zero manual scaffolding or boilerplate coding
- Immediate productivity on actual features

### Reduced Rework
- Specification refinement cycles before expensive coding
- Clear requirements prevent mid-implementation changes
- Tests provide early verification of completeness
- Fewer "back-to-the-drawing-board" moments

### Maintainable Documentation
- Specifications remain authoritative, always up-to-date
- Code is generated artifact, not permanent
- All architectural decisions documented in ADRs
- Knowledge transfer through specifications, not code reading

### Quality Assurance
- Automated testing built into implementation workflow
- Tests verify specifications were clear and complete
- Code generation follows specifications exactly
- No manual coding errors or shortcuts

### Knowledge Transfer
- New team members learn system through specifications
- All project decisions documented in /docs/adr/
- Getting-started guide tailored to specific project
- Workflow guides show how to extend system

### Scalability
- Grows from 1-endpoint APIs (Phase 1) to enterprise systems (Phase 3)
- No architectural refactoring needed as project grows
- Supports microservices and distributed systems
- Enables team scaling without knowledge loss

### Iteration Without Debt
- Restore and refine cycles without code accumulation
- Specifications remain clean and organized
- Easy to experiment with different approaches
- True agile development enabled (specification-driven, not code-driven)

---

## Getting Started with SCRIBE

To start using SCRIBE for your next project:

### Step 1: Create Your Project Description
Write a natural language description of your project:
- What does it do?
- Why does it exist?
- Who will use it?
- What problems does it solve?

### Step 2: Use Your Selected Speculator
- Select or create the appropriate Speculator for your technology (e.g., API Speculator, Application Speculator, etc.)
- Answer the questionnaire questions in the Speculator
- Answer all 15-25 questions (or mark "not applicable")
- Provide specific, detailed answers
- Take 30-45 minutes for thorough answers

### Step 3: Review Generated Specifications
- Review the 11+ generated specification files
- Check that core architecture matches your vision
- Verify technology choices are appropriate
- Plan customizations for Phase 3-5

### Step 4: Refine Specifications
- Edit files that don't match your project specifics
- Validate cross-references after edits
- Maintain naming conventions
- Spend 30-60 minutes refining (typical)

### Step 5: Generate Implementation Code
- Run IMPLEMENTATION-PROMPT.md Part 1
- Review generated /src and /tests directories
- Ensure code follows project conventions
- Takes 15-30 minutes

### Step 6: Verify Implementation
- Run the appropriate compilation command for your technology
- Run the appropriate test command for your technology
- Perform manual testing of endpoints/features
- Verify implementation matches specifications
- Takes 5-15 minutes

### Step 7: Use and Iterate
- Deploy or continue development
- Use getting-started.md for team onboarding
- Refine specifications as you learn
- Use restoration-prompt.md to restore and iterate
- Can regenerate code and retest in seconds

---

## Critical Points to Remember

### SCRIBE is Not a New Theory
It synthesizes five proven industry patterns. No experimental or unvalidated approaches—all foundational patterns are established and proven at scale.

### The Primary Artifact is the Speculator
Speculators (technology-specific specification generator prompts like API Speculator, Application Speculator, etc.) are the core deliverables. The SCRIBE methodology exists to explain how and why Speculators work effectively.

### Specifications Enable Code Generation
Specifications are not generated from code—code is generated from specifications. Specifications are permanent; code is temporary and regeneratable.

### Iteration is First-Class
Restoration and refinement are built into the methodology from the start, not afterthoughts. True agile development is enabled by specification-first approach.

### Start Small, Think Big
Phase 1 specifications are lean (8 markdown files). Projects grow to Phase 2 (18-20 files) and Phase 3 (35+ files) without architectural changes needed.

---

## Summary

SCRIBE provides a complete, proven methodology for AI-assisted specification-first development. By following the six phases (Specification Capture → Refinement → Review → Implementation → Build & Execution → Restoration & Iteration), teams can move from project ideas to tested systems in hours, maintain clean specifications as source of truth, and iterate with confidence.

The methodology is built on established industry patterns, follows the AGENTS.md standard, and enables teams to scale from simple APIs to enterprise systems without architectural refactoring.

For detailed implementation guidance, see your selected Speculator (e.g., API Speculator, Application Speculator, etc.). For architecture decisions, see /docs/adr/. For specific project examples, see /docs/specs/.

---

## Credits

**SCRIBE Methodology** was developed with valuable contributions from **Anthony Harrison**, a software engineer and architect with extensive experience across mobile platforms and cloud technologies.

Anthony's background includes:
- **Mobile Platforms**: Windows Phone, Android, and iOS development
- **Technologies**: Predominantly Silverlight, Windows Phone, Xamarin, and .NET MAUI; additionally Java and XCode
- **Cloud Infrastructure**: Extensive Azure Cloud experience
- **Career Roles**: Producer, Senior Software Engineer, Tech Lead, and Architect
- **Portfolio**: Contributed to over 40 mobile applications published in the Google Play Store and Apple App Store

His deep architectural expertise, refined through multiple roles across diverse teams and projects, has significantly influenced the design and methodology of the SCRIBE framework.
