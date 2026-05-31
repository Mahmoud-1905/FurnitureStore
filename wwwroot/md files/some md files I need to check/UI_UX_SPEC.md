# 10 — UI & UX Specification
## Ruqi Store — Design Rules & Accessibility

---

## AI Instructions
> The presentation layer is built using ASP.NET Core MVC (Razor Views), Bootstrap 5, and vanilla JavaScript. A core requirement of this project is full bilingual support (Arabic and English) with perfect RTL (Right-to-Left) rendering for the Arabic interface.

---

## 1. Bilingual & RTL Layout Rules (FR-C16, NFR-U3)

### Core Requirement
The application must support Arabic (primary) and English (secondary). The UI strings must not be hardcoded in the views.

### Implementation Guidelines
- **HTML Tag:** For Arabic, the root tag must be `<html lang="ar" dir="rtl">`.
- **CSS Framework:** Use the RTL version of Bootstrap 5 (`bootstrap.rtl.min.css`) when the language is set to Arabic.
- **Alignment:** Default text alignment must be `text-align: right` for Arabic. Use logical CSS properties (e.g., `margin-inline-start`, `padding-inline-end`) instead of absolute properties (`margin-left`, `padding-right`) so they flip automatically based on the `dir` attribute.
- **Language Toggle:** Provide a persistent language toggle in the main navigation. The selected language should be stored in a cookie (`.AspNetCore.Culture`) so it persists across sessions.

---

## 2. Responsive Design (NFR-U1)

### Viewports
The interface must be usable on all modern devices:
- **Mobile (320px - 767px):** Single-column layouts. Navigation collapses into a hamburger menu. Data tables must scroll horizontally or stack vertically.
- **Tablet (768px - 1023px):** Two-column grids for products.
- **Desktop (1024px+):** Three or four-column grids. Sidebar layouts for admin/manager dashboards.

---

## 3. Feedback & Error Messaging (NFR-U4, NFR-U5)

### Validation
- **Client-Side:** Use jQuery Validation Unobtrusive (standard with ASP.NET Core MVC) to validate forms before submission.
- **Server-Side:** If the Node.js API returns a `400 Validation Error`, the MVC controller must map those errors back to the `ModelState` and display them adjacent to the offending form fields.
- **No Raw Exceptions:** Users must NEVER see stack traces or raw database error messages.

### Loading States
- When a user submits a form (e.g., Checkout, Login), the submit button must be disabled and display a loading spinner to prevent duplicate submissions.
- Asynchronous actions (like adding to cart via AJAX) must show a brief loading indicator and a success "Toast" notification.

---

## 4. Accessibility (NFR-U2)

### WCAG 2.1 AA Compliance
- **Color Contrast:** Text and interactive elements must have sufficient color contrast against their backgrounds.
- **Keyboard Navigation:** All links, buttons, and form fields must be reachable via the `Tab` key. Focus states must be clearly visible (do not remove `outline: none` without providing an alternative focus style).
- **ARIA Labels:** Image galleries, modal dialogs, and icon-only buttons must have appropriate `aria-label` or `alt` attributes for screen readers.

---

## 5. Visual Aesthetics (Furniture Domain)

- **Imagery Focus:** The UI should be clean and minimalist to let the high-quality furniture images stand out.
- **Whitespace:** Use generous padding and margins to create a premium, uncrowded shopping experience.
- **Color Palette:** Neutral backgrounds (whites, off-whites, soft grays) with a sophisticated primary accent color (e.g., deep navy, emerald green, or muted gold) that conveys trust and quality.

## 6. Page-Specific UI Specifications (Based on Provided Video Demos)

### 1. Home Page
- **Header:** Contains the logo ("Ruqi"), navigation links (Home, Products, Collections, About Us), and standard icons (Cart, Search, User profile or Sign In).
- **Hero Section:** Features a large, high-quality image of an interior space with a bold, elegant typography overlay (e.g., "Refined Living."). Includes a clear Call to Action (CTA) button (e.g., "Explore Collection").
- **Featured Categories/Products:** A grid layout showcasing top categories or selected items. Images should be large and clear with minimal text overlay, using hover effects to reveal more details.

### 2. Products Page (المنتجات.mp4)
- **Header/Title:** Clean heading (e.g., "Refined Living.") with a subtle subtitle. A row of category filters or sorting options (e.g., All, Chairs, Tables, Sofas, Lighting) sits just below the title.
- **Product Grid:** A clean grid layout displaying products. Each product card features:
  - A large product image with a subtle hover effect.
  - Product name in an elegant font.
  - Subtitle or short description.
  - Price aligned to the right or below the title.
- **Visual Style:** Emphasis on clean lines, ample whitespace between grid items, and a focus on product imagery.

### 3. Collection Page / Cart (collection.mp4)
- **Title:** "Your Curated Collection" indicating a personalized or selected group of items (functioning as a cart or wishlist).
- **List Layout:** Items are displayed in a list format, each occupying a full-width row or card.
- **Item Details:**
  - Product thumbnail image on the left.
  - Product name and material/finish (e.g., "Natural Oak Finish").
  - Quantity selector (e.g., `- 01 +`) and a remove icon (X).
- **Order Summary Sidebar/Section:** A contrasting dark panel displaying the subtotal, estimated tax, total amount, and a prominent "PROCEED TO CHECKOUT" button.
- **Additional Options:** "Continue Shopping" link and "Architectural Warranty" information.

### 4. About Us Page (من نحن.mp4)
- **Hero Section:** A full-width, high-quality image of a beautifully designed room or furniture piece.
- **Brand Story:** A clean, centered text section outlining the brand's philosophy, commitment to craftsmanship, and design ethos.
- **Features/Values:** A three-column section highlighting key brand values (e.g., Sustainable Sourcing, Master Craftsmanship, Timeless Design) with a small icon and descriptive text.
- **Visuals:** An image grid or gallery showcasing the manufacturing process, materials (e.g., wood textures, fabrics), and the final product, emphasizing quality.

### 5. Connect Us Page (تواصل معنا.mp4)
- **Header:** "CONTACT - Let's Design Your Space".
- **Two-Column Layout:**
  - **Left Column (Visit Our Studio):** An image of the studio or showroom, followed by the physical address, operating hours (e.g., Mon-Fri 10:00 - 18:00), and a "Get Directions" link.
  - **Right Column (Direct Inquiry):** A contact form for users to send messages directly, including fields for Name, Email, and Message.
- **Connect Socially:** A dark panel at the bottom or side with links to social media platforms (WhatsApp Business, Instagram Studio).
- **Visual Style:** Functional but elegant, with clear typography and distinct sections for physical and digital contact methods.
