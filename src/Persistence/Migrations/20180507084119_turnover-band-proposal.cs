using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Linn.SalesAccounts.Persistence.Migrations
{
    public partial class turnoverbandproposal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasedOnFinancialYear",
                table: "SalesAccountActivity",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProposedTurnoverBands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    AppliedToAccount = table.Column<bool>(nullable: false),
                    CalculatedTurnoverBandUri = table.Column<string>(nullable: true),
                    FinancialYear = table.Column<string>(nullable: true),
                    IncludeInUpdate = table.Column<bool>(nullable: false),
                    ProposedTurnoverBandUri = table.Column<string>(nullable: true),
                    SalesAccountId = table.Column<int>(nullable: true),
                    SalesValueBase = table.Column<decimal>(nullable: false),
                    SalesValueCurrency = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProposedTurnoverBands", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProposedTurnoverBands_SalesAccounts_SalesAccountId",
                        column: x => x.SalesAccountId,
                        principalTable: "SalesAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProposedTurnoverBands_SalesAccountId",
                table: "ProposedTurnoverBands",
                column: "SalesAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProposedTurnoverBands");

            migrationBuilder.DropColumn(
                name: "BasedOnFinancialYear",
                table: "SalesAccountActivity");
        }
    }
}
