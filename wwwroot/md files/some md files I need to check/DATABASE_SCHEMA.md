# 02 — Database Schema
## Ruqi Store — SQL Server Database

---

### 10.1 Entity: Users
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| UserId | INT | PK, IDENTITY(1,1), NOT NULL | Unique user identifier (auto-increment) |
| Email | NVARCHAR(255) | UNIQUE, NOT NULL | Login credential; must be globally unique |
| PasswordHash | NVARCHAR(MAX) | NOT NULL | bcrypt hash (12 rounds); plaintext never stored |
| FullName | NVARCHAR(150) | NOT NULL | Display name |
| PhoneNumber | NVARCHAR(20) | NULL | Optional; used for WhatsApp contact link |
| Role | NVARCHAR(30) | NOT NULL, DEFAULT 'Customer' | Enum: Customer \| StoreManager \| Administrator |
| IsActive | BIT | NOT NULL, DEFAULT 1 | 0 = deactivated by admin; login blocked |
| EmailVerified | BIT | NOT NULL, DEFAULT 0 | Email verification status |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Account creation timestamp (UTC) |
| UpdatedAt | DATETIME2 | NOT NULL | Last profile update timestamp |

### 10.2 Entity: Categories
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| CategoryId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment primary key |
| Name | NVARCHAR(100) | UNIQUE, NOT NULL | e.g., Living Room, Bedroom, Dining, Accent |
| Description | NVARCHAR(500) | NULL | Optional category description shown on category page |
| SortOrder | INT | NOT NULL, DEFAULT 0 | Display ordering in navigation |
| IsActive | BIT | NOT NULL, DEFAULT 1 | 0 = hidden from customer catalog |

### 10.3 Entity: Products
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| ProductId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment primary key |
| CategoryId | INT | FK → Categories(CategoryId), NOT NULL | Product's furniture category |
| Name | NVARCHAR(200) | NOT NULL | Furniture product display name |
| Description | NVARCHAR(4000) | NULL | Full product description including style and materials |
| Price | DECIMAL(10,2) | NOT NULL, CHECK(Price > 0) | Selling price; changes do not affect historical orders |
| StockQuantity | INT | NOT NULL, DEFAULT 0, CHECK(StockQuantity >= 0) | Current available units; enforced non-negative at DB |
| SKU | NVARCHAR(50) | UNIQUE, NOT NULL | Stock-keeping unit; unique per product |
| Material | NVARCHAR(100) | NULL | Primary material: Solid Oak, Velvet, Steel, etc. |
| DimensionW | DECIMAL(6,1) | NULL | Width in centimeters |
| DimensionD | DECIMAL(6,1) | NULL | Depth in centimeters |
| DimensionH | DECIMAL(6,1) | NULL | Height in centimeters |
| WeightKg | DECIMAL(6,2) | NULL | Weight for shipping cost calculation |
| IsActive | BIT | NOT NULL, DEFAULT 1 | Soft-delete flag; 0 = hidden from catalog |
| IsFeatured | BIT | NOT NULL, DEFAULT 0 | Featured on homepage when 1 |

### 10.4 Entity: ProductImages
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| ImageId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment primary key |
| ProductId | INT | FK → Products(ProductId) ON DELETE CASCADE, NOT NULL | Parent product; images deleted with product |
| ImageUrl | NVARCHAR(500) | NOT NULL | Relative path or CDN URL to stored image file |
| IsPrimary | BIT | NOT NULL, DEFAULT 0 | Exactly one primary image per product (enforced in app) |
| SortOrder | INT | NOT NULL, DEFAULT 0 | Display order in the product image gallery |

### 10.5 Entity: Orders
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| OrderId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment order identifier |
| UserId | INT | FK → Users(UserId), NOT NULL | Purchasing customer |
| Status | NVARCHAR(30) | NOT NULL, DEFAULT 'Pending' | Enum: Pending \| Processing \| Shipped \| Delivered \| Cancelled |
| TotalAmount | DECIMAL(10,2) | NOT NULL, CHECK(TotalAmount > 0) | Final total at order time (items + tax + shipping); immutable |
| ShippingCost | DECIMAL(8,2) | NOT NULL | Shipping fee at order time |
| TaxAmount | DECIMAL(8,2) | NOT NULL | Tax at order time |
| DeliveryAddress | NVARCHAR(600) | NOT NULL | Snapshot of delivery address at order time |
| Notes | NVARCHAR(1000) | NULL | Customer delivery instructions |
| PaymentStatus | NVARCHAR(20) | NOT NULL, DEFAULT 'Unpaid' | Enum: Unpaid \| Paid \| Refunded |
| PaymentMethod | NVARCHAR(50) | NULL | e.g., CashOnDelivery, BankTransfer |
| PlacedAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Order creation timestamp |
| UpdatedAt | DATETIME2 | NOT NULL | Last status change timestamp |

### 10.6 Entity: OrderItems
*Resolves the many-to-many relationship between Orders and Products. Stores a price snapshot to preserve historical financial accuracy.*
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| OrderItemId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment primary key |
| OrderId | INT | FK → Orders(OrderId) ON DELETE CASCADE, NOT NULL | Parent order |
| ProductId | INT | FK → Products(ProductId) — NO CASCADE, NOT NULL | Ordered product (no cascade preserves history) |
| Quantity | INT | NOT NULL, CHECK(Quantity > 0) | Units ordered |
| UnitPrice | DECIMAL(10,2) | NOT NULL | Price at time of purchase — immutable snapshot |
| Subtotal | DECIMAL(10,2) | NOT NULL | Computed: Quantity × UnitPrice |

### 10.7 Entity: Carts & CartItems
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| CartId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment cart identifier |
| UserId | INT | FK → Users(UserId), UNIQUE, NOT NULL | One cart per user — enforced at DB level |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Cart creation date |
| CartItemId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment cart item identifier |
| CartId | INT | FK → Carts(CartId) ON DELETE CASCADE, NOT NULL | Parent cart |
| ProductId | INT | FK → Products(ProductId), NOT NULL | Product in cart |
| Quantity | INT | NOT NULL, CHECK(Quantity > 0), DEFAULT 1 | Item quantity |

### 10.8 Entity: Reviews
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| ReviewId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment review identifier |
| ProductId | INT | FK → Products(ProductId) ON DELETE CASCADE, NOT NULL | Reviewed product |
| UserId | INT | FK → Users(UserId), NOT NULL | Review author |
| Rating | TINYINT | NOT NULL, CHECK(Rating BETWEEN 1 AND 5) | Star rating 1–5 |
| Body | NVARCHAR(2000) | NULL | Full review text |
| IsVerifiedPurchase | BIT | NOT NULL, DEFAULT 0 | 1 if reviewer has a Delivered order containing this product |
| IsVisible | BIT | NOT NULL, DEFAULT 1 | Admin moderation flag; 0 = hidden |

### 10.9 Entity: Appointments
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| AppointmentId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment appointment identifier |
| UserId | INT | FK → Users(UserId), NOT NULL | Customer who booked |
| AppointmentDate | DATETIME2 | NOT NULL | Requested showroom visit date and time (UTC) |
| Status | NVARCHAR(20) | NOT NULL, DEFAULT 'Pending' | Enum: Pending \| Confirmed \| Cancelled |
| ProductInterests| NVARCHAR(1000) | NULL | Free-text: products customer wants to see |

### 10.10 Entity: AuditLogs
| COLUMN | TYPE | CONSTRAINTS | DESCRIPTION |
|--------|------|-------------|-------------|
| LogId | INT | PK, IDENTITY(1,1), NOT NULL | Auto-increment log entry identifier |
| ActorUserId | INT | FK → Users(UserId), NOT NULL | Administrator who performed the action |
| ActionType | NVARCHAR(50) | NOT NULL | e.g., USER_DEACTIVATE, REVIEW_DELETE |
| TargetEntityType| NVARCHAR(50) | NOT NULL | e.g., User, Review, Product |
| TargetEntityId | NVARCHAR(50) | NOT NULL | Primary key of the affected entity |
| CreatedAt | DATETIME2 | NOT NULL, DEFAULT GETUTCDATE() | Action timestamp (UTC) |

---

## 10.11 Entity Relationships Summary
| FROM ENTITY | RELATIONSHIP | TO ENTITY | CARDINALITY | ENFORCEMENT |
|-------------|--------------|-----------|-------------|-------------|
| Users | places many | Orders | 1 : N | FK: Orders.UserId → Users.UserId |
| Users | has one | Cart | 1 : 1 | UNIQUE constraint on Carts.UserId |
| Users | writes many | Reviews | 1 : N | FK: Reviews.UserId + UNIQUE(ProductId,UserId) |
| Categories | contains many | Products | 1 : N | FK: Products.CategoryId → Categories.CategoryId |
| Products | has many | ProductImages | 1 : N | FK: ProductImages.ProductId CASCADE |
| Products | appears in many| OrderItems| 1 : N | FK: OrderItems.ProductId NO CASCADE |
| Carts | contains many | CartItems | 1 : N | FK: CartItems.CartId CASCADE |

---

## 10.12 ER Diagram (dbdiagram.io Syntax)

```dbml
// Ruqi Store — ER Diagram (dbdiagram.io)

Table Users {
  UserId int [pk, increment]
  Email nvarchar(255) [unique, not null]
  PasswordHash nvarchar(max) [not null]
  FullName nvarchar(150) [not null]
  PhoneNumber nvarchar(20)
  Role nvarchar(30) [not null, default: 'Customer']
  IsActive bit [not null, default: 1]
  CreatedAt datetime2 [not null]
}

Table Categories {
  CategoryId int [pk, increment]
  Name nvarchar(100) [unique, not null]
  Description nvarchar(500)
  SortOrder int [default: 0]
  IsActive bit [default: 1]
}

Table Products {
  ProductId int [pk, increment]
  CategoryId int [ref: > Categories.CategoryId]
  Name nvarchar(200) [not null]
  Price decimal(10,2) [not null]
  StockQuantity int [not null, default: 0]
  SKU nvarchar(50) [unique, not null]
  Material nvarchar(100)
  DimensionW decimal(6,1)
  DimensionD decimal(6,1)
  DimensionH decimal(6,1)
  IsActive bit [default: 1]
  IsFeatured bit [default: 0]
}

Table ProductImages {
  ImageId int [pk, increment]
  ProductId int [ref: > Products.ProductId]
  ImageUrl nvarchar(500) [not null]
  IsPrimary bit [default: 0]
  SortOrder int [default: 0]
}

Table Orders {
  OrderId int [pk, increment]
  UserId int [ref: > Users.UserId]
  Status nvarchar(30) [default: 'Pending']
  TotalAmount decimal(10,2) [not null]
  ShippingCost decimal(8,2) [not null]
  TaxAmount decimal(8,2) [not null]
  DeliveryAddress nvarchar(600) [not null]
  PaymentStatus nvarchar(20) [default: 'Unpaid']
  PlacedAt datetime2 [not null]
}

Table OrderItems {
  OrderItemId int [pk, increment]
  OrderId int [ref: > Orders.OrderId]
  ProductId int [ref: > Products.ProductId]
  Quantity int [not null]
  UnitPrice decimal(10,2) [not null]
  Subtotal decimal(10,2) [not null]
}

Table Carts {
  CartId int [pk, increment]
  UserId int [ref: - Users.UserId]
}

Table CartItems {
  CartItemId int [pk, increment]
  CartId int [ref: > Carts.CartId]
  ProductId int [ref: > Products.ProductId]
  Quantity int [not null]
}

Table Reviews {
  ReviewId int [pk, increment]
  ProductId int [ref: > Products.ProductId]
  UserId int [ref: > Users.UserId]
  Rating tinyint [not null]
  Body nvarchar(2000)
  IsVerifiedPurchase bit [default: 0]
  IsVisible bit [default: 1]
}

Table Appointments {
  AppointmentId int [pk, increment]
  UserId int [ref: > Users.UserId]
  AppointmentDate datetime2 [not null]
  Status nvarchar(20) [default: 'Pending']
  ProductInterests nvarchar(1000)
}

Table AuditLogs {
  LogId int [pk, increment]
  ActorUserId int [ref: > Users.UserId]
  ActionType nvarchar(50) [not null]
  TargetEntityType nvarchar(50) [not null]
  TargetEntityId nvarchar(50) [not null]
  CreatedAt datetime2 [not null]
}
```
