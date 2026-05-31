# 03_USER_ROLES_AND_PERMISSIONS

## Overview

This document defines the access‑control model for the Furniture Store system. The **Store Manager** and **Administrator** are the same individual, so their permissions are merged. Each role lists the actions it **can** perform and those it **cannot**. The rules drive ASP.NET Core `[Authorize(Roles=...)]` attributes and the role‑based checks in the Node.js API.

---

### 1. Guest (Unauthenticated)
- **Can**: Browse the public catalog, view product details, register an account.
- **Cannot**: Access protected endpoints, place orders, book appointments, submit reviews.

### 2. Customer (Authenticated)
- **Can**: All Guest actions, plus add items to a cart, checkout, book showroom appointments (up to 24 h in advance), track orders, manage address book, and submit reviews **only for delivered purchases**.
- **Cannot**: Manage products, view other users' data, change system configuration.

### 3. Store Manager / Administrator (Authenticated, combined role)
- **Can**: All Customer actions, plus create/edit/soft‑delete products, update inventory, process orders, confirm/reject appointments, view sales analytics, manage user accounts, assign/revoke roles, moderate reviews, and inspect audit logs.
- **Cannot**: Perform domain‑specific clinical actions (not applicable).

---

## Role Matrix

| Action                     | Guest | Customer | Store Manager / Administrator |
|----------------------------|:-----:|:--------:|:-----------------------------:|
| View Public Catalog        | ✅   | ✅      | ✅                           |
| Register Account           | ✅   | ❌      | ❌                           |
| Add to Cart / Checkout     | ❌   | ✅      | ✅                           |
| Book Appointment           | ❌   | ✅      | ✅                           |
| Manage Products            | ❌   | ❌      | ✅                           |
| Manage Users / Roles       | ❌   | ❌      | ✅                           |
| View Audit Logs            | ❌   | ❌      | ✅                           |

---

## Implementation Notes

- ASP.NET Core: Use `[Authorize(Roles = "Admin,Manager")]` on controllers that require combined privileges.
- Node.js API: Enforce combined role checks via Passport‑JWT strategies.
- The merged role simplifies administration and aligns with the project requirement that the Store Manager and Administrator are the same person.
