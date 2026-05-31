# 7. Functional Requirements

This section details the specific behaviors and functions the system must support, organized by user domain.

## 7.1 Public / Guest Functions
- **FR-P01:** The system shall display a paginated catalog of all active furniture products.
- **FR-P02:** The system shall provide a search bar to query products by name or description.
- **FR-P03:** The system shall allow users to filter products by Category, Material, and Price Range.
- **FR-P04:** The system shall provide a registration form requiring Name, Email, Password, and Phone Number.

## 7.2 Customer Functions
- **FR-C01:** The system shall securely authenticate customers using email and password.
- **FR-C02:** The system shall allow customers to add specific quantities of products to a shopping cart.
- **FR-C03:** The system shall calculate a running total in the cart, including applicable taxes.
- **FR-C04:** The system shall provide a checkout process collecting shipping and billing information.
- **FR-C05:** The system shall allow customers to select an available date and time slot to book a showroom appointment.
- **FR-C06:** The system shall allow customers to view the status of their current and past orders.
- **FR-C07:** The system shall allow customers to submit a 1-5 star rating and text review for a product, provided they have a completed order containing that product.

## 7.3 Store Manager Functions
- **FR-M01:** The system shall allow the manager to Create, Read, Update, and (Soft) Delete product records.
- **FR-M02:** The system shall allow the manager to update the stock level integer for any product.
- **FR-M03:** The system shall display a list of all customer orders, filterable by status (Pending, Shipped, Delivered, etc.).
- **FR-M04:** The system shall allow the manager to change the status of an order.
- **FR-M05:** The system shall display a calendar or list view of requested showroom appointments.
- **FR-M06:** The system shall allow the manager to confirm or decline appointment requests.

## 7.4 Administrator Functions
- **FR-A01:** The system shall allow the admin to view a list of all registered users.
- **FR-A02:** The system shall allow the admin to toggle the active status (ban/unban) of any Customer account.
- **FR-A03:** The system shall allow the admin to elevate a Customer account to a Store Manager role.
- **FR-A04:** The system shall allow the admin to delete any product review deemed inappropriate.
- **FR-A05:** The system shall maintain and display an immutable audit log of all actions performed by Admin and Manager accounts.
