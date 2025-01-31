﻿namespace P01_StudentSystem.Data.ModelsConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;
    using System;
    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> homework)
        {
            homework
                .HasKey(h => h.HomeworkId);

            homework
                .Property(h => h.ContentType)
                .IsRequired(true);

            homework
                .Property(h => h.SubmissionTime)
                .HasDefaultValue(DateTime.UtcNow);

            


        }
    }
}
