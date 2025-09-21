using Microsoft.EntityFrameworkCore;
using LenguajesVisualesAPI.Models;
using BCrypt.Net;

namespace LenguajesVisualesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallesPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de decimales
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<DetallePedido>()
                .Property(dp => dp.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pedido>()
                .Property(p => p.Total)
                .HasPrecision(18, 2);

            // Datos semilla para Usuario
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Admin",
                    Email = "admin@ejemplo.com",
                    PasswordHash = "$2a$11$e5kQWlGb0H1Yl/ZPpS/0Fe2uPku5fz8x6fr/2X5kUOq7Uu.8vXYK6", // hash generado previamente
                    Rol = "Admin",
                    Pedidos = new List<Pedido>(),
                }
            );

            // Datos semilla para Categorias
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria
                {
                    Id = 1,
                    Nombre = "Electrónicos",
                    Descripcion = "Productos electrónicos",
                    Activo = true,
                    Productos = new List<Producto>()
                },
                new Categoria
                {
                    Id = 2,
                    Nombre = "Ropa",
                    Descripcion = "Productos de ropa",
                    Activo = true,
                    Productos = new List<Producto>()
                }
            );

            // Datos semilla para Productos (opcional, ejemplo estático)
            modelBuilder.Entity<Producto>().HasData(
                new Producto
                {
                    Id = 1,
                    Nombre = "Laptop HP",
                    Descripcion = "Laptop HP con 8GB RAM",
                    Precio = 899.99m,
                    CategoriaId = 1,
                    Stock = 15,
                    Activo = true
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
