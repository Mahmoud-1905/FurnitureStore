# 01. Project Introduction

## 1.1 Project Overview

Smart Furniture Store is a web-based furniture retail management system that combines digital shopping with personalized sales consultation to improve the customer purchasing experience. Unlike traditional e-commerce platforms that rely solely on online checkout, this system adopts a consultative selling approach suitable for furniture products, where customers often require additional guidance before making a purchase.

Customers can browse the furniture catalog, search for products, and add their preferred items to a shopping cart. Instead of completing an online payment, they submit a purchase request containing their selected products and contact information. The request is then delivered to the administrator through a centralized dashboard.

The administrator, acting as a sales representative, reviews incoming requests and contacts customers to answer questions, provide product recommendations, clarify specifications, discuss delivery options, and build customer confidence before finalizing the purchase. Once both parties reach an agreement, the order can either be delivered to the customer's location or completed at the physical showroom, depending on the customer's preference.

This hybrid sales model combines the convenience of online product discovery with the trust and personalized communication of traditional furniture sales, resulting in a more effective and customer-

---

## 1.2 Problem Statement

A traditional physical furniture boutique with a medium-scale catalog loses an estimated **$142,000 annually** due to manual, non-digitized workflows:

| Cost Area | Annual Impact (Estimated) |
| :--- | :--- |
| **Lost Sales from Inventory Out-of-Stock** | $48,000 |
| **Manual Stocktaking & Staff Overhead** | $32,000 |
| **Paperwork, Invoicing, & Billing Errors** | $18,000 |
| **Inefficient Order Tracking & Delivery Coordination** | $25,000 |
| **Customer Retention Loss (No Digital Engagement)** | $19,000 |

Beyond financial leaks, the offline model degrades the customer experience: buyers cannot browse custom finishes from home, inventory changes are not reflected in real-time (leading to overselling on rare items), and management lacks any data-driven insights to optimize seasonal stock demand.

---

## 1.3 Project Scope

### 🟢 In Scope (Version 10.0):
* **User & Identity Management:** Secure registration, authentication, and profile management for Customers and Administrators with role-based access control.
* **Catalog Management:** Dynamic furniture categorization (Living Room, Bedroom, Office, Dining Room) with product management including SKU, stock quantity, pricing, images, and specifications.
* **Interactive Shopping Tools:** Persistent database-backed shopping sessions (`Cart` & `CartItem` junction entities) and custom user `Wishlist` collections.
* **Purchase Request Management:** Customers can submit purchase requests based on the items in their shopping cart. Requests are stored in the system and forwarded to the administrator for review and customer follow-up.
* **Sales Consultation Workflow:** Administrators review submitted purchase requests and communicate with customers to answer inquiries, confirm product availability, discuss delivery options, and finalize the purchase.
* **Inventory Management:** Real-time stock level reduction and automated threshold alerts to prevent overselling.

### 🔴 Out of Scope:
* **Custom AR/VR Visualizer:** Virtual room-planning tools (planned for v2.0).
* **Third-Party Logistics Integration:** External fleet routing or delivery courier APIs (handled manually by administrators in v10.0).
* **Online payment processing and financing services:** are outside the scope of Version 10.0. Payment arrangements are completed after direct communication between the administrator and the customer.
* **Native Mobile Apps:** Standalone iOS/Android applications (fully responsive web interface only for v10.0).

---

## Project Objective

|   #   | Objective                                 | Success Metric                                                                                                                         |
| :---: | :---------------------------------------- | :------------------------------------------------------------------------------------------------------------------------------------- |
| **1** | **Simplify Customer Purchase Requests**   | 100% of purchase requests are successfully submitted from the shopping cart to the administrator without errors.                       |
| **2** | **Improve Customer Engagement**           | At least 90% of customer requests receive an initial response from the administrator within 24 hours.                                  |
| **3** | **Centralize Request Management**         | All customer requests are stored and managed through a single administrative dashboard with 100% request traceability.                 |
| **4** | **Enhance Product Management Efficiency** | Administrators can create, update, or remove furniture products with all changes reflected immediately in the product catalog.         |
| **5** | **Provide a Responsive User Experience**  | The system functions correctly on desktop, tablet, and mobile devices with page loading times below 3 seconds under normal conditions. |

---

## 1.5 Methodology

This project follows the Agile (Scrum) methodology with 2-week sprints to support iterative development, continuous stakeholder feedback, and gradual system enhancement. This approach was selected because the project requirements may evolve during development as users interact with early prototypes and provide feedback.

The rationale behind this decision includes:

* **Iterative Requirement Refinement:** Furniture product information, customer request workflows, and administrative features can be continuously refined based on stakeholder feedback throughout the development process.
* **Incremental Feature Validation:** Core system modules—including product browsing, shopping cart management, purchase request submission, and the administrative dashboard—are developed and validated incrementally to ensure each feature functions correctly before proceeding to the next.
* **Continuous Stakeholder Feedback:** Regular sprint reviews enable store representatives and project supervisors to evaluate system functionality, suggest improvements, and ensure the solution aligns with real business needs.
* **Risk Reduction:** Developing the system in small iterations minimizes implementation risks, allows early detection of defects, and ensures that customer request management and product administration operate reliably before deployment.

---

## 1.6 Document Audience

| Audience | What They Need |
| :--- | :--- |
| **Developers** | Software architecture layers, database tables, schema normalization, and C# service contracts. |
| **Business Analysts** | Core requirements, behavioral constraints, and traceability mapping back to business objectives. |
| **Project Managers** | Scope boundaries, sprint planning structure, and functional overview. |
| **QA / Testers** | Use cases, detailed scenarios, sequence flows, and strict acceptance criteria. |

---

[← Back to Index](./00-index.md) | [Next: Stakeholder Analysis →](./02-stakeholder-analysis.md)
