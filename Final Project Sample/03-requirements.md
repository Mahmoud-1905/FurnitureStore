# 03. System Requirements Specification

## 3.1 Functional Requirements

### User Identity & Access Management

| ID | Requirement | Priority |
| :--- | :--- | :---: |
| **FR-001** | The system shall allow customers to register an account and log in securely using their email and password. | Must |
| **FR-002** | The system shall enforce Role-Based Access Control (RBAC) with defined roles: Customer, Store Manager, Accountant, and Administrator. | Must |
| **FR-003** | The system shall allow authenticated users to update their profile information and shipping addresses. | Should |
| **FR-004** | The system shall allow users to securely trigger password resets via email verification. | Must |
| **FR-005** | The system shall support persistent user authentication using secure, cryptographically signed cookies (`.AspNetCore.Identity`). | Must |

### Dynamic Product Catalog

| ID | Requirement | Priority |
| :--- | :--- | :---: |
| **FR-006** | The system shall display a categorized, searchable product catalog of furniture items (e.g., Living Room, Bedroom, Office). | Must |
| **FR-007** | The system shall allow users to filter products by price range, material composition, and real-time stock availability. | Should |
| **FR-008** | The system shall display detailed product pages showing descriptions, dimensions, SKU, physical stock quantities, and high-res WebP galleries. | Must |
| **FR-009** | The system shall allow Store Managers to create, update, and toggle the active status (`IsActive`) of products and categories. | Must |

### Shopping Cart & Wishlist

| ID | Requirement | Priority |
| :--- | :--- | :---: |
| **FR-010** | The system shall provide a database-backed, persistent shopping cart that preserves items across user login sessions. | Must |
| **FR-011** | The system shall allow users to add, update quantity, and remove items from their cart in real time without refreshing the page. | Must |
| **FR-012** | The system shall prevent users from adding items to their cart that exceed current stock limits. | Must |
| **FR-013** | The system shall provide a Wishlist ("Curated Collection") where users can save furniture items for future checkout. | Should |
| **FR-014** | Upon successful checkout, the system shall automatically and immediately clear all items from the active cart. | Must |

### Order & Inventory Processing

| ID | Requirement | Priority |
| :--- | :--- | :---: |
| **FR-015** | The system shall execute checkout, stock verification, and stock deduction inside a single, atomic database transaction. | Must |
| **FR-016** | The system shall capture and save the product price as a static snapshot (`PriceSnapshot`) inside the `OrderItems` table upon checkout. | Must |
| **FR-017** | The system shall support order state tracking through a predefined pipeline: `Pending` → `Processing` → `Shipped` → `Delivered` → `Cancelled`. | Must |
| **FR-018** | The system shall automatically generate system notifications or emails to the Store Manager if an item's stock falls below a low-stock threshold (e.g., < 5). | Should |

### Reviews & Showroom Appointments

| ID | Requirement | Priority |
| :--- | :--- | :---: |
| **FR-019** | The system shall allow customers to submit product reviews (rating and comments) only for products they have verifiedly purchased. | Must |
| **FR-020** | The system shall restrict customers to exactly one review per product to maintain review integrity. | Must |
| **FR-021** | The system shall allow users to book physical showroom viewing appointments through a dynamic booking calendar. | Should |
| **FR-022** | The system shall allow Store Managers to approve, reschedule, or cancel pending showroom appointments. | Must |

---

## 3.2 Non-Functional Requirements

### Performance & Scalability

| ID | Requirement | Metric |
| :--- | :--- | :--- |
| **NFR-001** | The system shall respond to API requests within 500ms under normal conditions. | 95th percentile response < 500ms |
| **NFR-002** | The system shall load fully styled pages in less than 2 seconds. | Tested with modern broadband speeds |
| **NFR-003** | The system shall support up to 200 concurrent users performing cart operations without degradation. | CPU utilization remains < 75% |

### Security & Privacy

| ID | Requirement | Metric |
| :--- | :--- | :--- |
| **NFR-004** | The system shall prevent SQL injection automatically using parameterized queries through Entity Framework Core. | Security audit verified |
| **NFR-005** | The system shall encrypt all data in transit using TLS 1.3 (HTTPS). | Zero HTTP fallback allowed |
| **NFR-006** | The system shall protect all post submissions against CSRF attacks using secure anti-forgery tokens. | Enforced on all state-changing forms |
| **NFR-007** | The system shall automatically terminate inactive user sessions after 30 minutes. | Automatic token/cookie invalidation |
| **NFR-008** | The system shall log all administrative actions, catalog updates, and order status changes with a User ID and timestamp. | Retained in `AuditLogs` database |

### Usability & Accessibility

| ID | Requirement | Metric |
| :--- | :--- | :--- |
| **NFR-009** | The system shall fully support RTL layouts when Arabic is selected, utilizing logical CSS properties for alignment. | Validated via `dir="rtl"` toggling |
| **NFR-010** | The system interface must comply with WCAG 2.1 Level AA accessibility guidelines. | Automated contrast and keyboard check |
| **NFR-011** | The checkout interface shall require no more than 3 steps from the cart page to order finalization. | Verified via UX workflow audit |

### Reliability & Support

| ID | Requirement | Metric |
| :--- | :--- | :--- |
| **NFR-012** | The system database must perform scheduled automated backups every 12 hours. | 30-day rotation history retained |
| **NFR-013** | The system must achieve a recovery time objective (RTO) of less than 4 hours in the event of failure. | Backed by a disaster recovery plan |

---

## 3.3 Prioritization Summary (MoSCoW)

| Priority | Count | Examples |
| :--- | :---: | :--- |
| **Must Have** | 16 | Login, transactional checkout, stock deduction, price snapshots, cart clearance, RBAC. |
| **Should Have** | 6 | Wishlist collections, automated low-stock alerts, showroom scheduling, catalog filters, RTL toggling. |
| **Could Have** | 0 | *(Deferred to v1.1 roadmap planning)* |
| **Won't Have** | — | AR/VR visualizer, mobile native apps, direct carrier delivery API integrations. |

---

[← Previous: Stakeholder Analysis](./02-stakeholder-analysis.md) | [Back to Index](./00-index.md) | [Next: Use Case Model & Descriptions →](./04-use-case-model.md)
