using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tamagotchi.Migrations
{
    public partial class PetLastInteractedWithDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastInteractedWithDate",
                table: "Pets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastInteractedWithDate",
                table: "Pets");
        }
    }
}
