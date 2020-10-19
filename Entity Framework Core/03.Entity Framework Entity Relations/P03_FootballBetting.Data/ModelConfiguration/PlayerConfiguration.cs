namespace P03_FootballBetting.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> player)
        {
            player.HasKey(p => p.PlayerId);

            player
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

            player
                .Property(p => p.SquadNumber)
                .IsRequired(true);


            player
                .HasOne(x => x.Team)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.TeamId);
          

            player
                .HasOne(x => x.Position)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.PositionId);

            player
                .Property(p => p.IsInjured)
                .HasDefaultValue(false);
        }
    }
}
