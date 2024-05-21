using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Identity.Migrations
{
    /// <inheritdoc />
    public partial class UserClaimsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "admin@localhost.com", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "admin@localhost.com", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "8e445865-a24d-4543-a6c6-9443d048cdb9", "8e445865-a24d-4543-a6c6-9443d048cdb9" },
                    { 4, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "8e445865-a24d-4543-a6c6-9443d048cdb9" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a823ac5-4f94-4228-9472-a6bb378fd304", "AQAAAAIAAYagAAAAEGOcrYAhAOwlH3d8PQilwGKNf6BzTokrXo1AQEqpfi1CB9BJ0o6GXJUTzJqZDmjucg==", "8add2fe5-eb2f-4cef-ba30-bd43e7ed1953" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "626de8e4-b591-43ec-a755-774cc23e1ebc", "AQAAAAIAAYagAAAAEBDxAahkMIeCHHrin/mE2MC+YxodA2Tofunh9OyVLW3vhGhDs+Sl5Cw9k+lDcFJ3dQ==", "be079834-b04e-4daf-a4b4-455233d8f022" });
        }
    }
}
