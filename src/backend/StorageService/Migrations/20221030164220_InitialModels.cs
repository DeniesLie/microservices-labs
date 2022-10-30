using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StorageService.Migrations
{
    public partial class InitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Storages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storages", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("03dec121-ac0e-4189-bd9e-68013cd175f1"), "Winners avenue, 10", "Storage A" });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("7c164a1e-84ab-44b9-9542-9c0a681172a6"), "Magic street, 70/2", "Storage B" });

            migrationBuilder.InsertData(
                table: "Storages",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { new Guid("8f515945-5f20-4acb-95fc-b0aa2eb91490"), "Central square", "Storage C" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Storages");
        }
    }
}
