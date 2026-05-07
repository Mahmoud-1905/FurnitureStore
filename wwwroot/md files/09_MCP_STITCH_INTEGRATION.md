# 09 — MCP Stitch Integration
> **Destination:** Antigravity  
> **Purpose:** Bridges the Google Stitch design to Antigravity via MCP. Converts Stitch pages to Razor Views.  
> **Rule:** Replace the placeholder below with the REAL Stitch Project ID — MCP cannot connect without it.

---

## ⚠️ Action Required Before Uploading
Replace `PUT_YOUR_STITCH_PROJECT_ID_HERE` below with your actual Google Stitch Project ID.

```
STITCH_PROJECT_ID: PUT_YOUR_STITCH_PROJECT_ID_HERE
```

---

## Stitch → Razor View Page Mapping

| Stitch Page | Razor View Path | Controller Area |
|---|---|---|
| Page 1: Home | `/Views/Home/Index.cshtml` | Home |
| Page 2: Product Catalog | `/Views/Products/Index.cshtml` | Products |
| Page 3: Product Detail | `/Views/Products/Detail.cshtml` | Products |
| Page 4: Register | `/Views/Account/Register.cshtml` | Account |
| Page 5: Login | `/Views/Account/Login.cshtml` | Account |
| Page 6: Cart | `/Views/Cart/Index.cshtml` | Cart |
| Page 7: Checkout | `/Views/Orders/Checkout.cshtml` | Orders |
| Page 8: Order Confirmation | `/Views/Orders/Confirmation.cshtml` | Orders |
| Page 9: Order History | `/Views/Orders/History.cshtml` | Orders |
| Page 10: Order Detail | `/Views/Orders/Detail.cshtml` | Orders |
| Page 11: Book Appointment | `/Views/Appointments/Book.cshtml` | Appointments |
| Page 12: My Appointments | `/Views/Appointments/MyAppointments.cshtml` | Appointments |
| Page 13: Manager Dashboard | `/Views/StoreManager/Dashboard.cshtml` | StoreManager |
| Page 14: Product List | `/Views/StoreManager/Products.cshtml` | StoreManager |
| Page 15: Create/Edit Product | `/Views/StoreManager/CreateProduct.cshtml` | StoreManager |
| Page 16: Order Management | `/Views/StoreManager/Orders.cshtml` | StoreManager |
| Page 17: Appointment Calendar | `/Views/StoreManager/Appointments.cshtml` | StoreManager |
| Page 18: Inventory | `/Views/StoreManager/Inventory.cshtml` | StoreManager |
| Page 19: Admin Dashboard | `/Views/Admin/Index.cshtml` | Admin |
| Page 20: User Management | `/Views/Admin/Users.cshtml` | Admin |
| Page 21: Review Moderation | `/Views/Admin/Reviews.cshtml` | Admin |
| Page 22: Audit Log | `/Views/Admin/AuditLog.cshtml` | Admin |

---

## Frontend Conversion Rules for Antigravity

When converting Stitch designs to Razor Views, follow these rules **exactly**:

### What to Use
- ✅ HTML5 semantic elements (`<nav>`, `<main>`, `<section>`, `<article>`)
- ✅ CSS3 — custom properties (CSS variables) for the color palette
- ✅ Vanilla JavaScript — placed in `wwwroot/js/` as separate `.js` files
- ✅ Razor syntax for dynamic data (`@Model.PropertyName`, `@foreach`, `@Html.AntiForgeryToken()`)
- ✅ ASP.NET Tag Helpers (`asp-controller`, `asp-action`, `asp-for`, `asp-validation-for`)
- ✅ Shared layout: `/Views/Shared/_Layout.cshtml`
- ✅ Arabic RTL layout variant: `/Views/Shared/_LayoutArabic.cshtml`

### What NOT to Use
- ❌ React, Vue, Angular — not allowed
- ❌ jQuery — not allowed
- ❌ Bootstrap JavaScript — not allowed
- ❌ Any CDN JavaScript framework

### CSS Organization
```
wwwroot/
  css/
    site.css          ← global styles, CSS variables, typography
    components/
      nav.css
      cards.css
      buttons.css
      forms.css
      product-gallery.css
  js/
    cart.js
    product-gallery.js
    appointment-slots.js
    filters.js
```

### CSS Variables (copy these into site.css)
```css
:root {
  --color-bg:       #EDE9E2;
  --color-surface:  #F5F2ED;
  --color-dark:     #1A1A1A;
  --color-border:   #D4CFC8;
  --color-text:     #1A1A1A;
  --color-muted:    #6B6560;
  --color-gold:     #BB9A5B;
  --color-gold-dk:  #9A7D42;
  --color-success:  #4A7A52;
  --color-error:    #A0522D;
  --font-heading:   'Cormorant Garamond', serif;
  --font-body:      'DM Sans', sans-serif;
  --radius-card:    12px;
  --radius-pill:    100px;
}
```

---

## Antigravity Prompt
```
Read 09_MCP_STITCH_INTEGRATION.md.
Connect to Google Stitch via MCP using the Project ID provided.
List all pages found in the Stitch project.
Report the layout structure of each page.
Identify any gaps between Stitch pages and the required 22 Razor Views.
Do not convert any pages yet.
```
