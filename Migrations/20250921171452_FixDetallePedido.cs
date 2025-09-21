using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LenguajesVisualesAPI.Migrations
{
    /// <inheritdoc />
    public partial class FixDetallePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesPedido",
                table: "DetallesPedido");

            migrationBuilder.DeleteData(
                table: "DetallesPedido",
                keyColumns: new[] { "PedidoId", "ProductoId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "DetallesPedido",
                keyColumns: new[] { "PedidoId", "ProductoId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "DetallesPedido",
                keyColumns: new[] { "PedidoId", "ProductoId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "DetallesPedido",
                keyColumns: new[] { "PedidoId", "ProductoId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pedidos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "DetallesPedido",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesPedido",
                table: "DetallesPedido",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Electrónicos");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Productos de ropa");

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "Stock" },
                values: new object[] { "Laptop HP con 8GB RAM", 15 });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Nombre", "PasswordHash" },
                values: new object[] { "admin@ejemplo.com", "Admin", "$2a$11$e5kQWlGb0H1Yl/ZPpS/0Fe2uPku5fz8x6fr/2X5kUOq7Uu.8vXYK6" });

            migrationBuilder.CreateIndex(
                name: "IX_DetallesPedido_PedidoId",
                table: "DetallesPedido",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesPedido",
                table: "DetallesPedido");

            migrationBuilder.DropIndex(
                name: "IX_DetallesPedido_PedidoId",
                table: "DetallesPedido");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "DetallesPedido");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesPedido",
                table: "DetallesPedido",
                columns: new[] { "PedidoId", "ProductoId" });

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Electrónica");

            migrationBuilder.UpdateData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: 2,
                column: "Descripcion",
                value: "Prendas de vestir");

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Activo", "Descripcion", "Nombre" },
                values: new object[] { 3, true, "Comestibles y bebidas", "Alimentos" });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Fecha", "Total", "UsuarioId" },
                values: new object[] { 1, new DateTime(2025, 9, 21, 14, 10, 45, 943, DateTimeKind.Local).AddTicks(9844), 0m, 1 });

            migrationBuilder.UpdateData(
                table: "Productos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Descripcion", "Stock" },
                values: new object[] { "Laptop HP 8GB RAM", 10 });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Activo", "CategoriaId", "Descripcion", "Nombre", "Precio", "Stock" },
                values: new object[,]
                {
                    { 2, true, 1, "Smartphone 128GB", "Smartphone Samsung", 599.99m, 15 },
                    { 3, true, 2, "Camisa de algodón", "Camisa Polo", 29.99m, 20 }
                });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Nombre", "PasswordHash" },
                values: new object[] { "bryan@ejemplo.com", "Bryan", "$2a$11$O1rwjfPzK6hZWng/jNCYK./Vn.i2iv2qmrzQJZgEXdWBStUfXuyeG" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "Nombre", "PasswordHash", "Rol" },
                values: new object[] { 2, "ana@ejemplo.com", "Ana", "$2a$11$CpJsR29uTHhpOxIcAt4SI.amiK7x/Knk5RdsITl69RSWEYNWKz7ea", "User" });

            migrationBuilder.InsertData(
                table: "DetallesPedido",
                columns: new[] { "PedidoId", "ProductoId", "Cantidad", "PrecioUnitario" },
                values: new object[,]
                {
                    { 1, 1, 1, 899.99m },
                    { 1, 3, 2, 29.99m }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "Fecha", "Total", "UsuarioId" },
                values: new object[] { 2, new DateTime(2025, 9, 21, 14, 10, 45, 947, DateTimeKind.Local).AddTicks(2808), 0m, 2 });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Activo", "CategoriaId", "Descripcion", "Nombre", "Precio", "Stock" },
                values: new object[] { 4, true, 3, "Paquete de galletas", "Galletas Choco", 4.99m, 50 });

            migrationBuilder.InsertData(
                table: "DetallesPedido",
                columns: new[] { "PedidoId", "ProductoId", "Cantidad", "PrecioUnitario" },
                values: new object[,]
                {
                    { 2, 2, 1, 599.99m },
                    { 2, 4, 3, 4.99m }
                });
        }
    }
}
