using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorRPG.Server.Migrations
{
    public partial class users1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users1",
                table: "Users1",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users1",
                table: "Users1");

            migrationBuilder.RenameTable(
                name: "Users1",
                newName: "Users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }
    }
}
