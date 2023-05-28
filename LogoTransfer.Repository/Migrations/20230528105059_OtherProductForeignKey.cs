using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoTransfer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class OtherProductForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterCode",
                table: "OrderTransactions");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductMatchingId",
                table: "OrderTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 28, 13, 50, 59, 785, DateTimeKind.Local).AddTicks(2465));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 28, 13, 50, 59, 785, DateTimeKind.Local).AddTicks(2469));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 28, 13, 50, 59, 785, DateTimeKind.Local).AddTicks(2733));

            migrationBuilder.CreateIndex(
                name: "IX_OrderTransactions_ProductMatchingId",
                table: "OrderTransactions",
                column: "ProductMatchingId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTransactions_ProductMatchings_ProductMatchingId",
                table: "OrderTransactions",
                column: "ProductMatchingId",
                principalTable: "ProductMatchings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTransactions_ProductMatchings_ProductMatchingId",
                table: "OrderTransactions");

            migrationBuilder.DropIndex(
                name: "IX_OrderTransactions_ProductMatchingId",
                table: "OrderTransactions");

            migrationBuilder.DropColumn(
                name: "ProductMatchingId",
                table: "OrderTransactions");

            migrationBuilder.AddColumn<string>(
                name: "MasterCode",
                table: "OrderTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 27, 21, 31, 20, 960, DateTimeKind.Local).AddTicks(2398));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 27, 21, 31, 20, 960, DateTimeKind.Local).AddTicks(2403));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 27, 21, 31, 20, 960, DateTimeKind.Local).AddTicks(2743));
        }
    }
}
