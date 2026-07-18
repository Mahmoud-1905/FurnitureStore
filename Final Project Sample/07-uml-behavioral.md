# 07. UML Behavioral Models

## 7.1 Sequence Diagram: Checkout & Place Order

This diagram illustrates the step-by-step API interactions and database transactions when a customer completes a purchase (UC-004).

```mermaid
sequenceDiagram
    autonumber

    actor C as Customer
    participant UI as Web/Mobile Client
    participant CC as CartController
    participant OC as OrderController
    participant IS as InventoryService
    participant DB as Database
    participant ES as EmailService

    C->>UI: Click "Proceed to Checkout"

    UI->>CC: GET /api/v1/carts/active
    activate CC
    CC->>DB: SELECT cart and cart_items WHERE userId = active
    DB-->>CC: Active cart data with products
    CC-->>UI: 200 OK (Cart payload)
    deactivate CC

    UI-->>C: Display Order Summary and Price Snapshot
    C->>UI: Select shipping and payment then click Confirm Order

    UI->>OC: POST /api/v1/orders
    activate OC

    OC->>IS: validateStockAndReserve(cartItems)
    activate IS

    IS->>DB: SELECT stock_quantity FROM products
    DB-->>IS: Available stock counts

    alt Stock unavailable
        IS-->>OC: StockValidationException
        OC-->>UI: 400 Bad Request
    else Stock available
        IS-->>OC: Stock Reserved
    end

    deactivate IS

    OC->>DB: BEGIN TRANSACTION
    OC->>DB: INSERT INTO orders
    DB-->>OC: orderId

    loop For each cart item
        OC->>DB: INSERT INTO order_items
    end

    OC->>DB: UPDATE product stock
    OC->>DB: DELETE cart items
    OC->>DB: COMMIT TRANSACTION
    DB-->>OC: Transaction committed

    OC->>ES: Send confirmation email
    OC-->>UI: 201 Created

    deactivate OC

    UI-->>C: Display Order Confirmation
```

---

## 7.2 Sequence Diagram: Book Showroom Visit

This diagram tracks the interactions when a customer schedules a showroom appointment (UC-007).

```mermaid
sequenceDiagram
    autonumber

    actor C as Customer
    participant UI as Web/Mobile Client
    participant SC as ShowroomController
    participant AS as AppointmentService
    participant DB as Database
    participant NS as NotificationService

    C->>UI: Select showroom and date

    UI->>SC: GET available slots
    activate SC

    SC->>AS: getAvailableTimeSlots()
    activate AS

    AS->>DB: Read booked appointments
    DB-->>AS: Existing bookings

    AS-->>SC: Available time slots
    deactivate AS

    SC-->>UI: 200 OK
    deactivate SC

    UI-->>C: Display available slots

    C->>UI: Confirm booking

    UI->>SC: POST appointment
    activate SC

    SC->>AS: scheduleAppointment()
    activate AS

    AS->>DB: Check duplicate booking
    DB-->>AS: No duplicates

    AS->>DB: Insert appointment
    DB-->>AS: appointmentId

    AS->>NS: Notify administrator

    activate NS
    NS-->>AS: Notification queued
    deactivate NS

    AS-->>SC: Booking confirmed
    deactivate AS

    SC-->>UI: 201 Created
    deactivate SC

    UI-->>C: Display booking confirmation
```

## 7.3 Activity Diagram: Order Processing & Checkout

This flowchart details the checkout process.

```mermaid
flowchart TD

    Start([Start Checkout]) --> A[Retrieve Active Cart]

    A --> B{Cart Empty?}

    B -->|Yes| C[Display Empty Cart]
    C --> End1([End])

    B -->|No| D{Items In Stock?}

    D -->|No| E[Highlight Out of Stock Items]
    E --> F[Update Cart]
    F --> A

    D -->|Yes| G[Reserve Stock]
    G --> H[Display Order Summary]

    H --> I{Payment Completed?}

    I -->|No| J[Release Reserved Stock]
    J --> End2([End])

    I -->|Yes| K[Create Order]
    K --> L[Update Inventory]
    L --> M[Clear Shopping Cart]
    M --> N[Send Receipt]
    N --> End3([End])

    style B fill:#ff9800,color:#fff
    style D fill:#ff9800,color:#fff
    style I fill:#ff9800,color:#fff
    style K fill:#4caf50,color:#fff
```

---
## 7.4 State Machine Diagram: Order Lifecycle

```mermaid
stateDiagram-v2

    [*] --> Pending

    Pending --> Processing : Payment Approved
    Pending --> Cancelled : Payment Timeout / Manual Cancellation

    Processing --> Shipped : Dispatched from Warehouse
    Processing --> Cancelled : Cancelled before Shipping

    Shipped --> Delivered : Customer Received Items

    Delivered --> [*]
    Cancelled --> [*]
```

### State Descriptions & Rules


| State | Description | Allowed Transitions |
| :--- | :--- | :--- |
| **Pending** | Order is created, payment review is pending, and stock is reserved. | Processing, Cancelled |
| **Processing** | Payment is verified, and the warehouse manager is preparing the furniture order. | Shipped, Cancelled |
| **Shipped** | Order has been handed over to the delivery team. | Delivered |
| **Delivered** | Final successful state. The furniture is delivered to the customer. | None |
| **Cancelled** | The order is terminated, and any reserved stock is immediately released. | None |

---

## 7.5 Activity Diagram: Dynamic Price & Discount Calculation

```mermaid
flowchart TD

    Start([Begin Price Calculation]) --> A[Calculate Cart Subtotal]

    A --> B[Apply Category Discounts]

    B --> C{Coupon Available?}

    C -->|Yes| D{Coupon Valid?}

    D -->|Yes| E[Apply Discount]
    D -->|No| F[Ignore Coupon]

    C -->|No| G[Determine Shipping Region]
    E --> G
    F --> G

    G --> H[Calculate Shipping Cost]

    H --> I{Eligible For Free Shipping?}

    I -->|Yes| J[Apply Free Shipping]
    I -->|No| K[Add Shipping Fee]

    J --> L[Calculate Sales Tax]
    K --> L

    L --> M[Calculate Grand Total]
    M --> N[Store Price Snapshot]
    N --> End([End])

    style B fill:#2196f3,color:#fff
    style I fill:#ff9800,color:#fff
    style M fill:#4caf50,color:#fff
```

### Scenario Calculation Example

| Step | Item | Rule Type | Calculation Process | Resulting Subtotal |
|------|------|-----------|---------------------|-------------------:|
| 1 | Raw Cart Total | 3 Premium Items | $400.00 + $250.00 + $150.00 | **$800.00** |
| 2 | Promo Code | SUMMER15 (15%) | Apply 15% discount | **$680.00** |
| 3 | Shipping | Standard Delivery | Shipping fee +$45.00 | **$725.00** |
| 4 | Free Shipping | Threshold Rule | Shipping rebate -$45.00 | **-$45.00** |
| 5 | Sales Tax | 8.25% | Tax +$56.10 | **+$56.10** |
| 6 | Final Total | Price Snapshot | Rounded total | **$736.10** |

---

[← Previous: Domain Model](./06-domain-model.md) | [Back to Index](./00-index.md) | [Next: Database Design →](./08-database-design.md)
