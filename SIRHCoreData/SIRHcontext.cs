using System;
using System.Collections.Generic;
using System.Text;
//using SIRHCoreData.Models.DB;
using SIRHCoreDomain;
//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace SIRHCoreData
{
    public class SIRHcontext : IdentityDbContext<Personne>
    {
        public SIRHcontext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DB_SIRH");
        }

        public DbSet<Conge> Conge { get; set; }
            public DbSet<Solde> Solde { get; set; }
            public DbSet<NoteDeFrais> NoteDeFrais { get; set; }
            public DbSet<Personne> Personnes { get; set; }
            public DbSet<Incident> Incidents{ get; set; }
            public DbSet<Document> Documents { get; set; }
            public DbSet<Projet> Projets { get; set; }
            public DbSet<Collaboration> collaborations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Collaboration>(entity =>
            {

            entity.HasOne(x => x.Personne).WithMany(s => s.Collaborations);
                entity.HasOne(x => x.Projet).WithMany(s => s.collaborateurs);
            }
            );


            modelBuilder.Entity<Personne>(entity =>
            {
                entity.ToTable("AspNetUsers").HasNoDiscriminator();
                entity.HasMany(c => c.Conge)

                      .WithOne(p => p.Personne);
                entity.HasMany(q => q.Projets).WithOne(x => x.createur);
            });

            modelBuilder.Entity<Conge>(entity =>
            {
                entity.HasKey(c => c.CongeID);

            });

            modelBuilder.Entity<NoteDeFrais>(entity =>
            {
                entity.HasKey(m => m.NoteDeFraisID);

                entity.HasOne(p => p.Personne)
                      .WithMany(n => n.NoteDeFrais);
            });

            //modelBuilder.Entity<Solde>(entity =>
            //{
            //    entity.HasKey(m => m.SoldeID);

            //    entity.HasOne(p => p.Persoone);
            //});


            modelBuilder.Entity<Projet>(entity =>
            {

                entity.HasKey(m => m.id);
                entity.HasOne(x => x.createur);
            }

            );

            base.OnModelCreating(modelBuilder);
        }
    }
}