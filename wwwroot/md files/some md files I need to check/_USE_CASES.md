# System Use Cases (Ruqi Store System)

This document describes 10 complete user-system interactions with main scenarios and alternative scenarios (which form the basis for our error handling). Each use case is also mapped to its corresponding controller action to guide the technical implementation.

---

## UC-01: Customer Register
- **Primary Actor:** Guest
- **Main Success Scenario:** 
  1. Guest navigates to the registration page. 
  2. Guest enters valid email, password, and personal details. 
  3. System validates input, creates the account, and redirects to the login page with a success message.
- **Alternative Scenarios:** 
  - *Email already exists:* System rejects registration, displays "Email already in use" error, and keeps the user on the form.
  - *Weak password:* System displays validation error requiring stronger password criteria.
- **Implementation Mapping:**
  - **Controller:** AccountController
  - **Action:** Register
  - **HTTP Method:** GET (View) / POST (Submit)
  - **View File:** `Views/Account/Register.cshtml`

---

## UC-02: Customer Login
- **Primary Actor:** Guest / Customer
- **Main Success Scenario:**
  1. User navigates to the login page.
  2. User enters correct email and password.
  3. System authenticates credentials, issues JWT/Cookie, and redirects to the Homepage or previously requested URL.
- **Alternative Scenarios:**
  - *Invalid credentials:* System denies access, clears password field, and displays "Invalid email or password."
  - *Account deactivated:* System denies access and displays "Account is locked or deactivated. Contact Admin."
- **Implementation Mapping:**
  - **Controller:** AccountController
  - **Action:** Login
  - **HTTP Method:** GET (View) / POST (Submit)
  - **View File:** `Views/Account/Login.cshtml`

---

## UC-03: Book Showroom Appointment
- **Primary Actor:** Customer
- **Main Success Scenario:**
  1. Customer navigates to the Appointments section.
  2. Customer selects a date and an available time slot.
  3. Customer submits the booking request.
  4. System reserves the slot, marks it as "Pending", and shows a success confirmation.
- **Alternative Scenarios:**
  - *Slot taken:* If another user books the slot concurrently, the system rejects the booking and prompts the customer to choose another time.
  - *Past date selected:* System validation prevents selection of dates in the past.
- **Implementation Mapping:**
  - **Controller:** AppointmentsController
  - **Action:** Book
  - **HTTP Method:** GET (Form) / POST (Submit)
  - **View File:** `Views/Appointments/Book.cshtml`

---

## UC-04: View My Appointments
- **Primary Actor:** Customer
- **Main Success Scenario:**
  1. Customer navigates to their profile/appointments area.
  2. System retrieves and displays a list of the customer's upcoming and past appointments, showing their current statuses (Pending, Confirmed, Cancelled).
- **Alternative Scenarios:**
  - *No appointments:* System displays a friendly "You have no appointments booked" message with a link to book one.
- **Implementation Mapping:**
  - **Controller:** AppointmentsController
  - **Action:** Index (or MyAppointments)
  - **HTTP Method:** GET
  - **View File:** `Views/Appointments/Index.cshtml`

---

## UC-05: Cancel Appointment
- **Primary Actor:** Customer
- **Main Success Scenario:**
  1. Customer views their upcoming appointments.
  2. Customer clicks "Cancel" on an appointment scheduled more than 24 hours in the future.
  3. System prompts for confirmation.
  4. Customer confirms. System updates status to "Cancelled" and frees up the time slot.
- **Alternative Scenarios:**
  - *Under 24 hours:* System rejects cancellation attempt, displaying "Appointments cannot be cancelled within 24 hours of the scheduled time."
- **Implementation Mapping:**
  - **Controller:** AppointmentsController
  - **Action:** Cancel
  - **HTTP Method:** POST
  - **View File:** N/A (Redirects back to `Views/Appointments/Index.cshtml` with temp data message)

---

## UC-06: Manager View Appointments
- **Primary Actor:** Store Manager
- **Main Success Scenario:**
  1. Manager navigates to the Appointment Dashboard.
  2. System displays a calendar or list of all upcoming customer appointments, highlighting pending requests.
- **Alternative Scenarios:**
  - *Unauthorized access:* If a standard Customer tries to access this route, the system returns a 403 Forbidden or redirects to the homepage.
- **Implementation Mapping:**
  - **Controller:** ManagerAppointmentsController
  - **Action:** Dashboard
  - **HTTP Method:** GET
  - **View File:** `Views/ManagerAppointments/Dashboard.cshtml`

---

## UC-07: Update Order Status
- **Primary Actor:** Store Manager
- **Main Success Scenario:**
  1. Manager views the list of active orders.
  2. Manager selects an order and changes its status from "Processing" to "Shipped".
  3. System updates the database and notifies the customer (via UI or email logic).
- **Alternative Scenarios:**
  - *Invalid state transition:* System prevents moving an order directly from "Pending" to "Delivered" without intermediate steps, returning a validation error.
- **Implementation Mapping:**
  - **Controller:** ManagerOrdersController
  - **Action:** UpdateStatus
  - **HTTP Method:** POST
  - **View File:** N/A (Redirects to `Views/ManagerOrders/Details.cshtml`)

---

## UC-08: Admin Manage Users
- **Primary Actor:** Administrator
- **Main Success Scenario:**
  1. Admin navigates to the User Management panel.
  2. Admin views a list of all registered users.
  3. Admin clicks "Deactivate" on a specific user.
  4. System updates the user's status to inactive and logs the action in the audit log.
- **Alternative Scenarios:**
  - *Self-deactivation:* System prevents the Admin from deactivating their own currently active account to avoid lockout.
- **Implementation Mapping:**
  - **Controller:** AdminUsersController
  - **Action:** ToggleStatus
  - **HTTP Method:** POST
  - **View File:** N/A (Redirects to `Views/AdminUsers/Index.cshtml`)

---

## UC-09: Customer Add to Cart & Checkout
- **Primary Actor:** Customer
- **Main Success Scenario:**
  1. Customer adds a product to the cart.
  2. Customer proceeds to checkout, enters shipping info, and confirms.
  3. System deducts inventory, creates the order record, and empties the cart.
- **Alternative Scenarios:**
  - *Out of stock during checkout:* If inventory was purchased by another user before confirmation, system halts checkout, alerts the customer, and adjusts cart quantities.
- **Implementation Mapping:**
  - **Controller:** CheckoutController
  - **Action:** SubmitOrder
  - **HTTP Method:** POST
  - **View File:** `Views/Checkout/Success.cshtml`

---

## UC-10: Manager Manage Products
- **Primary Actor:** Store Manager
- **Main Success Scenario:**
  1. Manager navigates to Catalog Management.
  2. Manager creates a new product entry, filling out dimensions, material, and price, and uploads an image.
  3. System saves the product and displays it in the public catalog.
- **Alternative Scenarios:**
  - *Missing mandatory fields:* System blocks creation and highlights missing required fields (e.g., Price must be > 0).
- **Implementation Mapping:**
  - **Controller:** ManagerProductsController
  - **Action:** Create
  - **HTTP Method:** GET (Form) / POST (Submit)
  - **View File:** `Views/ManagerProducts/Create.cshtml`
