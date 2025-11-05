using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SaveData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "EsCompartida",
                table: "ReadingLists",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Nombre", "Password", "Rol" },
                values: new object[,]
                {
                    { 1, "valen@gmail.com", "Valentina García", "$2a$11$sVpwtDAQq2R.A1cMwtzjzemtHsSJ2fF943vTBP3s/rKF2ADNz/DGO", "usuario" },
                    { 2, "anto@gmail.com", "Antonella Garcia", "$2a$11$xHjiX5IWuDuIFmISx9mrWOlrH9IJGJoNWlJYNWjw77gpCgXmtXodW", "usuario" },
                    { 3, "giuli@gmail.com", "Giuliana Alonzo", "$2a$11$owt7SWM7itbzeYrb/wxm1OK1XBnpFi3ptbQFri7ZgUU36V.IAJU8G", "admin" }
                });

            migrationBuilder.InsertData(
                table: "ReadingLists",
                columns: new[] { "Id", "CreadorId", "Descripcion", "Titulo" },
                values: new object[,]
                {
                    { 1, 1, "Libros favoritos", "Favoritos de Valen" },
                    { 2, 2, "Libros de programación", "Lecturas de Anto" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Autor", "Genero", "ListId", "Resenia", "Titulo" },
                values: new object[,]
                {
                    { 1, "Jane Austen", "Romance", 2, null, "Orgullo y Prejuicio" },
                    { 2, "J.R.R. Tolkien", "Fantasía", 1, null, "El Hobbit" },
                    { 3, "Gabriel García Márquez", "Realismo mágico", 1, null, "Cien años de soledad" }
                });

            migrationBuilder.InsertData(
                table: "Votes",
                columns: new[] { "Id", "LibroId", "UsuarioId", "Valor" },
                values: new object[,]
                {
                    { 1, 1, 1, 5 },
                    { 2, 2, 2, 4 },
                    { 3, 3, 2, 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Votes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Votes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Votes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ReadingLists",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ReadingLists",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<bool>(
                name: "EsCompartida",
                table: "ReadingLists",
                type: "tinyint(1)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "tinyint(1)",
                oldDefaultValue: false);
        }
    }
}
