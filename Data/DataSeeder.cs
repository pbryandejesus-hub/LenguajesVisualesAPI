using BCrypt.Net;
using LenguajesVisualesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LenguajesVisualesAPI.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.Migrate();

            if (!context.Usuarios.Any())
            {
                var admin = new Usuario
                {
                    Nombre = "Bryan PG",
                    Email = "bryan@test.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234"),
                    Rol = "admin"
                };
                context.Usuarios.Add(admin);
                context.SaveChanges();
            }

            if (!context.Categorias.Any())
            {
                context.Categorias.AddRange(
                    new Categoria { Nombre = "Perifericos" },
                    new Categoria { Nombre = "Hardware" }
                );
                context.SaveChanges();
            }

            if (!context.Productos.Any())
            {
                var cat = context.Categorias.First();
                context.Productos.AddRange(
                    new Producto { Nombre = "Teclado Mecánico", Precio = 150.50m, CategoriaId = cat.Id },
                    new Producto { Nombre = "Mouse Gamer", Precio = 75.25m, CategoriaId = cat.Id }
                );
                context.SaveChanges();
            }

            // No se crean pedidos por defecto; los crearás desde Postman.
        }
    }
}
