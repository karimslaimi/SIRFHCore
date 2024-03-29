﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SIRHCoreData;

namespace SIRHCoreData.Migrations
{
    [DbContext(typeof(SIRHcontext))]
    partial class SIRHcontextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SIRHCoreDomain.Collaboration", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PersonneId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Projetid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("PersonneId");

                    b.HasIndex("Projetid");

                    b.ToTable("collaborations");
                });

            modelBuilder.Entity("SIRHCoreDomain.Conge", b =>
                {
                    b.Property<int>("CongeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Annuel")
                        .HasColumnType("float");

                    b.Property<string>("CommentaireC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentaireRH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateDeb")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateFin")
                        .HasColumnType("datetime2");

                    b.Property<double>("Duree")
                        .HasColumnType("float");

                    b.Property<string>("Justificatif")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Maladie")
                        .HasColumnType("float");

                    b.Property<double>("Maternite")
                        .HasColumnType("float");

                    b.Property<string>("PersonneId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("SansSolde")
                        .HasColumnType("float");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CongeID");

                    b.HasIndex("PersonneId");

                    b.ToTable("Conge");
                });

            modelBuilder.Entity("SIRHCoreDomain.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateAjout")
                        .HasColumnType("datetime2");

                    b.Property<string>("Propritere")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("SIRHCoreDomain.Incident", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AttributionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CreeparId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateReglage")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Modifiepar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AttributionId");

                    b.HasIndex("CreeparId");

                    b.ToTable("Incidents");
                });

            modelBuilder.Entity("SIRHCoreDomain.NoteDeFrais", b =>
                {
                    b.Property<int>("NoteDeFraisID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentaireF")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentaireRH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Justificatif")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Montant")
                        .HasColumnType("float");

                    b.Property<string>("PersonneId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Statut")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("nbrper")
                        .HasColumnType("int");

                    b.HasKey("NoteDeFraisID");

                    b.HasIndex("PersonneId");

                    b.ToTable("NoteDeFrais");
                });

            modelBuilder.Entity("SIRHCoreDomain.Personne", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Adresse")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CIN")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DateDeNais")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Fonction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Nom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Prenom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SoldeID")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SoldeID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("SIRHCoreDomain.Projet", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("createurId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("datedeb")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("datefin")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nom")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("createurId");

                    b.ToTable("Projets");
                });

            modelBuilder.Entity("SIRHCoreDomain.Solde", b =>
                {
                    b.Property<int>("SoldeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Annuel")
                        .HasColumnType("float");

                    b.Property<double>("Maladie")
                        .HasColumnType("float");

                    b.Property<double>("Maternité")
                        .HasColumnType("float");

                    b.Property<double>("Payé")
                        .HasColumnType("float");

                    b.Property<double>("SansSolde")
                        .HasColumnType("float");

                    b.Property<string>("Userid")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SoldeID");

                    b.ToTable("Solde");
                });

            modelBuilder.Entity("SIRHCoreDomain.Taches", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Projetid")
                        .HasColumnType("int");

                    b.Property<string>("creatorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("state")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Projetid");

                    b.HasIndex("creatorId");

                    b.ToTable("Taches");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SIRHCoreDomain.Personne", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SIRHCoreDomain.Collaboration", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", "Personne")
                        .WithMany("Collaborations")
                        .HasForeignKey("PersonneId");

                    b.HasOne("SIRHCoreDomain.Projet", "Projet")
                        .WithMany("collaborateurs")
                        .HasForeignKey("Projetid");

                    b.Navigation("Personne");

                    b.Navigation("Projet");
                });

            modelBuilder.Entity("SIRHCoreDomain.Conge", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", "Personne")
                        .WithMany("Conge")
                        .HasForeignKey("PersonneId");

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("SIRHCoreDomain.Incident", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", "Attribution")
                        .WithMany("traitements")
                        .HasForeignKey("AttributionId");

                    b.HasOne("SIRHCoreDomain.Personne", "Creepar")
                        .WithMany("Incidents")
                        .HasForeignKey("CreeparId");

                    b.Navigation("Attribution");

                    b.Navigation("Creepar");
                });

            modelBuilder.Entity("SIRHCoreDomain.NoteDeFrais", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", "Personne")
                        .WithMany("NoteDeFrais")
                        .HasForeignKey("PersonneId");

                    b.Navigation("Personne");
                });

            modelBuilder.Entity("SIRHCoreDomain.Personne", b =>
                {
                    b.HasOne("SIRHCoreDomain.Solde", "Solde")
                        .WithMany("Persoone")
                        .HasForeignKey("SoldeID");

                    b.Navigation("Solde");
                });

            modelBuilder.Entity("SIRHCoreDomain.Projet", b =>
                {
                    b.HasOne("SIRHCoreDomain.Personne", "createur")
                        .WithMany("Projets")
                        .HasForeignKey("createurId");

                    b.Navigation("createur");
                });

            modelBuilder.Entity("SIRHCoreDomain.Taches", b =>
                {
                    b.HasOne("SIRHCoreDomain.Projet", "Projet")
                        .WithMany("Taches")
                        .HasForeignKey("Projetid");

                    b.HasOne("SIRHCoreDomain.Personne", "creator")
                        .WithMany("Taches")
                        .HasForeignKey("creatorId");

                    b.Navigation("creator");

                    b.Navigation("Projet");
                });

            modelBuilder.Entity("SIRHCoreDomain.Personne", b =>
                {
                    b.Navigation("Collaborations");

                    b.Navigation("Conge");

                    b.Navigation("Incidents");

                    b.Navigation("NoteDeFrais");

                    b.Navigation("Projets");

                    b.Navigation("Taches");

                    b.Navigation("traitements");
                });

            modelBuilder.Entity("SIRHCoreDomain.Projet", b =>
                {
                    b.Navigation("collaborateurs");

                    b.Navigation("Taches");
                });

            modelBuilder.Entity("SIRHCoreDomain.Solde", b =>
                {
                    b.Navigation("Persoone");
                });
#pragma warning restore 612, 618
        }
    }
}
