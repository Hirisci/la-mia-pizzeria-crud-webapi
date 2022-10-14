using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace La_mia_pizzeria_refactoring.Migrations
{
    public partial class visible : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Pizzas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Pizzas");
        }
    }
}
