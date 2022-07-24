using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using VCare2.DatabaseLayer.Models;

namespace VCare2.DatabaseLayer
{
    public partial class CareHomeContext : DbContext
    {
        public CareHomeContext()
        {
        }

        public CareHomeContext(DbContextOptions<CareHomeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Location> Locations { get; set; } = null!;
        public virtual DbSet<Qualification> Qualifications { get; set; } = null!;
        public virtual DbSet<StaffQualification> StaffQualifications { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=DESKTOP-5FIU4L6;Initial Catalog=CareHome;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasKey(e => e.JobTitleId)
                    .HasName("PK_JobTitle");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.JobTitle).HasMaxLength(50);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.CareHomeId)
                    .HasName("PK_CareHome");

                entity.ToTable("Location");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Qualification>(entity =>
            {
                entity.HasKey(e => e.QualificationsId);

                entity.Property(e => e.QualificationType).HasMaxLength(50);
            });

            modelBuilder.Entity<StaffQualification>(entity =>
            {
                entity.ToTable("StaffQualification");

                entity.Property(e => e.AttainmentDate).HasColumnType("datetime");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DateModified).HasColumnType("datetime");

                entity.Property(e => e.Grade).HasMaxLength(10);

                entity.HasOne(d => d.QualificationType)
                    .WithMany(p => p.StaffQualifications)
                    .HasForeignKey(d => d.QualificationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffQualification_Qualifications");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.StaffQualifications)
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffQualification_Staff");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.HasIndex(e => e.StaffId, "IX_Staff")
                    .IsUnique();

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Forename).HasMaxLength(50);

                entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Surname).HasMaxLength(50);

                entity.HasOne(d => d.CareHome)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.CareHomeId)
                    .HasConstraintName("FK_Staff_CareHome");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.JobTitleId)
                    .HasConstraintName("FK_Staff_JobTitle");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
