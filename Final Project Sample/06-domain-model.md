# 06. Domain Model & Class Diagram

## 6.1 Identifying Classes from Use Cases

Domain classes are discovered by extracting nouns from use case descriptions, database structures, and e-commerce requirements. The key technique is to examine actors, objects mentioned in purchasing/booking scenarios, and persistent data.

| Source | Nouns Extracted | Candidate Class |
| :--- | :--- | :--- |
| **UC-004: Checkout & Place Order** | customer, shopping cart, product, order, order item, price snapshot, inventory stock | Customer, Cart, CartItem, Product, Order, OrderItem |
| **UC-007: Book Showroom Visit** | customer, showroom, appointment, booking date, time slot, status | ShowroomAppointment |
| **UC-006: Submit Product Review** | customer, product, review rating, comments, verified status | ProductReview |
| **FR-002: Role-Based Access Control** | user, identity, role, email, password | ApplicationUser, Role |
| **FR-006: Dynamic Catalog** | category, product catalog, search filters | Category |

After filtering out attributes, transient request states, and out-of-scope system nouns, the final structural domain classes are defined below.

---

## 6.2 Domain Model

```mermaid
classDiagram
    class User {
        -int userId
        -string firstName
        -string lastName
        -string email
        -string passwordHash
        -Role role
        -boolean isActive
        -datetime createdAt
        +login()
        +logout()
        +updateProfile()
        +resetPassword()
    }

    class Customer {
        -int customerId
        -string defaultShippingAddress
        -string phone
        +addToCart()
        +checkout()
        +bookShowroomVisit()
    }

    class StoreManager {
        -int managerId
        -string employeeId
        +updateProductCatalog()
        +manageStock()
        +approveAppointment()
    }

    class Product {
        -int productId
        -string sku
        -string name
        -string description
        -decimal price
        -int stockQuantity
        -string material
        -string dimensions
        -boolean isActive
        +updateStock(int quantity)
        +deactivate()
        +isLowStock() boolean
    }

    class Category {
        -int categoryId
        -string name
        -string description
        -boolean isActive
        +getProducts()
    }

    class Cart {
        -int cartId
        -datetime createdAt
        -datetime updatedAt
        +addItem(Product product, int qty)
        +removeItem(int productId)
        +clearCart()
        +calculateTotal() decimal
    }

    class CartItem {
        -int cartItemId
        -int quantity
        +updateQuantity(int qty)
    }

    class Order {
        -int orderId
        -datetime orderDate
        -OrderStatus orderStatus
        -BillingStatus billingStatus
        -decimal totalAmount
        -string shippingAddress
        +updateStatus(OrderStatus status)
        +cancelOrder()
    }

    class OrderItem {
        -int orderItemId
        -decimal priceSnapshot
        -int quantity
        +getSubtotal() decimal
    }

    class ShowroomAppointment {
        -int appointmentId
        -string showroomLocation
        -datetime appointmentDate
        -string timeSlot
        -AppointmentStatus status
        +approve()
        +reschedule(datetime newDate)
        +cancel()
    }

    class ProductReview {
        -int reviewId
        -int rating
        -string comment
        -datetime submittedAt
        -boolean isModerated
        +approveReview()
        +flagReview()
    }
class Wishlist {
        -int wishlistId
        -datetime createdAt
        +addItem(Product product)
        +removeItem(int productId)
        +clearWishlist()
    }

    class AuditLog {
        -int logId
        -int userId
        -string actionType
        -string ipAddress
        -datetime timestamp
    }

    %% Inheritance
    User <|-- Customer
    User <|-- StoreManager

    %% Associations
    Category "1" --> "*" Product : classifies
    Customer "1" --> "1" Cart : owns
    Cart "1" --> "*" CartItem : contains
    Product "1" --> "*" CartItem : references
    Customer "1" --> "*" Order : places
    Order "1" --> "*" OrderItem : contains
    Product "1" --> "*" OrderItem : snapshotted in
    Customer "1" --> "*" ShowroomAppointment : schedules
    Customer "1" --> "*" ProductReview : writes
    Product "1" --> "*" ProductReview : receives
    Customer "1" --> "1" Wishlist : owns
    Wishlist "1" --> "*" Product : contains
    User "1" --> "*" AuditLog : generates
```
## 6.3 Class Relationship Summary

| Relationship | Type | Description |
| :--- | :--- | :--- |
| **User → Customer / StoreManager** | Inheritance | All active roles inherit identity properties. Each child class extends the base `User` class with role-specific behavior. |
| **Category → Product** | Association (1:*) | A category contains many products. Every product belongs to exactly one category. |
| **Customer → Cart** | Composition (1:1) | A cart exists only for its owning customer and cannot exist independently. |
| **Cart → CartItem** | Composition (1:*) | Cart items are dependent child entities. Removing a cart removes all associated cart items. |
| **Order → OrderItem** | Composition (1:*) | Order items represent immutable purchase lines belonging exclusively to a single order. |
| **Product → OrderItem** | Association (1:*) | Each order item stores a `priceSnapshot` to preserve historical pricing. |
| **Customer → ShowroomAppointment** | Association (1:*) | A customer may schedule multiple showroom appointments. |
| **Customer → ProductReview** | Association (1:*) | A verified customer may submit one review per purchased product. |
| **Customer → Wishlist** | Composition (1:1) | A wishlist belongs exclusively to one registered customer. |
| **Wishlist → Product** | Association (1:*) | A wishlist contains multiple products selected by the user. |
| **User → AuditLog** | Association (1:*) | A user (specifically administrators) generates multiple system logs during operations. |

---

## 6.4 Enumeration Types

```mermaid
classDiagram
    class Role {
        <<enumeration>>
        CUSTOMER
        STORE_MANAGER
        ACCOUNTANT
        ADMINISTRATOR
    }

    class OrderStatus {
        <<enumeration>>
        PENDING
        PROCESSING
        SHIPPED
        DELIVERED
        CANCELLED
    }

    class AppointmentStatus {
        <<enumeration>>
        PENDING_APPROVAL
        APPROVED
        RESCHEDULED
        CANCELLED
    }

    class BillingStatus {
        <<enumeration>>
        PENDING_PAYMENT
        PAID
        REFUNDED
    }
```

---
[← Previous: User Stories](./05-user-stories.md) | [Back to Index](./00-index.md) | [Next:  UML Behavioral Models →](./07-uml-behavioral.md)


