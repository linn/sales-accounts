using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Linn.SalesAccounts.Persistence.Migrations
{
    public partial class salesaccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountId = table.Column<int>(nullable: false),
                    ClosedOn = table.Column<DateTime>(nullable: true),
                    DiscountSchemeUri = table.Column<string>(nullable: true),
                    EligibleForGoodCreditDiscount = table.Column<bool>(nullable: false),
                    EligibleForRebate = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TurnoverBandUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalesAccountActivity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActivityType = table.Column<string>(nullable: false),
                    ChangedOn = table.Column<DateTime>(nullable: false),
                    SalesAccountId = table.Column<int>(nullable: true),
                    ClosedOn = table.Column<DateTime>(nullable: true),
                    AccountId = table.Column<int>(nullable: true),
                    SalesAccountCreateActivity_ClosedOn = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DiscountSchemeUri = table.Column<string>(nullable: true),
                    EligibleForGoodCreditDiscount = table.Column<bool>(nullable: true),
                    EligibleForRebate = table.Column<bool>(nullable: true),
                    TurnoverBandUri = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesAccountActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesAccountActivity_SalesAccounts_SalesAccountId",
                        column: x => x.SalesAccountId,
                        principalTable: "SalesAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesAccountActivity_SalesAccountId",
                table: "SalesAccountActivity",
                column: "SalesAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesAccountActivity");

            migrationBuilder.DropTable(
                name: "SalesAccounts");
        }
    }
}
