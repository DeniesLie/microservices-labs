using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("03dec121-ac0e-4189-bd9e-68013cd175f9"), "Javelin" },
                    { new Guid("7c164a1e-84ab-44b9-9542-9c0a681172a7"), "NLAW" },
                    { new Guid("8f515945-5f20-4acb-95fc-b0aa2eb91497"), "Switchblade 300" }
                });

            migrationBuilder.InsertData(
                table: "Storages",
                column: "Id",
                values: new object[]
                {
                    new Guid("15ec584d-b6fb-4fd6-b064-76cc9e71f2e5"),
                    new Guid("a872e805-9c4e-4589-af4c-8ed0af421b4c")
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "ItemId", "Notes", "StorageId", "Type" },
                values: new object[] { new Guid("1cf2eaab-050f-471c-b45f-cd082efcf8bd"), 204, new Guid("03dec121-ac0e-4189-bd9e-68013cd175f9"), null, new Guid("15ec584d-b6fb-4fd6-b064-76cc9e71f2e5"), 0 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "ItemId", "Notes", "StorageId", "Type" },
                values: new object[] { new Guid("84dbec22-7dc7-4319-9735-88029331fc8d"), 103, new Guid("7c164a1e-84ab-44b9-9542-9c0a681172a7"), null, new Guid("a872e805-9c4e-4589-af4c-8ed0af421b4c"), 0 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "ItemId", "Notes", "StorageId", "Type" },
                values: new object[] { new Guid("e6517314-65d4-407e-9d77-f148300701bc"), 20, new Guid("8f515945-5f20-4acb-95fc-b0aa2eb91497"), null, new Guid("15ec584d-b6fb-4fd6-b064-76cc9e71f2e5"), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ItemId",
                table: "Transactions",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StorageId",
                table: "Transactions",
                column: "StorageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Storages");
        }
    }
}
