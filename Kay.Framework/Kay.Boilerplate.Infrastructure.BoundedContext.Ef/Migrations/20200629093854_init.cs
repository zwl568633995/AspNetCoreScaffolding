using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kay.Boilerplate.Infrastructure.BoundedContext.Ef.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbAccount",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbCity",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CityCase = table.Column<string>(nullable: true),
                    CityName = table.Column<string>(nullable: true),
                    IsHotCity = table.Column<bool>(nullable: false),
                    Province = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbCity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbItem",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CityId = table.Column<long>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    OriPrice = table.Column<decimal>(nullable: false),
                    DisPrice = table.Column<decimal>(nullable: false),
                    Cashback = table.Column<decimal>(nullable: false),
                    SaleType = table.Column<int>(nullable: false),
                    SaleCount = table.Column<int>(nullable: false),
                    StockCount = table.Column<int>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Introduction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbItemImage",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    ImageSource = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbItemImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbItemShopRelated",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    ShopId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbItemShopRelated", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbShop",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ShopName = table.Column<string>(nullable: true),
                    ShopAddress = table.Column<string>(nullable: true),
                    ShopPhone = table.Column<string>(nullable: true),
                    Longitude = table.Column<double>(nullable: false),
                    Latitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbShop", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbAccount");

            migrationBuilder.DropTable(
                name: "TbCity");

            migrationBuilder.DropTable(
                name: "TbItem");

            migrationBuilder.DropTable(
                name: "TbItemImage");

            migrationBuilder.DropTable(
                name: "TbItemShopRelated");

            migrationBuilder.DropTable(
                name: "TbShop");
        }
    }
}
