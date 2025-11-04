using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCascadeDelete : Migration
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$ZnzvKLqafNLyvTr5U0dFiusJZErC4oT9U8r6fzgg0jRSD4soRFqqG");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$LzKuH4GVMrFD/bvl6YREzeMT4lB3QEza7no1pVmV59aeMRpTfXBYm");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$LmIDsjg5EtLFN3z9zY7Td.Vuk5ReV3tBxF9erj.vS2BW3O75hqszC");

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$2VpNSVIhqk/IxJPzQTS4Y.N5.JPv/QjEpHHOmy8U/FBUEUbbZTMam");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "Password",
                value: "$2a$11$NpV0Bt34G87hJf2QpNvur.sPSavQi6kJ2063CArrPvMxwI.M6rHJ2");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                column: "Password",
                value: "$2a$11$6Vy0hkc/42kV4DzmKP5OyerKtTuacCidh91VtFOn7IqqMgh89D.Ku");

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
    }
}
