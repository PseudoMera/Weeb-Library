using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeebLibraryApi.Migrations
{
    public partial class Added_registerdate_to_usertable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "Users",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "Users");
        }
    }
}
