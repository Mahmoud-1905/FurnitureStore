# Ruqi Store
## AI-Ready Project Specification — Master Index

---

## ⚡ How to Use These Files with AI Tools

> **Read this before feeding any file to an AI tool (Lovable / Bolt / Cursor / Windsurf / Replit).**

### Execution Order — STRICTLY FOLLOW THIS SEQUENCE

```text
00_README.md                ← You are here (always read first)
01_PROJECT_OVERVIEW.md
02_ARCHITECTURE_AND_TECH.md
03_DATABASE_SCHEMA.md
04_API_DOCUMENTATION.md
05_AUTH_AND_ROLES.md
06_CUSTOMER_FEATURES.md
07_MANAGER_FEATURES.md
08_ADMIN_FEATURES.md
09_SHOWROOM_INTEGRATION.md
10_UI_UX_SPEC.md            ← Responsive layout, language support (RTL/LTR)
11_TESTING_STRATEGY.md      ← Unit, Integration, and UAT cases
```

### Prompting Strategy for AI Tools

**Never** paste all files at once. Feed them **one phase at a time**:

```text
Phase 1 — Foundation & DB:
  "Read 01_PROJECT_OVERVIEW.md, 02_ARCHITECTURE_AND_TECH.md, and 03_DATABASE_SCHEMA.md.
   Set up the Node.js API scaffold, ASP.NET Core MVC project, and generate database migrations."

Phase 2 — API & Auth:
  "Read 04_API_DOCUMENTATION.md and 05_AUTH_AND_ROLES.md.
   Implement the Node.js REST API, JWT authentication, and RBAC middleware."

Phase 3 — Core Features (Backend & Frontend):
  "Read 06_CUSTOMER_FEATURES.md and 07_MANAGER_FEATURES.md.
   Build the product catalog, cart, checkout flow, and Store Manager dashboard."

Phase 4 — Advanced Features & Admin:
  "Read 08_ADMIN_FEATURES.md and 09_SHOWROOM_INTEGRATION.md.
   Implement appointment booking logic, user moderation, and audit logging."

Phase 5 — Polish & Testing:
  "Read 10_UI_UX_SPEC.md and 11_TESTING_STRATEGY.md.
   Apply bilingual (Arabic/English) UI support and run integration tests."
```

---

## Project Summary

| Field | Value |
|-------|-------|
| **Project Title** | Ruqi Store |
| **Type** | Single-Vendor Furniture E-Commerce + Showroom |
| **Domain** | Furniture retail (sofas, beds, tables, etc.) |
| **Architecture** | 3-Tier (ASP.NET Core MVC + Node.js API + SQL Server/Redis) |
| **Language** | Bilingual: Arabic (RTL) & English (LTR) |
| **Academic Level** | Graduation Project — Bachelor of Computer Science |
| **Year** | 2026 |

---

## File Map

| File | Contents |
|------|----------|
| `01_PROJECT_OVERVIEW.md` | Problem statement, stakeholders, scope, assumptions, constraints. |
| `02_ARCHITECTURE_AND_TECH.md` | 3-Tier architecture justification, code folder structure, request lifecycle. |
| `03_DATABASE_SCHEMA.md` | 10 entities, normalization, ER diagram syntax, constraints. |
| `04_API_DOCUMENTATION.md` | Node.js Express REST API endpoints, request/response envelopes. |
| `05_AUTH_AND_ROLES.md` | JWT access/refresh tokens, bcrypt hashing, cookie handling, role matrix. |
| `06_CUSTOMER_FEATURES.md` | Registration, catalog browsing, cart, checkout, order tracking, reviews. |
| `07_MANAGER_FEATURES.md` | Product CRUD, stock update, category management, order fulfillment. |
| `08_ADMIN_FEATURES.md` | User management, review moderation, reports, system audit logs. |
| `09_SHOWROOM_INTEGRATION.md` | Appointment booking, slot capacity validation, cancellation rules. |
| `10_UI_UX_SPEC.md` | Responsive design rules, RTL layout logic, loading feedback, accessibility. |
| `11_TESTING_STRATEGY.md` | Unit/Integration tests with Jest/Supertest, UAT scenarios. |

---

## Technology Stack

```text
Presentation Layer (Frontend): ASP.NET Core MVC (C#) / Razor Views / Bootstrap 5 / JavaScript
Business Logic Layer (API):    Node.js / Express.js / Joi (Validation)
Data Access (Backend):         Sequelize ORM
Database:                      SQL Server (Primary) / Redis (Cache & Session)
Authentication:                JWT (API) + ASP.NET Core Cookie Session
```

---

## Non-Negotiable Rules for AI Tools

1. **Strict 3-Tier Architecture** — MVC controllers MUST consume the Node.js API via `HttpClient`. MVC must NEVER connect directly to SQL Server. Justification: Enforces a strict API contract, decouples business logic from presentation, and allows independent scaling.
2. **Single-Vendor Constraint** — Do NOT implement multi-vendor or marketplace features. Only the Store Manager role manages the product catalog. Justification: Scope is explicitly bounded to a single physical store's operations.
3. **Bilingual Support** — Full RTL layout for Arabic and LTR for English. UI strings must be translatable. Justification: Required for regional accessibility and academic requirements.
4. **Data Integrity** — Inventory deduction during checkout MUST be wrapped in a database transaction. Prevent overselling atomically. Justification: Prevents race conditions and partial commits during concurrent checkouts.
5. **No Destructive Deletes** — Products must be soft-deleted (`IsActive=false`). Historical orders and prices must remain intact (no cascading deletes for products in orders). Justification: Preserves historical financial data and order accuracy.
6. **Authentication Security** — Use JWT for the API. Hash passwords with `bcrypt` (min 12 rounds). Enforce role claims on all protected routes. Justification: Protects sensitive user data and prevents unauthorized privilege escalation.
7. **Audit Logging** — All administrative actions (user deactivations, review deletions, role changes) MUST be logged in the `AuditLogs` table. Justification: Ensures traceability and accountability for sensitive system changes.
8. **Parameterized Queries** — All SQL queries MUST use parameterized statements (via Sequelize) to prevent SQL injection. Justification: Fundamental security requirement to protect against injection attacks.
