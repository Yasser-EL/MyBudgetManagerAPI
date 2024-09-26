using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Data
{

    public partial class cl_MyBudgetMangerApiDbContext : DbContext
    {
        public cl_MyBudgetMangerApiDbContext()
        {
        }

        public cl_MyBudgetMangerApiDbContext(DbContextOptions<cl_MyBudgetMangerApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<cl_Depense> p_clDepenses { get; set; }

        public virtual DbSet<cl_NatureDepense> p_clNaturesDepenses { get; set; }

        public virtual DbSet<cl_Param> p_clParams { get; set; }

        public virtual DbSet<cl_Personne> p_clPersonnes { get; set; }

        public virtual DbSet<cl_TypeDepense> p_clTypesDepenses { get; set; }

        public virtual DbSet<cl_Wishlist> p_clWishlists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=EL-PSY-CONGROO\\SQLEXPRESS;Initial Catalog=BudgetDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

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