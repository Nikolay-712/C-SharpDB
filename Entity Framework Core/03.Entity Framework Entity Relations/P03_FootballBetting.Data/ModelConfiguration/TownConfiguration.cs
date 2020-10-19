namespace P03_FootballBetting.Data.ModelConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P03_FootballBetting.Data.Models;
    public class TownConfiguration : IEntityTypeConfiguration<Town>
    {
        public void Configure(EntityTypeBuilder<Town> town)
        {
            town
                .HasKey(t => t.TownId);

            town
                .Property(t => t.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            town
                .HasOne(x => x.Country)
                .WithMany(x => x.Towns)
                .HasForeignKey(x => x.TownId);
        }
    }
}
