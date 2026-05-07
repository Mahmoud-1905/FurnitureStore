# 03 — User Roles & Permissions
> **Destination:** Antigravity  
> **Purpose:** Defines what each role CAN and CANNOT do. Drives role-based authorization.  
> **Rule:** The "Cannot Do" list is as important as "Can Do" — missing it creates security holes.

---

## Role 1 — Customer

### Can Do
- Register a new account with email + password
- Login and logout
- Browse the full furniture product catalog
- Search and filter products
- View product detail pages including reviews
- Add products to cart and update quantities
- Complete checkout and place orders
- View own order history and track order status
- Cancel own orders (Pending status only)
- Submit one review per product (verified purchase only)
- Book showroom appointments
- Cancel own appointments (≥ 24 hours before slot)
- Manage own wishlist
- Manage own address book (up to 5 addresses)
- Update own profile (name, email, password)

### Cannot Do
- ❌ Access any other customer's cart, orders, or profile
- ❌ Access the Store Manager dashboard
- ❌ Access the Admin panel
- ❌ Create, edit, or delete products
- ❌ Update order status
- ❌ Confirm or reject appointments
- ❌ View other customers' reviews before submitting their own
- ❌ Submit a review without a Delivered order for that product
- ❌ Submit more than one review per product

---

## Role 2 — Store Manager

### Can Do
- Login and access the Store Manager dashboard
- Create new furniture product listings (name, category, description, price, dimensions, weight, material, SKU, stock, up to 8 images)
- Edit any product attribute
- Soft-delete products (IsActive=false)
- Update product StockQuantity
- Manage furniture categories (create, rename, reorder, deactivate)
- View all orders and filter by status
- Update order status: Pending → Processing → Shipped → Delivered (or Cancelled)
- View sales analytics dashboard (revenue by period, top products, inventory value)
- View all showroom appointment requests
- Confirm or reject appointments with optional notes
- View confirmed appointment calendar

### Cannot Do
- ❌ Access the Admin panel
- ❌ Manage user accounts (activate/deactivate/delete)
- ❌ Assign or revoke roles
- ❌ Delete customer reviews (only Admin can)
- ❌ View the audit log
- ❌ Hard-delete any product, order, or user
- ❌ Access another manager's data (single-store model: only one Store Manager)

---

## Role 3 — Administrator

### Can Do
- Login and access the full Admin panel
- View all registered users (search by name/email, filter by role and status)
- Activate or deactivate any user account (soft deactivation)
- Assign the StoreManager role to any Customer account
- Revoke the StoreManager role
- View all orders platform-wide
- Delete any product review that violates content policy
- Generate and export CSV reports (revenue, orders, products, inventory)
- View the append-only audit log of all admin actions

### Cannot Do
- ❌ Permanently delete user accounts or order records
- ❌ Edit product listings directly (that is the Store Manager's responsibility)
- ❌ Place orders or book appointments on behalf of customers
- ❌ Delete audit log entries (append-only by design)

--- 

## Role 4 — Payment Officer 

### Can Do
- Login and access the Payment Officer dashboard
- View all orders filtered by PaymentStatus (Unpaid / Paid / Rejected)
- View full order details including: items, totals, customer name, and contact info
- Mark an order payment as Paid
- Mark an order payment as Rejected with a mandatory reason note
- View the full payment history log (all status changes with timestamps)

### Cannot Do
- ❌ Edit, create, or delete any product
- ❌ Update order fulfillment status (Pending → Processing → Shipped — that belongs to Store Manager)
- ❌ Access the Admin panel
- ❌ Manage or view user accounts
- ❌ Assign or revoke roles
- ❌ Delete or moderate reviews
- ❌ Access the audit log
- ❌ View other financial data outside of order payment statuses

---


## Role Matrix

| Action | Customer | Store Manager | Payment Officer | Admin |
|---|:---:|:---:|:---:|:---:|
| Browse product catalog | ✅ | ✅ | ✅ | ✅ |
| Place orders | ✅ | ❌ | ❌ | ❌ |
| Submit reviews | ✅ | ❌ | ❌ | ❌ |
| Manage own profile | ✅ | ✅ | ✅ | ✅ |
| Manage products (CRUD) | ❌ | ✅ | ❌ | ❌ |
| Update order fulfillment status | ❌ | ✅ | ❌ | ❌ |
| View all orders | ❌ | ✅ | ✅ | ✅ |
| Mark payment Paid / Rejected | ❌ | ❌ | ✅ | ❌ |
| View payment history log | ❌ | ❌ | ✅ | ✅ |
| Manage inventory | ❌ | ✅ | ❌ | ❌ |
| Manage users | ❌ | ❌ | ❌ | ✅ |
| Assign roles | ❌ | ❌ | ❌ | ✅ |
| Delete reviews | ❌ | ❌ | ❌ | ✅ |
| Export reports | ❌ | ❌ | ❌ | ✅ |
| View audit log | ❌ | ❌ | ❌ | ✅ |

---
  

## ASP.NET Core Authorization Mapping

| Controller | Action | HTTP | Role Required | Notes |
|---|---|---|---|---|
| HomeController | Index | GET | Anonymous | Public landing page |
| ProductsController | Index | GET | Anonymous | Public catalog |
| ProductsController | Detail | GET | Anonymous | Public product page |
| ProductsController | SubmitReview | POST | Customer | Verified purchase check in service |
| CartController | Index | GET | Customer | |
| CartController | AddToCart | POST | Customer | |
| CartController | UpdateCart | POST | Customer | |
| CartController | RemoveFromCart | POST | Customer | |
| OrdersController | Checkout | GET + POST | Customer | |
| OrdersController | PlaceOrder | POST | Customer | Atomic transaction |
| OrdersController | Confirmation | GET | Customer | Data isolation enforced |
| OrdersController | History | GET | Customer | Own orders only |
| OrdersController | Detail | GET | Customer | Data isolation enforced |
| OrdersController | Cancel | POST | Customer | Pending status only |
| AccountController | Register | GET + POST | Anonymous | |
| AccountController | Login | GET + POST | Anonymous | |
| AccountController | Logout | POST | Authenticated | Any logged-in role |
| AccountController | Profile | GET + POST | Authenticated | Any logged-in role |
| AccountController | AccessDenied | GET | Anonymous | Shown on 403 |
| PaymentController | Dashboard | GET | PaymentOfficer | Orders filtered by PaymentStatus |
| PaymentController | OrderDetail | GET | PaymentOfficer | Read-only view |
| PaymentController | MarkPaid | POST | PaymentOfficer | Updates PaymentStatus = Paid |
| PaymentController | MarkRejected | POST | PaymentOfficer | Requires rejection reason note |
| PaymentController | History | GET | PaymentOfficer | Full payment log |
| StoreManagerController | Dashboard | GET | StoreManager | |
| StoreManagerController | Products | GET | StoreManager | |
| StoreManagerController | CreateProduct | GET + POST | StoreManager | File upload included |
| StoreManagerController | EditProduct | GET + POST | StoreManager | |
| StoreManagerController | SoftDeleteProduct | POST | StoreManager | Sets IsActive = false |
| StoreManagerController | Orders | GET | StoreManager | View + filter by status |
| StoreManagerController | UpdateOrderStatus | POST | StoreManager | Fulfillment only — not payment |
| StoreManagerController | Inventory | GET + POST | StoreManager | |
| AdminController | Index | GET | Admin | Dashboard |
| AdminController | Users | GET | Admin | |
| AdminController | DeactivateUser | POST | Admin | AuditLog written |
| AdminController | ActivateUser | POST | Admin | AuditLog written |
| AdminController | AssignRole | POST | Admin | AuditLog written |
| AdminController | RevokeRole | POST | Admin | AuditLog written |
| AdminController | Reviews | GET | Admin | |
| AdminController | DeleteReview | POST | Admin | AuditLog written |
| AdminController | AuditLog | GET | Admin | Read-only |
| AdminController | Reports | GET | Admin | CSV export |

---

## Antigravity Prompt
```
Read 03_USER_ROLES_AND_PERMISSIONS.md.
Plan the ASP.NET Core authorization structure:
- Which controllers need [Authorize(Roles="...")]?
- Which actions are [AllowAnonymous]?
- List every role claim that must be seeded in the database.
Do not write code yet.
```
