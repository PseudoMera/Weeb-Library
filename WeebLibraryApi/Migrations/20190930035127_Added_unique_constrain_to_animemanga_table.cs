using Microsoft.EntityFrameworkCore.Migrations;

namespace WeebLibraryApi.Migrations
{
    public partial class Added_unique_constrain_to_animemanga_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AnimeMangas_MalCode",
                table: "AnimeMangas",
                column: "MalCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AnimeMangas_MalCode",
                table: "AnimeMangas");
        }
    }
}
