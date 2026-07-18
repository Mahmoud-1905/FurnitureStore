# 12. Requirements Traceability Matrix (RTM)

## 📌 Overview
This document serves as the **Requirements Traceability Matrix (RTM)** for the **Ruqi Store** application. It maps each defined system requirement (Functional and Non-Functional) to its corresponding domain entities, database tables, service operations, and UI pages, ensuring 100% development coverage and clear auditability.

---

## 🗺️ Functional Requirements Mapping (FR)

This matrix maps user-facing features to their database and service-level implementations:

| Req ID | Requirement Description | Domain Entities | Database Tables | Service Method / API Contract | Target UI Page |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **FR-01** | User Authentication & Roles | `ApplicationUser` | `ApplicationUsers` | ASP.NET Identity API | Login / Register Pages |
| **FR-02** | Product Catalog Browsing | `Product`, `Category` | `Products`, `Categories` | `IProductService.GetActiveProducts()` | Products Page (`المنتجات.mp4`) |
| **FR-03** | Manage Shopping Cart | `Cart`, `CartItem` | `Carts`, `CartItems` | `ICartService.AddToCartAsync()` <br> `ICartService.UpdateCartItemQuantityAsync()` | Curated Collection / Cart (`collection.mp4`) |
| **FR-04** | Save to Wishlist | `Wishlist` | `Wishlists` | `IWishlistService.AddToWishlistAsync()` | Wishlist Page (RTL View Example) |
| **FR-05** | Checkout & Order Placement | `Order`, `OrderItem` | `Orders`, `OrderItems` | `IOrderService.PlaceOrderAsync()` | Checkout Modal & Summary Sidebar |
| **FR-06** | Showroom Appointment | `ShowroomAppointment` | `ShowroomAppointments` | `IWishlistService.MoveToCartAsync()` <br> `POST /api/showroom/appointments` | Showroom Appointment Page (`صالة العرض`) |
| **FR-07** | System Audit Logging | None | `AuditLogs` | Internal Logging Middleware | Admin Dashboard Log Viewer |
| **FR-08** | Contact Studio & Inquiry | None | None | `IContactService.SubmitInquiryAsync()` | Connect Us Page (`تواصل معنا.mp4`) |
---

## ⚙️ Non-Functional Requirements Mapping (NFR)

This matrix maps system constraints, performance, and accessibility targets to their architectural solutions:

| Req ID | NFR Category | Technical Implementation & Enforcement | Architectural File Reference |
| :--- | :--- | :--- | :--- |
| **NFR-U1** | **Responsive Design** | UI grids adapt smoothly from single-column (mobile) to multi-column layouts using CSS Flexbox/Grid and Bootstrap 5. | `11-ui-ux-design.md` |
| **NFR-U2** | **Accessibility (WCAG)** | Enforces a contrast ratio > 4.5:1, semantic HTML tags, keyboard navigability (Tab-focus), and descriptive ARIA labels. | `11-ui-ux-design.md` |
| **NFR-U3** | **Localization (RTL/LTR)** | Implements a physical-to-logical CSS compiler using `bootstrap.rtl.min.css` and directory-aware language cookies (`.AspNetCore.Culture`). | `11-ui-ux-design.md` |
| **NFR-U4** | **Double-Submit Prevention** | Disables transaction buttons immediately upon click and initiates an asynchronous spinner state to prevent concurrent requests. | `11-ui-ux-design.md`, `07-uml-behavioral.md` |
| **NFR-S1** | **Concurrency Control** | Enforces atomic database transactions (`IDbContextTransaction`) during stock deduction to lock rows and prevent overselling. | `10-detailed-design.md`, `07-uml-behavioral.md` |
| **NFR-D1** | **Price Consistency** | Implements a **Price Freeze** constraint that saves the current price to `OrderItem.PriceSnapshot` instead of linking back dynamically. | `08-database-design.md`, `10-detailed-design.md` |
---

## 📑 Traceability Rules & Verification Checklist

* **Verification of Code:** No controller endpoint or frontend view should be built unless it maps back to at least one **Req ID** in this matrix.
* **Database Integrity:** Foreign keys mapped in `04_DATABASE_DESIGN.md` are actively checked against the Domain Relationships defined in `06-domain-model.md` to prevent dangling references during operations.
* **UX Continuity:** Design feedback mechanisms (like toast alerts and loading spinners) are aligned with the asynchronous operations documented in `07-uml-behavioral.md` to maintain system transparency.

---
[← Previous: UI/UX Design](./11-ui-ux-design.md) | [Back to Index](./00-index.md) | [Next: Appendices →](./13-appendices.md)
