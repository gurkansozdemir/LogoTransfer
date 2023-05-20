using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogoTransfer.Repository.Migrations
{
    /// <inheritdoc />
    public partial class EditOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "OrderNo",
                table: "Orders",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "CustomerLastName",
                table: "Orders",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "CustomerFirstName",
                table: "Orders",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Orders",
                newName: "CustomerSurName");

            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Orders",
                newName: "Date_");

            migrationBuilder.AlterColumn<double>(
                name: "Amount",
                table: "Orders",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "AuxilCode",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CurrTransaction",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "RcXrate",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TcXrate",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "OrderTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MasterCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TransDescripntion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitConv1 = table.Column<double>(type: "float", nullable: false),
                    UnitConv2 = table.Column<double>(type: "float", nullable: false),
                    CurrTrans = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TcXrate = table.Column<double>(type: "float", nullable: false),
                    VatRate = table.Column<double>(type: "float", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTransactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("45456c11-f1f1-447b-a55d-c8f4110da3fe"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 20, 15, 44, 50, 890, DateTimeKind.Local).AddTicks(1565));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7e212bbe-3059-464f-be67-ec8064063f6b"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 20, 15, 44, 50, 890, DateTimeKind.Local).AddTicks(1572));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2f9cba8-d1ab-477d-91cf-caf4ba435b83"),
                column: "CreatedOn",
                value: new DateTime(2023, 5, 20, 15, 44, 50, 890, DateTimeKind.Local).AddTicks(1941));

            migrationBuilder.CreateIndex(
                name: "IX_OrderTransactions_OrderId",
                table: "OrderTransactions",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTransactions");

            migrationBuilder.DropColumn(
                name: "AuxilCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CurrTransaction",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RcXrate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TcXrate",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Orders",
                newName: "OrderNo");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Orders",
                newName: "CustomerLastName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "CustomerFirstName");

            migrationBuilder.RenameColumn(
                name: "Date_",
                table: "Orders",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "CustomerSurName",
                table: "Orders",
                newName: "Currency");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceRatio = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");
        }
    }
}
