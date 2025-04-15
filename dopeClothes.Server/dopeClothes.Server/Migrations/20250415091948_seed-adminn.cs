using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dopeClothes.Server.Migrations
{
    /// <inheritdoc />
    public partial class seedadminn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "22222222-2222-2222-2222-222222222222", 0, "82a6d107-7290-4722-8e7d-805095196bfd", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN123", "AQAAAAIAAYagAAAAEAMK7r5BL2pV9AHAJRWqSodx1/tDrTbOKB6xECp+m3PHhU8smUeZDnjjtdoY2vJE/g==", null, false, "4ae9da26-fa89-45ec-a74e-4f841ff002fc", false, "admin123" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "11111111-1111-1111-1111-111111111111", "22222222-2222-2222-2222-222222222222" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "11111111-1111-1111-1111-111111111111", "22222222-2222-2222-2222-222222222222" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "11111111-1111-1111-1111-111111111111");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "22222222-2222-2222-2222-222222222222");

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
    }
}
