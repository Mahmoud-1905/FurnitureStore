# 04_DATABASE_DESIGN

## Overview

This document describes the relational database schema for the **Furniture Store** system. It lists the tables, primary keys, important columns, and the relationships between tables.

## Tables

| Table | Primary Key | Important Columns |
|-------|-------------|-------------------|
| `Users` | `UserId` | `Username`, `Email`, `PasswordHash`, `Role`, `IsActive` |
| `Products` | `ProductId` | `Name`, `Description`, `Price`, `Stock`, `IsActive` |
| `Orders` | `OrderId` | `UserId`, `OrderDate`, `TotalAmount`, `Status` |
| `OrderItems` | `OrderItemId` | `OrderId`, `ProductId`, `Quantity`, `UnitPrice` |
| `Appointments` | `AppointmentId` | `UserId`, `ProductId`, `ScheduledAt`, `Status` |
| `Reviews` | `ReviewId` | `UserId`, `ProductId`, `Rating`, `Comment`, `CreatedAt`, `IsVerified` |
| `AuditLogs` | `LogId` | `UserId`, `Action`, `Timestamp` |

## Relationships

- `Users` 1‑* `Orders`
- `Orders` 1‑* `OrderItems`
- `Products` 1‑* `OrderItems`
- `Users` 1‑* `Appointments`
- `Products` 1‑* `Appointments`
- `Users` 1‑* `Reviews`
- `Products` 1‑* `Reviews`
- `Users` 1‑* `AuditLogs`

## Diagram

*(Insert ER diagram here)*

---

*Standard Markdown format, no front‑matter required.*
