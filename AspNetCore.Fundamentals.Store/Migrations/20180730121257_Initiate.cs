using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Fundamentals.Store.Migrations
{
    public partial class Initiate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "HR");

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "HR",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IdNo = table.Column<string>(maxLength: 10, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Address_City = table.Column<string>(maxLength: 100, nullable: false),
                    Address_Line1 = table.Column<string>(maxLength: 200, nullable: false),
                    Address_Line2 = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UniqueIdNo",
                schema: "HR",
                table: "Employees",
                column: "IdNo",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees",
                schema: "HR");
        }
    }
}
