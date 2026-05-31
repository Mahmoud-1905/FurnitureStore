# 04 — API Documentation
## Ruqi Store — Node.js Express REST API

---

## AI Instructions
> The Node.js API acts as the core business logic layer. The ASP.NET Core MVC application consumes this API via HTTP clients. It does NOT connect directly to the database.

**Base URL:** `/api/v1`
**Authorization:** All protected endpoints require an HTTP header `Authorization: Bearer <token>`.

### Standard Response Envelopes
All responses must adhere to this JSON structure:

```json
// Standard Success Response
{ 
  "success": true, 
  "data": { ... }, 
  "message": "OK" 
}

// Standard Error Response
{ 
  "success": false, 
  "error": { 
    "code": "VALIDATION_ERROR", 
    "message": "...", 
    "details": [...] 
  } 
}
```

---

## 1. Authentication Endpoints

| METHOD | ENDPOINT | AUTH REQUIRED | REQUEST BODY | SUCCESS RESPONSE | ERROR CODES |
|--------|----------|---------------|--------------|------------------|-------------|
| POST | `/auth/register` | None | `{ email, password, fullName, phone? }` | 201 — `{ userId, email, role }` | 400 (validation), 409 (duplicate) |
| POST | `/auth/login` | None | `{ email, password }` | 200 — `{ accessToken, refreshToken, role }` | 400 (missing), 401 (invalid) |
| POST | `/auth/logout` | JWT | `{ refreshToken }` | 200 — `{ message }` | 401 |
| POST | `/auth/refresh` | None | `{ refreshToken }` | 200 — `{ accessToken }` | 401 (expired/invalid) |

---

## 2. Product Endpoints

| METHOD | ENDPOINT | AUTH REQUIRED | REQUEST / PARAMS | SUCCESS RESPONSE | ERROR CODES |
|--------|----------|---------------|------------------|------------------|-------------|
| GET | `/products` | None | Query: `page`, `limit`, `category`, `minPrice`, `maxPrice`, `search` | 200 — `{ data:[], total, page }` | 400 |
| GET | `/products/featured` | None | None | 200 — `{ data:[] }` | — |
| GET | `/products/:id` | None | Path: `id` | 200 — Product object with images/reviews | 404 |
| POST | `/products` | StoreManager | Multipart formData with required fields & images | 201 — `{ productId }` | 400, 409 (SKU), 403 |
| PUT | `/products/:id` | StoreManager | JSON body — subset of fields | 200 — Updated product | 400, 404, 403 |
| DELETE| `/products/:id` | StoreManager | Path: `id` | 200 — `{ message: 'Deactivated' }` | 404, 403 |

---

## 3. Order Endpoints

| METHOD | ENDPOINT | AUTH REQUIRED | REQUEST BODY / PARAMS | SUCCESS RESPONSE | ERROR CODES |
|--------|----------|---------------|-----------------------|------------------|-------------|
| POST | `/orders` | Customer | `{ deliveryAddress, paymentMethod, notes? }` | 201 — `{ orderId, total, status }` | 400 (empty cart), 409 (stock limit) |
| GET | `/orders` | Customer | Query: `page`, `limit`, `status` | 200 — `{ data:[], total }` | 401 |
| GET | `/orders/:id` | Customer/Admin| Path: `id` | 200 — Full order with items | 403, 404 |
| PATCH| `/orders/:id/status`| StoreManager | `{ status }` (valid transition only) | 200 — `{ orderId, newStatus }` | 400, 403, 404 |
| DELETE| `/orders/:id` | Customer | Path: `id` (Only if Pending) | 200 — `{ message }` | 409 (not cancellable) |

---

## 4. Cart Endpoints

| METHOD | ENDPOINT | AUTH REQUIRED | REQUEST BODY | SUCCESS RESPONSE | ERROR CODES |
|--------|----------|---------------|--------------|------------------|-------------|
| GET | `/cart` | Customer | None | 200 — `{ cartId, items[], subtotal }` | 401 |
| POST | `/cart/items` | Customer | `{ productId, quantity }` | 201 — Updated cart | 400 (stock limit), 404 |
| PUT | `/cart/items/:id` | Customer | `{ quantity }` | 200 — Updated cart | 400, 404 |
| DELETE| `/cart/items/:id` | Customer | None | 200 — Updated cart | 404 |
| DELETE| `/cart` | Customer | None | 200 — `{ message: 'Cart cleared' }` | 401 |

---

## 5. Appointment Endpoints

| METHOD | ENDPOINT | AUTH REQUIRED | REQUEST BODY / PARAMS | SUCCESS RESPONSE | ERROR CODES |
|--------|----------|---------------|-----------------------|------------------|-------------|
| GET | `/appointments/slots` | None | Query: `date` (YYYY-MM-DD) | 200 — `{ slots:[] }` | 400 |
| POST | `/appointments` | Customer | `{ appointmentDate, productInterests? }` | 201 — `{ appointmentId }` | 400 (full), 409 |
| GET | `/appointments` | Customer | Query: `page`, `status` | 200 — `{ data:[] }` | 401 |
| DELETE| `/appointments/:id`| Customer | None (Must be ≥ 24h away) | 200 — `{ message }` | 409, 403 |
| GET | `/appointments/manager/all` | StoreManager | Query: `date`, `status` | 200 — `{ data:[] }` | 403 |
| PATCH| `/appointments/:id/status` | StoreManager | `{ status, managerNotes? }` | 200 — Updated appointment | 403, 404 |

---

## 6. Review Endpoints

| METHOD | ENDPOINT | AUTH REQUIRED | REQUEST BODY | SUCCESS RESPONSE | ERROR CODES |
|--------|----------|---------------|--------------|------------------|-------------|
| GET | `/products/:id/reviews`| None | Query: `page`, `limit` | 200 — `{ data:[], avgRating }` | 404 |
| POST | `/products/:id/reviews`| Customer | `{ rating (1-5), title?, body? }` | 201 — `{ reviewId }` | 400, 409, 403 (no purchase) |
| DELETE| `/reviews/:id` | Customer/Admin| None | 200 — `{ message }` | 403, 404 |
