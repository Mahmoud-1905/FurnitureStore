# 8. Non-Functional Requirements & Technical Constraints

These requirements dictate how the system must perform and the technical boundaries it must operate within.

## 8.1 Performance
- **Response Time:** 95% of backend API requests must be processed and return a response within 500 milliseconds under normal load.
- **Page Load:** Frontend views must render completely within 2 seconds on standard broadband connections.
- **Concurrency:** The system must comfortably support at least 100 concurrent active shopping sessions without degradation.

## 8.2 Security
- **Authentication:** All secure endpoints must require a valid JSON Web Token (JWT) transmitted via secure, HttpOnly cookies.
- **Password Storage:** All user passwords must be hashed and salted using bcrypt prior to database storage; plain text passwords must never be logged or stored.
- **Data Protection:** The system must use parameterized SQL queries (via an ORM like Sequelize) to eliminate the risk of SQL injection.
- **Transport:** All data in transit must be encrypted using TLS/HTTPS.

## 8.3 Reliability
- **Uptime:** The application must target a 99.5% uptime availability.
- **Backups:** The database must undergo automated, full backups daily to prevent catastrophic data loss.

## 8.4 Usability
- **Localization:** The UI must natively support Arabic (Right-to-Left, RTL) as the primary language, alongside English.
- **Responsiveness:** The UI must be built with a mobile-first philosophy, ensuring full operability on smartphones, tablets, and desktop displays.
- **Accessibility:** UI components should strive for WCAG 2.1 AA compliance (e.g., sufficient color contrast, keyboard navigability).

---

## 8.5 STRICT TECHNICAL CONSTRAINTS

To satisfy the specific academic and architectural requirements of this graduation project, the following constraints are absolute and cannot be violated:

1. **Architecture:** The project MUST utilize a strict 3-tier architecture.
2. **Backend:** The API layer MUST be built using **Node.js** and **Express.js**.
3. **Frontend:** The Presentation layer MUST be built using **ASP.NET Core MVC** (Razor Views).
4. **NO FRONTEND FRAMEWORKS:** The use of Single Page Application (SPA) frameworks or libraries such as **React, Angular, Vue.js, or Svelte is strictly prohibited**. All UI rendering must be handled server-side via ASP.NET Core MVC Razor views, enhanced only with vanilla JavaScript or basic jQuery where absolutely necessary.
5. **Database:** The primary relational data store MUST be **SQL Server**.
