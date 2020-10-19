namespace P03_SalesDatabase.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_SalesDatabase.Data.Models;
    public class CustumerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
                .HasKey(c => c.CustomerId);

            builder
                .Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

            builder
                .Property(c => c.Email)
                .HasMaxLength(80)
                .IsRequired(true);

            builder
                .Property(c => c.CreditCardNumber)
                .HasMaxLength(25)
                .IsRequired(true);

           
        }
    }
}
