﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBudgetManagerAPI.Data;

#nullable disable

namespace MyBudgetManagerAPI.Migrations
{
    [DbContext(typeof(cl_MyBudgetManagerApiDbContext))]
    partial class cl_MyBudgetMangerApiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_Depense", b =>
                {
                    b.Property<int>("p_nIdDepense")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_depense")
                        .HasAnnotation("Relational:JsonPropertyName", "IdDepense");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("p_nIdDepense"));

                    b.Property<DateOnly?>("p_dDate")
                        .HasColumnType("date")
                        .HasColumnName("date")
                        .HasAnnotation("Relational:JsonPropertyName", "Date");

                    b.Property<short?>("p_nAnnee")
                        .HasColumnType("smallint")
                        .HasColumnName("annee")
                        .HasAnnotation("Relational:JsonPropertyName", "Annee");

                    b.Property<byte>("p_nIdPersonne")
                        .HasColumnType("tinyint")
                        .HasColumnName("id_personne")
                        .HasAnnotation("Relational:JsonPropertyName", "IdPersonne");

                    b.Property<int?>("p_nIdType")
                        .HasColumnType("int")
                        .HasColumnName("id_type")
                        .HasAnnotation("Relational:JsonPropertyName", "IdType");

                    b.Property<byte?>("p_nMois")
                        .HasColumnType("tinyint")
                        .HasColumnName("mois")
                        .HasAnnotation("Relational:JsonPropertyName", "Mois");

                    b.Property<byte?>("p_nSemaine")
                        .HasColumnType("tinyint")
                        .HasColumnName("semaine")
                        .HasAnnotation("Relational:JsonPropertyName", "Semaine");

                    b.Property<decimal?>("p_rMontant")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("montant")
                        .HasAnnotation("Relational:JsonPropertyName", "Montant");

                    b.Property<string>("p_sLibelle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("libelle")
                        .HasAnnotation("Relational:JsonPropertyName", "Libelle");

                    b.HasKey("p_nIdDepense")
                        .HasName("PK_tbl_Depense");

                    b.HasIndex("p_nIdPersonne");

                    b.HasIndex("p_nIdType");

                    b.ToTable("tbl_depenses", (string)null);
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_NatureDepense", b =>
                {
                    b.Property<byte>("p_nIdNature")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("id_nature")
                        .HasAnnotation("Relational:JsonPropertyName", "IdNature");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("p_nIdNature"));

                    b.Property<byte>("p_rPourcentage")
                        .HasColumnType("tinyint")
                        .HasColumnName("pourcentage")
                        .HasAnnotation("Relational:JsonPropertyName", "Pourcentage");

                    b.Property<string>("p_sLibelle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("libelle")
                        .HasAnnotation("Relational:JsonPropertyName", "Libelle");

                    b.HasKey("p_nIdNature")
                        .HasName("PK_tbl_NatureDepense");

                    b.ToTable("tbl_natures_depenses", (string)null);

                    b.HasData(
                        new
                        {
                            p_nIdNature = (byte)1,
                            p_rPourcentage = (byte)50,
                            p_sLibelle = "Primaire"
                        },
                        new
                        {
                            p_nIdNature = (byte)2,
                            p_rPourcentage = (byte)30,
                            p_sLibelle = "Secondaire"
                        },
                        new
                        {
                            p_nIdNature = (byte)3,
                            p_rPourcentage = (byte)20,
                            p_sLibelle = "Epargnes"
                        });
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_Param", b =>
                {
                    b.Property<int>("p_nIdParam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_param")
                        .HasAnnotation("Relational:JsonPropertyName", "IdParam");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("p_nIdParam"));

                    b.Property<decimal?>("p_rValue")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("value")
                        .HasAnnotation("Relational:JsonPropertyName", "Value");

                    b.Property<string>("p_sParameter")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("parameter")
                        .HasAnnotation("Relational:JsonPropertyName", "Parameter");

                    b.Property<string>("p_sSection")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("section")
                        .HasAnnotation("Relational:JsonPropertyName", "Section");

                    b.HasKey("p_nIdParam")
                        .HasName("PK_tbl_Param");

                    b.ToTable("tbl_param", (string)null);

                    b.HasData(
                        new
                        {
                            p_nIdParam = 1,
                            p_rValue = 1m,
                            p_sParameter = "SEMAINE_EN_COURS",
                            p_sSection = "PARAM"
                        },
                        new
                        {
                            p_nIdParam = 2,
                            p_rValue = 1m,
                            p_sParameter = "MOIS_EN_COURS",
                            p_sSection = "PARAM"
                        },
                        new
                        {
                            p_nIdParam = 3,
                            p_sParameter = "LOYER",
                            p_sSection = "CHARGES_FIXES"
                        },
                        new
                        {
                            p_nIdParam = 4,
                            p_sParameter = "INTERNET",
                            p_sSection = "CHARGES_FIXES"
                        },
                        new
                        {
                            p_nIdParam = 5,
                            p_sParameter = "TELEPHONIE",
                            p_sSection = "CHARGES_FIXES"
                        },
                        new
                        {
                            p_nIdParam = 6,
                            p_sParameter = "EAU",
                            p_sSection = "CHARGES_VARIABLES"
                        },
                        new
                        {
                            p_nIdParam = 7,
                            p_sParameter = "ELECTRICITE",
                            p_sSection = "CHARGES_VARIABLES"
                        },
                        new
                        {
                            p_nIdParam = 8,
                            p_sParameter = "TRANSPORT",
                            p_sSection = "CHARGES_VARIABLES"
                        },
                        new
                        {
                            p_nIdParam = 9,
                            p_sParameter = "VOITURE",
                            p_sSection = "CREDITS"
                        });
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_Personne", b =>
                {
                    b.Property<byte>("p_nIdPersonne")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("tinyint")
                        .HasColumnName("id_personne")
                        .HasAnnotation("Relational:JsonPropertyName", "IdPersonne");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<byte>("p_nIdPersonne"));

                    b.Property<decimal?>("p_rDettes")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("dettes")
                        .HasAnnotation("Relational:JsonPropertyName", "Dettes");

                    b.Property<decimal?>("p_rSalaire")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("salaire")
                        .HasAnnotation("Relational:JsonPropertyName", "Salaire");

                    b.Property<string>("p_sNom")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("nom")
                        .HasAnnotation("Relational:JsonPropertyName", "Nom");

                    b.HasKey("p_nIdPersonne")
                        .HasName("PK_tbl_Personne");

                    b.ToTable("tbl_personne", (string)null);

                    b.HasData(
                        new
                        {
                            p_nIdPersonne = (byte)1,
                            p_sNom = "PERE"
                        },
                        new
                        {
                            p_nIdPersonne = (byte)2,
                            p_sNom = "MERE"
                        });
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_TypeDepense", b =>
                {
                    b.Property<int>("p_nIdType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_type")
                        .HasAnnotation("Relational:JsonPropertyName", "IdType");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("p_nIdType"));

                    b.Property<bool?>("p_bEquallyDivisible")
                        .HasColumnType("bit")
                        .HasColumnName("equally_divisible")
                        .HasAnnotation("Relational:JsonPropertyName", "EquallyDivisible");

                    b.Property<bool>("p_bHebdo")
                        .HasColumnType("bit")
                        .HasColumnName("hebdo")
                        .HasAnnotation("Relational:JsonPropertyName", "Hebdo");

                    b.Property<byte>("p_nIdNature")
                        .HasColumnType("tinyint")
                        .HasColumnName("id_nature")
                        .HasAnnotation("Relational:JsonPropertyName", "IdNature");

                    b.Property<byte?>("p_nRepartition")
                        .HasColumnType("tinyint")
                        .HasColumnName("repartition")
                        .HasAnnotation("Relational:JsonPropertyName", "Repartition");

                    b.Property<decimal>("p_rSemaine1")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("semaine_1")
                        .HasAnnotation("Relational:JsonPropertyName", "Semaine1");

                    b.Property<decimal>("p_rSemaine2")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("semaine_2")
                        .HasAnnotation("Relational:JsonPropertyName", "Semaine2");

                    b.Property<decimal>("p_rSemaine3")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("semaine_3")
                        .HasAnnotation("Relational:JsonPropertyName", "Semaine3");

                    b.Property<decimal>("p_rSemaine4")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("semaine_4")
                        .HasAnnotation("Relational:JsonPropertyName", "Semaine4");

                    b.Property<decimal>("p_rTotal")
                        .HasColumnType("numeric(7, 2)")
                        .HasColumnName("total")
                        .HasAnnotation("Relational:JsonPropertyName", "Total");

                    b.Property<string>("p_sLibelle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("libelle")
                        .HasAnnotation("Relational:JsonPropertyName", "Libelle");

                    b.HasKey("p_nIdType")
                        .HasName("PK_tbl_TypeDepense");

                    b.HasIndex("p_nIdNature");

                    b.ToTable("tbl_types_depenses", (string)null);

                    b.HasData(
                        new
                        {
                            p_nIdType = 1,
                            p_bEquallyDivisible = false,
                            p_bHebdo = true,
                            p_nIdNature = (byte)1,
                            p_nRepartition = (byte)38,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Nourriture et sanitaire"
                        },
                        new
                        {
                            p_nIdType = 2,
                            p_bEquallyDivisible = false,
                            p_bHebdo = false,
                            p_nIdNature = (byte)1,
                            p_nRepartition = (byte)24,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Santé"
                        },
                        new
                        {
                            p_nIdType = 3,
                            p_bEquallyDivisible = false,
                            p_bHebdo = false,
                            p_nIdNature = (byte)2,
                            p_nRepartition = (byte)24,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Equipements"
                        },
                        new
                        {
                            p_nIdType = 4,
                            p_bEquallyDivisible = false,
                            p_bHebdo = true,
                            p_nIdNature = (byte)1,
                            p_nRepartition = (byte)38,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Essence"
                        },
                        new
                        {
                            p_nIdType = 5,
                            p_bEquallyDivisible = true,
                            p_bHebdo = false,
                            p_nIdNature = (byte)2,
                            p_nRepartition = (byte)32,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Vêtements"
                        },
                        new
                        {
                            p_nIdType = 6,
                            p_bEquallyDivisible = false,
                            p_bHebdo = false,
                            p_nIdNature = (byte)2,
                            p_nRepartition = (byte)26,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Voyage"
                        },
                        new
                        {
                            p_nIdType = 7,
                            p_bEquallyDivisible = false,
                            p_bHebdo = false,
                            p_nIdNature = (byte)2,
                            p_nRepartition = (byte)9,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Resto"
                        },
                        new
                        {
                            p_nIdType = 8,
                            p_bEquallyDivisible = false,
                            p_bHebdo = false,
                            p_nIdNature = (byte)2,
                            p_nRepartition = (byte)9,
                            p_rSemaine1 = 0m,
                            p_rSemaine2 = 0m,
                            p_rSemaine3 = 0m,
                            p_rSemaine4 = 0m,
                            p_rTotal = 0m,
                            p_sLibelle = "Loisirs"
                        });
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_Wishlist", b =>
                {
                    b.Property<int>("p_nIdWishlist")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id_wishlist")
                        .HasAnnotation("Relational:JsonPropertyName", "IdWishlist");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("p_nIdWishlist"));

                    b.Property<int>("p_nIdType")
                        .HasColumnType("int")
                        .HasColumnName("id_type")
                        .HasAnnotation("Relational:JsonPropertyName", "IdType");

                    b.Property<decimal>("p_rPrix")
                        .HasColumnType("decimal(18, 2)")
                        .HasColumnName("prix")
                        .HasAnnotation("Relational:JsonPropertyName", "Prix");

                    b.Property<string>("p_sLibelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("libelle")
                        .HasAnnotation("Relational:JsonPropertyName", "Libelle");

                    b.HasKey("p_nIdWishlist")
                        .HasName("PK_tbl_wishlist_1");

                    b.HasIndex("p_nIdType");

                    b.ToTable("tbl_wishlist", (string)null);
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_Depense", b =>
                {
                    b.HasOne("MyBudgetManagerAPI.Models.cl_Personne", "p_clIdPersonneNavigation")
                        .WithMany("p_aclDepenses")
                        .HasForeignKey("p_nIdPersonne")
                        .IsRequired()
                        .HasConstraintName("FK_tbl_depenses_tbl_personne");

                    b.HasOne("MyBudgetManagerAPI.Models.cl_TypeDepense", "p_clIdTypeNavigation")
                        .WithMany("p_aclDepenses")
                        .HasForeignKey("p_nIdType")
                        .HasConstraintName("FK_tbl_depenses_tbl_type_depense");

                    b.Navigation("p_clIdPersonneNavigation");

                    b.Navigation("p_clIdTypeNavigation");
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_TypeDepense", b =>
                {
                    b.HasOne("MyBudgetManagerAPI.Models.cl_NatureDepense", "p_clIdNatureNavigation")
                        .WithMany("p_aclTypesDepenses")
                        .HasForeignKey("p_nIdNature")
                        .IsRequired()
                        .HasConstraintName("FK_tbl_type_depense_tbl_natures_depenses");

                    b.Navigation("p_clIdNatureNavigation");
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_Wishlist", b =>
                {
                    b.HasOne("MyBudgetManagerAPI.Models.cl_TypeDepense", "p_clIdTypeNavigation")
                        .WithMany("p_aclWishlists")
                        .HasForeignKey("p_nIdType")
                        .IsRequired()
                        .HasConstraintName("FK_tbl_wishlist_tbl_type_depense");

                    b.Navigation("p_clIdTypeNavigation");
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_NatureDepense", b =>
                {
                    b.Navigation("p_aclTypesDepenses");
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_Personne", b =>
                {
                    b.Navigation("p_aclDepenses");
                });

            modelBuilder.Entity("MyBudgetManagerAPI.Models.cl_TypeDepense", b =>
                {
                    b.Navigation("p_aclDepenses");

                    b.Navigation("p_aclWishlists");
                });
#pragma warning restore 612, 618
        }
    }
}
