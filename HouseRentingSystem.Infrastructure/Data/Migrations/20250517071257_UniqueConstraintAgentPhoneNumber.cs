using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UniqueConstraintAgentPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "665575be-21d2-4f3c-9233-1b05591fe300", "AQAAAAIAAYagAAAAENNAcvLJOq4FPP0la0TdGrHQkS8y1mJ0QTM2+MvQ2e5+WsQ/nKKPLTQxdrdPWI6tkg==", "0fde9a90-8e96-49e9-aa6e-03e55e012666" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "add0e0b1-6b44-42e1-acb1-d6550b030aea", "AQAAAAIAAYagAAAAEKqaC0h21BaH8OL00yify4uYmytg5HT3AUiIFiJvlQl5lBtF6KMwqDe6IL6usMJl0Q==", "19a64f81-0dfc-4f6a-a8a6-4e54973884f8" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d7d6c511-a535-4e6f-92e6-5a21162a2fa4", "AQAAAAIAAYagAAAAEBSFhv0AWXpWO3fo6GCa3nDx5s9St7ig5/HfYR8FjEiDgdXmHt0zAxrXRZe6Y6EGKg==", "b565215b-f9ae-43bd-82ff-9416cdab4e72" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a51cc7b7-428e-4b07-ba29-5b91ee47319c", "AQAAAAIAAYagAAAAEF2E4pSYoItyYXL7dXCYp+yqTkCPJzwuneBJ52F2GRU2FREu6ycTWErWYLvr0h6qWA==", "73edf650-0494-482f-9dc6-5d9340910454" });
        }
    }
}
