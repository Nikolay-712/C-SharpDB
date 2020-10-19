namespace P01_StudentSystem.Data.ModelsConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;
    public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> studentCourse)
        {
            studentCourse
                .HasKey(s => new { s.StudentId, s.CourseId });
        }
    }
}
