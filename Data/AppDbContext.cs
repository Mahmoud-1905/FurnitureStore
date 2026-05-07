using FurnitureStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FurnitureStore.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // ──────────────────────────────────────────────
            // ApplicationUser
            // ──────────────────────────────────────────────
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(150);
                entity.Property(u => u.IsActive).HasDefaultValue(true);
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            // ──────────────────────────────────────────────
            // Categories
            // ──────────────────────────────────────────────
            builder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryId);
                entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(c => c.Name).IsUnique();
                entity.Property(c => c.Description).HasMaxLength(500);
                entity.Property(c => c.ImageUrl).HasMaxLength(500);
                entity.Property(c => c.SortOrder).HasDefaultValue(0);
                entity.Property(c => c.IsActive).HasDefaultValue(true);
            });

            // ──────────────────────────────────────────────
            // Products
            // ──────────────────────────────────────────────
            builder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.ProductId);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(4000);
                entity.Property(p => p.Price).HasColumnType("decimal(10,2)");
                entity.Property(p => p.StockQuantity).HasDefaultValue(0);
                entity.Property(p => p.SKU).IsRequired().HasMaxLength(50);
                entity.HasIndex(p => p.SKU).IsUnique();
                entity.Property(p => p.Material).HasMaxLength(100);
                entity.Property(p => p.DimensionW).HasColumnType("decimal(6,1)");
                entity.Property(p => p.DimensionD).HasColumnType("decimal(6,1)");
                entity.Property(p => p.DimensionH).HasColumnType("decimal(6,1)");
                entity.Property(p => p.WeightKg).HasColumnType("decimal(6,2)");
                entity.Property(p => p.IsActive).HasDefaultValue(true);
                entity.Property(p => p.IsFeatured).HasDefaultValue(false);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(p => p.ImageUrl).HasMaxLength(500);

                // CHECK constraints
                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Products_Price", "[Price] > 0");
                    t.HasCheckConstraint("CK_Products_StockQuantity", "[StockQuantity] >= 0");
                });

                // FK to Category
                entity.HasOne(p => p.Category)
                      .WithMany(c => c.Products)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ──────────────────────────────────────────────
            // ProductImages
            // ──────────────────────────────────────────────
            builder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(pi => pi.ImageId);
                entity.Property(pi => pi.ImageUrl).IsRequired().HasMaxLength(500);
                entity.Property(pi => pi.IsPrimary).HasDefaultValue(false);
                entity.Property(pi => pi.SortOrder).HasDefaultValue(0);

                entity.HasOne(pi => pi.Product)
                      .WithMany(p => p.ProductImages)
                      .HasForeignKey(pi => pi.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ──────────────────────────────────────────────
            // Orders
            // ──────────────────────────────────────────────
            builder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.OrderId);
                entity.Property(o => o.UserId).IsRequired().HasMaxLength(450);
                entity.Property(o => o.Status).IsRequired().HasMaxLength(30).HasDefaultValue("Pending");
                entity.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");
                entity.Property(o => o.ShippingCost).HasColumnType("decimal(8,2)");
                entity.Property(o => o.TaxAmount).HasColumnType("decimal(8,2)");
                entity.Property(o => o.DeliveryAddress).IsRequired().HasMaxLength(600);
                entity.Property(o => o.Notes).HasMaxLength(1000);
                entity.Property(o => o.PaymentStatus).IsRequired().HasMaxLength(20).HasDefaultValue("Unpaid");
                entity.Property(o => o.PaymentMethod).HasMaxLength(50);
                entity.Property(o => o.PlacedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Orders_TotalAmount", "[TotalAmount] > 0");
                });

                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ──────────────────────────────────────────────
            // OrderItems
            // ──────────────────────────────────────────────
            builder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.OrderItemId);
                entity.Property(oi => oi.UnitPrice).HasColumnType("decimal(10,2)");
                entity.Property(oi => oi.Subtotal).HasColumnType("decimal(10,2)");

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_OrderItems_Quantity", "[Quantity] > 0");
                });

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.NoAction); // NO CASCADE — preserves history
            });

            // ──────────────────────────────────────────────
            // Carts
            // ──────────────────────────────────────────────
            builder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => c.CartId);
                entity.Property(c => c.UserId).IsRequired().HasMaxLength(450);
                entity.HasIndex(c => c.UserId).IsUnique(); // One cart per user — DB enforced
                entity.Property(c => c.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(c => c.User)
                      .WithOne(u => u.Cart)
                      .HasForeignKey<Cart>(c => c.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ──────────────────────────────────────────────
            // CartItems
            // ──────────────────────────────────────────────
            builder.Entity<CartItem>(entity =>
            {
                entity.HasKey(ci => ci.CartItemId);
                entity.Property(ci => ci.Quantity).HasDefaultValue(1);
                entity.Property(ci => ci.AddedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_CartItems_Quantity", "[Quantity] > 0");
                });

                entity.HasOne(ci => ci.Cart)
                      .WithMany(c => c.CartItems)
                      .HasForeignKey(ci => ci.CartId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(ci => ci.Product)
                      .WithMany(p => p.CartItems)
                      .HasForeignKey(ci => ci.ProductId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ──────────────────────────────────────────────
            // Reviews
            // ──────────────────────────────────────────────
            builder.Entity<Review>(entity =>
            {
                entity.HasKey(r => r.ReviewId);
                entity.Property(r => r.UserId).IsRequired().HasMaxLength(450);
                entity.Property(r => r.Title).HasMaxLength(150);
                entity.Property(r => r.Body).HasMaxLength(2000);
                entity.Property(r => r.IsVerifiedPurchase).HasDefaultValue(false);
                entity.Property(r => r.IsVisible).HasDefaultValue(true);
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                // One review per customer per product — DB enforced
                entity.HasIndex(r => new { r.ProductId, r.UserId }).IsUnique();

                entity.ToTable(t =>
                {
                    t.HasCheckConstraint("CK_Reviews_Rating", "[Rating] >= 1 AND [Rating] <= 5");
                });

                entity.HasOne(r => r.Product)
                      .WithMany(p => p.Reviews)
                      .HasForeignKey(r => r.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ──────────────────────────────────────────────
            // Appointments
            // ──────────────────────────────────────────────
            builder.Entity<Appointment>(entity =>
            {
                entity.HasKey(a => a.AppointmentId);
                entity.Property(a => a.UserId).IsRequired().HasMaxLength(450);
                entity.Property(a => a.Status).IsRequired().HasMaxLength(20).HasDefaultValue("Pending");
                entity.Property(a => a.ProductInterests).HasMaxLength(1000);
                entity.Property(a => a.ManagerNotes).HasMaxLength(500);
                entity.Property(a => a.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(a => a.User)
                      .WithMany(u => u.Appointments)
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ──────────────────────────────────────────────
            // AuditLogs
            // ──────────────────────────────────────────────
            builder.Entity<AuditLog>(entity =>
            {
                entity.HasKey(al => al.LogId);
                entity.Property(al => al.ActorUserId).IsRequired().HasMaxLength(450);
                entity.Property(al => al.ActionType).IsRequired().HasMaxLength(50);
                entity.Property(al => al.TargetEntityType).IsRequired().HasMaxLength(50);
                entity.Property(al => al.TargetEntityId).IsRequired().HasMaxLength(50);
                entity.Property(al => al.Description).HasMaxLength(500);
                entity.Property(al => al.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

                entity.HasOne(al => al.ActorUser)
                      .WithMany(u => u.AuditLogs)
                      .HasForeignKey(al => al.ActorUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        /// <summary>
        /// Auto-set UpdatedAt on SaveChanges for entities that track it.
        /// </summary>
        public override int SaveChanges()
        {
            SetUpdatedAt();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetUpdatedAt();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetUpdatedAt()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is Product product)
                    product.UpdatedAt = DateTime.UtcNow;
                else if (entry.Entity is Order order)
                    order.UpdatedAt = DateTime.UtcNow;
                else if (entry.Entity is Appointment appointment)
                    appointment.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
