using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniMarket.Identity.Migrations
{
    /// <inheritdoc />
    public partial class dfrgty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05446344-f9cc-4566-bd2c-36791b4e28ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b1e2064-d473-4851-bd06-6a60005b71f0", "AQAAAAEAACcQAAAAED5z...", "83750391-8ba5-40d4-9592-3c02c5e086f6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c86be7a2-a26b-4c40-bfee-2635d4b4ddd1", "AQAAAAEAACcQAAAAED5z...", "2a68576f-fef7-4eeb-b63a-939213dea824" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05446344-f9cc-4566-bd2c-36791b4e28ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a4f400ae-fb86-4c37-8461-c82dc0024ff2", "AQAAAAIAAYagAAAAENTKHFcyAGupZ/dcuF+aHrcaLTs8+n6SmYipTeqi+nuOPL+EiwSoDuM1ZPbnvEHewQ==", "5dc3daf2-6593-4518-98ae-b9638d8c2941" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8e35b06a-9104-455e-879e-01cc61f6d33f", "AQAAAAIAAYagAAAAECAjgZ2p/1lwqcoi7x1BGQiill2fZoGrzdiK/D+bMVC2nLl3NRi+hofmmn5NgizZhg==", "91dfc5f8-e610-4e8b-8767-06ac9cc3f036" });
        }
    }
}
