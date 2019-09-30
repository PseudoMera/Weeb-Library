using Microsoft.EntityFrameworkCore.Migrations;

namespace WeebLibraryApi.Migrations
{
    public partial class Recreating_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnimeMangas",
                table: "UserAnimeMangas");

            migrationBuilder.DropIndex(
                name: "IX_UserAnimeMangas_UserId",
                table: "UserAnimeMangas");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnimeMangas",
                table: "UserAnimeMangas",
                columns: new[] { "UserId", "AnimeMangaId" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeMangas_AnimeMangaId",
                table: "UserAnimeMangas",
                column: "AnimeMangaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnimeMangas",
                table: "UserAnimeMangas");

            migrationBuilder.DropIndex(
                name: "IX_UserAnimeMangas_AnimeMangaId",
                table: "UserAnimeMangas");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnimeMangas",
                table: "UserAnimeMangas",
                columns: new[] { "AnimeMangaId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnimeMangas_UserId",
                table: "UserAnimeMangas",
                column: "UserId");
        }
    }
}
