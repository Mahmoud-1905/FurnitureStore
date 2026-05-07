# 11 — Implementation Plan
> **Destination:** Antigravity  
> **Purpose:** Master build plan — forces Antigravity to build phase-by-phase in dependency order.  
> **Rule:** The phase order is NOT a suggestion. It is a dependency order. Data before Services before Auth before UI.

---

## ⚠️ Critical Rule
Do NOT run "build everything" in one prompt.  
Execute Phase by Phase.  
Show a completion summary after EACH phase before starting the next.

---

## Phase 1 — Read & Understand (No Code)
**Goal:** Confirm full understanding before writing anything.

Tasks:
- Read Files 01–07 in order
- Summarize: project name, roles, features, technology stack
- List all 11 database tables with their primary and foreign keys
- List all 8 controllers with their authorization requirements
- List all 9 Service classes and their primary responsibilities
- Confirm: single-vendor furniture store, ASP.NET Core MVC only, SQL Server + SQLite

**Success:** Complete written summary with zero misunderstandings before proceeding.

---

## Phase 2 — Project Scaffold
**Goal:** Create the solution structure.

Tasks:
- Create ASP.NET Core MVC project: `RuqiStore`
- Create folder structure: Controllers/, Services/, Repositories/, Models/Entities/, Models/ViewModels/, Data/, Middleware/, Exceptions/, Uploads/
- Install NuGet packages:
  - `Microsoft.EntityFrameworkCore.SqlServer`
  - `Microsoft.EntityFrameworkCore.Sqlite`
  - `Microsoft.EntityFrameworkCore.Tools`
  - `Microsoft.AspNetCore.Identity.EntityFrameworkCore`
  - `Microsoft.AspNetCore.Identity.UI`
- Add appsettings.json (SQL Server) and appsettings.Development.json (SQLite)

**Success:** Project builds with no errors. Folder structure matches File 10.

---

## Phase 3 — Data Layer
**Goal:** All EF Core entities, DbContext, relationships, migrations, and seed data.

Tasks:
- Create all 11 entity classes in `Models/Entities/`
- Create `ApplicationUser.cs` extending `IdentityUser`
- Create `AppDbContext.cs` with all DbSets and Fluent API configuration:
  - UNIQUE constraints (Carts.UserId, Products.SKU, Reviews unique per product+user)
  - CHECK constraints (Price > 0, StockQuantity >= 0, Rating 1–5)
  - FK cascade rules (CASCADE on OrderItems.OrderId, NO CASCADE on OrderItems.ProductId)
- Run `dotnet ef migrations add InitialCreate`
- Run `dotnet ef database update`
- Create `DbInitializer.cs` — seeds roles (Customer, StoreManager, Admin), Admin account, and 4 furniture categories

**Success:** All 11 tables created in DB. Seed data present. Admin can log in.

---

## Phase 4 — Service & Repository Layer
**Goal:** All business logic and data access implemented.

Tasks:
- Create all Repository interfaces in `Repositories/Interfaces/`
- Implement all Repositories using EF Core (async throughout — all methods `Async`)
- Create all custom exception classes in `Exceptions/`
- Create all Service interfaces in `Services/Interfaces/`
- Implement all Services with business rules from File 05:
  - CartService: one cart per user, stock validation
  - OrderService: atomic transaction, price snapshot
  - InventoryService: non-negative stock enforcement
  - AppointmentService: slot capacity, 24h window, duplicate check
  - ReviewService: verified purchase check, duplicate review check
  - FileUploadService: extension check, magic byte check, size limit
  - ProductService: unique SKU, category validation
  - UserService: deactivation + audit log
- Register all services and repositories in Program.cs (Scoped lifetime)

**Success:** All services compile. Unit tests for business rules pass.

---

## Phase 5 — Authentication & Authorization
**Goal:** Working login/register with role-based access.

Tasks:
- Configure ASP.NET Core Identity in Program.cs (password rules, lockout settings)
- Configure Cookie Authentication (HttpOnly, Secure, SameSite=Strict, 24h expiry)
- Enable AutoValidateAntiforgeryToken globally
- Create AccountController with Register and Login actions
- Create Register.cshtml and Login.cshtml views (plain HTML + CSS, no framework)
- Apply [Authorize(Roles=...)] to all appropriate controllers
- Configure LoginPath and AccessDeniedPath
- Configure GlobalExceptionMiddleware and RequestTimingMiddleware

**Success:** All 3 roles can register/login and see correct dashboard. Cross-role access returns 403.

---

## Phase 6 — UI from Stitch (MCP Integration)
**Goal:** Convert all 22 Stitch pages to Razor Views.

Tasks:
- Connect to Google Stitch via MCP using Project ID from File 09
- For each of the 22 pages:
  - Extract HTML/CSS structure from Stitch
  - Convert to Razor View (.cshtml) at the path specified in File 09
  - Replace static content with Razor `@Model` bindings
  - Apply CSS variables from File 09 (`--color-bg`, `--color-teal`, etc.)
  - Ensure all forms include `@Html.AntiForgeryToken()`
  - Ensure all form fields use ASP.NET Tag Helpers (`asp-for`, `asp-validation-for`)
- Create `_Layout.cshtml` and `_LayoutArabic.cshtml` (RTL Arabic variant)
- Create CSS files in `wwwroot/css/` and JS files in `wwwroot/js/`

**Success:** All 22 pages render. No broken layouts. Mobile-responsive.

---

## Phase 7 — Controllers
**Goal:** Wire all 8 controllers to services and views.

Tasks:
- Implement all controller actions from File 10
- Each action: validate ModelState → call service → map to ViewModel → return View
- Implement data isolation checks in customer-facing actions
- Add TempData messages for success/error feedback
- Implement pagination where required (product catalog, user list, order history)

**Success:** All routes functional. All role restrictions enforced. No 500 errors.

---

## Phase 8 — Order & Booking Flow (End-to-End)
**Goal:** Complete purchase and appointment flows working end-to-end.

Tasks:
- Test full cart → checkout → order placement flow (atomic transaction)
- Test stock deduction on order placement
- Test price snapshot on OrderItems
- Test showroom appointment booking with slot capacity enforcement
- Test 24-hour cancellation window
- Test review submission with verified-purchase check
- Test file upload: valid images, invalid type, size over limit
- Test all error cases from File 05 business rules

**Success:** Every business rule from File 05 produces the correct outcome.

---

## Phase 9 — Testing & Final Polish
**Goal:** Every acceptance criterion in File 12 shows PASS.

Tasks:
- Run through File 12 checklist for each role
- Test on mobile (375px viewport)
- Test Arabic RTL layout
- Test all error messages appear inline
- Test all loading indicators
- Check all audit log entries are created for admin actions
- Ensure no plaintext passwords in logs or responses
- Final review of all 22 pages for visual consistency

**Success:** File 12 checklist = 100% PASS. Project ready for submission.

---

## Antigravity Prompt
```
Read 11_IMPLEMENTATION_PLAN.md.
Execute the implementation plan PHASE BY PHASE.
After completing each phase, show a summary of what was built.
Do NOT proceed to the next phase until the current phase is complete and confirmed.
Start with Phase 1 now.
```
