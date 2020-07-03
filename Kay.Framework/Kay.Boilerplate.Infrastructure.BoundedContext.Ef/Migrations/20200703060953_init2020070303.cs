using Microsoft.EntityFrameworkCore.Migrations;

namespace Kay.Boilerplate.Infrastructure.BoundedContext.Ef.Migrations
{
    public partial class init2020070303 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "TbItemSku",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "TbItemSku");
        }
    }
}
