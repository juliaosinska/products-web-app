using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ProductsWebApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedingAuthDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "36523c96-06b3-4d52-9bda-9f02f1ef07cb", "36523c96-06b3-4d52-9bda-9f02f1ef07cb", "SuperAdmin", "SuperAdmin" },
                    { "f23d102f-3ee1-45af-b5fb-1ab8c017e9a7", "f23d102f-3ee1-45af-b5fb-1ab8c017e9a7", "Admin", "Admin" },
                    { "fa04a730-bbae-401b-a46f-9195699fab95", "fa04a730-bbae-401b-a46f-9195699fab95", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11", 0, "eb94998c-d785-421c-b788-edb754dba394", "superadmin@productswebapp.com", false, false, null, "SUPERADMIN@PRODUCTSWEBAPP.COM", "SUPERADMIN@PRODUCTSWEBAPP.COM", "AQAAAAIAAYagAAAAEC3T5lqjjP+VdHYBUbehlCwydYqg7sWHXW3hqUETsF+1X/PJL+d233ud6JgzKjaZfA==", null, false, "1dc3d806-3f78-446d-bca8-a4b2c5a58ccf", false, "superadmin@productswebapp.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "36523c96-06b3-4d52-9bda-9f02f1ef07cb", "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11" },
                    { "f23d102f-3ee1-45af-b5fb-1ab8c017e9a7", "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11" },
                    { "fa04a730-bbae-401b-a46f-9195699fab95", "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "36523c96-06b3-4d52-9bda-9f02f1ef07cb", "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "f23d102f-3ee1-45af-b5fb-1ab8c017e9a7", "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fa04a730-bbae-401b-a46f-9195699fab95", "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "36523c96-06b3-4d52-9bda-9f02f1ef07cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f23d102f-3ee1-45af-b5fb-1ab8c017e9a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa04a730-bbae-401b-a46f-9195699fab95");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "50bb2ba5-c1c1-474e-9a6d-337ae28b2f11");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", "1", "Admin", "Admin" },
                    { "2", "2", "SuperAdmin", "SuperAdmin" },
                    { "3", "3", "User", "User" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "1fe9ca26-4a33-49b1-893b-4658d41b7788", "superadmin@productswebapp.com", false, false, null, "SUPERADMIN@PRODUCTSWEBAPP.COM", "SUPERADMIN@PRODUCTSWEBAPP.COM", "AQAAAAIAAYagAAAAELjYRk6/ctIjjc3aNkNsMW+xdFSASUm/Ff2LTecj2CSXWxlFZJSwYEtyU9k53XYr9g==", null, false, "bbfd1dff-bea7-42da-80f3-837fee643ada", false, "superadmin@productswebapp.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "1" },
                    { "3", "1" }
                });
        }
    }
}
