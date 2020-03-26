using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantPosApp.Migrations
{
    public partial class PascalCasingOwnerIdProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_userId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AspNetUsers_userId",
                table: "Owners");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "689f22e5-9053-4281-8819-45ccffd25ebc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8a9cf92-7709-4130-978e-b014647fc3c9");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Owners",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Owners_userId",
                table: "Owners",
                newName: "IX_Owners_UserId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Employees",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_userId",
                table: "Employees",
                newName: "IX_Employees_UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "00b59690-292d-443f-a4e0-21b120bca757", "a94ffec3-262e-415e-9ab1-21498d4d8c24", "Owner", "OWNER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fb5ac9b7-9df0-4df7-a846-72504f435f78", "9e5d281d-4b86-4c0a-90e2-1227c5500cba", "Employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AspNetUsers_UserId",
                table: "Owners",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_AspNetUsers_UserId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_AspNetUsers_UserId",
                table: "Owners");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00b59690-292d-443f-a4e0-21b120bca757");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb5ac9b7-9df0-4df7-a846-72504f435f78");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Owners",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Owners_UserId",
                table: "Owners",
                newName: "IX_Owners_userId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Employees",
                newName: "userId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_UserId",
                table: "Employees",
                newName: "IX_Employees_userId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "689f22e5-9053-4281-8819-45ccffd25ebc", "7435f67d-074e-4f26-8290-930f05d9dbdf", "Owner", "OWNER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8a9cf92-7709-4130-978e-b014647fc3c9", "7bfff54e-8860-4a23-b376-747ee59e85d7", "Employee", "EMPLOYEE" });

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_AspNetUsers_userId",
                table: "Employees",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_AspNetUsers_userId",
                table: "Owners",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
