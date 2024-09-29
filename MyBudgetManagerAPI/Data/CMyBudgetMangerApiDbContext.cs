using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Controllers;
using MyBudgetManagerAPI.Models;
using static MyBudgetManagerAPI.Services.CParamService;

namespace MyBudgetManagerAPI.Data
{

    public partial class CMyBudgetManagerApiDbContext : DbContext
    {
        public CMyBudgetManagerApiDbContext()
        {
        }

        public CMyBudgetManagerApiDbContext(DbContextOptions<CMyBudgetManagerApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CDepense> p_oDepenses { get; set; }

        public virtual DbSet<CNatureDepense> p_oNaturesDepenses { get; set; }

        public virtual DbSet<CParam> p_oParams { get; set; }

        public virtual DbSet<CPersonne> p_oPersonnes { get; set; }

        public virtual DbSet<CTypeDepense> p_oTypesDepenses { get; set; }

        public virtual DbSet<CWishlist> p_oWishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder a_oModelBuilder)
        {
            a_oModelBuilder.Entity<CDepense>(entity =>
            {
                entity.HasKey(e => e.p_nIdDepense).HasName("PK_tbl_Depense");

                entity.ToTable("tbl_depenses");

                entity.Property(e => e.p_nIdDepense).HasColumnName("id_depense");
                entity.Property(e => e.p_nAnnee).HasColumnName("annee");
                entity.Property(e => e.p_dDate).HasColumnName("date");
                entity.Property(e => e.p_nIdPersonne).HasColumnName("id_personne");
                entity.Property(e => e.p_nIdType).HasColumnName("id_type");
                entity.Property(e => e.p_sLibelle)
                    .HasMaxLength(50)
                    .HasColumnName("libelle");
                entity.Property(e => e.p_nMois).HasColumnName("mois");
                entity.Property(e => e.p_rMontant)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("montant");
                entity.Property(e => e.p_nSemaine).HasColumnName("semaine");

                entity.HasOne(d => d.p_oIdPersonneNavigation).WithMany(p => p.p_aoDepenses)
                    .HasForeignKey(d => d.p_nIdPersonne)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_depenses_tbl_personne");

                entity.HasOne(d => d.p_oIdTypeNavigation).WithMany(p => p.p_aoDepenses)
                    .HasForeignKey(d => d.p_nIdType)
                    .HasConstraintName("FK_tbl_depenses_tbl_type_depense");

            });

            a_oModelBuilder.Entity<CNatureDepense>(entity =>
            {
                entity.HasKey(e => e.p_nIdNature).HasName("PK_tbl_NatureDepense");

                entity.ToTable("tbl_natures_depenses");

                entity.Property(e => e.p_nIdNature)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_nature");
                entity.Property(e => e.p_rPourcentage).HasColumnName("pourcentage");

                entity.Property(e => e.p_sLibelle)
                    .HasMaxLength(50)
                    .HasColumnName("libelle");

                // Seeding default data
                entity.HasData(
                    new CNatureDepense { p_nIdNature = 1, p_rPourcentage = 50, p_sLibelle = "Primaire" },
                    new CNatureDepense { p_nIdNature = 2, p_rPourcentage = 30, p_sLibelle = "Secondaire" },
                    new CNatureDepense { p_nIdNature = 3, p_rPourcentage = 20, p_sLibelle = "Epargnes" }
                );
            });

            a_oModelBuilder.Entity<CParam>(entity =>
            {
                entity.HasKey(e => e.p_nIdParam).HasName("PK_tbl_Param");

                entity.ToTable("tbl_param");

                entity.Property(e => e.p_nIdParam).HasColumnName("id_param");
                entity.Property(e => e.p_sParameter)
                    .HasMaxLength(50)
                    .HasColumnName("parameter");
                entity.Property(e => e.p_sSection)
                    .HasMaxLength(50)
                    .HasColumnName("section");
                entity.Property(e => e.p_rValue)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("value");

                // Seeding default data
                entity.HasData(
                    new CParam { p_nIdParam = 1, p_sSection = eSection.PARAM.ToString(), p_sParameter = eParam.SEMAINE_EN_COURS.ToString(), p_rValue = 1 },
                    new CParam { p_nIdParam = 2, p_sSection = eSection.PARAM.ToString(), p_sParameter = eParam.MOIS_EN_COURS.ToString(), p_rValue = 1 },
                    new CParam { p_nIdParam = 3, p_sSection = eSection.CHARGES_FIXES.ToString(), p_sParameter = eChargesFixes.LOYER.ToString() },
                    new CParam { p_nIdParam = 4, p_sSection = eSection.CHARGES_FIXES.ToString(), p_sParameter = eChargesFixes.INTERNET.ToString() },
                    new CParam { p_nIdParam = 5, p_sSection = eSection.CHARGES_FIXES.ToString(), p_sParameter = eChargesFixes.TELEPHONIE.ToString() },
                    new CParam { p_nIdParam = 6, p_sSection = eSection.CHARGES_VARIABLES.ToString(), p_sParameter = eChargesVariables.EAU.ToString() },
                    new CParam { p_nIdParam = 7, p_sSection = eSection.CHARGES_VARIABLES.ToString(), p_sParameter = eChargesVariables.ELECTRICITE.ToString() },
                    new CParam { p_nIdParam = 8, p_sSection = eSection.CHARGES_VARIABLES.ToString(), p_sParameter = eChargesVariables.TRANSPORT.ToString() },
                    new CParam { p_nIdParam = 9, p_sSection = eSection.CREDITS.ToString(), p_sParameter = eCredits.VOITURE.ToString() }
                );
            });

            a_oModelBuilder.Entity<CPersonne>(entity =>
            {
                entity.HasKey(e => e.p_nIdPersonne).HasName("PK_tbl_Personne");

                entity.ToTable("tbl_personne");

                entity.Property(e => e.p_nIdPersonne)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id_personne");
                entity.Property(e => e.p_rDettes)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("dettes");
                entity.Property(e => e.p_sNom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");
                entity.Property(e => e.p_rSalaire)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("salaire");

                // Seeding default data
                entity.HasData(
                    new CPersonne { p_nIdPersonne = 1, p_sNom = "PERE" },
                    new CPersonne { p_nIdPersonne = 2, p_sNom = "MERE" }
                );
            });

            a_oModelBuilder.Entity<CTypeDepense>(entity =>
            {
                entity.HasKey(e => e.p_nIdType).HasName("PK_tbl_TypeDepense");

                entity.ToTable("tbl_types_depenses");

                entity.Property(e => e.p_nIdType).HasColumnName("id_type");
                entity.Property(e => e.p_bEquallyDivisible).HasColumnName("equally_divisible");
                entity.Property(e => e.p_bHebdo).HasColumnName("hebdo");
                entity.Property(e => e.p_nIdNature).HasColumnName("id_nature");
                entity.Property(e => e.p_sLibelle)
                    .HasMaxLength(50)
                    .HasColumnName("libelle");
                entity.Property(e => e.p_nRepartition).HasColumnName("repartition");
                entity.Property(e => e.p_rSemaine1)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("semaine_1");
                entity.Property(e => e.p_rSemaine2)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("semaine_2");
                entity.Property(e => e.p_rSemaine3)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("semaine_3");
                entity.Property(e => e.p_rSemaine4)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("semaine_4");
                entity.Property(e => e.p_rTotal)
                    .HasColumnType("numeric(7, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.p_oIdNatureNavigation).WithMany(p => p.p_aoTypesDepenses)
                    .HasForeignKey(d => d.p_nIdNature)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_type_depense_tbl_natures_depenses");
                // Seeding default data
                entity.HasData(
                    new CTypeDepense { p_nIdType = 1, p_sLibelle = "Nourriture et sanitaire", p_nIdNature = 1, p_nRepartition = 38, p_bEquallyDivisible = false, p_bHebdo = true },
                    new CTypeDepense { p_nIdType = 2, p_sLibelle = "Santé", p_nIdNature = 1, p_nRepartition = 24, p_bEquallyDivisible = false, p_bHebdo = false },
                    new CTypeDepense { p_nIdType = 3, p_sLibelle = "Equipements", p_nIdNature = 2, p_nRepartition = 24, p_bEquallyDivisible = false, p_bHebdo = false },
                    new CTypeDepense { p_nIdType = 4, p_sLibelle = "Essence", p_nIdNature = 1, p_nRepartition = 38, p_bEquallyDivisible = false, p_bHebdo = true },
                    new CTypeDepense { p_nIdType = 5, p_sLibelle = "Vêtements", p_nIdNature = 2, p_nRepartition = 32, p_bEquallyDivisible = true, p_bHebdo = false },
                    new CTypeDepense { p_nIdType = 6, p_sLibelle = "Voyage", p_nIdNature = 2, p_nRepartition = 26, p_bEquallyDivisible = false, p_bHebdo = false },
                    new CTypeDepense { p_nIdType = 7, p_sLibelle = "Resto", p_nIdNature = 2, p_nRepartition = 9, p_bEquallyDivisible = false, p_bHebdo = false },
                    new CTypeDepense { p_nIdType = 8, p_sLibelle = "Loisirs", p_nIdNature = 2, p_nRepartition = 9, p_bEquallyDivisible = false, p_bHebdo = false }
                );
            });

            a_oModelBuilder.Entity<CWishlist>(entity =>
            {
                entity.HasKey(e => e.p_nIdWishlist).HasName("PK_tbl_wishlist_1");

                entity.ToTable("tbl_wishlist");

                entity.Property(e => e.p_nIdWishlist).HasColumnName("id_wishlist");
                entity.Property(e => e.p_nIdType).HasColumnName("id_type");
                entity.Property(e => e.p_sLibelle).HasColumnName("libelle");
                entity.Property(e => e.p_rPrix)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("prix");

                entity.HasOne(d => d.p_oIdTypeNavigation).WithMany(p => p.p_aoWishlists)
                    .HasForeignKey(d => d.p_nIdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_wishlist_tbl_type_depense");
            });

            OnModelCreatingPartial(a_oModelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder a_oModelBuilder);
    }
}