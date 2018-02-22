using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Linn.SalesAccounts.Persistence.Migrations
{
    public partial class address : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "SalesAccounts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "SalesAccountActivity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalesAccountAddress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CountryUri = table.Column<string>(nullable: true),
                    Line1 = table.Column<string>(nullable: true),
                    Line2 = table.Column<string>(nullable: true),
                    Line3 = table.Column<string>(nullable: true),
                    Line4 = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesAccountAddress", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesAccounts_AddressId",
                table: "SalesAccounts",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesAccountActivity_AddressId",
                table: "SalesAccountActivity",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesAccountActivity_SalesAccountAddress_AddressId",
                table: "SalesAccountActivity",
                column: "AddressId",
                principalTable: "SalesAccountAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesAccounts_SalesAccountAddress_AddressId",
                table: "SalesAccounts",
                column: "AddressId",
                principalTable: "SalesAccountAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesAccountActivity_SalesAccountAddress_AddressId",
                table: "SalesAccountActivity");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesAccounts_SalesAccountAddress_AddressId",
                table: "SalesAccounts");

            migrationBuilder.DropTable(
                name: "SalesAccountAddress");

            migrationBuilder.DropIndex(
                name: "IX_SalesAccounts_AddressId",
                table: "SalesAccounts");

            migrationBuilder.DropIndex(
                name: "IX_SalesAccountActivity_AddressId",
                table: "SalesAccountActivity");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "SalesAccounts");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "SalesAccountActivity");
        }
    }
}
