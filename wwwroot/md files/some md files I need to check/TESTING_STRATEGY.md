# 11 — Testing Strategy
## Ruqi Store — Verification & Validation Plan

---

## 1. Overview
> **Testing Goal:** To ensure the Ruqi Store system is secure, performant, and functions correctly across all three tiers (ASP.NET MVC, Node.js API, and SQL Server). The testing strategy focuses on both automated verification and manual validation of core business logic.

---

## 2. Unit Testing
- **Tier 2 (API):** Unit tests for Node.js services (e.g., `ProductService`, `OrderService`) using **Jest**.
  - **Focus:** Business rules (e.g., stock cannot be negative, one review per customer).
- **Tier 1 (MVC):** Unit tests for ViewModels and Utility classes in C# using **xUnit**.
  - **Focus:** Correct mapping of API responses to UI models.

---

## 3. Integration Testing
- **API-to-Database:** Testing the Sequelize models against a test SQL Server database.
- **MVC-to-API:** Using `WebApplicationFactory` in ASP.NET Core to test the full request/response cycle, mocking the Node.js API where necessary, or running against a live test API instance.

---

## 4. Security & Performance Testing

### 4.1 Security Audit
- **JWT Validation:** Ensure protected endpoints return `401 Unauthorized` without a valid token and `403 Forbidden` with a valid token but incorrect role.
- **SQL Injection:** Attempt string interpolation in search fields to verify Sequelize's parameterization is working.
- **CSRF:** Verify that forms without a valid `__RequestVerificationToken` are rejected by the MVC layer.

### 4.2 Performance (Artillery)
- **Normal Load:** 50 concurrent users. Target API response time < 500ms.
- **Stress Test:** 200 concurrent users. Target 0% error rate.
- **Image Delivery:** Verify WebP compression and CDN/static file serving speed (< 300ms per image).

---

## 5. User Acceptance Testing (UAT) — Manual Checklist

| ID | Test Scenario | Expected Result |
|----|---------------|-----------------|
| **UAT-01** | **Full Purchase Flow** | Guest browses → Registers → Adds item → Completes Checkout → Stock decrements → Order appears in history. |
| **UAT-02** | **Overselling Prevention**| Attempt to add 10 items when only 5 are in stock. System must block and show error. |
| **UAT-03** | **Showroom Booking** | Customer books slot → Manager confirms → Customer sees "Confirmed" status. |
| **UAT-04** | **Verified Review** | Customer with "Delivered" order submits review. Review appears on product page. |
| **UAT-05** | **Role Restriction** | Customer attempts to access `/manager/dashboard`. System redirects to `/account/login` or shows 403. |
| **UAT-06** | **RTL/Bilingual** | Switch to Arabic. Navbar, product grid, and checkout form must flip to Right-to-Left. |

---

## 6. Testing Tools
- **Backend:** Jest, Supertest (API testing)
- **Frontend:** Selenium / Playwright (for automated UI tests)
- **Performance:** Artillery.io
- **Manual:** Postman (API debugging), Chrome DevTools (UI/Network)
