namespace P01_StudentSystem.Data.ModelsConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> resource)
        {
            resource
                .HasKey(r => r.ResourceId);

            resource
                .Property(r => r.Name)
                .HasMaxLength(50)
                .IsRequired(true)
                .IsUnicode(true);

            resource
                .Property(r => r.ResourceType)
                .IsRequired(true);

            resource
                .Property(r => r.CourseId)
                .IsRequired(true);
        }
    }
}
