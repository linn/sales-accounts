﻿// <auto-generated />
using Linn.SalesAccounts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Linn.SalesAccounts.Persistence.Migrations
{
    [DbContext(typeof(ServiceDbContext))]
    [Migration("20180511153128_generate-id")]
    partial class generateid
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActivityType")
                        .IsRequired();

                    b.Property<DateTime>("ChangedOn");

                    b.Property<int?>("SalesAccountId");

                    b.HasKey("Id");

                    b.HasIndex("SalesAccountId");

                    b.ToTable("SalesAccountActivity");

                    b.HasDiscriminator<string>("ActivityType").HasValue("SalesAccountActivity");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.ProposedTurnoverBand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AppliedToAccount");

                    b.Property<string>("CalculatedTurnoverBandUri");

                    b.Property<string>("FinancialYear");

                    b.Property<bool>("IncludeInUpdate");

                    b.Property<string>("ProposedTurnoverBandUri");

                    b.Property<int?>("SalesAccountId");

                    b.Property<decimal>("SalesValueBase");

                    b.Property<decimal>("SalesValueCurrency");

                    b.HasKey("Id");

                    b.HasIndex("SalesAccountId");

                    b.ToTable("ProposedTurnoverBands");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.SalesAccount", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int?>("AddressId");

                    b.Property<DateTime?>("ClosedOn");

                    b.Property<string>("DiscountSchemeUri");

                    b.Property<bool>("EligibleForGoodCreditDiscount");

                    b.Property<bool>("EligibleForRebate");

                    b.Property<bool>("GrowthPartner");

                    b.Property<string>("Name");

                    b.Property<string>("TurnoverBandUri");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("SalesAccounts");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.SalesAccountAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CountryUri");

                    b.Property<string>("Line1");

                    b.Property<string>("Line2");

                    b.Property<string>("Line3");

                    b.Property<string>("Line4");

                    b.Property<string>("Postcode");

                    b.HasKey("Id");

                    b.ToTable("SalesAccountAddress");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountCloseActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<DateTime>("ClosedOn");

                    b.ToTable("SalesAccountCloseActivity");

                    b.HasDiscriminator().HasValue("close");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountCreateActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<int>("AccountId");

                    b.Property<DateTime?>("ClosedOn")
                        .HasColumnName("SalesAccountCreateActivity_ClosedOn");

                    b.Property<string>("Name");

                    b.ToTable("SalesAccountCreateActivity");

                    b.HasDiscriminator().HasValue("create");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountGrowthPartnerActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<bool>("GrowthPartner");

                    b.ToTable("SalesAccountGrowthPartnerActivity");

                    b.HasDiscriminator().HasValue("update-growth-partner");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateAddressActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<int?>("AddressId");

                    b.HasIndex("AddressId");

                    b.ToTable("SalesAccountUpdateAddressActivity");

                    b.HasDiscriminator().HasValue("update-address");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateDiscountSchemeUriActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<string>("DiscountSchemeUri");

                    b.ToTable("SalesAccountUpdateDiscountSchemeUriActivity");

                    b.HasDiscriminator().HasValue("update-discount-scheme");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateGoodCreditActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<bool>("EligibleForGoodCreditDiscount");

                    b.ToTable("SalesAccountUpdateGoodCreditActivity");

                    b.HasDiscriminator().HasValue("update-good-credit");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateNameActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<string>("Name")
                        .HasColumnName("SalesAccountUpdateNameActivity_Name");

                    b.ToTable("SalesAccountUpdateNameActivity");

                    b.HasDiscriminator().HasValue("update-name");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateRebateActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<bool>("EligibleForRebate");

                    b.ToTable("SalesAccountUpdateRebateActivity");

                    b.HasDiscriminator().HasValue("update-rebate");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateTurnoverBandUriActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity");

                    b.Property<string>("TurnoverBandUri");

                    b.ToTable("SalesAccountUpdateTurnoverBandUriActivity");

                    b.HasDiscriminator().HasValue("update-turnover-band");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountApplyTurnoverBandProposalActivity", b =>
                {
                    b.HasBaseType("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateTurnoverBandUriActivity");

                    b.Property<string>("BasedOnFinancialYear");

                    b.ToTable("SalesAccountApplyTurnoverBandProposalActivity");

                    b.HasDiscriminator().HasValue("apply-turnover-band");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountActivity", b =>
                {
                    b.HasOne("Linn.SalesAccounts.Domain.SalesAccount")
                        .WithMany("Activities")
                        .HasForeignKey("SalesAccountId");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.ProposedTurnoverBand", b =>
                {
                    b.HasOne("Linn.SalesAccounts.Domain.SalesAccount", "SalesAccount")
                        .WithMany()
                        .HasForeignKey("SalesAccountId");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.SalesAccount", b =>
                {
                    b.HasOne("Linn.SalesAccounts.Domain.SalesAccountAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("Linn.SalesAccounts.Domain.Activities.SalesAccounts.SalesAccountUpdateAddressActivity", b =>
                {
                    b.HasOne("Linn.SalesAccounts.Domain.SalesAccountAddress", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });
#pragma warning restore 612, 618
        }
    }
}
