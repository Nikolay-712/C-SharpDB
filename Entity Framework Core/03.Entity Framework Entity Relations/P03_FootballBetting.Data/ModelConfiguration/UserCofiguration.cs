namespace P03_FootballBetting.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;
    public class UserCofiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user
                .HasKey(u => u.UserId);

            user
                .Property(u => u.Username)
                .HasMaxLength(35)
                .IsRequired(true)
                .IsUnicode(true);

            user
                .Property(u => u.Password)
                .HasMaxLength(30)
                .IsRequired(true);

            user
                .Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired(true);

            user
                .Property(u => u.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

            user
                .Property(u => u.Balance);
        }
    }
}
