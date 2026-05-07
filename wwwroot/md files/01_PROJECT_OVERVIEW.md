# 01 — Project Overview
> **Destination:** Antigravity  
> **Purpose:** Give the AI a complete mental model before any coding begins.  
> **Rule:** Do NOT write code after reading this file. Understand first.

---

## Project Name
**Ruqi Store**

---

## Project Description
A single-vendor furniture e-commerce web application that allows customers to browse, purchase, and review furniture products online. The system is managed by one Store Manager and overseen by one Administrator.

This is NOT a multi-vendor marketplace. There is ONE product catalog, ONE inventory, and ONE store.
can be online payment.

---

## Technology Stack
| Layer | Technology |
|---|---|
| Frontend | HTML5, CSS3, Vanilla JavaScript + Razor Views (.cshtml) |
| Backend | ASP.NET Core MVC (C#) |
| Database (Production) | SQL Server + Entity Framework Core (Code-First) |
| Database (Development) | SQLite (local dev only — same EF Core models) |
| Authentication | ASP.NET Core Identity + Cookie-Based Auth |


---

## Main Users (Roles)
## Main Users (Roles)
| Role | Description |
|---|---|
| **Customer** | Registers, browses furniture, adds to cart, places orders, and submits product reviews |
| **Store Manager** | Manages the product catalog, inventory, and order fulfillment pipeline |
| **Payment Officer** | Reviews payment submissions, marks orders as Paid or Rejected, and logs all payment decisions |
| **Administrator** | Full system oversight — user management, role assignment, review moderation, audit logs, and reports |

---

## Main Goal
Enable a physical furniture store to operate a professional online presence — with full e-commerce functionality — while maintaining the trust of high-ticket buyers through showroom appointment integration.

---

## Core Features 
1. **Furniture Product Catalog** — searchable, filterable by category, price, material, stock status
2. **Shopping Cart** — persistent DB-backed cart 
3. **Checkout & Order Placement** — atomic transaction; price snapshot; stock deduction
4. **Order Tracking** — Pending → Processing → Shipped → Delivered → Cancelled
5. **Product Review System** — verified-purchase only; one review per customer per product
6. **Store Manager Dashboard** — product CRUD, inventory management, order processing, appointment calendar
7. **Admin Panel** — user management, role assignment, review moderation, audit logs, CSV reports

---

## What This System is NOT
- ❌ Not a multi-vendor marketplace
- ❌ Not a general merchandise store (furniture only)
- ❌ No native mobile app
- ❌ No Node.js, no React, no Vue, no Angular, no jQuery, no Bootstrap JS

---

## Antigravity Prompt
```
Read 01_PROJECT_OVERVIEW.md. Do not write code.
Summarize the project, the four roles, and all 7 features.
Confirm you understand: single-vendor, furniture-only, ASP.NET Core MVC, SQL Server (dev: SQLite).
```
