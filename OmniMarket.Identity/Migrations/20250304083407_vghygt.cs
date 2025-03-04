using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OmniMarket.Identity.Migrations
{
    /// <inheritdoc />
    public partial class vghygt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "05446344-f9cc-4566-bd2c-36791b4e28ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6153aba-b6d3-486d-9c61-5d2c62b74ee1", "AQAAAAIAAYagAAAAECOeVqCC7279baMQze+oYX7RGsp3Eoo3SW/vP2IE43GEfzp3gfdqs3QM0ML2p75cJg==", "e3ceef7a-dca1-4726-abb4-894a2123fa16" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ec9f480-7288-4d0f-a1cd-53cc89968b45",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "316cd61e-ef68-4103-9ab0-f5f079f770c8", "AQAAAAIAAYagAAAAELckfkTZrtktmrkghYbmggE+H8alnw18gVHclbMAhWOieb6WFJDuINfkq5clencTvw==", "0c0047fa-f51b-4176-80e7-a2b5cc204175" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
