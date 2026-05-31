# 4. Scope / Out of Scope

## 4.1 In Scope (Must Have)
The following features are strictly within the project boundaries and are mandatory for successful delivery:
- **Product Management:** Complete CRUD operations for a single-vendor catalog, including tracking stock levels, high-resolution image handling, and dimension specifications.
- **Customer Journey:** A persistent shopping cart, comprehensive order history, and saved address management.
- **Appointment System:** A complete workflow for booking, viewing, and managing physical showroom visits.
- **Authentication & Authorization:** Secure, JWT and cookie-based session management enforcing strict Role-Based Access Control (RBAC).
- **Review System:** A module allowing customers to submit product reviews *only* if they have a verified, delivered order containing that item.
- **Administrative Control:** Dashboards for both the Store Manager (operations) and the Administrator (system oversight and user moderation).
- **Localization:** Native support for both Arabic (RTL) and English.

## 4.2 Out of Scope (Will Not Implement)
The following items are explicitly excluded from this version of the project to prevent scope creep and ensure timely delivery:
- **Multi-Vendor Capabilities:** The system is exclusively for a single store; marketplace features (like multiple sellers) are not supported.
- **Real-Time Payment Gateways:** Integration with live payment processors (like Stripe or PayPal) is excluded; payments are assumed to be handled offline or Cash on Delivery (COD) for this academic iteration.
- **Non-Furniture Products:** The data schema and UI are optimized strictly for furniture (e.g., specific dimensional fields).
- **Augmented Reality (AR):** No 3D models or AR room visualization features will be included.
- **Native Mobile Apps:** The project is a responsive web application; no native iOS or Android apps will be developed.
- **Third-Party Logistics (3PL):** Live delivery driver tracking via external shipping APIs (e.g., FedEx, Aramex) is excluded.
