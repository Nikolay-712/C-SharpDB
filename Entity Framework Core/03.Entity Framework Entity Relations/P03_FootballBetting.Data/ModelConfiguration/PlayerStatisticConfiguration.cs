namespace P03_FootballBetting.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;

    public class PlayerStatisticConfiguration : IEntityTypeConfiguration<PlayerStatistic>
    {
        public void Configure(EntityTypeBuilder<PlayerStatistic> playerStatistic)
        {
            playerStatistic
                .HasKey(ps => new { ps.PlayerId, ps.GameId });

            playerStatistic
                .HasOne(p => p.Player)
                .WithMany(s => s.PlayerStatistics)
                .HasForeignKey(p => p.PlayerId)
                .OnDelete(DeleteBehavior.Restrict);

            playerStatistic
                .HasOne(s => s.Game)
                .WithMany(x => x.PlayerStatistics)
                .HasForeignKey(p => p.GameId)
                .OnDelete(DeleteBehavior.Restrict);


            playerStatistic
                .Property(ps => ps.ScoredGoals)
                .IsRequired(true)
                .HasDefaultValue(0);

            playerStatistic
                .Property(ps => ps.Assists)
                .IsRequired(true)
                .HasDefaultValue(0);

            playerStatistic
                .Property(ps => ps.MinutesPlayed)
                .IsRequired(true)
                .HasDefaultValue(0);
        }
    }
}
