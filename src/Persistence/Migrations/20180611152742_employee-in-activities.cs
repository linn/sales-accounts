using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Linn.SalesAccounts.Persistence.Migrations
{
    public partial class employeeinactivities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UpdatedByUri",
                table: "SalesAccountActivity",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedByUri",
                table: "SalesAccountActivity");
        }
    }
}
