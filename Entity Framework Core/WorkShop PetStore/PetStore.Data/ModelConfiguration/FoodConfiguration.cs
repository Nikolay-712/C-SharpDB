using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Models.Models;

namespace PetStore.Data.ModelConfiguration
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> food)
        {
            food.HasKey(x => x.Id);

            food
                .Property(x => x.FoodName)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            food
                .Property(x => x.Description)
                .HasMaxLength(1000)
                .IsUnicode(true);

           
        }
    }
}
