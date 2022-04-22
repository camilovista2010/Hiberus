using Microsoft.EntityFrameworkCore;
using Hiberus.DataAccessLayer.DataContext.ConfigBuilder;
using Hiberus.Model.Models.HiberusEntity;

namespace Hiberus.DataAccessLayer.DataContext
{
    public class HiberusDbContext : DbContext
    {
        #region DbSet
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Rate> Rates { get; set; }

        #endregion
        public HiberusDbContext(DbContextOptions<HiberusDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("hiberus");
            new RateConfig(modelBuilder.Entity<Rate>());
            new TransactionConfig(modelBuilder.Entity<Transaction>());
            base.OnModelCreating(modelBuilder);
        }
    }
}
