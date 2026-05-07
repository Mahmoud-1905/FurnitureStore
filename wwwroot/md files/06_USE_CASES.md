# 06 — Use Cases
> **Destination:** Antigravity  
> **Purpose:** 12 complete user-system interactions with main flows, alternative scenarios, and controller mappings.  
> **Rule:** Alternative Scenarios become your error handling. Do not skip them.

---

## UC-01 — Customer Registration
| Field | Detail |
|---|---|
| Actor | Guest |
| Controller | `AccountController` |
| Action | `Register` |
| HTTP | GET (form) + POST (submit) |
| View | `/Views/Account/Register.cshtml` |

**Main Flow:**
1. Guest navigates to `/Account/Register`
2. Fills in: FullName, Email, Password, Confirm Password
3. POST submits form (CSRF token included)
4. System validates ModelState
5. UserManager creates account with role = Customer
6. System redirects to `/Home/Index`

**Alternative Scenarios:**
- Email already registered → ModelState error: "An account with this email already exists."
- Password does not meet complexity rules → inline validation error per field
- Passwords do not match → inline error on Confirm Password field

---

## UC-02 — Customer Login
| Field | Detail |
|---|---|
| Actor | Guest |
| Controller | `AccountController` |
| Action | `Login` |
| HTTP | GET (form) + POST (submit) |
| View | `/Views/Account/Login.cshtml` |

**Main Flow:**
1. Guest submits email + password
2. SignInManager.PasswordSignInAsync() validates credentials
3. HttpOnly auth cookie issued with role claim
4. Redirect to role-based dashboard (Customer → Home, StoreManager → /Manager, Admin → /Admin)

**Alternative Scenarios:**
- Wrong password → error: "Invalid email or password." (do not reveal which is wrong)
- Account deactivated (IsActive=false) → error: "Your account has been deactivated."
- 5 failed attempts → 15-minute lockout message shown

---

## UC-03 — Browse & Filter Furniture Catalog
| Field | Detail |
|---|---|
| Actor | Customer / Guest |
| Controller | `ProductsController` |
| Action | `Index` |
| HTTP | GET |
| View | `/Views/Products/Index.cshtml` |

**Main Flow:**
1. User navigates to `/Products`
2. System returns IsActive=true products, paginated 20/page, newest first
3. User applies filters: category, price range, material, in-stock only
4. Search by keyword (name LIKE %keyword%)
5. Results update; URL reflects filter state (query string)

**Alternative Scenarios:**
- No products match filters → display "No furniture found matching your filters."
- Search keyword returns zero results → display "No results for '{keyword}'"

---

## UC-04 — View Product Detail
| Field | Detail |
|---|---|
| Actor | Customer / Guest |
| Controller | `ProductsController` |
| Action | `Detail` |
| HTTP | GET |
| View | `/Views/Products/Detail.cshtml` |

**Main Flow:**
1. User clicks product from catalog
2. System loads: image gallery (up to 8), name, description, dimensions, material, price, stock status, average rating, all visible reviews
3. "Add to Cart" button shown for authenticated customers
4. "Book Showroom Visit" CTA links to `/Appointments/Book?productId={id}`

**Alternative Scenarios:**
- Product IsActive=false → 404 Not Found page
- No images uploaded → placeholder image shown

---

## UC-05 — Add to Cart & Checkout
| Field | Detail |
|---|---|
| Actor | Customer (authenticated) |
| Controller | `CartController`, `OrdersController` |
| Actions | `AddToCart`, `Index`, `Checkout`, `PlaceOrder` |
| HTTP | POST (add) + GET (view) + POST (place) |
| Views | `/Views/Cart/Index.cshtml`, `/Views/Orders/Checkout.cshtml`, `/Views/Orders/Confirmation.cshtml` |

**Main Flow:**
1. Customer clicks "Add to Cart" on product detail
2. CartService validates quantity ≤ StockQuantity
3. Item added to DB cart; cart icon updates count
4. Customer proceeds to `/Cart`
5. Reviews cart items, quantities, subtotals, estimated total
6. Proceeds to checkout → selects/enters delivery address
7. Views final order summary with TotalAmount
8. Confirms order → OrderService.PlaceOrderAsync() runs atomically
9. Order confirmation page shown with OrderId

**Alternative Scenarios:**
- Quantity exceeds stock → inline error: "Only {N} units available."
- Stock runs out between add and checkout → InsufficientStockException → user notified before payment
- Customer not logged in → redirect to Login, return to Cart after auth

---

## UC-06 — Track Order Status
| Field | Detail |
|---|---|
| Actor | Customer |
| Controller | `OrdersController` |
| Action | `Detail` |
| HTTP | GET |
| View | `/Views/Orders/Detail.cshtml` |

**Main Flow:**
1. Customer navigates to `/Orders`
2. Order history displayed: OrderId, date, status, total
3. Customer clicks order → detail view with items, prices, delivery address, status timeline
4. Status updates reflected on page refresh

**Alternative Scenarios:**
- Customer tries to access another customer's order → 403 Forbidden (data isolation)
- Order not found → 404 Not Found

---

## UC-07 — Submit Product Review
| Field | Detail |
|---|---|
| Actor | Customer |
| Controller | `ProductsController` |
| Action | `SubmitReview` |
| HTTP | POST |
| View | Partial in `/Views/Products/Detail.cshtml` |

**Main Flow:**
1. Customer views product detail for a Delivered order item
2. "Write a Review" form shown (star rating 1–5 + optional comment)
3. ReviewService validates: Delivered order exists for this product + no existing review
4. Review saved with IsVerifiedPurchase = true
5. Product average rating recomputed
6. Review appears on product page

**Alternative Scenarios:**
- No Delivered order → form hidden; message: "Purchase this product to leave a review."
- Review already submitted → message: "You have already reviewed this product."

---

## UC-08 — Book Showroom Appointment
| Field | Detail |
|---|---|
| Actor | Customer |
| Controller | `AppointmentsController` |
| Action | `Book` |
| HTTP | GET (form) + POST (submit) |
| View | `/Views/Appointments/Book.cshtml` |

**Main Flow:**
1. Customer navigates to `/Appointments/Book`
2. System displays available time slots for selected date
3. Customer selects date, time slot, enters product interests (optional)
4. AppointmentService validates: date ≥ +24h, slot capacity not full, no duplicate booking
5. Appointment created with status = Pending
6. Confirmation shown; appears in `/Appointments/MyAppointments`

**Alternative Scenarios:**
- Slot full → "This time slot is fully booked. Please choose another."
- Date less than 24h away → "Please book at least 24 hours in advance."
- Duplicate booking → "You already have a booking for this time slot."

---

## UC-09 — Store Manager Adds Product
| Field | Detail |
|---|---|
| Actor | Store Manager |
| Controller | `StoreManagerController` |
| Action | `CreateProduct` |
| HTTP | GET (form) + POST (submit, multipart) |
| View | `/Views/StoreManager/CreateProduct.cshtml` |

**Main Flow:**
1. Manager navigates to `/Manager/Products/Create`
2. Fills form: name, category, description, price, dimensions, weight, material, SKU, stock, uploads up to 8 images
3. POST submits (multipart/form-data)
4. ProductService validates: unique SKU, valid category, price > 0
5. FileUploadService validates each image: extension, magic bytes, size ≤ 5MB
6. Product created; images saved to `/Uploads/Products/{ProductId}/`
7. Product appears in catalog (IsActive=true by default)

**Alternative Scenarios:**
- Duplicate SKU → ModelState error: "A product with this SKU already exists."
- Invalid image type → error: "Only JPEG, PNG, and WebP images are accepted."
- Image > 5MB → error: "Each image must be under 5MB."
- More than 8 images → error: "Maximum 8 images allowed per product."

---

## UC-10 — Admin Deactivates User
| Field | Detail |
|---|---|
| Actor | Admin |
| Controller | `AdminController` |
| Action | `DeactivateUser` |
| HTTP | POST |
| View | `/Views/Admin/Users.cshtml` |

**Main Flow:**
1. Admin navigates to `/Admin/Users`
2. Finds user by search (name/email)
3. Clicks "Deactivate" → confirmation dialog
4. AdminService sets user.IsActive = false
5. AuditLog entry created: ActionType = "USER_DEACTIVATE"
6. Deactivated user blocked on next login attempt

**Alternative Scenarios:**
- Admin tries to deactivate own account → error: "You cannot deactivate your own account."
- User already deactivated → "Reactivate" button shown instead

---

## UC-11 — Store Manager Processes Order
| Field | Detail |
|---|---|
| Actor | Store Manager |
| Controller | `StoreManagerController` |
| Action | `UpdateOrderStatus` |
| HTTP | POST |
| View | `/Views/StoreManager/Orders.cshtml` |

**Main Flow:**
1. Manager views all orders filtered by status
2. Selects an order → updates status (e.g., Pending → Processing → Shipped)
3. OrderService validates transition is a valid next step
4. Status updated; customer's tracking view reflects change on next refresh

**Alternative Scenarios:**
- Invalid status transition (e.g., Delivered → Processing) → error: "Invalid status transition."
- Order already Cancelled → transition blocked

---

## UC-12 — Admin Assigns StoreManager Role
| Field | Detail |
|---|---|
| Actor | Admin |
| Controller | `AdminController` |
| Action | `AssignRole` |
| HTTP | POST |
| View | `/Views/Admin/Users.cshtml` |

**Main Flow:**
1. Admin finds a Customer account
2. Clicks "Assign StoreManager Role"
3. UserManager.AddToRoleAsync(user, "StoreManager")
4. AuditLog entry: ActionType = "ROLE_ASSIGN"
5. User's next login shows Store Manager dashboard

**Alternative Scenarios:**
- User already has StoreManager role → button hidden; message: "Already a Store Manager."

---

## Controller → Action → View Mapping
| Controller | Action | Method | View File |
|---|---|---|---|
| HomeController | Index | GET | `/Views/Home/Index.cshtml` |
| AccountController | Register | GET+POST | `/Views/Account/Register.cshtml` |
| AccountController | Login | GET+POST | `/Views/Account/Login.cshtml` |
| AccountController | Profile | GET+POST | `/Views/Account/Profile.cshtml` |
| ProductsController | Index | GET | `/Views/Products/Index.cshtml` |
| ProductsController | Detail | GET | `/Views/Products/Detail.cshtml` |
| CartController | Index | GET | `/Views/Cart/Index.cshtml` |
| CartController | AddToCart | POST | Redirect |
| OrdersController | Checkout | GET+POST | `/Views/Orders/Checkout.cshtml` |
| OrdersController | Confirmation | GET | `/Views/Orders/Confirmation.cshtml` |
| OrdersController | History | GET | `/Views/Orders/History.cshtml` |
| OrdersController | Detail | GET | `/Views/Orders/Detail.cshtml` |
| AppointmentsController | Book | GET+POST | `/Views/Appointments/Book.cshtml` |
| AppointmentsController | MyAppointments | GET | `/Views/Appointments/MyAppointments.cshtml` |
| StoreManagerController | Products | GET | `/Views/StoreManager/Products.cshtml` |
| StoreManagerController | CreateProduct | GET+POST | `/Views/StoreManager/CreateProduct.cshtml` |
| StoreManagerController | Orders | GET | `/Views/StoreManager/Orders.cshtml` |
| StoreManagerController | Appointments | GET+POST | `/Views/StoreManager/Appointments.cshtml` |
| StoreManagerController | Dashboard | GET | `/Views/StoreManager/Dashboard.cshtml` |
| AdminController | Users | GET+POST | `/Views/Admin/Users.cshtml` |
| AdminController | Reviews | GET+POST | `/Views/Admin/Reviews.cshtml` |
| AdminController | AuditLog | GET | `/Views/Admin/AuditLog.cshtml` |
| AdminController | Reports | GET | `/Views/Admin/Reports.cshtml` |

---

## Antigravity Prompt
```
Read 06_USE_CASES.md.
Map each use case to its controller name, action name, HTTP method, and view file.
Confirm all alternative scenarios are covered as error handling in the Service Layer.
Do not write code yet.
```
