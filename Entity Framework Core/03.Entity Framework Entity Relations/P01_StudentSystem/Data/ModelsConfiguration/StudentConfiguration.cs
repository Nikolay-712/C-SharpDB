namespace P01_StudentSystem.Data.ModelsConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;
    using System;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student
                .HasKey(s => s.StudentId);

            student
                .Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired(true)
                .IsUnicode(true);

            student
                .Property(s => s.PhoneNumber)
                .HasMaxLength(10);

            student
                .Property(s => s.RegisteredOn)
                .HasDefaultValueSql("GETDATE()");

            student.Property(s => s.Birthday).IsRequired(false);

           
                
        }
    }
}
