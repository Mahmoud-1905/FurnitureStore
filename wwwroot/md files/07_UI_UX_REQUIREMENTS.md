# 07 — UI/UX Requirements
> **Destination:** Antigravity  
> **Purpose:** Complete page inventory organized by role — define all pages BEFORE going to Google Stitch.  
> **Rule:** What is not written here will be forgotten in both the design and the implementation.

---

## Design Language
| Property | Value |
|---|---|
| Style | Modern luxury — bold black, warm gold, soft cream |
| Background | Cream `#EDE9E2` |
| Surface | `#F5F2ED` |
| Primary / Dark | Deep Black `#1A1A1A` |
| Accent / Gold | Warm Gold `#BB9A5B` |
| Accent Hover | Dark Gold `#9A7D42` |
| Text | Deep Black `#1A1A1A` |
| Secondary Text | `#6B6560` |
| Success | `#4A7A52` |
| Error | `#A0522D` |
| Typography | Cormorant Garamond (headings) + DM Sans (body) |
| Corner Radius | 12px cards; 100px pill buttons |
| Tone | Modern, premium, trustworthy — bold and editorial |
| Brand Palette Source | Logo (black `#1A1A1A` + gold `#BB9A5B` + cream `#EDE9E2`) |

---

## Frontend Rules (MANDATORY)
- ✅ HTML5 + CSS3 + Vanilla JavaScript ONLY
- ✅ Razor Views (.cshtml) for server rendering
- ✅ CSS stored in `wwwroot/css/`
- ✅ JS stored in `wwwroot/js/`
- ❌ NO React, Vue, Angular
- ❌ NO jQuery
- ❌ NO Bootstrap JavaScript (CSS utility classes only if needed)
- ❌ NO external JS frameworks

---

## Page Inventory (22 Pages Total)

### Public Pages (5) — No authentication required
| # | Page Name | Route | Description |
|---|---|---|---|
| P1 | Home / Landing | `/` | Featured products, category grid, showroom CTA, hero section |
| P2 | Product Catalog | `/Products` | Paginated list with search, filter sidebar (category, price, material, stock) |
| P3 | Product Detail | `/Products/Detail/{id}` | Image gallery (zoom), specs, reviews, Add to Cart, Book Showroom CTA |
| P4 | Register | `/Account/Register` | Full Name, Email, Password, Confirm Password |
| P5 | Login | `/Account/Login` | Email, Password, Remember Me, Forgot Password link |

### Customer Pages (7) — Role: Customer
| # | Page Name | Route | Description |
|---|---|---|---|
| C1 | Shopping Cart | `/Cart` | Item list, quantities, subtotals, estimated total, Proceed to Checkout |
| C2 | Checkout | `/Orders/Checkout` | Multi-step: Address → Summary → Confirm |
| C3 | Order Confirmation | `/Orders/Confirmation/{id}` | Order number, items, total, WhatsApp link |
| C4 | Order History | `/Orders/History` | Paginated orders list: ID, date, status, total |
| C5 | Order Detail | `/Orders/Detail/{id}` | Full order with items, timeline, delivery address |
| C6 | Book Appointment | `/Appointments/Book` | Date picker, time slot selector, product interests field |
| C7 | My Appointments | `/Appointments/MyAppointments` | List of bookings with status and cancel option |

### Store Manager Pages (6) — Role: StoreManager
| # | Page Name | Route | Description |
|---|---|---|---|
| M1 | Manager Dashboard | `/Manager/Dashboard` | Revenue summary, recent orders, low-stock alerts, today's appointments |
| M2 | Product List | `/Manager/Products` | All products table with edit/soft-delete/image management |
| M3 | Create / Edit Product | `/Manager/Products/Create` | Full product form with multi-image upload (up to 8) |
| M4 | Order Management | `/Manager/Orders` | All orders table; status filter; update status dropdown |
| M5 | Appointment Calendar | `/Manager/Appointments` | Pending requests list + confirmed calendar view |
| M6 | Inventory Management | `/Manager/Inventory` | Stock quantity editor per product; low-stock indicators |

### Admin Pages (4) — Role: Admin
| # | Page Name | Route | Description |
|---|---|---|---|
| A1 | Admin Dashboard | `/Admin` | User count, order count, revenue, recent audit events |
| A2 | User Management | `/Admin/Users` | Search/filter users; activate/deactivate; assign/revoke StoreManager role |
| A3 | Review Moderation | `/Admin/Reviews` | All reviews with delete option; deletion logged to AuditLog |
| A4 | Audit Log | `/Admin/AuditLog` | Read-only log of all admin actions |

---

## Shared UX Rules
- All form validation errors displayed **inline** adjacent to the offending field
- All async operations show a **loading indicator**; submit buttons disabled after first click
- All pages **responsive** from 320px (mobile) to 2560px (4K)
- **Arabic RTL** + **English LTR** toggle; language stored in cookie
- Navigation: sticky header with cart icon (item count badge), language toggle, role-aware menu
- Footer: store name, WhatsApp link, showroom address, social icons

---

## Antigravity Prompt
```
Read 07_UI_UX_REQUIREMENTS.md.
List all 22 required Razor View files (.cshtml) grouped by controller folder area.
Confirm the design language tokens (colors, fonts, corner radius).
Do not create any files yet.
```
