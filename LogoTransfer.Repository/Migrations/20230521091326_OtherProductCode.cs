using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoTransfer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class OtherProductCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtherCode",
                table: "OrderTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherCode",
                table: "OrderTransactions");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 21, 2, 16, 43, 5, DateTimeKind.Local).AddTicks(7012));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 21, 2, 16, 43, 5, DateTimeKind.Local).AddTicks(7017));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 21, 2, 16, 43, 5, DateTimeKind.Local).AddTicks(7253));
        }
    }
}
