using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ManyToManyBooksLists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_ReadingLists_ListId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_ListId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ListId",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BookReadingLists",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    ReadingListId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReadingLists", x => new { x.BookId, x.ReadingListId });
                    table.ForeignKey(
                        name: "FK_BookReadingLists_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookReadingLists_ReadingLists_ReadingListId",
                        column: x => x.ReadingListId,
                        principalTable: "ReadingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "BookReadingLists",
                columns: new[] { "BookId", "ReadingListId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 2, 1 },
                    { 3, 1 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ttN3Zv173zJ.eiTNnLCsRe8sNkXbs/Gl.tRPasxdqOQIxe5xwK1MC");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$89lnIEX12xfEuhpgnQueuujdhT.NOZ/LsK.FArP1WdB1ozIHnmfOy");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$4.nClq0.Ste/iAQEPkIzM.la3JFkH0hxM8bullDC26br2dzN4tbkO");

            migrationBuilder.CreateIndex(
                name: "IX_BookReadingLists_ReadingListId",
                table: "BookReadingLists",
                column: "ReadingListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookReadingLists");

            migrationBuilder.AddColumn<int>(
                name: "ListId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "ListId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "ListId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "ListId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$sVpwtDAQq2R.A1cMwtzjzemtHsSJ2fF943vTBP3s/rKF2ADNz/DGO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$xHjiX5IWuDuIFmISx9mrWOlrH9IJGJoNWlJYNWjw77gpCgXmtXodW");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$owt7SWM7itbzeYrb/wxm1OK1XBnpFi3ptbQFri7ZgUU36V.IAJU8G");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ListId",
                table: "Books",
                column: "ListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_ReadingLists_ListId",
                table: "Books",
                column: "ListId",
                principalTable: "ReadingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
