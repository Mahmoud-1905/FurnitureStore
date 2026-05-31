# 04 — Admin Features
## Ruqi Smart Store — Complete Administrator Use Cases

---

## AI Instructions
> All admin pages live under `/admin/`. Every administrative page or API endpoint MUST check `Session["UserRole"] == "Administrator"` (or validate the JWT `role` claim) prior to execution. If unauthorized, immediately return a 401/403 status or redirect to `/account/login`. Every administrative action (role modifications, account deactivations, data deletions) MUST securely write to the `AuditLog` table to maintain a non-repudiable history of system changes.

---

## Admin Control Panel Sidebar Navigation

```text
Dashboard (لوحة التحكم)
├── Users          (المستخدمين)
├── Orders         (مراقبة الطلبات)
├── Reviews        (مراقبة التقييمات)
├── Reports        (التقارير والإحصائيات)
└── Audit Logs     (سجل النظام)
```

Dashboard cards show: total registered users, total completed orders, aggregated revenue for the current month, and a summary badge of recent critical audit events.

---

## SECTION A — Manage Users & Roles

*Justification for Implementation: Centralized user management is the cornerstone of system security. It grants administrators the ability to enforce strict access control, handle user disputes by deactivating accounts, and safely provision Store Manager roles for operational staff without requiring direct database access.*

### A1: View All Users

```sql
TABLE COLUMNS: #, Name, Email, Role, Status, Reg. Date, Actions [Toggle Status][Change Role]
SOURCE: 
  SELECT user_id, full_name, email, role, is_active, created_at 
  FROM Users 
  ORDER BY created_at DESC
PAGINATION: 20 rows per page
```

### A2: Activate / Deactivate User

```text
GUARD: Block if target user_id == Session["UserID"]. Show: "لا يمكنك تعطيل حسابك الخاص" (Cannot deactivate your own account).

Confirmation modal: "هل أنت متأكد من تغيير حالة هذا الحساب؟" (Are you sure you want to change this account's status?)

ON CONFIRM:
  UPDATE Users SET is_active = CASE WHEN is_active=1 THEN 0 ELSE 1 END WHERE user_id=@id
  INSERT INTO AuditLog (action_type='STATUS_CHANGE', target_table='Users', target_id=@id)
  Toast: "تم تحديث حالة الحساب بنجاح" (Account status successfully updated)
```

### A3: Assign / Revoke Store Manager Role

```text
GUARD: Block if target user_id == Session["UserID"]. Show: "لا يمكنك تغيير صلاحياتك الخاصة" (Cannot modify your own permissions).

Confirmation modal: "هل تريد تعيين/إزالة صلاحية مدير المتجر لهذا المستخدم؟" (Are you sure you want to assign/revoke the Store Manager role for this user?)

ON CONFIRM:
  UPDATE Users SET role=@newRole WHERE user_id=@id
  INSERT INTO AuditLog (action_type='ROLE_CHANGE', target_table='Users', target_id=@id)
  Toast: "تم تحديث صلاحيات المستخدم" (User permissions updated)
  NOTE: The affected user's session reflects new permissions on their next login or token refresh.
```

### A4: Search and Sort Users

```sql
Search: full_name LIKE '%@k%' OR email LIKE '%@k%'
Sortable columns: full_name, email, role, created_at
Toggle ASC/DESC on each click. Show an arrow indicator on the active column.
```

---

## SECTION B — Order Oversight (Read-Only)

*Justification for Implementation: While the Store Manager is exclusively responsible for processing orders and managing inventory, Administrators require read-only access to all platform orders. This separation of concerns is vital for overseeing business operations, resolving high-level customer disputes, and monitoring the platform for fraudulent activity without interfering with daily fulfillment.*

### B1: View All Orders

```sql
COLUMNS: Order #, Customer Name, Date, Total Amount, Status, Actions [View Details]
SOURCE:
  SELECT o.order_id, u.full_name, o.created_at, o.total_amount, o.status
  FROM Orders o
  JOIN Users u ON o.customer_id = u.user_id
  ORDER BY o.created_at DESC
PAGINATION: 20 rows per page
```

### B2: View Order Details

```text
Display complete order timeline, shipping address, and individual line items.
No modification actions are permitted for the Administrator role in this view.
```

### B3: Search and Sort Orders

```sql
Search: order_id = @k OR u.full_name LIKE '%@k%'
Sort: created_at, total_amount, status
```

---

## SECTION C — Moderate Product Reviews

*Justification for Implementation: To maintain a professional e-commerce environment and preserve brand reputation, administrators must be equipped to remove inappropriate, abusive, or spam reviews. Permitting only deletion—and actively prohibiting review modification—protects the authenticity and trustworthiness of user-generated content.*

### C1: View Product Reviews

```sql
COLUMNS: #, Product Name, Customer, Rating, Comment, Date, Actions [Delete]
SOURCE:
  SELECT r.review_id, p.product_name, u.full_name, r.rating, r.comment, r.created_at
  FROM Reviews r
  JOIN Products p ON r.product_id = p.product_id
  JOIN Users u ON r.customer_id = u.user_id
  ORDER BY r.created_at DESC
PAGINATION: 15 rows per page
```

### C2: Delete Review

```text
Confirmation modal: "هل تريد حذف هذا التقييم نهائياً؟" (Are you sure you want to permanently delete this review?)

ON CONFIRM:
  DELETE FROM Reviews WHERE review_id=@id
  INSERT INTO AuditLog (action_type='DELETE', target_table='Reviews', target_id=@id)
  Toast: "تم حذف التقييم" (Review deleted)
  TRIGGER: System asynchronously recalculates the average rating for the affected product.
```

### C3: Search and Sort Reviews

```sql
Search: p.product_name LIKE '%@k%' OR u.full_name LIKE '%@k%' OR r.comment LIKE '%@k%'
Sort: created_at, rating
```

---

## SECTION D — Reports & Analytics

*Justification for Implementation: Data-driven decision-making relies on accurate, aggregated operational metrics. The reporting module empowers administrators to extract vital financial and operational insights directly from the UI, eliminating the need for manual, error-prone database queries and enabling seamless reporting to external stakeholders.*

### D1: Generate Operational Reports

```text
TYPES OF REPORTS:
  1. Total Revenue by Period (إيرادات المبيعات خلال فترة)
  2. Orders by Status        (الطلبات مصنفة حسب الحالة)
  3. Top Selling Products    (المنتجات الأكثر مبيعاً)
  4. Top Customers           (أفضل العملاء)
  5. Current Inventory Value (القيمة الإجمالية للمخزون الحالي)

FILTERS: Start Date, End Date, Category (for Product reports)
```

### D2: Export Reports

```text
ACTION: [Export to CSV] / [Export to Excel]

ON SUCCESS:
  System generates a downloadable payload containing the structured dataset based on selected filters.
  INSERT INTO AuditLog (action_type='EXPORT_REPORT', target_table='System', details='ReportType: @type')
```

---

## SECTION E — System Audit Logs

*Justification for Implementation: A non-repudiable, immutable audit trail is an essential enterprise security standard. It allows administrators to track exactly who performed critical system actions (e.g., assigning roles, deleting reviews) and when they occurred, ensuring operational accountability and enabling accurate forensic analysis in the event of an incident.*

### E1: View Audit Logs

```sql
COLUMNS: Log ID, Admin Name, Action Type, Target Table, Date, Details
SOURCE:
  SELECT a.log_id, u.full_name, a.action_type, a.target_table, a.created_at, a.details
  FROM AuditLog a
  JOIN Users u ON a.admin_id = u.user_id
  ORDER BY a.created_at DESC
PAGINATION: 50 rows per page (Strictly Read-Only)
```

### E2: Search and Filter Audit Logs

```sql
Search: u.full_name LIKE '%@k%' OR a.action_type LIKE '%@k%'
Filter by: Date Range (Start Date / End Date), Target Table Dropdown
```
