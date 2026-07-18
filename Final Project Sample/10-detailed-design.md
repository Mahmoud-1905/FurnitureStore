# 10. Detailed Class Design & API Contracts

## 10.1 Design Patterns Applied

The Ruqi Store architecture applies established software design patterns to solve common architectural challenges and maintain a clean separation of concerns.

| Pattern | Category | Where Applied | Problem Solved |
|---|---|---|---|
| **Repository** | Structural | Data Access Layer | Abstracts database operations behind a consistent interface. Isolates business services from raw database queries, allowing caching layers such as Redis to be injected transparently. |
| **MVC** | Architectural | System Presentation & Route Control | Separates UI, business logic, and request orchestration. Controllers process requests, invoke backend services, and return normalized responses or views. |
| **Observer** | Behavioral | Order Notification & Audit Logging | Triggers asynchronous background processes such as sending order confirmation emails, reducing stock, and writing audit logs when an order status changes without tight coupling. |
| **Factory** | Creational | Showroom Appointment Scheduler | A `BookingFactory` creates the correct appointment and notification structure depending on the selected showroom category. |
| **Strategy** | Behavioral | Discount & Promotion System | Different discount calculation strategies can be executed dynamically based on active shopping cart contents. |

---

## 10.2 SOLID Principles in Practice

### Single Responsibility Principle (SRP)

Each service has responsibility for one cohesive part of the business flow.

- `CartService` manages shopping cart operations, item additions, quantity updates, and cart state.
- `InventoryService` manages warehouse stock levels and product availability.
- `PaymentService` processes secure checkout payments.
- `InvoiceService` generates receipt documentation and invoice files.

Each class has only one reason to change.

---

### Open/Closed Principle (OCP)

The promotions and pricing engine supports adding new discount methods without modifying existing checkout logic.

Adding a new discount type only requires creating a new strategy class that implements the shared interface.

```csharp
public interface IDiscountStrategy
{
    decimal ApplyDiscount(
        IEnumerable<CartItemDto> cartItems,
        decimal originalTotal
    );
}

public class PercentageDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(
        IEnumerable<CartItemDto> cartItems,
        decimal originalTotal)
    {
        return originalTotal * 0.90m;
    }
}

public class FixedAmountDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(
        IEnumerable<CartItemDto> cartItems,
        decimal originalTotal)
    {
        return originalTotal - 50m;
    }
}
```

```csharp
// New strategy can be added without modifying existing checkout logic.

public class FlashSaleDiscountStrategy : IDiscountStrategy
{
    public decimal ApplyDiscount(
        IEnumerable<CartItemDto> cartItems,
        decimal originalTotal)
    {
        return originalTotal * 0.80m;
    }
}

```
public interface IWishlistService
{
    Task<IEnumerable<CartItemDto>> GetWishlistAsync(string userId);
    Task<bool> AddToWishlistAsync(string userId, int productId);
    Task<bool> RemoveFromWishlistAsync(string userId, int productId);
    Task<bool> MoveToCartAsync(string userId, int productId);
}
## B. ICartService

**Responsibility:**  
Manages the user's active shopping cart and validates inventory before checkout.

```csharp
public interface ICartService
{
    Task<CartDto> GetActiveCartAsync(
        string userId
    );

    Task<bool> AddToCartAsync(
        string userId,
        int productId,
        int quantity
    );

    Task<bool> UpdateCartItemQuantityAsync(
        string userId,
        int cartItemId,
        int newQuantity
    );

    Task<bool> RemoveFromCartAsync(
        string userId,
        int cartItemId
    );

    Task<bool> ClearCartAsync(
        string userId
    );
}
```

---

## C. CartItemDto

**Purpose:**  
Represents a single product displayed in the shopping cart.

```csharp
public class CartItemDto
{
    public int CartItemId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotal => UnitPrice * Quantity;
}
```

---
# 10.5 System Validation & Business Rules

Before any checkout transaction is committed, the following validation rules and pipeline operations are strictly enforced.

## Checkout Validation Pipeline

| Step | Validation Action |
|---|---|
| 1 | Verify product stock availability in the warehouse. |
| 2 | Read the live product price from the catalog. |
| 3 | Save and freeze the price inside `OrderItem.PriceSnapshot`. |
| 4 | Generate the base `Order` record. |
| 5 | Generate all associated `OrderItem` records. |
| 6 | Deduct physical inventory stock quantities. |
| 7 | Flush and clear the user's active shopping cart session. |
| 8 | Commit the transaction atomically. |

---

## Checkout Workflow

```mermaid
flowchart TD
    A[Checkout Request] --> B[Verify Stock]
    B --> C[Freeze Product Prices]
    C --> D[Create Order]
    D --> E[Create Order Items]
    E --> F[Update Inventory]
    F --> G[Clear Shopping Cart]
    G --> H[Commit Transaction]
    H --> I[Return Success Response]

    style A fill:#e3f2fd,stroke:#1e88e5,stroke-width:2px
    style H fill:#e8f5e9,stroke:#43a047,stroke-width:2px
    style I fill:#e8f5e9,stroke:#43a047,stroke-width:2px
```

### Stock Verification

Before creating an order, the application verifies the available inventory for every product in the shopping cart.

If the requested quantity exceeds the available stock, the checkout process is cancelled and a validation error is returned.

---

### Price Snapshot Pattern

During checkout, the current product price is copied into the `OrderItem.PriceSnapshot` field.

This ensures historical transaction and financial data remains accurate even if catalog prices change later.

---

### Atomic Transactions

All checkout operations run within a single shared database transaction.

If any operation fails, the transaction is rolled back automatically to preserve database consistency.

---
# 10.6 API Design (Sample Endpoints)

All endpoints follow RESTful conventions.

Authentication is required via a secure JSON Web Token (JWT) provided in the `Authorization` header.

---

## POST /api/carts/items

**Purpose:**  
Customer adds a product to their active shopping cart.

| Field | Value |
|---|---|
| Method | POST |
| URL | `/api/carts/items` |
| Authentication | Bearer JWT (Customer role) |
| Content-Type | `application/json` |

### Request Body

```json
{
  "productId": 204,
  "quantity": 2
}
```

### Success Response (201 Created)

```json
{
  "cartItemId": 982,
  "cartId": 55,
  "productId": 204,
  "quantity": 2,
  "addedAt": "2026-07-16T11:00:00Z"
}
```

### Error Responses

| Status | Condition | Body |
|---|---|---|
| 400 | Out of stock | `{ "error": "Requested quantity exceeds available inventory." }` |
| 404 | Product not found | `{ "error": "Target product ID does not exist." }` |
| 401 | Unauthorized | `{ "error": "Authentication token missing or expired." }` |

---
## GET /api/orders/{orderId}

**Purpose:**  
Customer views their finalized purchase invoice and order status.

| Field | Value |
|---|---|
| Method | GET |
| URL | `/api/orders/{orderId}` |
| Authentication | Bearer JWT (Customer or Administrator role) |

### Success Response (200 OK)

```json
{
  "orderId": 4011,
  "customerId": 88,
  "orderDate": "2026-07-15T15:30:00Z",
  "orderStatus": "SHIPPED",
  "totalAmount": 1250.00,
  "shippingAddress": "Al-Amal Street, Khan Yunis, Gaza"
}
```

### Error Responses

| Status | Condition | Body |
|---|---|---|
| 403 | Forbidden | `{ "error": "You do not have permission to view this order." }` |
| 404 | Not found | `{ "error": "Order ID not found." }` |

---
## POST /api/showroom/appointments

**Purpose:**  
Customer schedules a consultation session in the showroom.

| Field | Value |
|---|---|
| Method | POST |
| URL | `/api/showroom/appointments` |
| Authentication | Bearer JWT (Customer role) |
| Content-Type | `application/json` |

### Request Body

```json
{
  "showroomLocation": "Main Branch",
  "appointmentDate": "2026-07-20",
  "timeSlot": "14:00:00"
}


```

### Success Response (201 Created)

```json
{
  "appointmentId": 302,
  "customerId": 88,
  "showroomLocation": "Main Branch",
  "appointmentDate": "2026-07-20",
  "timeSlot": "14:00:00",
  "status": "PENDING_APPROVAL"

}
```
---

## POST /api/wishlists/items

**Purpose:**  
Customer saves an item to their wishlist.

| Field | Value |
|---|---|
| Method | POST |
| URL | `/api/wishlists/items` |
| Authentication | Bearer JWT (Customer role) |
| Content-Type | `application/json` |

### Request Body

```json
{
  "productId": 204
}



```
### Success Response (201 Created)
```json
{
  "wishlistId": 12,
  "customerId": 88,
  "productId": 204,
  "createdAt": "2026-07-18T10:00:00Z"
}
```
---
# 10.7 Error Handling Strategy

The Ruqi Store system follows a layered error handling approach to ensure consistent responses, maintain system stability, and protect sensitive internal information.

| Layer | Responsibility | Example Execution |
|---|---|---|
| Controller | Intercepts exceptions and returns standardized HTTP responses to clients. | Returns `400` for invalid input, `404` for missing resources, and `500` for unexpected server errors. |
| Service | Evaluates business rules and throws domain-specific exceptions. | Throws `OutOfStockException` when requested quantity exceeds warehouse availability or `TimeSlotOccupiedException` when a showroom booking conflicts. |
| Repository | Handles database-related failures and converts low-level database errors into application exceptions. | Converts database constraint violations into `ReferentialIntegrityException`. |
| Middleware | Handles unhandled application failures globally, records audit information, and hides internal stack traces in production. | Captures unexpected failures, writes diagnostic details into `audit_logs`, and returns a generic server error message. |

---

## System Scalability Considerations

Our system architecture is designed to handle up to **1,000 concurrent shopping sessions** efficiently.

The stateless application layer allows developers to add additional virtual server instances behind the Application Load Balancer.

For example:

- App Instance 1
- App Instance 2
- App Instance 3
- App Instance 4

This enables horizontal scaling during seasonal traffic peaks while maintaining consistent performance and availability.

---

[← Previous: Architecture](./09-architecture.md) | [Back to Index](./00-index.md) | [Next: UI/UX Design →](./11-ui-ux-design.md)
