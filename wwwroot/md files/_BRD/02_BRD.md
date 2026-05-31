# 02_Business Requirements Document (BRD)

## 1. Project Overview
The Ruqi Store System is a single-vendor, full-stack web application designed for furniture retail. It provides an online e-commerce catalog integrated with a physical showroom appointment booking system, enabling customers to bridge the digital and physical purchasing experience.

## 2. Problem Statement
Single-store furniture retailers typically lack a digital catalog, leading to lost sales from remote buyers. Furthermore, high-ticket furniture items suffer from an industry-average 87% cart abandonment rate online because customers cannot verify material quality, scale, or comfort remotely.

## 3. Objectives
- Provide a searchable, filterable digital catalog of all in-store furniture.
- Enable end-to-end order processing, from cart management to multi-step checkout.
- Facilitate a showroom appointment booking system synchronized with the store manager's schedule.
- Implement a verified-purchase review system to build customer trust.
- Provide a comprehensive Store Manager dashboard for real-time inventory and order management.

## 4. Scope / Out of Scope
### In Scope
- Single-vendor product catalog management (CRUD, stock, images, dimensions).
- Customer shopping cart, persistent sessions, and order history.
- Showroom appointment booking and management workflow.
- Secure authentication system (JWT + Cookie session, Role-based access).
- Verified product review system (strictly tied to delivered orders).
- Store manager operational dashboard and administrative oversight panel.
- Arabic (RTL) and English language support.

### Out of Scope
- Multi-vendor marketplace functionality.
- Real-time payment gateway integration.
- Non-furniture product categories.
- Augmented Reality (AR) room visualization.
- Native mobile applications (iOS/Android).
- Delivery tracking via external third-party logistics APIs.

## 5. Stakeholders
- **Customers:** End users of the e-commerce store.
- **Store Manager:** Operator managing catalog, orders, and appointments.
- **System Administrator:** Overseer of system stability, security, and users.
- **Development Team:** Creators of the application.
- **University Supervisor:** Academic evaluator of the project.

## 6. User Roles
- **Guest:** Can browse, search, and filter the catalog, and register an account.
- **Customer:** Can manage cart, checkout, book appointments, track orders, and submit reviews.
- **Store Manager:** Can manage products, orders, and appointments.
- **Administrator:** Can manage users, roles, reviews, and system reports.

## 7. Functional Requirements
- The system must allow users to browse and search for products by category, material, and price.
- The system must allow registered customers to add items to a shopping cart and place orders.
- The system must allow registered customers to book showroom appointments up to 24 hours in advance.
- The system must allow the store manager to add, edit, and soft-delete products, and update inventory.
- The system must allow the store manager to process orders and update their status.
- The system must allow customers to submit product reviews only for delivered orders.
- The system must allow the administrator to activate/deactivate users and manage roles.

## 8. Non-Functional Requirements
- **Performance:** API response times under 500ms (p95) and page rendering within 2 seconds.
- **Security:** Implement JWT for API calls, bcrypt password hashing, and parameterized SQL queries.
- **Reliability:** Target 99.5% uptime with automated daily database backups.
- **Usability:** Fully responsive design with WCAG 2.1 AA compliance and full RTL (Arabic) support.
- **Scalability:** Stateless API design leveraging Redis for session management and caching.

### Technical Constraints
- The project must use ASP.NET Core MVC for the frontend and Node.js/Express for the Backend API.
- **No frontend frameworks** (such as React, Angular, or Vue) are permitted; standard MVC Razor views must be used.
- SQL Server will be used as the primary relational database.

## 9. Business Rules
- **BR-01:** A customer can only submit a product review if they have a completed ("Delivered") order containing that specific product.
- **BR-02:** Adding an item to the cart or placing an order must fail if the requested quantity exceeds the current stock level.
- **BR-03:** Order placement must be handled atomically; if inventory deduction fails, the transaction rolls back.
- **BR-04:** Product price changes by the Store Manager shall never retroactively affect unit prices in historical orders.
- **BR-05:** Products can only be "soft-deleted" (IsActive=false).
- **BR-06:** Showroom appointments cannot be canceled by the customer within 24 hours of the scheduled time.

## 10. Use Case
- **Browse Catalog:** Guest/Customer navigates categories, applies filters, and views product details.
- **Checkout Process:** Customer adds to cart, provides shipping details, confirms order, and system deducts stock.
- **Book Appointment:** Customer selects a date/time slot, confirms booking, and Manager is notified.
- **Manage Inventory:** Manager logs in, navigates to products, updates quantity, and saves changes.
- **Moderate Reviews:** Admin logs in, views reported reviews, and deletes inappropriate content.

## 11. Success Criteria
- The system successfully allows users to browse and complete purchases without errors.
- Showroom appointment booking is functional and accurately reflects the manager's availability.
- The project meets all academic requirements, including the strict adherence to the 3-tier architecture and the "no frontend frameworks" constraint.
- The application passes all performance and security audits.
