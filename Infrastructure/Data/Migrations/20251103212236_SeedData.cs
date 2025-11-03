using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ReadingLists_ListId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingLists_Users_CreadorId",
                table: "ReadingLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Books_LibroId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_UsuarioId",
                table: "Votes");

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
                    { 1, "valen@gmail.com", "Valentina García", "$2a$11$2VpNSVIhqk/IxJPzQTS4Y.N5.JPv/QjEpHHOmy8U/FBUEUbbZTMam", "usuario" },
                    { 2, "anto@gmail.com", "Antonella Garcia", "$2a$11$NpV0Bt34G87hJf2QpNvur.sPSavQi6kJ2063CArrPvMxwI.M6rHJ2", "usuario" },
                    { 3, "giuli@gmail.com", "Giuliana Alonzo", "$2a$11$6Vy0hkc/42kV4DzmKP5OyerKtTuacCidh91VtFOn7IqqMgh89D.Ku", "admin" }
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

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ReadingLists_ListId",
                table: "Books",
                column: "ListId",
                principalTable: "ReadingLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingLists_Users_CreadorId",
                table: "ReadingLists",
                column: "CreadorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Books_LibroId",
                table: "Votes",
                column: "LibroId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_UsuarioId",
                table: "Votes",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ReadingLists_ListId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadingLists_Users_CreadorId",
                table: "ReadingLists");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Books_LibroId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Users_UsuarioId",
                table: "Votes");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ReadingLists_ListId",
                table: "Books",
                column: "ListId",
                principalTable: "ReadingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadingLists_Users_CreadorId",
                table: "ReadingLists",
                column: "CreadorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Books_LibroId",
                table: "Votes",
                column: "LibroId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Users_UsuarioId",
                table: "Votes",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
