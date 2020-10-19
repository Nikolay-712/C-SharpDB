namespace P03_FootballBetting.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> team)
        {
            team
                .HasKey(t => t.TeamId);

            team
                .Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            team
                .Property(t => t.LogoUrl)
                .HasMaxLength(250)
                .IsRequired(true);

            team
                .Property(t => t.Initials)
                .HasMaxLength(4)
                .IsRequired(true)
                .IsUnicode(true);

            team
                .Property(t => t.Budget)
                .IsRequired(true);


            team
                .HasOne(t => t.PrimaryKitColor)
                .WithMany(c => c.PrimaryKitTeams)
                .HasForeignKey(t => t.PrimaryKitColorId);


            team
                .HasOne(t => t.SecondaryKitColor)
                .WithMany(c => c.SecondaryKitTeams)
                .HasForeignKey(t => t.SecondaryKitColorId);


            team
                .HasOne(x => x.Town)
                .WithMany(x => x.Teams)
                .HasForeignKey(x => x.TownId);

        }
    }
}
