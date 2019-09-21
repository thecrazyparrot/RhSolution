using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public class ReclutamientoAPIDbContext : DbContext
    {
        public ReclutamientoAPIDbContext(DbContextOptions<ReclutamientoAPIDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity

            modelBuilder
                .ApplyConfiguration(new CompaniesConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Companies> Companies { get; set; }

        
    }
}

