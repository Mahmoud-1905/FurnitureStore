# 08 — Google Stitch UI Design Prompt
> **Destination:** ⭐ GOOGLE STITCH ONLY — do NOT upload this file to Antigravity  
> **Purpose:** Generates all 22 UI pages as high-fidelity designs inside Google Stitch.  
> **Rule:** File 08 has ONE destination: Google Stitch. If you send it to Antigravity, nothing connects.

---

## ⚠️ How to Use This File
1. Open [Google Stitch](https://stitch.withgoogle.com)
2. Create a new project
3. Paste the entire prompt below into Stitch
4. Let Stitch generate all 22 pages
5. **Copy the Project ID** from Stitch
6. Paste the Project ID into `09_MCP_STITCH_INTEGRATION.md`

---

## Google Stitch Design Prompt

```
Design a complete UI for a modern luxury furniture e-commerce store called "Ruqi Store".

BRAND IDENTITY:
- Logo mark: an elegant "R" fused with a chair silhouette
- Tagline: "Modern Living, Timeless Design."
- The brand palette is derived from the official logo:
  - Black #1A1A1A (dominant — backgrounds, text, buttons)
  - Warm Gold #BB9A5B (accent — CTAs, highlights, icons)
  - Cream #EDE9E2 (light surfaces and cards)

DESIGN SYSTEM:
- Style: Modern luxury — bold editorial with warm gold accents, inspired by high-end furniture showrooms
- Background (light pages): Cream #EDE9E2
- Surface / Cards: #F5F2ED
- Dark Background (hero, footer): Deep Black #1A1A1A
- Border: #D4CFC8
- Primary Text: #1A1A1A
- Secondary Text: #6B6560
- Accent / CTA Buttons: Warm Gold #BB9A5B (hover: #9A7D42)
- Success: #4A7A52
- Error: #A0522D
- Heading Font: Cormorant Garamond (serif) — large, editorial
- Body Font: DM Sans — clean, readable
- Corner Radius: 12px for cards; 100px for pill buttons
- NO pure white backgrounds; NO bright primary colors; NO dark mode for regular pages
- Product photography must dominate — UI is a frame, not the focus
- Hero sections use dark (#1A1A1A) background with cream text and gold accents

NAVIGATION (sticky header):
- Background: #1A1A1A (deep black)
- Left: back arrow or hamburger (cream colored)
- Center: store name "RUQI" + subtitle "FURNITURE" in DM Sans caps, letter-spacing 0.2em, cream color
- Right: language toggle (AR / EN) + cart icon with gold item count badge

FOOTER:
- Background: #1A1A1A (deep black)
- Text: cream #EDE9E2
- Accent links and icons: gold #BB9A5B
- Store name, showroom address, WhatsApp link, opening hours, social icons

DESIGN ALL 22 PAGES:

--- PUBLIC PAGES (5) ---

PAGE 1: Home / Landing (/)
- Hero section: full-width lifestyle furniture photo, headline in Cormorant Garamond serif
- "SHOP COLLECTION" pill button in Gold
- Category grid: 4 cards (Living Room, Bedroom, Dining Room, Accent & Decor) with furniture photography
- Featured products: horizontal scroll row of product cards
- Showroom section: "Visit Our Showroom" with booking CTA

PAGE 2: Product Catalog (/Products)
- Left sidebar: filters (Category checkboxes, Price range slider, Material checkboxes, In Stock toggle)
- Right: product grid (2 columns mobile, 3-4 desktop), 20 per page
- Each product card: primary image, category label, product name, price, star rating
- Sort dropdown: Newest / Price Low-High / Price High-Low / Top Rated
- Search bar at top

PAGE 3: Product Detail (/Products/Detail/{id})
- Image gallery: main large image + 4 thumbnail row, zoom on hover
- Floating material tags on image (pill badges: "VELVET FABRIC", "SOLID OAK")
- Collection label (small caps), product name in serif 36px
- Color swatches row + count
- Star rating + review count
- Description paragraph
- Specs grid: 4 cards (Material, Dimensions, Weight, Lead Time)
- Quantity selector + "IN STOCK" badge
- "ADD TO CART" gold button + wishlist heart button
- Sticky bottom bar: price + "ADD TO CART" + wishlist
- Accordions: Shipping & Returns, Sustainability, Care Instructions
- Reviews section: list of review cards with stars, name, date, text
- "Book Showroom Visit" secondary CTA

PAGE 4: Register (/Account/Register)
- Centered card on cream background
- Fields: Full Name, Email, Password, Confirm Password
- "CREATE ACCOUNT" gold button
- "Already have an account? Login" link

PAGE 5: Login (/Account/Login)
- Centered card
- Fields: Email, Password, Remember Me checkbox
- "LOGIN" gold button
- "Create an account" + "Forgot Password?" links

--- CUSTOMER PAGES (7) ---

PAGE 6: Shopping Cart (/Cart)
- List of cart items: image, name, material, quantity stepper, unit price, subtotal, remove button
- Order summary card: subtotal, shipping estimate, tax, TOTAL
- "PROCEED TO CHECKOUT" gold button
- "CONTINUE SHOPPING" text link

PAGE 7: Checkout (/Orders/Checkout)
- Step indicator: Cart → Address → Summary → Confirm (3 steps)
- Step 1: Select saved address or enter new (Name, Address Line 1, City, Phone)
- Step 2: Order summary (items + totals + selected address)
- Step 3: Payment method (Cash on Delivery / Bank Transfer radio)
- "CONFIRM ORDER" gold button

PAGE 8: Order Confirmation (/Orders/Confirmation/{id})
- Large checkmark icon in gold
- "Order Confirmed!" heading in serif
- Order ID, items summary, total, delivery address
- "Track Your Order" button
- WhatsApp contact link

PAGE 9: Order History (/Orders/History)
- Table / card list: Order ID, Date, Status badge (color-coded), Total
- Click row → Order Detail

PAGE 10: Order Detail (/Orders/Detail/{id})
- Status timeline: Pending → Processing → Shipped → Delivered (step indicators)
- Items table with images, names, quantities, prices
- Delivery address section
- Total breakdown

PAGE 11: Book Appointment (/Appointments/Book)
- Calendar date picker (warm cream style)
- Time slot grid: morning/afternoon slots (available gold / full gray)
- "Products I want to see" text area
- "BOOK VISIT" gold button
- Confirmation card on success

PAGE 12: My Appointments (/Appointments/MyAppointments)
- List of appointments: date, time, status badge, products of interest, Cancel button
- Status badges: Pending (amber), Confirmed (gold), Cancelled (muted red)

--- STORE MANAGER PAGES (6) ---

PAGE 13: Manager Dashboard (/Manager/Dashboard)
- 4 metric cards: Today's Revenue, Orders This Month, Low Stock Items, Today's Appointments
- Recent Orders table (5 rows)
- Low Stock Alert list
- Today's Appointments list

PAGE 14: Product List (/Manager/Products)
- Table: SKU, Image thumbnail, Name, Category, Price, Stock, Status, Actions
- Bulk action: soft-delete selected
- "ADD NEW PRODUCT" gold button

PAGE 15: Create / Edit Product (/Manager/Products/Create)
- Two-column layout: form fields left, image upload right
- Fields: Name, Category dropdown, Description, Price, SKU, Stock, Material, Width, Depth, Height, Weight
- Image upload zone: drag-and-drop, 8 slots, preview thumbnails, remove button per image
- "SAVE PRODUCT" button

PAGE 16: Order Management (/Manager/Orders)
- Table: Order ID, Customer Name, Date, Items Count, Total, Status dropdown, Update button
- Filter bar: All / Pending / Processing / Shipped / Delivered / Cancelled

PAGE 17: Appointment Calendar (/Manager/Appointments)
- Split view: left = pending requests list (Confirm / Reject buttons), right = calendar of confirmed
- Each appointment card: customer name, date/time, products of interest

PAGE 18: Inventory Management (/Manager/Inventory)
- Table: Product name, SKU, Current Stock, input field for update, Save button
- Low stock highlighted in warm amber

--- ADMIN PAGES (4) ---

PAGE 19: Admin Dashboard (/Admin)
- 4 metric cards: Total Users, Total Orders, Total Revenue, Pending Reviews
- Recent audit log entries list

PAGE 20: User Management (/Admin/Users)
- Table: Name, Email, Role badge, Status badge, Registered date, Actions
- Actions: Activate / Deactivate toggle, Assign / Revoke StoreManager role
- Search bar + Role filter dropdown

PAGE 21: Review Moderation (/Admin/Reviews)
- Table: Product name, Customer, Rating stars, Comment preview, Visible toggle, Delete button
- Deleted reviews show confirmation modal first

PAGE 22: Audit Log (/Admin/AuditLog)
- Read-only table: Timestamp, Admin name, Action type badge, Target entity, Description
- Filter by Action Type dropdown
- No delete or edit functionality

RESPONSIVE REQUIREMENTS:
- All pages must work correctly on mobile (375px) and desktop (1440px)
- Navigation collapses to hamburger on mobile
- Product grid: 2 columns on mobile, 3-4 on desktop
- Filter sidebar: hidden on mobile behind a "FILTERS" toggle button

IMPORTANT:
- Design UI ONLY — no backend, no database, no code
- Use the exact color palette and fonts specified
- Every page must feel premium and warm — like a luxury showroom, not a tech product
- After generating all 22 pages, provide the STITCH PROJECT ID
```

---

## After Stitch Completes
1. ✅ Verify all 22 pages were generated
2. ✅ Copy the **Stitch Project ID**
3. ✅ Open `09_MCP_STITCH_INTEGRATION.md`
4. ✅ Replace `PUT_YOUR_STITCH_PROJECT_ID_HERE` with the real ID
5. ✅ Upload File 09 to Antigravity
