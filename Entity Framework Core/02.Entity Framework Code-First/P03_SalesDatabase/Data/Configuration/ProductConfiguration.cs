namespace P03_SalesDatabase.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(p => p.ProductId);

            builder
                .Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            builder
                .Property(p => p.Quantity)
                .IsRequired(true);

            builder
                .Property(p => p.Price)
                .IsRequired(true);

            builder
                .Property(p => p.Description)
                .HasMaxLength(250)
                .HasDefaultValue("No description");
        }
    }
}
