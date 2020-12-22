using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EReceipt.DAL.Migrations
{
    public partial class AddPictureForMedicament : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                table: "Medicaments",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "Medicaments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Medicaments");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Medicaments");
        }
    }
}
