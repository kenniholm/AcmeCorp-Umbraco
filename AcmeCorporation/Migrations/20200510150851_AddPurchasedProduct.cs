using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AcmeCorporation.Migrations
{
    public partial class AddPurchasedProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "ProductSerial",
                table: "Submission",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProductSerial",
                table: "Submission",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
