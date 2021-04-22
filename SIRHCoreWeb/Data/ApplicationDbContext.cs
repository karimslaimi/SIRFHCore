using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SIRHCoreDomain;

namespace SIRHCoreWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=DB_SIRH;Trusted_Connection=True;");
            }
        }
        public DbSet<FidoStoredCredential> FidoStoredCredential { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<FidoStoredCredential>().HasKey(m => m.Username);

            base.OnModelCreating(builder);
        }

        //public DbSet<SIRHCoreDomain.Conge> Conge { get; set; }
      //  public DbSet<SIRHCoreDomain.Personne> Personne { get; set; }
    }
}
