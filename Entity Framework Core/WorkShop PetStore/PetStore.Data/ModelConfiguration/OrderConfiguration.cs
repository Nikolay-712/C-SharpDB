using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Models.Models;


namespace PetStore.Data.ModelConfiguration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> order)
        {
            order.HasKey(x => x.Id);

            order
                .HasOne(x => x.User)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId);

            order
                .HasMany(x => x.Pets)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            order
                .HasMany(x => x.Foods)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            order
                .HasMany(x => x.Toys)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

          
        }
    }
}
