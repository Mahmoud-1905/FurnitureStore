# 6. User Roles

The system enforces strict Role-Based Access Control (RBAC) to ensure security and logical separation of duties.

## 6.1 Guest (Unauthenticated User)
Guests are visitors to the site who have not logged in.
**Capabilities:**
- Browse the public furniture catalog.
- View detailed product pages (images, descriptions, dimensions).
- Search and filter products by category, material, and price range.
- Access the registration page to create a new Customer account.

## 6.2 Customer (Authenticated User)
Customers are users who have successfully registered and logged in.
**Capabilities:**
- Inherit all capabilities of the Guest role.
- Add items to their persistent shopping cart.
- Proceed through the multi-step checkout pipeline to place an order.
- Track current order statuses and view historical orders.
- Manage a personal address book for shipping.
- Book, view, and cancel physical showroom appointments (cancellations must be >24h prior).
- Submit product reviews **only** for items they have purchased and received (verified purchase).

## 6.3 Store Manager (Internal Operations)
The Store Manager operates the business logic of the single-vendor store.
**Capabilities:**
- Access the secure Manager Dashboard.
- **Catalog Management:** Add new products, update existing details (price, dimensions), upload images, and manage inventory quantities. Soft-delete discontinued items.
- **Order Management:** View all incoming orders, update order statuses (e.g., Pending -> Processing -> Shipped -> Delivered).
- **Appointment Management:** View the showroom schedule, confirm pending customer appointment requests, or reject them.
- **Analytics:** View basic sales and inventory alerts.

## 6.4 System Administrator (IT / Oversight)
The Administrator manages the technical and security aspects of the platform.
**Capabilities:**
- Access the secure Admin Control Panel.
- **User Management:** View all registered accounts. Activate, deactivate, or ban users who violate terms.
- **Role Management:** Assign or revoke the "Store Manager" role to specific accounts.
- **Moderation:** Review flagged product reviews and delete inappropriate content.
- **System Auditing:** Access read-only audit logs that track all administrative and managerial actions for security compliance.
