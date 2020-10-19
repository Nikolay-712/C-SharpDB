namespace P03_FootballBetting.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> game)
        {
            game
                .HasKey(g => g.GameId);

            game
                .HasOne(t => t.HomeTeam)
                .WithMany(g => g.HomeGames)
                .HasForeignKey(t => t.HomeTeamId);

            game
                .HasOne(t => t.AwayTeam)
                .WithMany(g => g.AwayGames)
                .HasForeignKey(t => t.AwayTeamId);


            game
                .Property(g => g.AwayTeamGoals)
                .IsRequired(true);

            game
                .Property(g => g.DateTime)
                .HasDefaultValueSql("GETDATE()");

            game
                .Property(g => g.HomeTeamBetRate)
                .IsRequired(true);

            game
                .Property(g => g.AwayTeamBetRate)
                .IsRequired(true);

            game
                .Property(g => g.DrawBetRate)
                .IsRequired(true);

            game
                .Property(g => g.Result)
                .HasMaxLength(10)
                .IsRequired(true)
                .HasDefaultValue("0 - 0");
        }
    }
}
