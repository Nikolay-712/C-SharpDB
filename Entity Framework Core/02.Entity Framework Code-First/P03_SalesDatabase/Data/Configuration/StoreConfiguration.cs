namespace P03_SalesDatabase.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder
                .HasKey(s => s.StoreId);

            builder
                .Property(s => s.Name)
                .HasMaxLength(80)
                .IsRequired(true)
                .IsUnicode(true);
        }
    }
}
