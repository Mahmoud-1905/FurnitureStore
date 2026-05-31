# 05 — Store Manager Features
## Ruqi Smart Store — Complete Store Manager Use Cases

---

## Technical & AI Implementation Instructions
> **Justification for Implementation:** The Store Manager is the operational core of the single-vendor architecture. To ensure system security and data integrity, all manager pages must be rigorously restricted.
> 
> All manager pages live under `/manager/`. Every backend API route must validate the JWT token and assert the `StoreManager` role claim. On the MVC frontend, the `[Authorize(Roles = "StoreManager")]` attribute must be applied to all controller actions. Since this is a single-vendor system, the manager has full access to the entire product catalog and order history; there is no need to filter queries by a `vendor_id`.

---

## Store Manager Dashboard (`/manager/dashboard`)

> **Justification for Implementation:** A centralized dashboard provides immediate visibility into store health and operational bottlenecks, allowing the manager to prioritize daily tasks such as low-stock replenishment, order processing, and appointment confirmations.

Summary cards shown on the dashboard:
- **Total Revenue:** (Daily / Weekly / Monthly figures)
- **Active Products:** Count of products currently visible to customers
- **Low Stock Alerts:** Count of products below the configurable stock threshold
- **Pending Orders:** Count of orders requiring immediate processing
- **Pending Appointments:** Count of showroom visits awaiting confirmation

Navigation sidebar:
```text
Dashboard (لوحة التحكم)
├── Product Catalog (إدارة المنتجات)
│   ├── All Products (جميع المنتجات)
│   └── Categories (الأقسام)
├── Sales & Orders (المبيعات والطلبات)
├── Appointments (حجوزات المعرض)
└── Store Reports (التقارير)
```

---

## SECTION A — Product Catalog Management

> **Justification for Implementation:** High-ticket furniture requires comprehensive product details (precise dimensions, materials, and multiple high-resolution images) to build buyer trust and reduce abandonment. The management interface must support these extensive data requirements while enforcing strict validation to prevent incomplete listings from reaching the customer frontend.

### A1: View Product Catalog (`/manager/products`)

```text
DISPLAY: Data Table with sorting and pagination
  Columns:
    - Image (Thumbnail)
    - SKU
    - Product Name
    - Category
    - Price
    - Stock Quantity (Highlight in red if below threshold)
    - Status (Active / Hidden)
    - Actions: [Edit] [Toggle Status] [Delete]

DATA SOURCE:
  SELECT ProductId, SKU, Name, CategoryId, Price, StockQuantity, IsActive
  FROM Products
  ORDER BY CreatedAt DESC

PAGINATION: 20 rows per page
FILTERS: Search by Name/SKU, Filter by Category, Filter by Status
```

### A2: Add New Product (`/manager/products/add`)

```text
FORM FIELDS:
  SKU             text      required  unique
  Name            text      required  min:3 max:250
  CategoryId      select    required  (populated from active Categories)
  Price           number    required  min:0 (decimal)
  Dimensions      text      required  format: W×D×H cm
  Weight          number    optional  in kg
  Material        text      required
  Description     textarea  required  min:20 max:2000 chars
  StockQuantity   number    required  min:0
  Images          file[]    required  min:1 max:8 (accept: .jpg,.png,.webp, max: 5MB each)

SERVER-SIDE VALIDATION:
  1. SKU unique?                 → "SKU must be unique"
  2. Required fields filled?     → "This field is required"
  3. Price & Stock >= 0?         → "Value cannot be negative"
  4. Images valid?               → "Unsupported format or file too large"

IMAGE HANDLING:
  - Generate GUID filenames
  - Upload to external storage (e.g., Azure Blob or CDN directory)
  - Store relative paths in ProductImages table

ON SUCCESS:
  BEGIN TRANSACTION
    INSERT INTO Products (SKU, Name, CategoryId, Price, ...)
    INSERT INTO ProductImages (ProductId, ImageUrl)
  COMMIT TRANSACTION
  
  Toast: "Product added successfully."
  REDIRECT to /manager/products
```

### A3: Edit Product (`/manager/products/edit/{id}`)

```text
FORM FIELDS: Pre-filled with current values. 

GUARD: Editing a product's price must NEVER alter the `UnitPrice` of historical `OrderItems`. Order totals must remain immutable.

VALIDATION: Same as Add Product (A2).

ON SUCCESS:
  UPDATE Products
  SET Name=@Name, CategoryId=@CatId, Price=@Price, Dimensions=@Dim, 
      Material=@Mat, Description=@Desc, StockQuantity=@Stock, UpdatedAt=GETUTCDATE()
  WHERE ProductId=@Id
  
  Toast: "Product updated successfully."
  REDIRECT to /manager/products
```

### A4: Manage Categories (`/manager/categories`)

```text
DISPLAY: List of categories with Drag-and-Drop for UI reordering.

ACTIONS:
  - Add Category (Name, Description)
  - Edit Category
  - Deactivate Category (Soft delete)

GUARD: Deactivating a category automatically hides all its associated products from the customer frontend, preventing customers from browsing orphaned items.
```

---

## SECTION B — Order Management (`/manager/orders`)

> **Justification for Implementation:** Furniture delivery involves complex, offline logistics. The system must track orders through a clear, linear pipeline (Pending → Processing → Shipped → Delivered) to ensure customers are kept informed, reducing support call volume and minimizing fulfillment errors.

### B1: View All Orders

```text
DISPLAY: Tabs or Dropdown for filtering by Pipeline Status:
  - Pending (New orders awaiting verification)
  - Processing (Being prepared/assembled)
  - Shipped (In transit)
  - Delivered (Completed)
  - Cancelled

TABLE COLUMNS:
  - Order ID
  - Date
  - Customer Name
  - Total Amount
  - Status
  - Actions: [View Details] [Update Status]

DATA SOURCE:
  SELECT OrderId, CreatedAt, CustomerId, TotalAmount, Status
  FROM Orders
  ORDER BY CreatedAt DESC
```

### B2: Update Order Status (`/manager/orders/update/{id}`)

```text
TRIGGER: Click [Update Status] from the order list or detail page.

STEP 1: Modal dropdown exposing only logically allowed next statuses (e.g., a 'Shipped' order cannot revert to 'Pending').

STEP 2 (On Confirm):
  BEGIN TRANSACTION
    UPDATE Orders SET Status=@NewStatus, UpdatedAt=GETUTCDATE() WHERE OrderId=@Id
    
    -- Automatic Inventory Reversion for Cancellations
    IF @NewStatus == 'Cancelled':
      UPDATE Products 
      SET StockQuantity = StockQuantity + OrderItem.Quantity
      WHERE ProductId = OrderItem.ProductId
  COMMIT TRANSACTION
  
  SEND NOTIFICATION (Email):
    Subject: "Update on your Ruqi Store Order #@Id"
    Body: "Your order status has been updated to @NewStatus."

  Toast: "Order status successfully updated."
```

---

## SECTION C — Showroom Appointment Management (`/manager/appointments`)

> **Justification for Implementation:** The hybrid omnichannel model relies on physical showroom visits to close high-ticket sales. Coordinating these visits digitally prevents showroom overcrowding, allows staff to prepare requested products in advance, and directly bridges the gap between online browsing and physical purchasing.

### C1: View Appointments Calendar

```text
DISPLAY: Interactive Calendar View (Month/Week/Day) togglable with a List View.
  - Pending requests highlighted in Yellow
  - Confirmed appointments highlighted in Green
  - Cancelled requests highlighted in Gray

DETAILS SHOWN ON CLICK:
  - Customer Name & Contact Number
  - Date & Time Slot
  - Products of Interest (Pre-qualification notes)
```

### C2: Confirm or Reject Appointment

```text
ACTIONS on Pending Appointment:
  [Confirm ✅] | [Reject ❌]

ON CONFIRM:
  UPDATE Appointments SET Status='Confirmed', UpdatedAt=GETUTCDATE() WHERE AppointmentId=@Id
  SEND EMAIL: "Your showroom visit at Ruqi Store on [Date] at [Time] is confirmed."
  Toast: "Appointment Confirmed."

ON REJECT (Prompt for reason):
  UPDATE Appointments SET Status='Rejected', UpdatedAt=GETUTCDATE() WHERE AppointmentId=@Id
  SEND EMAIL: "We could not confirm your visit. Reason: [Reason]."
  Toast: "Appointment Rejected."
```

---

## SECTION D — Reports and Analytics (`/manager/reports`)

> **Justification for Implementation:** Data-driven decision making is critical for optimizing a single-vendor retail business. The manager needs immediate, actionable insights into inventory value, product popularity, and revenue trends to guide procurement and promotional strategies.

### D1: Sales & Inventory Dashboard

```text
METRICS & VISUALIZATIONS:
  1. Revenue Trend (Line Chart: Daily/Monthly aggregates)
  2. Top 10 Selling Products (Bar Chart: Ranked by Units Sold)
  3. Sales Distribution by Category (Pie Chart)
  4. Total Inventory Value (Calculated: Current Stock * Unit Price)

FILTERS:
  - Date Range (Start Date -> End Date)
  - Category Filter

DATA SOURCE (Example for Revenue Trend):
  SELECT SUM(TotalAmount) AS Revenue, CAST(CreatedAt AS DATE) AS SalesDate
  FROM Orders
  WHERE Status = 'Delivered'
  GROUP BY CAST(CreatedAt AS DATE)
  ORDER BY SalesDate ASC
```
