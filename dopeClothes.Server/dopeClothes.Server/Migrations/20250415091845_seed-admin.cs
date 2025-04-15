using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dopeClothes.Server.Migrations
{
    /// <inheritdoc />
    public partial class seedadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "22e53318-7db9-4610-8611-04bcd42926d8", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "8b4221c4-db18-4aca-8360-59119ebe4323", 0, "d90f33c4-513a-427d-b9c9-b96dd41b9f99", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN123", "AQAAAAIAAYagAAAAELnvj3C93Os0BxlVpZe+KMR2y0w7YV2JqniZuIgmiOYv2sV0DugoDaBe9jRQXeQOtQ==", null, false, "e746bd59-e22e-45a5-99e5-e04bed6d0208", false, "admin123" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "22e53318-7db9-4610-8611-04bcd42926d8", "8b4221c4-db18-4aca-8360-59119ebe4323" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "22e53318-7db9-4610-8611-04bcd42926d8", "8b4221c4-db18-4aca-8360-59119ebe4323" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22e53318-7db9-4610-8611-04bcd42926d8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8b4221c4-db18-4aca-8360-59119ebe4323");
        }
    }
}
