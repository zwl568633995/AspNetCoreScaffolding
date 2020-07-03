using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kay.Boilerplate.Infrastructure.BoundedContext.Ef.Migrations
{
    public partial class init2020070302 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbItemSku",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    AddTime = table.Column<DateTime>(nullable: true),
                    ModTime = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    title = table.Column<string>(nullable: true),
                    OriPrice = table.Column<decimal>(nullable: false),
                    DisPrice = table.Column<decimal>(nullable: false),
                    Cashback = table.Column<decimal>(nullable: false),
                    SaleCount = table.Column<int>(nullable: false),
                    StockCount = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbItemSku", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbItemSku");
        }
    }
}
