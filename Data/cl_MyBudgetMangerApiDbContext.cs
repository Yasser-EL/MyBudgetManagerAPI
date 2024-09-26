using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Controllers;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Data
{

    public partial class cl_MyBudgetManagerApiDbContext : DbContext
    {
        public cl_MyBudgetManagerApiDbContext()
        {
        }

        public cl_MyBudgetManagerApiDbContext(DbContextOptions<cl_MyBudgetManagerApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<cl_Depense> p_clDepenses { get; set; }

        public virtual DbSet<cl_NatureDepense> p_clNaturesDepenses { get; set; }

        public virtual DbSet<cl_Param> p_clParams { get; set; }

        public virtual DbSet<cl_Personne> p_clPersonnes { get; set; }

        public virtual DbSet<cl_TypeDepense> p_clTypesDepenses { get; set; }

        public virtual DbSet<cl_Wishlist> p_clWishlists { get; set; }

        protected override void OnModelCreating(ModelBuilder pclModelBuilder)
        {
            pclModelBuilder.Entity<cl_Depense>(entity =>
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

                entity.HasOne(d => d.p_clIdPersonneNavigation).WithMany(p => p.p_aclDepenses)
                    .HasForeignKey(d => d.p_nIdPersonne)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_depenses_tbl_personne");

                entity.HasOne(d => d.p_clIdTypeNavigation).WithMany(p => p.p_aclDepenses)
                    .HasForeignKey(d => d.p_nIdType)
                    .HasConstraintName("FK_tbl_depenses_tbl_type_depense");

            });

            pclModelBuilder.Entity<cl_NatureDepense>(entity =>
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
                    new cl_NatureDepense { p_nIdNature = 1, p_rPourcentage = 50, p_sLibelle = "Primaire" },
                    new cl_NatureDepense { p_nIdNature = 2, p_rPourcentage = 30, p_sLibelle = "Secondaire" },
                    new cl_NatureDepense { p_nIdNature = 3, p_rPourcentage = 20, p_sLibelle = "Epargnes" }
                );
            });

            pclModelBuilder.Entity<cl_Param>(entity =>
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
                    new cl_Param { p_nIdParam = 1, p_sSection = eSection.PARAM.ToString(), p_sParameter = eParam.SEMAINE_EN_COURS.ToString(), p_rValue = 1 },
                    new cl_Param { p_nIdParam = 2, p_sSection = eSection.PARAM.ToString(), p_sParameter = eParam.MOIS_EN_COURS.ToString(), p_rValue = 1 },
                    new cl_Param { p_nIdParam = 3, p_sSection = eSection.CHARGES_FIXES.ToString(), p_sParameter = eChargesFixes.LOYER.ToString() },
                    new cl_Param { p_nIdParam = 4, p_sSection = eSection.CHARGES_FIXES.ToString(), p_sParameter = eChargesFixes.INTERNET.ToString() },
                    new cl_Param { p_nIdParam = 5, p_sSection = eSection.CHARGES_FIXES.ToString(), p_sParameter = eChargesFixes.TELEPHONIE.ToString() },
                    new cl_Param { p_nIdParam = 6, p_sSection = eSection.CHARGES_VARIABLES.ToString(), p_sParameter = eChargesVariables.EAU.ToString() },
                    new cl_Param { p_nIdParam = 7, p_sSection = eSection.CHARGES_VARIABLES.ToString(), p_sParameter = eChargesVariables.ELECTRICITE.ToString() },
                    new cl_Param { p_nIdParam = 8, p_sSection = eSection.CHARGES_VARIABLES.ToString(), p_sParameter = eChargesVariables.TRANSPORT.ToString() },
                    new cl_Param { p_nIdParam = 9, p_sSection = eSection.CREDITS.ToString(), p_sParameter = eCredits.VOITURE.ToString() }
                );
            });

            pclModelBuilder.Entity<cl_Personne>(entity =>
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
                    new cl_Personne { p_nIdPersonne = 1, p_sNom = "PERE" },
                    new cl_Personne { p_nIdPersonne = 2, p_sNom = "MERE" }
                );
            });

            pclModelBuilder.Entity<cl_TypeDepense>(entity =>
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

                entity.HasOne(d => d.p_clIdNatureNavigation).WithMany(p => p.p_aclTypesDepenses)
                    .HasForeignKey(d => d.p_nIdNature)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_type_depense_tbl_natures_depenses");
                // Seeding default data
                entity.HasData(
                    new cl_TypeDepense { p_nIdType = 1, p_sLibelle = "Nourriture et sanitaire", p_nIdNature = 1, p_nRepartition = 38, p_bEquallyDivisible = false, p_bHebdo = true },
                    new cl_TypeDepense { p_nIdType = 2, p_sLibelle = "Santé", p_nIdNature = 1, p_nRepartition = 24, p_bEquallyDivisible = false, p_bHebdo = false },
                    new cl_TypeDepense { p_nIdType = 3, p_sLibelle = "Equipements", p_nIdNature = 2, p_nRepartition = 24, p_bEquallyDivisible = false, p_bHebdo = false },
                    new cl_TypeDepense { p_nIdType = 4, p_sLibelle = "Essence", p_nIdNature = 1, p_nRepartition = 38, p_bEquallyDivisible = false, p_bHebdo = true },
                    new cl_TypeDepense { p_nIdType = 5, p_sLibelle = "Vêtements", p_nIdNature = 2, p_nRepartition = 32, p_bEquallyDivisible = true, p_bHebdo = false },
                    new cl_TypeDepense { p_nIdType = 6, p_sLibelle = "Voyage", p_nIdNature = 2, p_nRepartition = 26, p_bEquallyDivisible = false, p_bHebdo = false },
                    new cl_TypeDepense { p_nIdType = 7, p_sLibelle = "Resto", p_nIdNature = 2, p_nRepartition = 9, p_bEquallyDivisible = false, p_bHebdo = false },
                    new cl_TypeDepense { p_nIdType = 8, p_sLibelle = "Loisirs", p_nIdNature = 2, p_nRepartition = 9, p_bEquallyDivisible = false, p_bHebdo = false }
                );
            });

            pclModelBuilder.Entity<cl_Wishlist>(entity =>
            {
                entity.HasKey(e => e.p_nIdWishlist).HasName("PK_tbl_wishlist_1");

                entity.ToTable("tbl_wishlist");

                entity.Property(e => e.p_nIdWishlist).HasColumnName("id_wishlist");
                entity.Property(e => e.p_nIdType).HasColumnName("id_type");
                entity.Property(e => e.p_sLibelle).HasColumnName("libelle");
                entity.Property(e => e.p_rPrix)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("prix");

                entity.HasOne(d => d.p_clIdTypeNavigation).WithMany(p => p.p_aclWishlists)
                    .HasForeignKey(d => d.p_nIdType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_wishlist_tbl_type_depense");
            });

            OnModelCreatingPartial(pclModelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder pclModelBuilder);
    }
}