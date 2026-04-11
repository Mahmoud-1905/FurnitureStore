# User Stories & Backlog

## Epic Overview
| Epic                  | Description                                                                                     | Story Count |
| --------------------- | ----------------------------------------------------------------------------------------------- | ----------- |
| User Management       | Manage users including registration, login, profile updates, and password reset                 | 4           |
| Product Management    | Manage products: adding, updating, browsing, searching, and filtering                           | 5           |
| Shopping & Checkout   | Customer shopping experience: adding items to cart, applying discounts, and completing checkout | 4           |
| Order Management      | Manage orders: tracking, updating status, and handling cancellations                            | 4           |
| Reporting & Analytics | Generate sales and inventory reports for administrators                                         | 3           |
| Notifications         | Send notifications via email, SMS, or push for customers and sellers                            | 3           |


## 5.2 User Stories with Acceptance Criteria

### Epic 1: User Management

| ID   | User Story Name       | User Story                                                                               | Acceptance Criteria (Given/When/Then)                                                                                                                           |
| ---- | --------------------- | ---------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| US01 | Customer Registration | As a Customer, I want to register an account, so that I can make purchases.              | **Given** a new visitor, **When** they fill the registration form with valid details, **Then** the system creates a new account and sends a confirmation email. |
| US02 | Customer Login        | As a Customer, I want to log in, so that I can access my account.                        | **Given** a registered customer, **When** they enter valid credentials, **Then** they are logged in and redirected to the dashboard.                            |
| US03 | Update Profile        | As a Customer, I want to update my profile, so that my info is current.                  | **Given** a logged-in customer, **When** they edit their profile and submit, **Then** the system saves the updated information.                                 |
| US04 | Password Reset        | As a Customer, I want to reset my password, so that I can recover access if I forget it. | **Given** a customer who forgot their password, **When** they request a reset and provide valid email, **Then** the system sends a password reset link.         |


### Epic2: Product Management

| ID   | User Story Name | User Story                                                                                   | Acceptance Criteria (Given/When/Then)                                                                                                            |
| ---- | --------------- | -------------------------------------------------------------------------------------------- | ------------------------------------------------------------------------------------------------------------------------------------------------ |
| US05 | Add Product     | As a Seller, I want to add new products, so that they are available for sale.                | **Given** a logged-in seller, **When** they submit valid product details, **Then** the system adds the product to inventory.                     |
| US06 | Update Product  | As a Seller, I want to update product info, so that inventory is accurate.                   | **Given** a logged-in seller, **When** they edit a product and submit, **Then** the system updates the product info in inventory.                |
| US07 | Browse Products | As a Customer, I want to browse products, so that I can select items to buy.                 | **Given** a customer, **When** they access the product catalog, **Then** the system displays all products with images, prices, and descriptions. |
| US08 | Search Products | As a Customer, I want to search for products, so that I can find items quickly.              | **Given** a customer, **When** they enter a keyword and search, **Then** the system shows matching products.                                     |
| US09 | Filter Products | As a Customer, I want to filter products, so that I can narrow results by category or price. | **Given** a customer browsing products, **When** they apply a filter, **Then** the system displays only products that match the filter.          |

### Epic3: Shopping & Checkout

| ID   | User Story Name    | User Story                                                                                    | Acceptance Criteria (Given/When/Then)                                                                                                                                  |
| ---- | ------------------ | --------------------------------------------------------------------------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| US10 | Add to Cart        | As a Customer, I want to add items to my cart, so that I can buy multiple products.           | **Given** a customer viewing a product, **When** they click “Add to Cart”, **Then** the item is added to their cart and cart total updates.                            |
| US11 | Checkout Order     | As a Customer, I want to checkout my cart, so that I can complete the purchase.               | **Given** a customer with items in the cart, **When** they complete checkout and provide shipping/payment info, **Then** the order is confirmed and inventory updated. |
| US12 | Apply Discount     | As a Customer, I want to apply discount codes, so that I can reduce the total price.          | **Given** a customer at checkout, **When** they enter a valid discount code, **Then** the system applies the discount to the order total.                              |
| US13 | Payment Processing | As a Customer, I want the system to process payments, so that my order is completed securely. | **Given** a customer checking out, **When** they enter valid payment details, **Then** payment is processed and confirmation sent.                                     |

##  Story Map Summary

```mermaid
block-beta
    columns 5
    block:header:5
        A["User Activities"]
    end
    B1["Account Management"] B2["Product Management"] B3["Shopping & Checkout"] B4["Order Management"] B5["Notifications"]
    
    C1["US-001 Customer Registration"] C2["US-005 Add Product"] C3["US-010 Add to Cart"] C4["US-007 Track Orders"] C5["US-014 Email Notification"]
    D1["US-002 Customer Login"] D2["US-006 Update Product"] D3["US-011 Checkout Order"] D4["US-008 Update Order Status"] D5["US-015 SMS Notification"]
    E1["US-003 Update Profile"] E2["US-007 Browse Products"] E3["US-012 Apply Discount"] E4["US-009 Cancel Order"] E5["US-016 Push Notification"]
    F1["US-004 Password Reset"] E2["US-008 Search Products"] space E4["US-013 Payment Failed Handling"] space

    style B1 fill:#4caf50,color:#fff
    style B2 fill:#2196f3,color:#fff
    style B3 fill:#ff9800,color:#fff
    style B4 fill:#9c27b0,color:#fff
    style B5 fill:#f44336,color:#fff
