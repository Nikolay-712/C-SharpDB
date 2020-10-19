namespace P03_FootballBetting.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        public void Configure(EntityTypeBuilder<Bet> bet)
        {
            bet
                .HasKey(b => b.BetId);

            bet
                .Property(b => b.Amount)
                .IsRequired(true);

            bet
                .Property(b => b.Prediction)
                .IsRequired(true);
           

            bet
                .HasOne(x => x.User)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.UserId);

            bet
                .HasOne(x => x.Game)
                .WithMany(x => x.Bets)
                .HasForeignKey(x => x.GameId);
        }
    }
}
