using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoTransfer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class TransferStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "TransferStatus",
                table: "Orders",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 27, 15, 58, 29, 402, DateTimeKind.Local).AddTicks(1006));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 27, 15, 58, 29, 402, DateTimeKind.Local).AddTicks(1010));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 27, 15, 58, 29, 402, DateTimeKind.Local).AddTicks(1264));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TransferStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 21, 12, 13, 26, 332, DateTimeKind.Local).AddTicks(6186));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 21, 12, 13, 26, 332, DateTimeKind.Local).AddTicks(6190));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 21, 12, 13, 26, 332, DateTimeKind.Local).AddTicks(6484));
        }
    }
}
