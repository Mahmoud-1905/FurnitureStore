# 10 — Backend Requirements
> **Destination:** Antigravity  
> **Purpose:** Specifies the complete ASP.NET Core MVC structure — controllers, services, repositories, auth, and database config.  
> **Rule:** Authentication = who you are (login). Authorization = what you can do ([Authorize] attribute). They are different layers.

---

## Project Structure
```
RuqiStore/
├── Controllers/
│   ├── HomeController.cs
│   ├── ProductsController.cs
│   ├── CartController.cs
│   ├── OrdersController.cs
│   ├── AppointmentsController.cs
│   ├── AccountController.cs
│   ├── StoreManagerController.cs
│   └── AdminController.cs
│
├── Services/
│   ├── Interfaces/
│   │   ├── IProductService.cs
│   │   ├── IOrderService.cs
│   │   ├── ICartService.cs
│   │   ├── IInventoryService.cs
│   │   ├── IReviewService.cs
│   │   ├── IAppointmentService.cs
│   │   ├── IUserService.cs
│   │   ├── IReportService.cs
│   │   └── IFileUploadService.cs
│   ├── ProductService.cs
│   ├── OrderService.cs
│   ├── CartService.cs
│   ├── InventoryService.cs
│   ├── ReviewService.cs
│   ├── AppointmentService.cs
│   ├── UserService.cs
│   ├── ReportService.cs
│   └── FileUploadService.cs
│
├── Repositories/
│   ├── Interfaces/
│   │   ├── IProductRepository.cs
│   │   ├── IOrderRepository.cs
│   │   ├── ICartRepository.cs
│   │   ├── IUserRepository.cs
│   │   ├── IReviewRepository.cs
│   │   ├── IAppointmentRepository.cs
│   │   ├── ICategoryRepository.cs
│   │   └── IAuditLogRepository.cs
│   └── [Implementations for each]
│
├── Models/
│   ├── Entities/          ← EF Core classes
│   │   ├── ApplicationUser.cs
│   │   ├── Category.cs
│   │   ├── Product.cs
│   │   ├── ProductImage.cs
│   │   ├── Order.cs
│   │   ├── OrderItem.cs
│   │   ├── Cart.cs
│   │   ├── CartItem.cs
│   │   ├── Review.cs
│   │   ├── Appointment.cs
│   │   └── AuditLog.cs
│   └── ViewModels/        ← Razor input/output models
│
├── Data/
│   ├── AppDbContext.cs
│   └── Migrations/
│
├── Middleware/
│   ├── GlobalExceptionMiddleware.cs
│   └── RequestTimingMiddleware.cs
│
├── Exceptions/
│   └── [All custom exception classes from File 05]
│
├── Uploads/               ← Product images (outside wwwroot)
│   └── Products/{ProductId}/
│
├── Views/                 ← Razor Views from Stitch (22 files)
├── wwwroot/               ← CSS + JS only
├── appsettings.json       ← SQL Server connection string
├── appsettings.Development.json  ← SQLite connection string
└── Program.cs
```

---

## Controllers (8 Total)

### 1. HomeController
```
Route: /
Actions:
  [AllowAnonymous] GET Index() → View with featured products and categories
```

### 2. ProductsController
```
Route: /Products
Actions:
  [AllowAnonymous] GET Index(filters) → paginated catalog
  [AllowAnonymous] GET Detail(int id) → product detail
  [Authorize(Roles="Customer")] POST SubmitReview(ReviewViewModel) → review
```

### 3. CartController
```
Route: /Cart
[Authorize(Roles="Customer")] on all actions
Actions:
  GET  Index()                         → cart view
  POST AddToCart(int productId, int qty) → add item
  POST UpdateCart(int cartItemId, int qty) → update qty
  POST RemoveFromCart(int cartItemId)   → remove item
```

### 4. OrdersController
```
Route: /Orders
[Authorize(Roles="Customer")] on all actions
Actions:
  GET  Checkout()                → checkout form
  POST PlaceOrder(CheckoutViewModel) → atomic order placement
  GET  Confirmation(int id)      → confirmation page
  GET  History()                 → order history
  GET  Detail(int id)            → order detail (with data isolation check)
  POST Cancel(int id)            → cancel Pending order
```

### 5. AppointmentsController
```
Route: /Appointments
[Authorize(Roles="Customer")] on all actions
Actions:
  GET  Book()                    → booking form with available slots
  POST Book(BookAppointmentViewModel) → create appointment
  GET  MyAppointments()          → customer's bookings
  POST Cancel(int id)            → cancel (24h rule enforced)
```

### 6. AccountController
```
Route: /Account
Actions:
  [AllowAnonymous] GET  Register() → registration form
  [AllowAnonymous] POST Register(RegisterViewModel)
  [AllowAnonymous] GET  Login()    → login form
  [AllowAnonymous] POST Login(LoginViewModel)
  [Authorize]      POST Logout()
  [Authorize]      GET  Profile()
  [Authorize]      POST Profile(ProfileViewModel)
```

### 7. StoreManagerController
```
Route: /Manager
[Authorize(Roles="StoreManager")] on all actions
Actions:
  GET  Dashboard()
  GET  Products()
  GET  CreateProduct() / POST CreateProduct(CreateProductViewModel)
  GET  EditProduct(int id) / POST EditProduct(CreateProductViewModel)
  POST SoftDeleteProduct(int id)
  GET  Orders() / POST UpdateOrderStatus(int id, string status)
  GET  Appointments() / POST ConfirmAppointment(int id) / POST RejectAppointment(int id)
  GET  Inventory() / POST UpdateStock(int productId, int qty)
```

### 8. AdminController
```
Route: /Admin
[Authorize(Roles="Admin")] on all actions
Actions:
  GET  Index()          → admin dashboard
  GET  Users()          → user list with search
  POST DeactivateUser(string userId)
  POST ActivateUser(string userId)
  POST AssignRole(string userId, string role)
  POST RevokeRole(string userId, string role)
  GET  Reviews()
  POST DeleteReview(int reviewId)
  GET  AuditLog()
  GET  Reports()        → export CSV
```

---

## Authentication & Authorization Configuration (Program.cs)

```csharp
// Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options => {
    options.Password.RequireDigit           = true;
    options.Password.RequiredLength         = 8;
    options.Password.RequireUppercase       = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan  = TimeSpan.FromMinutes(15);
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

// Cookie auth
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath        = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.Cookie.HttpOnly  = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite  = SameSiteMode.Strict;
    options.SlidingExpiration = true;
    options.ExpireTimeSpan   = TimeSpan.FromHours(24);
});

// CSRF
builder.Services.AddControllersWithViews(options => {
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
});
```

---

## Database Configuration

### appsettings.json (SQL Server — Production)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=RuqiStore;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### appsettings.Development.json (SQLite — Local Dev)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=RuqiStore.db"
  }
}
```

### Program.cs — DB registration
```csharp
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString));
}
```

---

## Middleware Pipeline (Program.cs — in this exact order)
```csharp
app.UseMiddleware<GlobalExceptionMiddleware>();  // 1st — catch all
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();     // read cookie → populate User
app.UseAuthorization();      // enforce [Authorize]
app.UseRequestLocalization(); // Arabic/English culture
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
```

---

## EF Core Migrations Commands
```bash
# Initial migration
dotnet ef migrations add InitialCreate

# Apply to database
dotnet ef database update

# Seed data runs automatically via DbInitializer on first startup
```

---

## Antigravity Prompt
```
Read 10_BACKEND_REQUIREMENTS.md.
Build the complete backend skeleton:
1. All EF Core entity models + AppDbContext + Fluent API config
2. All Service interfaces + implementations (with business rules from File 05)
3. All Repository interfaces + implementations
4. All 8 Controllers with correct [Authorize] attributes
5. Program.cs with Identity, Cookie Auth, DI registrations, and Middleware pipeline
6. Database config for SQL Server (production) and SQLite (development)
7. Seed data: Admin account, 3 roles, 4 furniture categories
```
