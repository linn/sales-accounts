using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Linn.SalesAccounts.Persistence.Migrations
{
    public partial class anotherfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GrowthPartner",
                table: "SalesAccounts",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "GrowthPartner",
                table: "SalesAccountActivity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GrowthPartner",
                table: "SalesAccounts");

            migrationBuilder.DropColumn(
                name: "GrowthPartner",
                table: "SalesAccountActivity");
        }
    }
}
