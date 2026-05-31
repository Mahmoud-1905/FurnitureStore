# 07 — UI/UX Requirements

## Overview
This document defines the page list, layout guidelines, and design principles for the Ruqi Store application. It is used by designers and developers to ensure a consistent, premium UI that meets functional and non‑functional requirements.

## 1. Page List
| Page | URL | Audience | Key Elements |
|------|-----|----------|--------------|
| Home | `/` | All users | Hero banner, featured categories, navigation, CTA |
| Product Catalog | `/products` | Guests & Customers | Filter panel, pagination, product cards |
| Product Detail | `/products/{id}` | Guests & Customers | Image gallery, specs, add‑to‑cart, reviews |
| Cart | `/cart` | Customers | Cart items table, quantity selector, total, checkout CTA |
| Checkout | `/checkout` | Customers | Delivery address, order summary, payment form |
| Order History | `/account/orders` | Customers | List of past orders, status, view details |
| Appointment Booking | `/appointments/book` | Customers | Date‑picker, time slot, interests form |
| Manager Dashboard | `/manager` | Store Manager | KPIs, inventory summary, recent orders |
| Admin Panel | `/admin` | Administrator | User management, system reports, audit logs |

## 2. Layout Guidelines
- **Responsive Grid**: Use Bootstrap 5 grid with breakpoints (mobile: 1‑col, tablet: 2‑col, desktop: 3‑4‑col).
- **Bilingual & RTL**: Root `<html>` tag toggles `lang` and `dir`. Use logical CSS properties.
- **Whitespace & Visual Hierarchy**: Minimum 24 px vertical rhythm, generous padding around cards, primary accent color for CTAs.
- **Accessibility**: WCAG 2.1 AA, focus-visible outlines, ARIA labels for interactive elements.

## 3. Interaction Patterns
- **Hover Effects**: Subtle elevation (`box-shadow`) and image zoom on product cards.
- **Loading Indicators**: Spinner on async actions, disabled submit buttons.
- **Toast Notifications**: Success/failure messages displayed at top‑right.

## 4. Branding & Aesthetics
- **Color Palette**: Neutral backgrounds with deep navy accent (`#1A2B4C`).
- **Typography**: `Inter` font, 400/600 weights, 16 px base size.
- **Imagery**: High‑resolution product photos, aspect ratio 4:3, optimized WebP.

## 5. Future Extensions
- Placeholder sections for mobile app UI and AR visualizer can be added later.

*End of document.*
