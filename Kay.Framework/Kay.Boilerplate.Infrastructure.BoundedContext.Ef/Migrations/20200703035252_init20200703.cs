using Microsoft.EntityFrameworkCore.Migrations;

namespace Kay.Boilerplate.Infrastructure.BoundedContext.Ef.Migrations
{
    public partial class init20200703 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cashback",
                table: "TbItem");

            migrationBuilder.DropColumn(
                name: "DisPrice",
                table: "TbItem");

            migrationBuilder.DropColumn(
                name: "OriPrice",
                table: "TbItem");

            migrationBuilder.DropColumn(
                name: "SaleCount",
                table: "TbItem");

            migrationBuilder.DropColumn(
                name: "StockCount",
                table: "TbItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cashback",
                table: "TbItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DisPrice",
                table: "TbItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "OriPrice",
                table: "TbItem",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "SaleCount",
                table: "TbItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockCount",
                table: "TbItem",
                nullable: false,
                defaultValue: 0);
        }
    }
}
