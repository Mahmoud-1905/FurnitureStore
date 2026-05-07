# 12 — Acceptance Criteria
> **Destination:** Antigravity  
> **Purpose:** The final test checklist — every checkbox must show PASS before submission.  
> **Rule:** The project is not done when it runs. It is done when every checkbox in this file shows PASS.

---

## How to Use This File
For each checkbox below, test the exact scenario described.  
Mark each as: ✅ PASS or ❌ FAIL  
Fix ALL failures before submitting.

---

## Section 1 — Project Setup & Database

| # | Test | Expected Result |
|---|---|---|
| 1.1 | Run the application in Development mode | App starts without errors; SQLite DB created automatically |
| 1.2 | Run the application in Production mode | App uses SQL Server connection string |
| 1.3 | Check database tables | All 11 tables exist with correct columns and constraints |
| 1.4 | Check seed data | 3 roles exist: Customer, StoreManager, Admin |
| 1.5 | Check admin seed account | `admin@ruqistore.com` / `Admin@123456` logs in as Admin |
| 1.6 | Check category seed data | 4 categories exist: Living Room, Bedroom, Dining Room, Accent & Decor |
| 1.7 | UNIQUE constraint on Cart | Attempting to create 2 carts for same user → DB error |
| 1.8 | CHECK constraint on Stock | Attempting to set StockQuantity = -1 → DB error |
| 1.9 | UNIQUE constraint on Reviews | Attempting to submit 2 reviews for same product+user → error |

---

## Section 2 — Authentication

| # | Test | Expected Result |
|---|---|---|
| 2.1 | Register with valid data | Account created; role = Customer; redirected to Home |
| 2.2 | Register with duplicate email | Error: "An account with this email already exists." |
| 2.3 | Register with weak password | Inline error: password complexity requirements shown |
| 2.4 | Login with correct credentials | Auth cookie issued; redirected to correct dashboard per role |
| 2.5 | Login with wrong password | Error: "Invalid email or password." — does not reveal which field is wrong |
| 2.6 | Login with deactivated account | Error: "Your account has been deactivated." |
| 2.7 | 5 failed login attempts | Account locked for 15 minutes; lockout message shown |
| 2.8 | Customer accesses /Manager | Redirected to /Account/Login or 403 Forbidden |
| 2.9 | Customer accesses /Admin | Redirected to /Account/Login or 403 Forbidden |
| 2.10 | StoreManager accesses /Admin | 403 Forbidden |
| 2.11 | Auth cookie is HttpOnly | Inspecting browser cookies: HttpOnly=true, Secure=true, SameSite=Strict |
| 2.12 | CSRF protection | POST request without valid anti-forgery token → 400 Bad Request |

---

## Section 3 — Customer: Browse & Cart

| # | Test | Expected Result |
|---|---|---|
| 3.1 | Browse product catalog | Only IsActive=true products shown; paginated 20/page |
| 3.2 | Filter by category | Only products in selected category returned |
| 3.3 | Filter by price range | Only products within min–max range returned |
| 3.4 | Search by keyword | Products with matching name returned (case-insensitive) |
| 3.5 | Add product to cart | Item appears in cart; cart count in nav updates |
| 3.6 | Add more than stock quantity | Error: "Only {N} units available." |
| 3.7 | Update cart quantity | Subtotal and total recalculate correctly |
| 3.8 | Remove item from cart | Item removed; totals update |
| 3.9 | Cart persists across sessions | Log out, log back in → cart items still present |
| 3.10 | One cart per user | Only one Cart record exists per UserId in DB |

---

## Section 4 — Customer: Order Placement

| # | Test | Expected Result |
|---|---|---|
| 4.1 | Complete checkout with valid data | Order created; stock decremented; cart cleared; confirmation shown |
| 4.2 | Price snapshot on OrderItem | Change Product.Price after order → existing OrderItem.UnitPrice unchanged |
| 4.3 | Out-of-stock during checkout | InsufficientStockException caught; user notified before order committed |
| 4.4 | Transaction rollback on failure | Simulate DB failure mid-order → stock not decremented, cart preserved |
| 4.5 | Empty cart checkout attempt | Error: "Your cart is empty." |
| 4.6 | Order history shows all orders | All customer orders listed with correct status |
| 4.7 | Order detail data isolation | Customer A cannot view Customer B's order → 403 |
| 4.8 | Cancel Pending order | Order status = Cancelled; stock NOT restored (manual process) |
| 4.9 | Cancel non-Pending order | Error: order cannot be cancelled at this stage |

---

## Section 5 — Customer: Reviews

| # | Test | Expected Result |
|---|---|---|
| 5.1 | Submit review with Delivered order | Review saved with IsVerifiedPurchase=true; appears on product page |
| 5.2 | Submit review without purchase | Error: "You must purchase this product to leave a review." |
| 5.3 | Submit second review same product | Error: "You have already reviewed this product." |
| 5.4 | Average rating updates | Product.AverageRating recomputed after review submission |

---

## Section 6 — Customer: Showroom Appointments

| # | Test | Expected Result |
|---|---|---|
| 6.1 | Book valid slot (>24h ahead) | Appointment created with status=Pending |
| 6.2 | Book slot within 24 hours | Error: "Please book at least 24 hours in advance." |
| 6.3 | Book fully occupied slot | Error: "This time slot is fully booked." |
| 6.4 | Duplicate booking same slot | Error: "You already have a booking for this time slot." |
| 6.5 | Cancel appointment >24h before | Appointment status = Cancelled |
| 6.6 | Cancel appointment <24h before | Error: "Cancellation window has expired." |
| 6.7 | Cancel another customer's appointment | 403 Forbidden |

---

## Section 7 — Store Manager

| # | Test | Expected Result |
|---|---|---|
| 7.1 | Create product with 4 images | Product in catalog; 4 images displayed in gallery |
| 7.2 | Create product with duplicate SKU | Error: "A product with this SKU already exists." |
| 7.3 | Upload non-image file | Error: "Only JPEG, PNG, and WebP images are accepted." |
          | 7.4 | Upload image >5MB | Error: "Each image must be under 5MB." |
| 7.5 | Upload disguised file (renamed .exe to .jpg) | Error: magic byte check fails |
| 7.6 | Soft-delete product | Product IsActive=false; disappears from catalog; order history intact |
| 7.7 | Update stock quantity | StockQuantity updated; reflected in product detail |
| 7.8 | Set stock to -1 | Error: "Stock quantity cannot be negative." |
| 7.9 | Update order status (valid transition) | Status updated; customer tracking view reflects change |
| 7.10 | Update order status (invalid transition) | Error: "Invalid status transition." |
| 7.11 | Confirm appointment | Appointment status = Confirmed; appears in calendar |
| 7.12 | Reject appointment | Appointment status = Cancelled; customer sees update |
| 7.13 | Sales dashboard shows correct revenue | Dashboard figures match Order totals in DB |

---

## Section 8 — Administrator

| # | Test | Expected Result |
|---|---|---|
| 8.1 | View all users list | All registered users displayed; search by name/email works |
| 8.2 | Deactivate user account | User.IsActive = false; deactivated user cannot log in |
| 8.3 | Reactivate user account | User.IsActive = true; user can log in again |
| 8.4 | Admin deactivates own account | Error: "You cannot deactivate your own account." |
| 8.5 | Assign StoreManager role | User gets StoreManager role; AuditLog entry created |
| 8.6 | Revoke StoreManager role | Role removed; AuditLog entry created |
| 8.7 | Delete review | Review.IsVisible=false (or deleted); AuditLog entry created |
| 8.8 | AuditLog is append-only | No delete or edit button exists in the UI |
| 8.9 | AuditLog entries correct | Every admin action has a matching AuditLog record |
| 8.10 | Export CSV report | CSV downloads correctly; figures match DB records |

---

## Section 9 — UI & Frontend

| # | Test | Expected Result |
|---|---|---|
| 9.1 | Mobile at 375px viewport | All pages usable; no horizontal scroll; buttons tappable |
| 9.2 | Desktop at 1440px viewport | All pages render correctly; product grid 3-4 columns |
| 9.3 | Arabic language toggle | RTL layout activates; all navigation and content in Arabic |
| 9.4 | English language toggle | LTR layout; English content displayed |
| 9.5 | Form validation errors inline | Error message appears adjacent to the offending field |
| 9.6 | Submit button disabled after click | Prevents duplicate order/booking submissions |
| 9.7 | Product image gallery zoom | Zoom works on product detail page |
| 9.8 | No frontend frameworks present | Inspect browser Network tab — no React/Vue/jQuery loaded |
| 9.9 | UI matches Google Stitch design | Color palette, fonts, and layout match the Stitch pages |

---

## Section 10 — Security

| # | Test | Expected Result |
|---|---|---|
| 10.1 | No plaintext password in DB | Check AspNetUsers.PasswordHash — never plain text |
| 10.2 | No sensitive data in error logs | Force a 500 error — stack trace not shown to user |
| 10.3 | SQL injection test | Enter `'; DROP TABLE Products; --` in search → no DB damage |
| 10.4 | HTTPS redirect | Navigate to `http://` → automatically redirected to `https://` |
| 10.5 | File upload path traversal | Upload file named `../../web.config` → rejected or safe filename assigned |

---

## Final PASS/FAIL Summary Template

After testing, complete this summary:

```
Section 1 — Setup & Database:    [ ] PASS  [ ] FAIL  (__ / 9 passed)
Section 2 — Authentication:      [ ] PASS  [ ] FAIL  (__ / 12 passed)
Section 3 — Browse & Cart:       [ ] PASS  [ ] FAIL  (__ / 10 passed)
Section 4 — Order Placement:     [ ] PASS  [ ] FAIL  (__ / 9 passed)
Section 5 — Reviews:             [ ] PASS  [ ] FAIL  (__ / 4 passed)
Section 6 — Appointments:        [ ] PASS  [ ] FAIL  (__ / 7 passed)
Section 7 — Store Manager:       [ ] PASS  [ ] FAIL  (__ / 13 passed)
Section 8 — Administrator:       [ ] PASS  [ ] FAIL  (__ / 10 passed)
Section 9 — UI & Frontend:       [ ] PASS  [ ] FAIL  (__ / 9 passed)
Section 10 — Security:           [ ] PASS  [ ] FAIL  (__ / 5 passed)

TOTAL: __ / 88 PASSED

STATUS: [ ] READY FOR SUBMISSION  [ ] NEEDS FIXES
```

---

## Antigravity Prompt
```
Read 12_ACCEPTANCE_CRITERIA.md.
Test EVERY checkbox in order.
Mark each as PASS or FAIL.
Fix all failures immediately before moving to the next section.
Show the Final PASS/FAIL Summary at the end.
The project is complete ONLY when all 88 checkboxes show PASS.
```
