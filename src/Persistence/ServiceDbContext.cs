namespace Linn.SalesAccounts.Persistence
{
    using Linn.Common.Configuration;
    using Linn.SalesAccounts.Domain;
    using Linn.SalesAccounts.Domain.Activities.SalesAccounts;

    using Microsoft.EntityFrameworkCore;

    public class ServiceDbContext : DbContext
    {
        public DbSet<SalesAccount> SalesAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SalesAccount>().Property(s => s.Id).ValueGeneratedNever();
            builder.Entity<SalesAccount>().HasMany(s => s.Activities);

            builder.Entity<SalesAccountActivity>().HasKey(a => a.Id);
            builder.Entity<SalesAccountActivity>().HasDiscriminator<string>("ActivityType")
                .HasValue<SalesAccountCreateActivity>("create").HasValue<SalesAccountCloseActivity>("close")
                .HasValue<SalesAccountUpdateRebateActivity>("update-rebate")
                .HasValue<SalesAccountUpdateGoodCreditActivity>("update-good-credit")
                .HasValue<SalesAccountUpdateDiscountSchemeUriActivity>("update-discount-scheme")
                .HasValue<SalesAccountUpdateTurnoverBandUriActivity>("update-turnover-band")
                .HasValue<SalesAccountGrowthPartnerActivity>("update-growth-partner")
                .HasValue<SalesAccountUpdateNameActivity>("update-name");

            builder.Entity<SalesAccountCreateActivity>().HasBaseType<SalesAccountActivity>();
            builder.Entity<SalesAccountUpdateGoodCreditActivity>().HasBaseType<SalesAccountActivity>();
            builder.Entity<SalesAccountUpdateRebateActivity>().HasBaseType<SalesAccountActivity>();
            builder.Entity<SalesAccountUpdateDiscountSchemeUriActivity>().HasBaseType<SalesAccountActivity>();
            builder.Entity<SalesAccountUpdateTurnoverBandUriActivity>().HasBaseType<SalesAccountActivity>();
            builder.Entity<SalesAccountUpdateNameActivity>().HasBaseType<SalesAccountActivity>();
            builder.Entity<SalesAccountGrowthPartnerActivity>().HasBaseType<SalesAccountActivity>();

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var host = ConfigurationManager.Configuration["DATABASE_HOST"];
            var databaseName = ConfigurationManager.Configuration["DATABASE_NAME"];
            var userId = ConfigurationManager.Configuration["DATABASE_USER_ID"];
            var password = ConfigurationManager.Configuration["DATABASE_PASSWORD"];

            optionsBuilder.UseNpgsql($"User ID={userId};Password={password};Host={host};Database={databaseName};Port=5432;Pooling=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
