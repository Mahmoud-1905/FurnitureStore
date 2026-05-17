# FurnitureStore - Technical Explanation Document

## 1. Project Overview

- **What is this project?** FurnitureStore is a full-stack bilingual (Arabic/English) e-commerce web application designed for selling furniture online.
- **What problem does it solve?** It provides a seamless digital storefront that allows customers to browse products, manage a shopping cart, and place orders securely, bridging the gap between a physical showroom and digital customers.
- **Who is it for?** Customers looking to purchase premium furniture online and store administrators who need to manage inventory and process orders.
- **What are its main features?** 
  - Dynamic product catalog with search and category filtering.
  - Shopping cart and checkout system.
  - User authentication and authorization (registration, login, password recovery).
  - Bilingual support (Arabic and English localization).
  - Product reviews and appointment booking functionality.
- **What does a user experience from start to finish?** A user lands on the homepage, selects their preferred language, browses products by category or search term, views full product details, adds items to their cart, creates an account or logs in, and completes the checkout process to place an order.

---

## 2. Architecture Overview

This project uses a classic monolithic three-layer architecture:

| Layer | Technology | Responsibility |
|-------|-----------|----------------|
| **Frontend** | HTML / CSS (Tailwind + Custom) / JS | What the user sees and interacts with. It renders the user interface and captures input. |
| **Backend** | ASP.NET Core MVC | Business logic, routing, security, and data handling. It processes requests and builds the responses. |
| **Database** | SQLite via Entity Framework Core | Persistent data storage. It saves users, products, orders, etc., safely to disk. |

**How an HTTP request travels:**
1. **Browser** sends an HTTP request (e.g., `GET /Products/Index`).
2. **Routing** in ASP.NET Core maps the URL to the `ProductsController`.
3. **Controller** invokes the `Index` action method.
4. **Action Method** uses `AppDbContext` (via EF Core) to query the SQLite Database.
5. **Database** returns `Product` entities.
6. **Controller** passes these entities to the `Views/Products/Index.cshtml` view.
7. **View** generates HTML using Razor syntax.
8. **Browser** receives the HTML and renders the page using CSS/JS.

**What MVC means:**
- **Model:** Represents the data structure and business logic (e.g., `Product.cs`, `AppDbContext.cs`).
- **View:** The user interface template (e.g., `Index.cshtml`).
- **Controller:** The brain that connects the Model and the View (e.g., `ProductsController.cs`).

---

## 3. Folder & File Structure

```text
FurnitureStore/
├── Controllers/
│   ├── AccountController.cs       — Manages user login, registration, and password recovery.
│   ├── AdminController.cs         — Handles administrative tasks (managing products/categories).
│   ├── CartController.cs          — Manages the user's shopping cart (add/remove items).
│   ├── HomeController.cs          — Serves the landing page and generic static pages.
│   ├── LanguageController.cs      — Toggles the user's language preference (AR/EN).
│   └── ProductsController.cs      — Handles displaying the product catalog and details.
├── Models/
│   ├── ApplicationUser.cs         — Extends IdentityUser with custom fields like FullName.
│   ├── Product.cs                 — Represents a piece of furniture in the store.
│   ├── Order.cs & OrderItem.cs    — Represents a customer's placed order and its contents.
│   ├── Cart.cs & CartItem.cs      — Represents an active shopping session.
│   └── [Other Entity Classes]     — Category, ProductImage, Review, Appointment, AuditLog.
├── Views/
│   ├── Account/                   — Razor views for Login, Register, ChangePassword.
│   ├── Products/                  — Razor views like Index.cshtml (catalog) and Details.cshtml.
│   ├── Shared/                    — Reusable views like _Layout.cshtml (master template).
│   ├── _ViewImports.cshtml        — Defines global namespaces and tag helpers for all views.
│   └── _ViewStart.cshtml          — Sets the default layout for all views.
├── Data/
│   ├── AppDbContext.cs            — The Entity Framework Core database context configuring tables.
│   └── AppDbContextFactory.cs     — Helps run EF Core migrations at design-time.
├── wwwroot/
│   ├── css/site.css & account.css — Custom global styles and glassmorphism UI definitions.
│   ├── js/site.js                 — Global frontend JavaScript functions.
│   └── images/ & lib/             — Static assets and 3rd party libraries (like jQuery/Bootstrap).
├── appsettings.json               — Configuration file containing the SQLite connection string.
└── Program.cs                     — The application entry point, configuring services and middleware.
```

**Responsibilities and Impact:**
- **Controllers:** If removed, the application cannot respond to any URLs.
- **Models & Data:** If removed, the application loses its memory and data structure; the database cannot be queried.
- **Views:** If removed, the application can only return raw data (like JSON), breaking the visual website.
- **Program.cs:** If removed, the application won't even start.

---

## 4. Backend Explanation (ASP.NET Core MVC)

### 4.1 Program.cs / Startup Configuration

**What it does:** It registers services (Dependency Injection) and defines the HTTP request pipeline (Middleware).
**Why order matters:** Middleware executes sequentially. For example, `app.UseAuthentication()` must run *before* `app.UseAuthorization()` so the system knows *who* the user is before checking *what* they are allowed to do.

```csharp
// 1. Dependency Injection Container
var builder = WebApplication.CreateBuilder(args);

// Registers localization to support Arabic/English strings from the "Resources" folder
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// Adds MVC controllers and enables view localization
builder.Services.AddControllersWithViews()
    .AddViewLocalization()
    .AddDataAnnotationsLocalization();

// Configures Entity Framework Core to use SQLite with the connection string from appsettings.json
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlite(connStr); 
});

// Configures ASP.NET Core Identity for user management (Passwords, Roles)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { ... })
    .AddEntityFrameworkStores<AppDbContext>();

// 2. HTTP Request Pipeline (Middleware)
var app = builder.Build();

app.UseStaticFiles();         // Serves CSS, JS, and Images from wwwroot
app.UseRequestLocalization(); // Applies AR/EN language settings based on user preference
app.UseRouting();             // Matches URLs to Controllers
app.UseSession();             // Enables temporary user session storage
app.UseAuthentication();      // Identifies the user via login cookies
app.UseAuthorization();       // Enforces access rules ([Authorize])

// Maps default routes (e.g., /Home/Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); // Starts listening for requests
```

### 4.2 Controllers

Controllers manage specific areas. For example, `ProductsController` handles the catalog.

```csharp
public class ProductsController : Controller
{
    private readonly AppDbContext _context; // Injected Database Context

    public ProductsController(AppDbContext context)
    {
        _context = context; // Constructor injection
    }

    // Handles GET /Products?searchTerm=sofa&categoryId=1
    public async Task<IActionResult> Index(string searchTerm, int? categoryId)
    {
        // Start building a LINQ query to fetch active products
        var productsQuery = _context.Products
            .Include(p => p.Category) // Join with Category table
            .Where(p => p.IsActive)
            .AsQueryable();

        // Apply filters dynamically
        if (categoryId.HasValue)
            productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);

        if (!string.IsNullOrEmpty(searchTerm))
            productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm));

        // Execute query against SQLite and get the list
        var products = await productsQuery.ToListAsync();

        // Pass metadata to the view via ViewBag
        ViewBag.SearchTerm = searchTerm;
        ViewBag.Categories = await _context.Categories.Where(c => c.IsActive).ToListAsync();
        
        // Pass the main data model to the view
        return View(products); 
    }
}
```

### 4.3 Routing
- **Configuration:** Handled by `app.MapControllerRoute` in `Program.cs` utilizing the `{controller}/{action}/{id?}` pattern.
- **Example 1:** `GET /` → Routes to `HomeController.Index()`.
- **Example 2:** `GET /Products/Details/5` → Routes to `ProductsController.Details(int id)` where `id = 5`.
- **Example 3:** `POST /Account/Login` → Routes to the `AccountController.Login(LoginViewModel model)` method marked with `[HttpPost]`.

### 4.4 Models

Models represent database tables and business entities.

```csharp
public class Product
{
    public int ProductId { get; set; } // Primary Key

    public int CategoryId { get; set; } // Foreign Key to Category table

    [Required] // Data Annotation: Cannot be NULL
    [MaxLength(200)] // Data Annotation: Max length in DB is 200 chars
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string SKU { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Navigation property: Tells EF Core how to map the relationship
    public Category Category { get; set; } = null!;
}
```

### 4.5 DbContext (Entity Framework Core)

`AppDbContext` is the bridge between C# and SQLite.

- **What it is:** A class deriving from `IdentityDbContext<ApplicationUser>` that represents an active session with the database.
- **DbSet:** Every `DbSet<T>` (e.g., `public DbSet<Product> Products { get; set; }`) represents a physical table in SQLite.
- **EF Core Mapping:** In `OnModelCreating`, Fluent API is used to configure strict database rules (e.g., `entity.HasIndex(p => p.SKU).IsUnique()` ensuring no duplicate SKUs).
- **Migrations:** Migrations translate the C# models into SQL schema files, generating the `App_Data/furniture_store.db` file automatically.

### 4.6 Authentication & Authorization

- **Login/Logout:** Handled by `SignInManager` and `UserManager` in `AccountController.cs`. They validate passwords and issue secure encrypted cookies.
- **Identity:** ASP.NET Core Identity provides pre-built logic for hashing passwords, tracking users, and assigning roles (e.g., "Customer").
- **Protection:** Adding the `[Authorize]` attribute above a controller or action method forces the system to redirect unauthenticated users to the Login page.

---

## 5. Frontend Explanation

### 5.1 Views (.cshtml files)
- **Razor Syntax:** Razor allows embedding C# code within HTML using the `@` symbol.
- **Dynamic Data:** The `Views/Products/Index.cshtml` file receives `List<Product>` as its model (`@model List<FurnitureStore.Models.Product>`). It loops through it using `@foreach (var item in Model)` to dynamically render a card for each product.

### 5.2 Layouts and Shared Views
- **`_Layout.cshtml`:** The master template containing the `<head>`, `<nav>`, and `<footer>`. Every specific page is injected into its `@RenderBody()` method.
- **`_ViewStart.cshtml`:** Automatically applies `_Layout.cshtml` to all views so you don't have to declare it manually.
- **`_ViewImports.cshtml`:** Centralizes `using` statements and Tag Helpers globally.

### 5.3 HTML Structure & Tag Helpers
Forms use Tag Helpers instead of raw HTML attributes for security and routing safety.
```html
<!-- asp-action and asp-controller automatically generate the correct URL and CSRF tokens -->
<form asp-action="Login" asp-controller="Account" method="post">
    <!-- asp-for binds the input to the ViewModel property -->
    <input asp-for="Email" class="form-control" />
    <!-- asp-validation-for displays C# validation errors (like [Required]) directly in the UI -->
    <span asp-validation-for="Email" class="text-danger"></span>
    <button type="submit">Login</button>
</form>
```

### 5.4 CSS Styling
- **Approach:** The project utilizes a hybrid approach. It uses utility classes directly in the views, but relies on `wwwroot/css/site.css` for custom premium aesthetics.
- **Custom CSS:** The `site.css` file defines CSS variables (`--primary-teal`), Glassmorphism effects (`backdrop-filter: blur`), and smooth hover animations to provide a luxurious, modern look.

### 5.5 JavaScript
- **`wwwroot/js/site.js`:** Contains generic frontend logic. In this project, complex SPA frameworks were avoided in favor of traditional server-side rendering, keeping JavaScript lightweight and primarily used for UI toggles, alerts, or simple DOM manipulation.

---

## 6. Database Explanation (SQLite)

### 6.1 Why SQLite
- **Why:** SQLite is a self-contained, serverless database. It stores the entire database in a single file (`App_Data/furniture_store.db`).
- **Advantages:** Zero configuration, easy to share via GitHub, perfect for small-to-medium web apps.
- **Limitations:** Not ideal for massive concurrent write operations compared to SQL Server.

### 6.2 Tables
- **`Products` Table:** Stores furniture items. Columns include `ProductId` (PK, auto-increment), `Name` (Text, Not Null), `Price` (Decimal), and `CategoryId` (FK).
- **`AspNetUsers` Table:** Created by Identity. Stores user accounts, hashed passwords, and emails.
- **`Orders` Table:** Stores customer purchases. Tracks `TotalAmount` and `Status`.

### 6.3 Entity Relationships
- **One-to-Many:** `Category` (One) → `Product` (Many). A product belongs to one category. Enforced by `CategoryId` Foreign Key.
- **One-to-Many:** `ApplicationUser` (One) → `Order` (Many). A user can have multiple orders.
- **One-to-One:** `ApplicationUser` (One) → `Cart` (One). Enforced by a unique index on `UserId` in the `Carts` table.

### 6.4 How Data Is Read and Written
**C# LINQ:**
```csharp
var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == 1);
```
**Translated SQL (executed by EF Core behind the scenes):**
```sql
SELECT "p"."ProductId", "p"."CategoryId", "p"."Name", "p"."Price"
FROM "Products" AS "p"
WHERE "p"."ProductId" = 1
LIMIT 1;
```

---

## 7. Logic & Data Flow (Step-by-Step Traces)

### Scenario A: User opens the Product Catalog
1. User navigates to `/Products` in the browser.
2. Routing directs the request to `ProductsController.Index()`.
3. `Index()` uses `AppDbContext` to query SQLite for all active products.
4. EF Core executes `SELECT * FROM Products WHERE IsActive = 1`.
5. The list of `Product` objects is passed to `Views/Products/Index.cshtml`.
6. Razor loops through the list, generating HTML cards for each product.
7. The complete HTML page is sent back to the browser and styled by `site.css`.

### Scenario B: User registers a new account
1. User fills out the registration form and submits to `/Account/Register` via POST.
2. `AccountController.Register(RegisterViewModel model)` receives the data.
3. The model is validated (e.g., checking if the email format is correct and passwords match).
4. `UserManager.CreateAsync()` hashes the password and saves the `ApplicationUser` to the SQLite `AspNetUsers` table.
5. `SignInManager.SignInAsync()` generates an authentication cookie.
6. The user is redirected to `HomeController.Index()`.

---

## 8. Technical Decisions

| Decision | Choice Made | Why This / Why Not the Alternative |
|----------|------------|-------------------------------------|
| **Backend** | ASP.NET Core MVC | Provides strict typing, robust security out-of-the-box (Identity), and excellent performance compared to dynamic languages like Node.js or Python. |
| **Frontend** | HTML/CSS/JS (Razor) | Avoided heavy SPA frameworks (React/Angular) to improve initial page load speed, simplify SEO, and reduce architectural complexity. |
| **Database** | SQLite | Chosen for portability. SQL Server requires local installation or Docker, making project sharing difficult. SQLite lives as a simple `.db` file in the repository. |
| **ORM** | Entity Framework Core | Eliminates manual SQL writing, prevents SQL injection automatically, and allows schema tracking via Code-First Migrations. |

---

## 9. Security & Validation

- **Input validation:** Validated on the server via Model Data Annotations (e.g., `[Required]`, `[MaxLength(200)]` on `Product.Name`). The controller checks `if (!ModelState.IsValid)` before processing.
- **SQL injection prevention:** EF Core parameterizes all queries automatically. Direct strings are never concatenated into SQL commands.
- **XSS prevention:** Razor (`@Model.Name`) automatically HTML-encodes all output. If a user enters `<script>alert('x')</script>`, Razor safely renders it as plain text.
- **CSRF protection:** Forms use `[ValidateAntiForgeryToken]` and Tag Helpers automatically inject a hidden security token into every POST request to verify it originated from the actual site.
- **Authentication:** `SignInManager` utilizes secure, HttpOnly, and encrypted cookies to prevent session hijacking.

---

## 10. Performance Considerations

- **Database Optimization:** `Include(p => p.Category)` is used in `ProductsController` to fetch products and their categories in a *single* SQL query, completely avoiding the dreaded N+1 query problem.
- **Static Files:** Assets in `wwwroot` are served efficiently using the `app.UseStaticFiles()` middleware.
- **Scalability Limits:** SQLite is a file-based database and enforces file-level locks during write operations. It cannot handle thousands of simultaneous checkouts. To scale for thousands of users, the connection string in `appsettings.json` must be swapped to point to a full SQL Server or PostgreSQL instance.

---

## 11. Common Questions & Strong Answers

**1. What does this project do, and how would you summarize the architecture?**
It is a bilingual e-commerce furniture store. It uses a monolithic 3-tier architecture with an ASP.NET Core MVC backend, a server-side rendered HTML/CSS frontend, and an Entity Framework Core SQLite database.

**2. Why did you use ASP.NET Core MVC instead of a different backend?**
It provides an enterprise-grade ecosystem. Features like Identity (auth), EF Core (data), and localization (bilingual support) are built-in, drastically reducing development time compared to stitching together independent libraries in Express.js.

**3. Why SQLite instead of SQL Server or PostgreSQL?**
Portability. SQLite stores data in a single file, meaning any developer cloning the GitHub repository can run the application instantly without configuring a database server. 

**4. What is the MVC pattern and how does this project apply it?**
Model-View-Controller separates concerns. `Models/Product.cs` represents data. `Views/Products/Index.cshtml` handles UI. `Controllers/ProductsController.cs` acts as the middleman, pulling data from the database and pushing it to the view.

**5. How does routing work in this project? Walk me through a real URL.**
ASP.NET maps URLs to C# classes based on the `{controller}/{action}/{id}` convention defined in `Program.cs`. For `/Products/Details/5`, it instantiates `ProductsController`, calls the `Details` method, and passes `5` as the `id` argument.

**6. How does data get from the database to the screen?**
The Controller asks EF Core for data via LINQ (`_context.Products.ToList()`). EF Core generates SQL, fetches data from SQLite, maps it to C# objects, and hands it back to the Controller. The Controller passes those objects to a Razor View, which uses C# loops to embed the data into HTML tags.

**7. How does the frontend communicate with the backend?**
Primarily through traditional HTTP GET requests (for navigating pages) and HTTP POST requests (for submitting HTML forms, like logging in or adding to cart).

**8. How is user input validated before it reaches the database?**
We use Data Annotations (like `[Required]`) on ViewModels. When a POST request hits the controller, ASP.NET validates the data automatically. We enforce this by checking `ModelState.IsValid` before saving to the database.

**9. How is the application protected from SQL injection?**
We strictly use Entity Framework Core. EF Core automatically uses parameterized queries for all database interactions, neutralizing any malicious SQL commands typed by users.

**10. What is Entity Framework Core and how does this project use it?**
It is an Object-Relational Mapper (ORM). It allows us to interact with the SQLite database using C# classes instead of writing raw SQL strings. The `AppDbContext` maps our C# classes directly to SQLite tables.

**11. What is Dependency Injection and where is it used here?**
DI is a design pattern where an object receives its dependencies from the framework rather than creating them itself. In `Program.cs`, we register `AppDbContext`. In `ProductsController`, we request it via the constructor. This makes the code modular and testable.

**12. How would you add a new feature to this project?**
To add a "Wishlist" feature: I'd create a `Wishlist` class in `Models/`, add `DbSet<Wishlist>` to `AppDbContext`, generate a database migration, create a `WishlistsController` to handle logic, and create Razor views in `Views/Wishlists/` to display the UI.

**13. What would need to change if this app needed to scale to thousands of users?**
SQLite would become a bottleneck due to write-locking. We would change the `appsettings.json` connection string and `Program.cs` configuration to use SQL Server or PostgreSQL (`options.UseSqlServer(...)`). We would also implement a caching layer like Redis.

**14. What is the role of `Program.cs`?**
It is the heart of the application. It bootstraps the web server, configures Dependency Injection services (like Identity and Localization), and defines the exact order of middleware operations (routing, session, auth) that every HTTP request must flow through.

**15. How is the localization and bilingual support managed in this application?**
In `Program.cs`, we configured `AddLocalization` pointing to a `Resources` folder, and added supported cultures (`ar`, `en`). We use the `IViewLocalizer` interface in our Razor views (e.g., `@Localizer["OurPicks"]`). The `LanguageController` allows users to toggle a culture cookie, changing the active language dynamically.
