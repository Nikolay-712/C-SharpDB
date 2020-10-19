using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetStore.Models.Models;

namespace PetStore.Data.ModelConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(x => x.Id);

            user
                .Property(x => x.FirstName)
                .HasMaxLength(40)
                .IsRequired(true)
                .IsUnicode(true);

            user
                .Property(x => x.LastName)
                .HasMaxLength(40)
                .IsRequired(true)
                .IsUnicode(true);

            user
                .Property(x => x.Email)
                .HasMaxLength(50)
                .IsRequired(true);

            
                
        }
    }
}
