using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReclutamientoAPI.API.Models;
using ReclutamientoAPI.Enums;

namespace ReclutamientoAPI.API.Models
{
    public partial class Companies
    {
        public Companies()
        {

        }
        public Companies(int? CompanyId)
        {
            this.CompanyId = CompanyId;
        }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
       
    }
    public class CompaniesConfiguration : IEntityTypeConfiguration<Companies>
    {
        public void Configure(EntityTypeBuilder<Companies> builder)
        {
            // Set configuration for entity
            builder.ToTable("Companies","dbo");
            // Set key for entity
            builder.HasKey(p => p.CompanyId);

            // Set configuration for columns

            builder.Property(p => p.CompanyName).HasColumnType("nvarchar(200)").IsRequired();

            // Columns with default value

            builder
                .Property(p => p.CompanyId)
                .HasColumnType("int")
                .IsRequired()
                .HasDefaultValueSql("NEXT VALUE FOR [Sequences].[CompanyId]");
        }
    }

    public class ApplicantsConfiguration : IEntityTypeConfiguration<Applicants>
    {
        public void Configure(EntityTypeBuilder<Applicants> builder)
        {
            // Set configuration for entity
            builder.ToTable("Applicants");
            // Set key for entity
            builder.HasKey(p => p.ApplicantId);

            // Set configuration for columns

            builder.Property(p => p.ApellidoPaterno).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.ApellidoMaterno).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.Name).HasColumnType("nvarchar(50)").IsRequired();
            builder.Property(p => p.RegisterDate).HasColumnType("DateTime").IsRequired();
      

            // Columns with default value

            builder
                .Property(p => p.ApplicantId)
                .HasColumnType("int")
                .IsRequired()               
                .HasDefaultValueSql("NEXT VALUE FOR [Sequences].[ApplicantId]");
        }
    }

    public  class ReclutamientoAPIDbContext : DbContext
    {
        public ReclutamientoAPIDbContext(DbContextOptions<ReclutamientoAPIDbContext> options)
            : base(options)
        {
            //Database.SetInitializer<ReclutamientoAPIDbContext>(new CreateDatabaseIfNotExists<ReclutamientoAPIDbContext>());
        }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity

            modelBuilder
                .ApplyConfiguration( new CompaniesConfiguration());
            modelBuilder
                .ApplyConfiguration(new ApplicantsConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Companies> Companies { get; set; }
        public DbSet<Applicants> Applicants { get; set; }
    }
}

