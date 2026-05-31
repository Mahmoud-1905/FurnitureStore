# 06_USE_CASES.md

## Overview

This document consolidates the primary use cases for the Furniture Store system. It is derived from the existing feature specifications (`06_CUSTOMER_FEATURES.md`, `07_MANAGER_FEATURES.md`, `08_ADMIN_FEATURES.md`, `09_SHOWROOM_INTEGRATION.md`). The use cases are grouped by actor type.

---

### 1. Guest Use Cases
- **Browse Catalog** – View paginated product listings and apply filters/search.
- **View Product Detail** – See full product information, images, and reviews.
- **Register Account** – Create a new user profile.

---

### 2. Customer Use Cases
- **Add to Cart / Update Cart** – Add items, modify quantity, remove items.
- **Checkout** – Multi‑step process (review, shipping, order summary, place order) with atomic stock deduction.
- **Order History & Tracking** – View past orders and follow shipment status.
- **Book Showroom Appointment** – Select a date/time, provide product interest, and submit request.
- **Manage Appointments** – View, cancel (if >24 h before), or reschedule appointments.
- **Submit Verified Review** – Post a review only for delivered purchases.
- **Wishlist Management** – Save products for later and move them to the cart.

---

### 3. Store Manager Use Cases
- **Dashboard Overview** – Real‑time view of sales, inventory, and appointments.
- **Product Management** – Create, edit, soft‑delete, and adjust stock levels.
- **Order Fulfillment** – Update order status, handle refunds, and generate invoices.
- **Appointment Management** – Confirm, reject, or modify customer bookings.
- **Analytics & Reporting** – Export sales reports, inventory turnover, and appointment metrics.

---

### 4. Administrator Use Cases
- **User Administration** – Activate/deactivate accounts, assign/revoke roles.
- **System Configuration** – Adjust global settings, manage feature toggles.
- **Review Moderation** – Approve or delete user‑submitted reviews.
- **Audit Log Inspection** – View immutable logs of all privileged actions.

---

### 5. Non‑Functional Use Cases
- **Performance Monitoring** – Ensure API response < 500 ms, page load < 2 s.
- **Security Audits** – Validate JWT usage, role enforcement, and data encryption.
- **Backup & Recovery** – Automated daily backups with restore procedures.

Feel free to expand each bullet into a detailed interaction flow if needed.
