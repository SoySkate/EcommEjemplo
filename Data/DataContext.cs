using EcommerceEjemploApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceEjemploApi.Data
{
    public class DataContext : DbContext
    {
        //el context se comunica directamente con la base de datos
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
   
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>()
                .HasOne(r=>r.User)
                .WithMany(u=>u.Reviews)
                .HasForeignKey(r=>r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Review>()
                 .HasOne(r => r.Product)     // Un review tiene un solo producto
                 .WithMany(p => p.Reviews)   // Un producto tiene muchas reviews
                 .HasForeignKey(r => r.ProductId);  // Clave foránea en Review
            modelBuilder.Entity<OrderDetail>()
                .HasOne(p=>p.Order)
                .WithMany(r=>r.OrderDetails)
                .HasForeignKey(o=>o.OrderId)
                 .OnDelete(DeleteBehavior.Cascade); 
            modelBuilder.Entity<OrderDetail>()
                .HasOne(p=>p.Product)
                .WithMany()
                .HasForeignKey(o=>o.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasOne(u=>u.User)        // Una Orden tiene un Usuario
                .WithMany(u=>u.Orders)     // Un Usuario tiene muchas Órdenes
                .HasForeignKey(o=>o.UserId) // La clave foránea está en Order
                .OnDelete(DeleteBehavior.Cascade); // Si eliminas un Usuario, eliminas también sus Órdenes
            // Relación Order ↔ OrderDetail (Una Orden puede tener muchos detalles)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderDetails)      // Una Orden tiene muchos detalles
                .WithOne(od => od.Order)           // Cada detalle pertenece a una Orden
                .HasForeignKey(od => od.OrderId)   // Clave foránea en OrderDetail
                .OnDelete(DeleteBehavior.Cascade); // Si eliminas una Orden, también se eliminan los detalles

        }

        //Comandos para inicializar la Migracion una vez se ha creado o modificado las clases y por lo tanto
        //Se habran modificado las tablas: Comando:::
        //EntityFrameworkCore\Add-Migration (Migration'sName)
        //Después de esto el comando para actualizar la Database es:
        //EntityFrameworkCore\Update-DataBase

    }
}
