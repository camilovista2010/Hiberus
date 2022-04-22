using Hiberus.Model.Models.HiberusEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiberus.DataAccessLayer.DataContext.ConfigBuilder
{
    [Keyless]
    public class TransactionConfig
    {
        public TransactionConfig(EntityTypeBuilder<Transaction> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(r => r.Id);
            entityTypeBuilder.Property(r => r.Sku).HasMaxLength(45).IsRequired();
            entityTypeBuilder.Property(r => r.Amount).IsRequired();
            entityTypeBuilder.Property(r => r.Currency).HasMaxLength(45).IsRequired();
        }
    }
}
