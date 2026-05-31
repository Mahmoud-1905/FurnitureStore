# 06 — Customer Features
## Ruqi Store — Complete Customer Use Cases

---

## AI Instructions
> Customer features are accessible on the public-facing store. Some actions (browsing, searching) are available to Guests. Other actions (cart, checkout, orders, reviews, appointments) require the user to be authenticated with the `Customer` role.

---

## SECTION A — Catalog Browsing & Search (Guests & Customers)

### A1: Browse Catalog (`/products`) (FR-C03, FR-C04, FR-C05)

```text
DISPLAY: Paginated product grid (20 items per page, sorted by newest first).
  Only products where `IsActive = 1` and `Category.IsActive = 1` are shown.

FILTER PANEL:
  - Category (Checkboxes or dropdown)
  - Price Range (Min / Max inputs)
  - Material Type (Dropdown)
  - Availability (Toggle: "In Stock Only")

SEARCH BAR:
  - Keyword search against `Products.Name` and `Products.Description`.
  - Empty results should display a friendly "No products found" message.
```

### A2: View Product Detail (`/products/{id}`) (FR-C06)

```text
DISPLAY: Comprehensive product page.
  - Multi-image gallery (up to 8 images, zoom functionality). Primary image shown first.
  - Product Name, SKU, Category.
  - Price (Formatted in ILS/USD).
  - Stock Status (e.g., "In Stock", "Only X left", or "Out of Stock").
  - Dimensions (W×D×H cm) and Weight (kg).
  - Material details.
  - Average Rating (1-5 stars) and all approved verified reviews.
  
ACTIONS:
  - Quantity selector (Max limited by `StockQuantity`).
  - [Add to Cart] button.
```

---

## SECTION B — Shopping Cart & Checkout (FR-C07, C08, C09)

### B1: View & Update Cart (`/cart`)

```text
PRECONDITION: User must be authenticated to persist cart, but guest carts are stored in session cookie until login merge.

DISPLAY:
  - List of CartItems with product image, name, unit price.
  - Quantity input (with +/- buttons).
  - Subtotal per item.
  - Cart Total (Items + Tax + Shipping).

ACTIONS:
  - Update Quantity: Validates against `Products.StockQuantity`. Shows error if requested quantity exceeds stock.
  - Remove Item: Deletes the CartItem.
```

### B2: Multi-Step Checkout (`/checkout`)

```text
PRECONDITION: Cart must not be empty. User must be authenticated.

STEP 1: Cart Review
  - Final check of items and quantities.

STEP 2: Delivery & Shipping
  - Select existing saved address OR enter a new delivery address.
  - (Optional) Enter delivery notes.

STEP 3: Order Summary
  - Display final TotalAmount = sum(Subtotals) + Tax + Shipping.

STEP 4: Place Order
  - ATOMIC DATABASE TRANSACTION REQUIRED:
    1. Verify stock is still sufficient for all items.
    2. INSERT into Orders.
    3. INSERT into OrderItems (snapshotting UnitPrice).
    4. UPDATE Products (decrement StockQuantity).
    5. DELETE all CartItems for this user.
  - If stock insufficient during step 1: Rollback, show error indicating which items are out of stock.

ON SUCCESS:
  Redirect to Order Confirmation page (`/orders/confirmation/{id}`).
```

---

## SECTION C — Order History & Tracking (FR-C10, C11)

### C1: Order History (`/account/orders`)

```text
DISPLAY: List of all past orders for the authenticated customer.
  - Order ID, Date Placed, Total Amount.
  - Current Status (Pending, Processing, Shipped, Delivered, Cancelled).
  - Action: [View Details].
```

### C2: Order Tracking & Details (`/account/orders/{id}`)

```text
DISPLAY:
  - Pipeline visualization of current status:
    [Pending] -> [Processing] -> [Shipped] -> [Delivered]
  - List of purchased OrderItems with historical prices.
  - Shipping address and totals.
```

---

## SECTION D — Showroom Appointments (FR-C13, C14)

### D1: Book Appointment (`/appointments/book`)

```text
PURPOSE: Allow customer to physically visit the showroom before buying.

FORM FIELDS:
  - Date (Datepicker, must be ≥ 24 hours in future).
  - Time Slot (Select from available showroom hours, filtered by capacity).
  - Product Interests (Textarea, e.g., "I want to see the Oslo Sofa").

ON SUCCESS:
  INSERT into Appointments (UserId, Date, Status='Pending', Interests).
  Toast: "تم تسجيل طلب الحجز بنجاح" (Booking request submitted successfully).
```

### D2: Manage Appointments (`/account/appointments`)

```text
DISPLAY: List of user's appointments (Pending, Confirmed, Cancelled).

ACTION (Cancel):
  - Customer can click [Cancel] ONLY if the appointment is > 24 hours away.
  - Updates Status to 'Cancelled'.
```

---

## SECTION E — Reviews & Wishlist (FR-C12, C15)

### E1: Submit Review

```text
PRECONDITION:
  - Customer must have a `Delivered` order containing the specific product.
  - Customer can only submit ONE review per product.

FORM FIELDS (Modal on Product Detail page):
  - Rating (1 to 5 stars, required).
  - Title (Text, optional).
  - Body (Textarea, optional).

ON SUCCESS:
  INSERT into Reviews. Set `IsVerifiedPurchase = 1`.
  (Reviews may require Admin approval based on `IsVisible` flag).
```

### E2: Manage Wishlist

```text
PURPOSE: Save products for later.

ACTIONS:
  - Toggle heart icon on Product Card / Detail page to add/remove from Wishlist.
  - View Wishlist (`/account/wishlist`).
  - Move item from Wishlist to Cart.
```
