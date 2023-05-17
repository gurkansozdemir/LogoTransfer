using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoTransfer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InserLogoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LogoUser",
                table: "LogoUser");

            migrationBuilder.RenameTable(
                name: "LogoUser",
                newName: "LogoUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogoUsers",
                table: "LogoUsers",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 17, 21, 53, 47, 828, DateTimeKind.Local).AddTicks(9391));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 17, 21, 53, 47, 828, DateTimeKind.Local).AddTicks(9398));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 17, 21, 53, 47, 829, DateTimeKind.Local).AddTicks(1576));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LogoUsers",
                table: "LogoUsers");

            migrationBuilder.RenameTable(
                name: "LogoUsers",
                newName: "LogoUser");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LogoUser",
                table: "LogoUser",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 17, 21, 51, 31, 678, DateTimeKind.Local).AddTicks(9617));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 17, 21, 51, 31, 678, DateTimeKind.Local).AddTicks(9626));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 17, 21, 51, 31, 679, DateTimeKind.Local).AddTicks(1386));
        }
    }
}
