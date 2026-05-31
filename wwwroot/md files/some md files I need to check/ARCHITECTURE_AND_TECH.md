# 02 — Architecture & Technology Stack
## Ruqi Store — 3-Tier Client-Server System

---

## 1. Architectural Choice: 3-Tier Decoupled
> **Justification:** The system is built using a strict 3-tier architecture to ensure clear separation of concerns, scalability, and ease of testing. By decoupling the presentation layer from the business logic, we enable future expansion (e.g., native mobile apps) without rewriting the core logic.

| Tier | Layer | Technology | Primary Responsibility |
|------|-------|------------|------------------------|
| **Tier 1** | Presentation | **ASP.NET Core MVC (C#)** | Server-side HTML rendering (Razor Views), UI routing, session cookie management, and calling the API via HttpClient. |
| **Tier 2** | Business Logic | **Node.js + Express.js** | Processing business rules, JWT issuance, data validation (Joi), and orchestrating data access. |
| **Tier 3** | Data Access | **SQL Server + Redis** | Relational data persistence (SQL Server) and high-speed session/token caching (Redis). |

---

## 2. Technology Stack Detail

### 2.1 Frontend (Presentation Tier)
- **Framework:** ASP.NET Core 8.0 MVC
- **Templating:** Razor Views (Server-Side Rendering)
- **Styling:** Bootstrap 5 (Responsive)
- **Interaction:** Vanilla JavaScript + jQuery (for AJAX/DOM)
- **Communication:** `IApiClient` service using `HttpClient` to consume Node.js REST API.

### 2.2 Backend API (Logic Tier)
- **Runtime:** Node.js (v20+)
- **Framework:** Express.js
- **Validation:** Joi (Schema-based request validation)
- **Security:** Passport.js / JWT (JSON Web Tokens)
- **Logging:** Winston / Morgan

### 2.3 Data Layer
- **Relational DB:** Microsoft SQL Server (Transact-SQL)
- **ORM:** Sequelize (Node.js side)
- **Caching:** Redis (for Cart sessions and JWT Refresh Tokens)
- **Migrations:** Sequelize CLI (Version-controlled schema)

---

## 3. Request Lifecycle
> **Scenario:** A customer filters furniture products by "Sofa" category.

1. **Browser:** Sends HTTP GET request to `/products?category=sofa` (MVC Route).
2. **MVC Controller:** Receives request and calls `IApiClient.GetProductsAsync({ category: 'sofa' })`.
3. **API Client:** Injects the JWT Bearer token from the user's session cookie into the header and calls the Node.js API.
4. **Node.js Middleware:** Verifies the JWT signature and extracts the `UserId`.
5. **Node.js Controller:** Calls `ProductService.getFilteredProducts()`.
6. **SQL Server:** Executes a parameterized query: `SELECT * FROM Products WHERE CategoryId = 5 AND IsActive = 1`.
7. **Node.js API:** Returns a JSON response containing the product list.
8. **MVC Controller:** Maps the JSON to a `ProductListViewModel` and renders the Razor View.
9. **Browser:** Displays the fully rendered HTML to the customer.

---

## 4. Security Architecture
- **Stateless API:** The Node.js layer does not maintain sessions; it relies on JWT.
- **Secure Cookies:** The MVC layer stores the JWT in an `HttpOnly`, `Secure`, `SameSite=Strict` cookie to prevent XSS and CSRF.
- **Password Hashing:** `bcrypt` with 12 salt rounds ensures passwords are never stored in plain text.
- **Database Security:** Parameterized queries via Sequelize prevent SQL Injection.

---

## 5. Development Environment
- **IDE:** Visual Studio 2022 (MVC) & VS Code (Node.js)
- **Source Control:** Git
- **API Testing:** Postman / Thunder Client
- **Load Testing:** Artillery
