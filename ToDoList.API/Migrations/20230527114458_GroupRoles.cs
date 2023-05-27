using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.API.Migrations
{
    public partial class GroupRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "ApiGroupsUsers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "ApiGroupsUsers");
        }
    }
}
