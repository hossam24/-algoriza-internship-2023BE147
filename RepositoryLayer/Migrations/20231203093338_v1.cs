using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFLayer.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d10f64a1-4e2c-4fd8-8bda-3f7a57b029ae");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "Image", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "Type", "UserName" },
                values: new object[] { "76f86073-b51c-47c4-b7fa-731628055ebb", 0, "d37a443e-b4a7-456a-8cf2-f91e7431d41c", null, "admin@gmail.com", true, null, null, null, true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEP67oJbqNSLOUeeUOjBJX1oz9wxhrOyuo1ilgByurG0N8eXBqPdENDj2HD4rVCgzgQ==", null, null, false, null, "3fe49b40-216f-4c9f-91c7-a660862f51a1", false, 0, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "76f86073-b51c-47c4-b7fa-731628055ebb");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FullName", "Gender", "Image", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "Type", "UserId", "UserName" },
                values: new object[] { "d10f64a1-4e2c-4fd8-8bda-3f7a57b029ae", 0, "ded9c083-4142-460f-a3db-85b7008141b5", null, "admin@gmail.com", true, null, null, null, true, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEMlLcYUXtB/Do3Am3abzTN4xs3azM0r00L65Xk/O4mPiM+OUxoV4SbwkWAisVkd+0Q==", null, null, false, null, "1cb08cad-8d05-4c87-8842-fe44ac6a5125", false, 0, "76f86073-b51c-47c4-b7fa-731628055ebb", "admin" });
        }
    }
}
