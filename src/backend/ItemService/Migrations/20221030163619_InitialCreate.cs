using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ItemService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasureUnitId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "MeasureUnitId", "Name" },
                values: new object[] { new Guid("03dec121-ac0e-4189-bd9e-68013cd175f9"), null, "Javelin" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "MeasureUnitId", "Name" },
                values: new object[] { new Guid("7c164a1e-84ab-44b9-9542-9c0a681172a7"), null, "NLAW" });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "MeasureUnitId", "Name" },
                values: new object[] { new Guid("8f515945-5f20-4acb-95fc-b0aa2eb91497"), null, "Switchblade 300" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
