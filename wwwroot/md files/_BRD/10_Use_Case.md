# 10. Primary Use Cases

These use cases describe how actors interact with the system to achieve specific goals.

## 10.1 Use Case 1: Browse and Search Catalog
- **Actor:** Guest / Customer
- **Description:** The user wants to find a specific type of furniture.
- **Flow:**
  1. User navigates to the Products page.
  2. User enters "Oak Dining Table" into the search bar or applies the "Tables" category filter.
  3. System queries the API and returns matching active products.
  4. User clicks a product to view the detail page, inspecting high-resolution images, dimensions, and current stock.

## 10.2 Use Case 2: Checkout Process
- **Actor:** Customer
- **Description:** The user purchases items in their cart.
- **Flow:**
  1. User navigates to their Cart and clicks "Proceed to Checkout".
  2. System verifies stock levels for all items (enforcing BR-02).
  3. User selects or enters a shipping address.
  4. User confirms the order details.
  5. System processes the order atomically (enforcing BR-03), deducts inventory, and generates an Order ID.
  6. User is redirected to an Order Success page.

## 10.3 Use Case 3: Book Showroom Appointment
- **Actor:** Customer
- **Description:** The user wants to physically inspect a sofa before buying.
- **Flow:**
  1. User navigates to the Appointments section.
  2. System displays available dates and time slots based on the Store Manager's configured schedule.
  3. User selects a slot and adds a note (e.g., "Interested in viewing the Velvet Sofa").
  4. User confirms booking.
  5. System marks the slot as Pending and alerts the Store Manager.

## 10.4 Use Case 4: Manage Inventory
- **Actor:** Store Manager
- **Description:** The manager receives a new shipment of chairs and updates the system.
- **Flow:**
  1. Manager logs into the Manager Dashboard.
  2. Manager navigates to the Catalog Management page and searches for the specific chair.
  3. Manager clicks "Edit" and increases the stock quantity by 20.
  4. Manager saves changes.
  5. System updates the database and logs the action in the Audit Log (enforcing BR-08).

## 10.5 Use Case 5: Moderate Reviews
- **Actor:** Administrator
- **Description:** An inappropriate review is flagged on the site.
- **Flow:**
  1. Admin logs into the Admin Control Panel.
  2. Admin navigates to the Reviews oversight page.
  3. Admin locates the offending review and clicks "Delete".
  4. System removes the review from public visibility and logs the deletion action.
