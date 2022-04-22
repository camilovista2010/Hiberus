using Hiberus.Model.Models.HiberusEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hiberus.DataAccessLayer.DataContext.ConfigBuilder
{
    [Keyless]
    public class RateConfig
    {
        public RateConfig(EntityTypeBuilder<Rate> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(r => r.Id);
            entityTypeBuilder.Property(r => r.From).HasMaxLength(45).IsRequired();
            entityTypeBuilder.Property(r => r.To).HasMaxLength(45).IsRequired();
            entityTypeBuilder.Property(r => r.rate).IsRequired();
        }
    }
}
