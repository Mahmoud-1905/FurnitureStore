# 05 — Business Rules
> **Destination:** Antigravity  
> **Purpose:** Each rule becomes a validation method in the Service Layer.  
> **Rule:** Business rules NEVER go inside Controllers. They belong in Service classes only.

---

## Critical Architecture Rule
```
Controller → calls Service method → Service enforces rules → calls Repository → EF Core → DB
```
The Controller only checks `ModelState.IsValid` and calls the service.  
The Service is where ALL of the following rules live.

---

## Rule 1 — One Cart Per Customer
**Service:** `CartService`  
**Method:** `GetOrCreateCartAsync(string userId)`

```
IF a Cart already exists for this UserId → return existing Cart
ELSE → create new Cart for this UserId
ENFORCE: UNIQUE constraint on Carts.UserId at database level (belt AND suspenders)
```

---

## Rule 2 — Cart Quantity Cannot Exceed Stock
**Service:** `CartService`  
**Method:** `AddToCartAsync(string userId, int productId, int quantity)`

```
product = GetProductById(productId)
IF product == null OR product.IsActive == false → throw ProductNotFoundException
IF quantity > product.StockQuantity → throw InsufficientStockException
IF item already in cart → new total quantity must still be <= StockQuantity
```

---

## Rule 3 — Order Placement is Atomic (Transaction)
**Service:** `OrderService`  
**Method:** `PlaceOrderAsync(string userId, CheckoutViewModel model)`

```
1. Get cart for userId — IF empty → throw EmptyCartException
2. FOR EACH CartItem:
   - Get fresh product from DB (lock for update)
   - IF product.StockQuantity < item.Quantity → throw InsufficientStockException
3. BEGIN EF Core transaction
4. INSERT Order record (status = Pending, snapshot DeliveryAddress)
5. INSERT OrderItems (UnitPrice = product.Price AT THIS MOMENT — snapshot)
6. UPDATE Products.StockQuantity -= item.Quantity for each item
7. DELETE all CartItems for this user
8. COMMIT transaction
ON ANY EXCEPTION → ROLLBACK — stock NOT decremented, cart PRESERVED
```

---

## Rule 4 — Price Snapshot on Order Items
**Service:** `OrderService`  
**Rule:** `OrderItem.UnitPrice = product.Price at time of order placement`

```
NEVER reference Product.Price after order creation
NEVER update OrderItem.UnitPrice if Product.Price changes later
The snapshot is immutable — it is the legal price paid
```

---

## Rule 5 — Soft Delete Only
**Service:** `ProductService`, `UserService`

```
Products: IsActive = false (never DELETE from Products table)
Users: IsActive = false (never DELETE from AspNetUsers table)
Orders: never deleted under any circumstance
AuditLogs: never deleted — append-only
Reason: deleting a Product would orphan OrderItems and destroy purchase history
```

---

## Rule 6 — Stock Cannot Go Below Zero
**Service:** `InventoryService`  
**Method:** `DecrementStockAsync(int productId, int quantity)`

```
current = product.StockQuantity
IF current - quantity < 0 → throw InsufficientStockException
ELSE → product.StockQuantity -= quantity
ENFORCE: CHECK constraint on Products.StockQuantity >= 0 at DB level
```

---

## Rule 7 — Appointment: Slot Capacity Enforcement
**Service:** `AppointmentService`  
**Method:** `BookAppointmentAsync(string userId, BookAppointmentViewModel model)`

```
MaxCapacityPerSlot = from app configuration (default: 3)
slotCount = count existing Confirmed/Pending appointments for same date+time
IF slotCount >= MaxCapacityPerSlot → throw SlotFullException
IF user already has a Pending/Confirmed booking for same slot → throw DuplicateBookingException
IF appointmentDate < DateTime.UtcNow.AddHours(24) → throw InvalidBookingTimeException
```

---

## Rule 8 — Appointment: 24-Hour Cancellation Window
**Service:** `AppointmentService`  
**Method:** `CancelAppointmentAsync(string userId, int appointmentId)`

```
appointment = GetByIdAsync(appointmentId)
IF appointment.UserId != userId → throw ForbiddenException (data isolation)
IF appointment.AppointmentDate <= DateTime.UtcNow.AddHours(24) → throw CancellationWindowExpiredException
IF appointment.Status == "Cancelled" → throw AlreadyCancelledException
ELSE → appointment.Status = "Cancelled"
```

---

## Rule 9 — One Review Per Customer Per Product (Verified Purchase)
**Service:** `ReviewService`  
**Method:** `SubmitReviewAsync(string userId, int productId, ReviewViewModel model)`

```
1. Check: does a Delivered Order exist for this userId containing this productId?
   IF NOT → throw NoPurchaseFoundException (403)
2. Check: does a Review already exist for this (userId, productId) pair?
   IF YES → throw DuplicateReviewException (409)
3. Set IsVerifiedPurchase = true
4. Save review
ENFORCE: UNIQUE(ProductId, UserId) constraint at DB level
```

---

## Rule 10 — Admin Actions Must Be Audit-Logged
**Service:** `AdminService` (or inline in specific services)  
**Method:** called after every admin action

```
AFTER any of the following actions:
- User deactivated / reactivated
- Role assigned / revoked
- Review deleted
→ INSERT AuditLog { ActorUserId, ActionType, TargetEntityType, TargetEntityId, Description, CreatedAt }
NO update or delete endpoint exists for AuditLogs
```

---

## Rule 11 — Data Isolation Between Customers
**Service:** All customer-facing services

```
A customer can ONLY access their OWN:
- Cart
- Orders
- Appointments
- Reviews
- Wishlist
- Addresses

Validation pattern in every service method:
IF entity.UserId != currentUserId → throw ForbiddenException
```

---

## Custom Exception Classes Required
```csharp
EmptyCartException
InsufficientStockException
ProductNotFoundException
SlotFullException
DuplicateBookingException
InvalidBookingTimeException
CancellationWindowExpiredException
AlreadyCancelledException
NoPurchaseFoundException
DuplicateReviewException
ForbiddenException
DuplicateSkuException
InvalidFileException
NotFoundException
```

---

## Antigravity Prompt
```
Read 05_BUSINESS_RULES.md.
Write C# validation methods for each rule inside the appropriate Service class.
Create all custom exception classes listed.
Do NOT create controllers yet.
Each rule must be a method that either returns a result or throws a typed exception.
```
