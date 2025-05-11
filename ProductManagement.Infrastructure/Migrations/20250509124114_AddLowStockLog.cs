using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.Infrastructure.Migrations
{
    public partial class AddLowStockLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LowStockLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    LoggedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LowStockLogs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LowStockLogs");
        }
    }
}
