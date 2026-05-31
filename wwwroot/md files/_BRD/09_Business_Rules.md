# 9. Business Rules

Business rules are immutable logic constraints that protect the operational integrity of the furniture store. The system must enforce these rules without exception.

- **BR-01 (Verified Reviews):** A customer is strictly prohibited from submitting a product review unless the database confirms they have an order containing that specific product, and the order status is exactly "Delivered".
- **BR-02 (Inventory Protection):** Adding an item to the shopping cart, or finalizing an order checkout, must immediately fail and alert the user if the requested quantity exceeds the integer value of the current available stock.
- **BR-03 (Atomic Transactions):** Order placement must be handled as an atomic database transaction. If inventory deduction, order creation, or payment logging fails at any point, the entire transaction must roll back to prevent orphaned data or overselling.
- **BR-04 (Historical Pricing):** If the Store Manager changes the price of a product in the catalog, this change must only affect future orders. The unit prices recorded in historical (completed or pending) orders must remain locked to the price at the time of purchase.
- **BR-05 (Data Preservation):** Products cannot be hard-deleted from the database (DELETE command). They must only be "soft-deleted" by setting an `IsActive` flag to `false`. This preserves relational integrity with historical orders and past reviews.
- **BR-06 (Appointment Cancellation):** Customers are permitted to cancel their showroom appointments through the portal. However, the system must block cancellation requests made less than 24 hours before the scheduled appointment time.
- **BR-07 (API Security):** All API endpoints, excluding public catalog routes and login/registration, must cryptographically validate the user's JWT and assert that the user's Role Claim matches the endpoint's requirements before executing any logic.
- **BR-08 (Administrative Auditing):** Every action performed by an Administrator (e.g., banning a user, deleting a review) or Store Manager (e.g., changing stock) must be permanently recorded in an immutable Audit Log table, detailing the User ID, Timestamp, Action Type, and Target ID.
