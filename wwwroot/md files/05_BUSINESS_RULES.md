# 05_BUSINESS_RULES.md

## Business Rules

| Rule ID | Description |
|---------|-------------|
| **BR-01** | A customer can only submit a product review if they have a completed (**"Delivered"**) order containing that specific product. |
| **BR-02** | Adding an item to the cart or placing an order must fail if the requested quantity exceeds the current stock level. |
| **BR-03** | Order placement must be handled atomically; if inventory deduction fails, the entire transaction must roll back. |
| **BR-04** | Product price changes by the Store Manager shall never retroactively affect the unit prices recorded in historical orders. |
| **BR-05** | Products can only be **soft‑deleted** (`IsActive = false`) to preserve order history and relational integrity. |
| **BR-06** | Showroom appointments cannot be canceled by the customer within **24 hours** of the scheduled time. |
| **BR-07** | All non‑public API endpoints must validate the JWT and assert the required role claim before processing. |
| **BR-08** | Administrative actions (user deactivation, role changes, review deletion) must be permanently recorded in an **audit log**. |

These rules are enforced in the backend services (Node.js/Express) and reflected in the ASP.NET Core MVC authorization attributes.

---

*The above file has been created with standard Markdown, no extra front‑matter required.*
