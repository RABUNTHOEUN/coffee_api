using thoeun_coffee.Models;

namespace thoeun_coffee.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions dbContextOptions) : base(options: dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<StaffShift> StaffShifts { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<CoffeeBean> CoffeeBeans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-many relationships
            modelBuilder.Entity<User>()
                .HasMany(u => u.orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);


            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Reviews)
                .WithOne(r => r.Product)
                .HasForeignKey(r => r.ProductId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.StaffShifts)
                .WithOne(ss => ss.User)
                .HasForeignKey(ss => ss.UserId);

            // One-to-one relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Inventories)
                .WithOne(i => i.Product)
                .HasForeignKey<Inventory>(i => i.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId);
                
            modelBuilder.Entity<Discount>()
                           .HasOne(d => d.Product)   // Discount has one Product
                           .WithMany(p => p.Discounts)  // Product can have many Discounts
                           .HasForeignKey(d => d.ProductId) // Foreign key in Discount table
                           .OnDelete(DeleteBehavior.Cascade);  // When Product is deleted, also delete related Discounts

            base.OnModelCreating(modelBuilder);
        }
    }
}
