using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Repository.Migrations
{
    public partial class Step2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SerialNumber",
                table: "LightPoles",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SerialNumber",
                table: "LightPoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
