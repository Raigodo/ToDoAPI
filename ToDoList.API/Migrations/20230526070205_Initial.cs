using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToDoList.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiGroups", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "ApiTaskBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AssociatedGroupId = table.Column<int>(type: "integer", nullable: false),
                    ParrentBoxId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiTaskBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiTaskBoxes_ApiGroups_AssociatedGroupId",
                        column: x => x.AssociatedGroupId,
                        principalTable: "ApiGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiTaskBoxes_ApiTaskBoxes_ParrentBoxId",
                        column: x => x.ParrentBoxId,
                        principalTable: "ApiTaskBoxes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ApiGroupsUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiGroupsUsers", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ApiGroupsUsers_ApiGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "ApiGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiGroupsUsers_ApiUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApiUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApiTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ParrentBoxId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApiTasks_ApiTaskBoxes_ParrentBoxId",
                        column: x => x.ParrentBoxId,
                        principalTable: "ApiTaskBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiGroupsUsers_GroupId",
                table: "ApiGroupsUsers",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiTaskBoxes_AssociatedGroupId",
                table: "ApiTaskBoxes",
                column: "AssociatedGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiTaskBoxes_ParrentBoxId",
                table: "ApiTaskBoxes",
                column: "ParrentBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_ApiTasks_ParrentBoxId",
                table: "ApiTasks",
                column: "ParrentBoxId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiGroupsUsers");

            migrationBuilder.DropTable(
                name: "ApiTasks");

            migrationBuilder.DropTable(
                name: "ApiUsers");

            migrationBuilder.DropTable(
                name: "ApiTaskBoxes");

            migrationBuilder.DropTable(
                name: "ApiGroups");
        }
    }
}
