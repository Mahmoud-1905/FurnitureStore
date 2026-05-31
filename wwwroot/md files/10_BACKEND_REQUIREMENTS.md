# 10_Backend Requirements

## Overview

This document describes the backend components required for the **FurnitureStore** application, covering API design, data models, authentication mechanisms, and deployment considerations.

## Controllers

- **AuthController** – Handles login, registration, JWT issuance, and password reset.
- **ProductController** – CRUD operations for catalog items, stock management, and image handling.
- **OrderController** – Cart processing, checkout workflow, order creation, status updates.
- **AppointmentController** – Scheduling, confirming, and cancelling showroom visits.
- **UserManagementController** – Administrator actions: activate/deactivate accounts, assign roles, audit logging.

## Models (Sequelize)

| Model | Primary Fields | Relationships |
|---|---|---|
| User | id, name, email, passwordHash, role, isActive | hasMany Orders, hasMany Appointments |
| Product | id, name, description, price, dimensions, material, stock, isActive | hasMany Reviews |
| Order | id, userId, status, totalAmount, createdAt | belongsTo User, hasMany OrderItems |
| OrderItem | id, orderId, productId, quantity, price | belongsTo Order, belongsTo Product |
| Appointment | id, userId, managerId, productId, scheduledAt, status | belongsTo User, belongsTo Manager, belongsTo Product |
| Review | id, userId, productId, rating, comment, isVerified | belongsTo User, belongsTo Product |

## Authentication & Authorization

- **JWT** – Stateless token issued on login, includes `sub` (user id) and `role` claims.
- **Role‑Based Access** – Middleware checks for roles: `Guest`, `Customer`, `Manager`, `Admin`.
- **Password Security** – Bcrypt with a cost factor of 12.

## Configuration

- **Environment Variables** – `DB_CONNECTION`, `JWT_SECRET`, `PORT`, `REDIS_URL`.
- **CORS** – Allow origins from the frontend domain only.
- **Rate Limiting** – 100 requests per minute per IP.

## Deployment

- **Containerization** – Dockerfile builds a Node.js image; use Docker Compose for API + Redis + SQL Server.
- **CI/CD** – GitHub Actions workflow builds Docker image, runs unit tests, and pushes to Azure Container Registry.
- **Logging** – Winston logger writes JSON logs to a mounted volume; errors are sent to Azure Monitor.

## Tests

- **Unit Tests** – Mocha/Chai for each controller and service.
- **Integration Tests** – Supertest to verify endpoint contracts, authentication flow, and role enforcement.
- **Performance Tests** – Artillery scripts targeting key API routes.

---

*Prepared for inclusion in the project documentation.*
