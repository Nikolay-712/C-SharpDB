using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Models.Models;

namespace PetStore.Data.ModelConfiguration
{
    public class ToyConfiguration : IEntityTypeConfiguration<Toy>
    {
        public void Configure(EntityTypeBuilder<Toy> toy)
        {
            toy.HasKey(x => x.Id);
          

            toy
                .Property(x => x.ToyName)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            toy
                .Property(x => x.Description)
                .HasMaxLength(1000)
                .IsUnicode(true);

          
        }
    }
}
