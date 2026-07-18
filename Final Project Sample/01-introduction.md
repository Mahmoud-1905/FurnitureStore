# 01. Project Introduction

## 1.1 Project Overview

Many local and specialized furniture boutiques with medium-scale operations commonly rely on manual physical showrooms, paper-based stock records, disconnected spreadsheets, and manual order tracking. This operational model leads to inventory discrepancies, slow order fulfillment, poor shopping experiences, and a complete lack of real-time visibility into sales and stock levels.

The **Ruqi Store** is a premium, web-based e-commerce platform designed specifically to digitize and streamline core furniture retail operations. It integrates user identity management, an interactive catalog, digital shopping carts, persistent wishlists, secure order checkout, and automated inventory sync into a single, cohesive software system.

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

### 🟢 In Scope (Version 1.0):
* **User & Identity Management:** Secure registration, login, and profile tracking with distinct roles (Customer, Store Manager, Accountant, Administrator).
* **Catalog Management:** Dynamic categorization (Living Room, Bedroom, Office) and robust product tracking with critical commercial attributes (SKU, StockQuantity, Pricing).
* **Interactive Shopping Tools:** Persistent database-backed shopping sessions (`Cart` & `CartItem` junction entities) and custom user `Wishlist` collections.
* **Order & Transaction Management:** Structured checkout flows, stateful order lifecycles, and **immutable price freezing** at the exact moment of checkout.
* **Automated Stock Synchronization:** Real-time stock level reduction and automated threshold alerts to prevent overselling.

### 🔴 Out of Scope:
* **Custom AR/VR Visualizer:** Virtual room-planning tools (planned for v2.0).
* **Third-Party Logistics Integration:** External fleet routing or delivery courier APIs (handled manually by administrators in v1.0).
* **Installment Financing / Credit Engines:** Native long-term payment installments (only standard payment methods are supported in v1.0).
* **Native Mobile Apps:** Standalone iOS/Android applications (fully responsive web interface only for v1.0).

---

## 1.4 Project Objectives

| # | Objective | Success Metric |
| :---: | :--- | :--- |
| **1** | **Reduce Order Processing Overhead** | 50% reduction in average time required to log and verify custom orders. |
| **2** | **Prevent Overselling & Discrepancies** | 100% accuracy in stock deduction immediately post-checkout via isolated transactions. |
| **3** | **Enhance Customer Retainability** | At least 35% of registered users utilizing the "Curated Collection" Wishlist feature. |
| **4** | **Guarantee Price Auditing Security** | 0% variance between historical order invoices and subsequent catalog pricing modifications. |
| **5** | **Responsive Deployment** | Seamless layout rendering across mobile, tablet, and desktop viewports on schedule within 12 weeks. |

---

## 1.5 Methodology

This project follows the **Agile (Scrum)** framework with 2-week sprints. The rationale behind this decision includes:

* **Dynamic Catalog Needs:** Requirements around product attributes and custom furniture finishes evolve as store managers interact with early prototypes.
* **Incremental Validation:** Essential e-commerce pathways (like Cart updates and Checkout validations) need to be validated piece-by-piece by stakeholders.
* **Risk Mitigation:** Iterative sprint reviews significantly reduce the risk of deploying broken transaction sequences or incorrect inventory logic.

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
