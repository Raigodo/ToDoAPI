using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoList.API.Migrations
{
    public partial class SeedingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37e28bcb-1500-416f-b53b-ccbbda040e0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86e88717-1111-4655-a31b-c21d2fcf0626");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "876a8037-916f-4a21-b67c-a4d0dba4177a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e33cf38d-53dd-4ec3-bfb1-7840de5d7351");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3faf7475-51a3-41df-b9f8-11f70e55d025", "879b9718-f9b8-4283-8443-0bfb7b88a491", "ApiAdmin", "APIADMIN" },
                    { "a19759fa-5794-4127-8453-e3b81e0b0a6f", "405d0567-91c9-4ed0-a184-ee06e6f7e65e", "GroupMember", "GROUPMEMBER" },
                    { "ae26a502-f940-4515-971c-dd9dd811a044", "d287954d-e4d1-45d0-bdd0-ee675ba1667d", "GroupAdmin", "GROUPADMIN" },
                    { "b1dddcd4-7f1e-4f7b-92b2-4ca675e522e8", "bb0bb436-cc6f-4911-b562-70bdfac1515e", "ApiUser", "APIUSER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3faf7475-51a3-41df-b9f8-11f70e55d025");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a19759fa-5794-4127-8453-e3b81e0b0a6f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae26a502-f940-4515-971c-dd9dd811a044");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1dddcd4-7f1e-4f7b-92b2-4ca675e522e8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37e28bcb-1500-416f-b53b-ccbbda040e0f", "35824157-5b5b-4b52-872b-6aad8859f4d7", "Member", "MEMBER" },
                    { "86e88717-1111-4655-a31b-c21d2fcf0626", "082ca92d-c0f8-4c11-bde7-0eb02062ea10", "User", "USER" },
                    { "876a8037-916f-4a21-b67c-a4d0dba4177a", "a20836de-eb27-41df-ab9f-a376d8993b35", "Admin", "ADMIN" },
                    { "e33cf38d-53dd-4ec3-bfb1-7840de5d7351", "dbfdd810-8320-4c3a-83df-73d079a2c0dc", "Admin", "ADMIN" }
                });
        }
    }
}
