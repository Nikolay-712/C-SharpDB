using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Models.Models;


namespace PetStore.Data.ModelConfiguration
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> pet)
        {
            pet.HasKey(x => x.Id);

            pet
                .Property(x => x.Breed)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

            pet
                .Property(x => x.Description)
                .HasMaxLength(1000)
                .IsUnicode(true);

           
        }
    }
}
