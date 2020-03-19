using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantPosApp.Migrations
{
    public partial class AddedListOfOrderedItemsToPlacedOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02cbb000-60f0-45dc-8264-b95039bfde6d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3e1dab9-add2-4211-8380-b4a90fc3a809");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "25f7ac36-4863-40ae-9b6a-f44339cd3a50", "c0a59624-9ca7-49c1-b7b9-92f7c9515381", "Owner", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "63d8d48d-bb9a-4119-8113-e48b5bad87eb", "e05404c5-a81c-46f5-98d4-00422d06566d", "Employee", "EMPLOYEE" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25f7ac36-4863-40ae-9b6a-f44339cd3a50");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "63d8d48d-bb9a-4119-8113-e48b5bad87eb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f3e1dab9-add2-4211-8380-b4a90fc3a809", "95dd16b3-6440-4899-ac4e-55cd5b52026e", "Owner", "CUSTOMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "02cbb000-60f0-45dc-8264-b95039bfde6d", "fc71f0e2-2cf9-48f0-ad97-0612d112bf99", "Employee", "EMPLOYEE" });
        }
    }
}
