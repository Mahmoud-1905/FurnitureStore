# 03 — Authentication & Role Management
## Smart Furniture Store System

---

## 1. Authentication Architecture & Endpoints

> **Implementation Justification:** The system utilizes a modern, decoupled authentication architecture. The Node.js REST API is strictly stateless and issues JWTs (JSON Web Tokens), while the ASP.NET Core MVC frontend securely stores these tokens in HttpOnly cookies. This hybrid approach ensures maximum security (mitigating XSS and CSRF attacks) while allowing the Node.js API to remain scalable and accessible for future mobile clients.

Unlike traditional systems, **there is a single, unified login endpoint** for all user roles. The backend dynamically resolves the user's role upon successful authentication, allowing the MVC layer to execute role-based routing.

| Role | MVC Login Route | Node.js API Endpoint | Redirects To (On Success) |
|------|----------------|----------------------|---------------------------|
| All Roles | `/account/login` | `POST /api/v1/auth/login` | Dynamically Routed |
| Customer | - | - | `/` (Home / Catalog) |
| Store Manager | - | - | `/manager/dashboard` |
| Administrator | - | - | `/admin/dashboard` |

---

## 2. Session Security & Storage

> **Implementation Justification:** To bridge the gap between a stateless API and a stateful web frontend, the system relies on encrypted ASP.NET Core authentication cookies. The raw JWT is never exposed to client-side JavaScript (e.g., `localStorage`), significantly reducing the attack surface.

### 2.1 Unified Cookie Session (ASP.NET Core MVC)
```text
CookieName:      "SmartStoreAuth"
Attributes:      HttpOnly = true, Secure = true, SameSite = Strict
Session Claims:
- ClaimTypes.NameIdentifier = UserId          (INT)
- ClaimTypes.Email          = Email           (STRING)
- ClaimTypes.Role           = Role            (STRING — Customer|StoreManager|Administrator)
- "AccessToken"             = JWT string      (Stored inside the encrypted cookie)
```

### 2.2 API Token Management (Node.js & Redis)
```text
AccessToken:     JWT signed with RS256/HS256, contains UserId & Role (1h expiry)
RefreshToken:    Cryptographically secure random string, stored in Redis (7d expiry)
```

---

## 3. Login Flow — Step by Step

> **Implementation Justification:** A unified login flow centralizes credential validation, reduces code duplication across roles, and ensures a single choke point for applying security measures like rate limiting and audit logging.

### 3.1 Unified Login Process (`/account/login`)

```text
INPUT:  Email (NVARCHAR), Password (Plain text)

STEP 1: MVC validates inputs — ensures neither field is empty.
STEP 2: MVC securely forwards credentials to Node.js API: POST /api/v1/auth/login.
STEP 3: Node.js queries the Users table by Email.
        → If no record found: return 401 "Invalid Credentials".
STEP 4: Node.js checks IsActive flag.
        → If IsActive = 0: return 403 "Account deactivated by Administration".
STEP 5: Node.js verifies the provided password against the stored bcrypt hash.
        → If hash mismatch: return 401 "Invalid Credentials".
STEP 6: Authentication Successful.
        → Node.js generates a JWT Access Token and a Refresh Token.
        → Node.js stores the Refresh Token in Redis.
STEP 7: MVC receives the API response.
        → Extracts the JWT and Role.
        → Generates an encrypted ASP.NET Core session cookie holding the claims.
STEP 8: MVC executes role-based redirection:
        → Role == "Administrator" : Redirect to /admin/dashboard
        → Role == "StoreManager"  : Redirect to /manager/dashboard
        → Role == "Customer"      : Redirect to /
```

---

## 4. Logout Flow

> **Implementation Justification:** Proper session termination must occur on both the presentation layer (destroying the cookie) and the business logic layer (invalidating the refresh token) to prevent token hijacking.

### 4.1 Unified Logout Process (`/account/logout`)

```text
STEP 1: User initiates logout (via navigation bar).
STEP 2: MVC retrieves the current Refresh Token from the session claims.
STEP 3: MVC calls Node.js API: POST /api/v1/auth/logout.
STEP 4: Node.js removes the Refresh Token from Redis, effectively revoking it.
STEP 5: MVC executes `HttpContext.SignOutAsync()` to destroy the local encrypted cookie.
STEP 6: User is redirected to the public Home page (`/`).
```

---

## 5. Registration & Onboarding Flows

> **Implementation Justification:** To maintain strict administrative control over the single-vendor store environment, public self-registration is strictly limited to Customers. Elevated roles must be manually provisioned by an Administrator.

### 5.1 Customer Registration (`/account/register`)

```text
INPUT FIELDS:
  FullName       NVARCHAR(150)  — Required
  Email          NVARCHAR(255)  — Required, valid email format, unique
  Password       NVARCHAR(MAX)  — Required, meets complexity rules
  PhoneNumber    NVARCHAR(20)   — Optional

VALIDATION (Server-Side via Joi / MVC Model State):
  1. All required fields filled?      → If not: 400 Validation Error
  2. Password meets complexity rules? → If not: 400 Validation Error
  3. Email already exists in Users?   → If yes: 409 Conflict "Email already registered"

ON SUCCESS:
  → Node.js hashes the password using bcrypt (12 salt rounds).
  → INSERT into Users (FullName, Email, PasswordHash, Role='Customer').
  → Auto-login: Issue JWT and Refresh Token.
  → MVC generates the session cookie.
  → REDIRECT to / (Home) with a success toast notification.
```

### 5.2 Store Manager & Admin Onboarding (Manual Assignment)

```text
PROCESS:
  There are NO public registration endpoints for Store Managers or Administrators.
  To provision a new elevated account:
  
  STEP 1: The user must first register as a standard Customer via /account/register.
  STEP 2: An existing Administrator navigates to the Admin Panel.
  STEP 3: Administrator locates the user and executes UC23 (Assign/Revoke Roles).
  STEP 4: The system updates the User's Role column in the database.
  STEP 5: The action is permanently recorded in the AuditLogs (FR-A07).
  STEP 6: Upon the user's next login, the system issues a new JWT containing the elevated role claims, unlocking the respective dashboards.
```

---

## 6. Page Access Control (RBAC)

> **Implementation Justification:** Role-Based Access Control (RBAC) must be enforced at both the UI routing level (ASP.NET MVC) and the data access level (Node.js API) to ensure complete system security.

### 6.1 Access Rules Matrix

| Page / URL | Administrator | Store Manager | Customer | Guest |
|-----------|---------------|---------------|----------|---------|
| `/` (Home) | ✅ | ✅ | ✅ | ✅ |
| `/products` | ✅ | ✅ | ✅ | ✅ |
| `/account/login` | ❌ | ❌ | ❌ | ✅ |
| `/account/register`| ❌ | ❌ | ❌ | ✅ |
| `/cart` | ❌ | ❌ | ✅ | ✅ (Merged on login) |
| `/orders/checkout`| ❌ | ❌ | ✅ | ❌ |
| `/appointments/book`| ❌ | ❌ | ✅ | ❌ |
| `/manager/*` | ❌ | ✅ | ❌ | ❌ |
| `/admin/*` | ✅ | ❌ | ❌ | ❌ |

### 6.2 ASP.NET Core MVC Enforcement Pattern
Access control in the Presentation Tier is managed via ASP.NET Core's built-in authorization policies.

```csharp
// Example: Manager-only routing protection
[Authorize(Roles = "StoreManager")]
public class StoreManagerController : Controller
{
    public IActionResult Dashboard()
    {
        return View();
    }
}
```

### 6.3 Node.js API Enforcement Pattern
Access control in the Business Logic Tier is managed via JWT verification and role-assertion middleware.

```javascript
// Example: API-level route protection
router.post(
  '/products', 
  authenticate, // Middleware: Verifies JWT signature and expiry
  authorize(['StoreManager']), // Middleware: Asserts user role matches
  productsController.createProduct
);
```

---

## 7. Password Rules

> **Implementation Justification:** Enforcing strong password policies protects customer data and administrative accounts from brute-force and dictionary attacks.

| Rule | Requirement |
|------|------------|
| Minimum Length | 8 characters |
| Complexity | Must contain at least one uppercase letter and one number |
| Storage Format | Hashed via bcrypt with a minimum of 12 salt rounds |
| Plain Text Policy| Passwords are NEVER stored, logged, or returned in API responses |
| Validation Tier | Server-side validation via Joi (Node.js); Client-side validation for UX |

---

## 8. System & API Security Protocols

> **Implementation Justification:** Comprehensive security measures protect the application against common OWASP vulnerabilities, ensuring data integrity and user trust.

| Security Control | Implementation Detail |
|---------|-----------------------|
| HTTPS Enforcement | All traffic is strictly redirected to HTTPS; HSTS header applied (min-age=31536000). |
| JWT Expiry | 1 Hour (Access Token) |
| Refresh Token | 7 Days (Stored in Redis, rotated on use) |
| Rate Limiting | Login endpoints are restricted to 5 failed attempts per IP per 15 minutes before temporary blockage. |
| CSRF Protection | ASP.NET Core Anti-Forgery Tokens (`@Html.AntiForgeryToken()`) are required on all state-mutating POST forms. |
| SQL Injection | Prevented natively via Sequelize ORM parameterized queries; string interpolation is banned in raw SQL. |
