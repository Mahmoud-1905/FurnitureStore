# 02_Business Requirements Document (BRD)

## 1. Project Overview

The Ruqi Store System is a **single‑vendor, full‑stack web application** for furniture retail. It combines an online catalog with a physical showroom appointment booking system.

## 2. Problem Statement

Single‑store furniture retailers lack a digital catalog, causing lost sales. High‑ticket items suffer from high cart‑abandonment because customers cannot verify material quality, scale, or comfort remotely.

## 3. Objectives

- Provide a searchable, filterable digital catalog of all in‑store furniture.
- Enable end‑to‑end order processing (cart → checkout).
- Offer showroom appointment booking synchronized with the store manager’s schedule.
- Implement a verified‑purchase review system.
- Deliver a comprehensive Store Manager dashboard for real‑time inventory and order management.

## 4. Scope

### In Scope

- Single‑vendor product catalog management (CRUD, stock, images, dimensions).
- Customer shopping cart, persistent sessions, order history.
- Showroom appointment booking and management workflow.
- Secure authentication (JWT + cookie session, role‑based access).
- Verified product review system (tied to delivered orders).
- Store manager operational dashboard and admin oversight panel.
- Arabic (RTL) and English language support.

### Out of Scope

- Multi‑vendor marketplace functionality.
- Real‑time payment gateway integration.
- Non‑furniture product categories.
- Augmented Reality (AR) room visualization.
- Native mobile applications.
- Delivery tracking via external logistics APIs.

## 5. Stakeholders

- **Customers** – End users of the e‑commerce store.
- **Store Manager** – Operator managing catalog, orders, and appointments.
- **System Administrator** – Overseer of system stability, security, and users.
- **Development Team** – Creators of the application.
- **University Supervisor** – Academic evaluator of the project.

## 6. User Roles

- **Guest** – Browse catalog, search, filter, and register.
- **Customer** – All Guest actions + cart, checkout, appointment booking (up to 24 h in advance), order tracking, and verified reviews.
- **Store Manager** – Manage products, inventory, orders, and appointments.
- **Administrator** – Manage users, roles, reviews, and system reports.

## 7. Functional Requirements

- Browse and search products by category, material, and price.
- Registered customers can add items to a cart and place orders.
- Customers can book showroom appointments up to 24 h in advance.
- Store manager can add, edit, soft‑delete products and update inventory.
- Store manager can process orders and update status.
- Customers can submit reviews only for delivered orders.
- Administrator can activate/deactivate users and manage roles.

## 8. Non‑Functional Requirements

- **Performance**: API response < 500 ms (p95); page render < 2 s.
- **Security**: JWT authentication, bcrypt password hashing, parameterized SQL.
- **Reliability**: 99.5 % uptime, automated daily backups.
- **Usability**: Responsive design, WCAG 2.1 AA compliance, full RTL support.
- **Scalability**: Stateless API with Redis for session management and caching.

### Technical Constraints

- Frontend: ASP.NET Core MVC (Razor Views) – no React/Angular/Vue.
- Backend: Node.js + Express.
- Database: Microsoft SQL Server.

## 9. Business Rules

- **BR‑01**: Customers can submit a review only for a delivered order containing the product.
- **BR‑02**: Adding to cart or placing an order fails if quantity exceeds stock.
- **BR‑03**: Order placement must be atomic; inventory failures roll back the transaction.
- **BR‑04**: Product price changes never affect historical order prices.
- **BR‑05**: Products are soft‑deleted (IsActive = false) to preserve order history.
- **BR‑06**: Appointments cannot be canceled within 24 h of the scheduled time.
- **BR‑07**: All non‑public API endpoints validate JWT and required role.
- **BR‑08**: Administrative actions are recorded in an audit log.

## 10. Use Cases Summary

- **Browse Catalog** – Guest/Customer navigates categories, applies filters, views product details.
- **Checkout Process** – Customer adds to cart, provides shipping, confirms order, system deducts stock.
- **Book Appointment** – Customer selects date/time, confirms, manager is notified.
- **Manage Inventory** – Manager updates product quantities.
- **Moderate Reviews** – Admin reviews and deletes inappropriate content.

## 11. Success Criteria

- Users can browse and complete purchases without errors.
- Showroom appointment booking functions accurately with manager availability.
- Project meets all academic requirements (3‑tier architecture, no frontend frameworks).
- Application passes performance and security audits.
