# 01_PROJECT_OVERVIEW

## Ruqi Store System

---

## 1. Project Idea

The Ruqi Store System is a **single‑vendor, full‑stack web application** designed for furniture retail. It combines an online catalog with a physical showroom appointment booking system.

### Problem

Single‑store furniture retailers often lack a digital catalog, leading to lost sales. High‑ticket items suffer from high cart‑abandonment because customers cannot verify material quality, scale, or comfort remotely.

### Solution

An omnichannel platform where:
- **Customers** browse the catalog, filter by dimensions/materials, place orders, and book showroom appointments to inspect items before purchase.
- **Store Manager & Administrator** (the same individual) manages product lifecycle, inventory, order fulfillment, appointments, and system configuration.
- **Admin** responsibilities such as user management, role assignment, and audit‑log inspection are handled by the same person acting as Store Manager.

---

## 2. Project Objectives

### 2.1 Functional Objectives
- Searchable, filterable catalog of all furniture.
- End‑to‑end order processing (cart → checkout).
- Showroom appointment booking synchronized with manager schedule.
- Verified‑purchase review system.
- Dashboard for the Store Manager/Administrator to monitor sales, inventory, and appointments.

### 2.2 Non‑Functional Objectives
- **Performance**: API p95 response < 500 ms; page render < 2 s.
- **Security**: JWT authentication, bcrypt password hashing, parameterized SQL.
- **Reliability**: 99.5 % uptime, automated daily backups.
- **Usability**: Responsive design, WCAG 2.1 AA compliance, full RTL support.
- **Scalability**: Stateless API with Redis for session management and caching.

---

## 3. Stakeholders

| Stakeholder            | Role                              | Primary Concern                                            |
|-----------------------|-----------------------------------|------------------------------------------------------------|
| **Customers**         | External Users                    | Ease of use, accurate product representation, order tracking, appointment booking |
| **Store Manager / Administrator** | Internal Operator (single person) | Operational efficiency, inventory tracking, order fulfillment, system configuration |
| **System Administrator** | Internal Overseer               | System stability, security compliance, data integrity |
| **Development Team**  | Creators                          | Technical feasibility, code quality, timely delivery |
| **University Supervisor** | Academic Evaluator            | Completeness, academic rigor, correct application of software engineering principles |

---

## 4. Development Methodology — Iterative (Agile)

Given the decoupled 3‑tier architecture, a rigid Waterfall model is unsuitable. An iterative Agile approach enables independent development, continuous testing, and seamless integration of API and MVC layers.

**Phases**:
```text
Phase 1: Requirements & Architecture → SAD, ERD, API contracts
Phase 2: Backend API Development → Node.js services, DB schema, JWT auth
Phase 3: Frontend Integration → ASP.NET Core MVC, Razor views, state management
Phase 4: Testing & Refinement → Load testing, security audits
Phase 5: Final Delivery → Deployment, academic defense
```

---

## 5. System Scope

### In Scope (Must be built)
- Single‑vendor product catalog management (CRUD, stock, images, dimensions).
- Customer shopping cart, persistent sessions, order history.
- Showroom appointment booking and management workflow.
- Secure authentication (JWT + cookie session, role‑based access).
- Verified product review system (tied to delivered orders).
- Store Manager/Administrator operational dashboard and admin oversight panel.
- Arabic (RTL) and English language support.

### Out of Scope (Not in this version)
- Multi‑vendor marketplace functionality.
- Real‑time payment gateway integration.
- Non‑furniture product categories.
- Augmented Reality room visualization.
- Native mobile applications.
- Delivery tracking via external logistics APIs.

---

*Prepared for inclusion in the project repository.*
