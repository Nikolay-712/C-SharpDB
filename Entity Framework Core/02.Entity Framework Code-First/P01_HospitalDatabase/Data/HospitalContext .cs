namespace P01_HospitalDatabase.Data
{
    using Microsoft.EntityFrameworkCore;
    using P01_HospitalDatabase.Data.Models;
   

    public class HospitalContex : DbContext
    {
        public HospitalContex()
        {
        }

        public HospitalContex(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visitation> Visitations { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Connection.ConnectingString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>(entity =>
                {
                    entity.HasKey(p => p.PatientId);

                    entity
                        .Property(p => p.FirstName)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);

                    entity
                        .Property(p => p.LastName)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);

                    entity
                        .Property(p => p.Address)
                        .HasMaxLength(25)
                        .IsRequired(true)
                        .IsUnicode(true);

                    entity
                        .Property(p => p.Email)
                        .HasMaxLength(80);

                    entity
                        .Property(p => p.HasInsurance);


                });

            modelBuilder
                .Entity<Visitation>(entity =>
            {
                entity.HasKey(v => v.VisitationId);

                entity
                    .Property(v => v.Date)
                    .IsRequired(true);

                entity
                    .Property(v => v.Comments)
                    .HasMaxLength(250)
                    .IsRequired(true)
                    .IsUnicode(true);


                entity
                    .Property(v => v.PatientId);
               


            });

            modelBuilder
                .Entity<Diagnose>(entity =>
            {
                entity.HasKey(d => d.DiagnoseId);

                entity
                    .Property(d => d.Name)
                    .HasMaxLength(50)
                    .IsRequired(true)
                    .IsUnicode(true);

                entity
                    .Property(d => d.Comments)
                    .HasMaxLength(250)
                    .IsRequired()
                    .IsUnicode(true);

                entity
                    .Property(d => d.PatientId);


            });

            modelBuilder
                .Entity<Medicament>(entity =>
                {
                    entity.HasKey(m => m.MedicamentId);

                    entity
                        .Property(m => m.Name)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);
                });

            modelBuilder
                .Entity<PatientMedicament>(entity =>
                 {
                     entity.HasKey(e => new { e.PatientId, e.MedicamentId });
                 });

            modelBuilder
                .Entity<Doctor>(entity => 
                {
                    entity.HasKey(d => d.DoctorId);

                    entity
                        .Property(d => d.Name)
                        .HasMaxLength(50)
                        .IsRequired(true)
                        .IsUnicode(true);

                    entity
                        .Property(d => d.Specialty)
                        .HasMaxLength(150)
                        .IsRequired(true)
                        .IsUnicode(true);

                    
                });

        }
    }
}

