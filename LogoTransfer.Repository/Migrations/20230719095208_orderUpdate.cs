using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoTransfer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class orderUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaxNo",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxOffice",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TckNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 19, 12, 52, 8, 90, DateTimeKind.Local).AddTicks(2473));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 19, 12, 52, 8, 90, DateTimeKind.Local).AddTicks(2475));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 19, 12, 52, 8, 90, DateTimeKind.Local).AddTicks(2593));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxNo",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TaxOffice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TckNumber",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 10, 15, 7, 35, 514, DateTimeKind.Local).AddTicks(2683));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 10, 15, 7, 35, 514, DateTimeKind.Local).AddTicks(2687));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 7, 10, 15, 7, 35, 514, DateTimeKind.Local).AddTicks(2957));
        }
    }
}
