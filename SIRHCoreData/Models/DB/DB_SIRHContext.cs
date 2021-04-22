using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SIRHCoreData.Models.DB
{
    public partial class DB_SIRHContext : DbContext
    {
        public DB_SIRHContext()
        {
        }

        public DB_SIRHContext(DbContextOptions<DB_SIRHContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblDimDepartement> TblDimDepartement { get; set; }
        public virtual DbSet<TblDimDirection> TblDimDirection { get; set; }
        public virtual DbSet<TblDimFonction> TblDimFonction { get; set; }
        public virtual DbSet<TblDimPays> TblDimPays { get; set; }
        public virtual DbSet<TblDimSituationFamiliale> TblDimSituationFamiliale { get; set; }
        public virtual DbSet<TblDimStructure> TblDimStructure { get; set; }
        public virtual DbSet<TblDimVille> TblDimVille { get; set; }
        public virtual DbSet<TblDwEtatCivil> TblDwEtatCivil { get; set; }
        public virtual DbSet<TblDwSalarie> TblDwSalarie { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=DB_SIRH");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblDimDepartement>(entity =>
            {
                entity.HasKey(e => e.IdDepartement);

                entity.ToTable("TBL_DIM_DEPARTEMENT");

                entity.HasIndex(e => e.IdDepartement)
                    .IsUnique();

                entity.Property(e => e.IdDepartement)
                    .HasColumnName("ID_DEPARTEMENT")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdDirection).HasColumnName("ID_DIRECTION");

                entity.Property(e => e.LibelleDepartement)
                    .IsRequired()
                    .HasColumnName("LIBELLE_DEPARTEMENT")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDirectionNavigation)
                    .WithMany(p => p.TblDimDepartement)
                    .HasForeignKey(d => d.IdDirection)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_DIM_DEPARTEMENT_TBL_DIM_DIRECTION");
            });

            modelBuilder.Entity<TblDimDirection>(entity =>
            {
                entity.HasKey(e => e.IdDirection)
                    .HasName("PK_DIRECTION");

                entity.ToTable("TBL_DIM_DIRECTION");

                entity.HasIndex(e => e.IdDirection)
                    .IsUnique();

                entity.Property(e => e.IdDirection)
                    .HasColumnName("ID_DIRECTION")
                    .ValueGeneratedNever();

                entity.Property(e => e.LibelleDirection)
                    .IsRequired()
                    .HasColumnName("LIBELLE_DIRECTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDimFonction>(entity =>
            {
                entity.HasKey(e => e.IdFonction)
                    .HasName("PK_FONCTION");

                entity.ToTable("TBL_DIM_FONCTION");

                entity.HasIndex(e => e.IdFonction)
                    .IsUnique();

                entity.Property(e => e.IdFonction)
                    .HasColumnName("ID_FONCTION")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdDirection).HasColumnName("ID_DIRECTION");

                entity.Property(e => e.LibelleFonction)
                    .IsRequired()
                    .HasColumnName("LIBELLE_FONCTION")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdDirectionNavigation)
                    .WithMany(p => p.TblDimFonction)
                    .HasForeignKey(d => d.IdDirection)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_fonction");
            });

            modelBuilder.Entity<TblDimPays>(entity =>
            {
                entity.HasKey(e => e.IdPays)
                    .HasName("PK__TBL_DIM___B68ABC4D6B18DA05");

                entity.ToTable("TBL_DIM_PAYS");

                entity.Property(e => e.IdPays)
                    .HasColumnName("ID_PAYS")
                    .ValueGeneratedNever();

                entity.Property(e => e.NomPays)
                    .HasColumnName("NOM_PAYS")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDimSituationFamiliale>(entity =>
            {
                entity.HasKey(e => e.IdSituationFamiliale)
                    .HasName("PK__TBL_DIM___88406D8F40C882DD");

                entity.ToTable("TBL_DIM_SITUATION_FAMILIALE");

                entity.Property(e => e.IdSituationFamiliale)
                    .HasColumnName("ID_SITUATION_FAMILIALE")
                    .ValueGeneratedNever();

                entity.Property(e => e.LibelleSituationFamiliale)
                    .HasColumnName("LIBELLE_SITUATION_FAMILIALE")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDimStructure>(entity =>
            {
                entity.HasKey(e => e.IdStructure)
                    .HasName("PK_STRUCTURE");

                entity.ToTable("TBL_DIM_STRUCTURE");

                entity.HasIndex(e => e.IdStructure)
                    .IsUnique();

                entity.Property(e => e.IdStructure)
                    .HasColumnName("ID_STRUCTURE")
                    .ValueGeneratedNever();

                entity.Property(e => e.LibelleStructure)
                    .IsRequired()
                    .HasColumnName("LIBELLE_STRUCTURE")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblDimVille>(entity =>
            {
                entity.HasKey(e => e.IdVille)
                    .HasName("PK__TBL_DIM___1FFE7135E409D1CC");

                entity.ToTable("TBL_DIM_VILLE");

                entity.Property(e => e.IdVille)
                    .HasColumnName("ID_VILLE")
                    .ValueGeneratedNever();

                entity.Property(e => e.IdPays).HasColumnName("ID_PAYS");

                entity.Property(e => e.NomVille)
                    .HasColumnName("NOM_VILLE")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPaysNavigation)
                    .WithMany(p => p.TblDimVille)
                    .HasForeignKey(d => d.IdPays)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_DIM_VILLE_TBL_DIM_PAYS");
            });

            modelBuilder.Entity<TblDwEtatCivil>(entity =>
            {
                entity.HasKey(e => e.Matricule);

                entity.ToTable("TBL_DW_ETAT_CIVIL");

                entity.HasIndex(e => e.Matricule)
                    .IsUnique();

                entity.Property(e => e.Matricule)
                    .HasColumnName("MATRICULE")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adresse)
                    .IsRequired()
                    .HasColumnName("ADRESSE")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.DateNaissance)
                    .HasColumnName("DATE_NAISSANCE")
                    .HasColumnType("date");

                entity.Property(e => e.EmailPersonnel)
                    .HasColumnName("EMAIL_PERSONNEL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdPaysNaissance).HasColumnName("ID_PAYS_NAISSANCE");

                entity.Property(e => e.IdSituationFamiliale).HasColumnName("ID_SITUATION_FAMILIALE");

                entity.Property(e => e.IdVilleNaissance).HasColumnName("ID_VILLE_NAISSANCE");

                entity.Property(e => e.Linkedin)
                    .HasColumnName("LINKEDIN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nationalite)
                    .IsRequired()
                    .HasColumnName("NATIONALITE")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NumSecuSociale)
                    .HasColumnName("NUM_SECU_SOCIALE")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdSituationFamilialeNavigation)
                    .WithMany(p => p.TblDwEtatCivil)
                    .HasForeignKey(d => d.IdSituationFamiliale)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_DW_ETAT_CIVIL_TBL_DIM_SITUATION_FAMILIALE");

                entity.HasOne(d => d.IdVilleNaissanceNavigation)
                    .WithMany(p => p.TblDwEtatCivil)
                    .HasForeignKey(d => d.IdVilleNaissance)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_DW_ETAT_CIVIL_TBL_DIM_VILLE");

                entity.HasOne(d => d.MatriculeNavigation)
                    .WithOne(p => p.TblDwEtatCivil)
                    .HasForeignKey<TblDwEtatCivil>(d => d.Matricule)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TBL_DW_ETAT_CIVIL_TBL_DW_SALARIE");
            });

            modelBuilder.Entity<TblDwSalarie>(entity =>
            {
                entity.HasKey(e => e.Matricule)
                    .HasName("PK_MATRICULE");

                entity.ToTable("TBL_DW_SALARIE");

                entity.HasIndex(e => e.Matricule)
                    .IsUnique();

                entity.Property(e => e.Matricule)
                    .HasColumnName("MATRICULE")
                    .ValueGeneratedNever();

                entity.Property(e => e.Civilite)
                    .IsRequired()
                    .HasColumnName("CIVILITE")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("EMAIL")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdFonction).HasColumnName("ID_FONCTION");

                entity.Property(e => e.IdStructure).HasColumnName("ID_STRUCTURE");

                entity.Property(e => e.Nom)
                    .IsRequired()
                    .HasColumnName("NOM")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Prenom)
                    .IsRequired()
                    .HasColumnName("PRENOM")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .IsRequired()
                    .HasColumnName("TEL")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdFonctionNavigation)
                    .WithMany(p => p.TblDwSalarie)
                    .HasForeignKey(d => d.IdFonction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Fonction_salarie");

                entity.HasOne(d => d.IdStructureNavigation)
                    .WithMany(p => p.TblDwSalarie)
                    .HasForeignKey(d => d.IdStructure)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_structure_salarie");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
