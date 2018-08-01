using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetCore.Fundamentals.Store.Migrations
{
    public partial class AddSalaryPhoneEmailColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "HR",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                schema: "HR",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Salary",
                schema: "HR",
                table: "Employees",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                schema: "HR",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Phone",
                schema: "HR",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Salary",
                schema: "HR",
                table: "Employees");
        }
    }
}
