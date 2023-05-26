using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToDoList.API.Migrations
{
    public partial class IdentityEdit_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiGroupsUsers_ApiUsers_UserId",
                table: "ApiGroupsUsers");

            migrationBuilder.DropTable(
                name: "ApiUsers");

            migrationBuilder.AddColumn<string>(
                name: "Nickname",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ApiGroupsUsers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_ApiGroupsUsers_AspNetUsers_UserId",
                table: "ApiGroupsUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApiGroupsUsers_AspNetUsers_UserId",
                table: "ApiGroupsUsers");

            migrationBuilder.DropColumn(
                name: "Nickname",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "ApiGroupsUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "ApiUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nickname = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiUsers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ApiGroupsUsers_ApiUsers_UserId",
                table: "ApiGroupsUsers",
                column: "UserId",
                principalTable: "ApiUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
