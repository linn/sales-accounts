using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Linn.SalesAccounts.Persistence.Migrations
{
    public partial class nameupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SalesAccountActivity",
                newName: "SalesAccountUpdateNameActivity_Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SalesAccountActivity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SalesAccountActivity");

            migrationBuilder.RenameColumn(
                name: "SalesAccountUpdateNameActivity_Name",
                table: "SalesAccountActivity",
                newName: "Name");
        }
    }
}
