using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBudgetManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_natures_depenses",
                columns: table => new
                {
                    id_nature = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pourcentage = table.Column<byte>(type: "tinyint", nullable: false),
                    libelle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_NatureDepense", x => x.id_nature);
                });

            migrationBuilder.CreateTable(
                name: "tbl_param",
                columns: table => new
                {
                    id_param = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    section = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    parameter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    value = table.Column<decimal>(type: "numeric(7,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Param", x => x.id_param);
                });

            migrationBuilder.CreateTable(
                name: "tbl_personne",
                columns: table => new
                {
                    id_personne = table.Column<byte>(type: "tinyint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dettes = table.Column<decimal>(type: "numeric(7,2)", nullable: true),
                    salaire = table.Column<decimal>(type: "numeric(7,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Personne", x => x.id_personne);
                });

            migrationBuilder.CreateTable(
                name: "tbl_types_depenses",
                columns: table => new
                {
                    id_type = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libelle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    id_nature = table.Column<byte>(type: "tinyint", nullable: false),
                    repartition = table.Column<byte>(type: "tinyint", nullable: true),
                    equally_divisible = table.Column<bool>(type: "bit", nullable: true),
                    hebdo = table.Column<bool>(type: "bit", nullable: false),
                    semaine_1 = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    semaine_2 = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    semaine_3 = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    semaine_4 = table.Column<decimal>(type: "numeric(7,2)", nullable: false),
                    total = table.Column<decimal>(type: "numeric(7,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_TypeDepense", x => x.id_type);
                    table.ForeignKey(
                        name: "FK_tbl_type_depense_tbl_natures_depenses",
                        column: x => x.id_nature,
                        principalTable: "tbl_natures_depenses",
                        principalColumn: "id_nature");
                });

            migrationBuilder.CreateTable(
                name: "tbl_depenses",
                columns: table => new
                {
                    id_depense = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libelle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    id_type = table.Column<int>(type: "int", nullable: true),
                    montant = table.Column<decimal>(type: "numeric(7,2)", nullable: true),
                    semaine = table.Column<byte>(type: "tinyint", nullable: true),
                    mois = table.Column<byte>(type: "tinyint", nullable: true),
                    annee = table.Column<short>(type: "smallint", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    id_personne = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Depense", x => x.id_depense);
                    table.ForeignKey(
                        name: "FK_tbl_depenses_tbl_personne",
                        column: x => x.id_personne,
                        principalTable: "tbl_personne",
                        principalColumn: "id_personne");
                    table.ForeignKey(
                        name: "FK_tbl_depenses_tbl_type_depense",
                        column: x => x.id_type,
                        principalTable: "tbl_types_depenses",
                        principalColumn: "id_type");
                });

            migrationBuilder.CreateTable(
                name: "tbl_wishlist",
                columns: table => new
                {
                    id_wishlist = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_type = table.Column<int>(type: "int", nullable: false),
                    libelle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    prix = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_wishlist_1", x => x.id_wishlist);
                    table.ForeignKey(
                        name: "FK_tbl_wishlist_tbl_type_depense",
                        column: x => x.id_type,
                        principalTable: "tbl_types_depenses",
                        principalColumn: "id_type");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_depenses_id_personne",
                table: "tbl_depenses",
                column: "id_personne");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_depenses_id_type",
                table: "tbl_depenses",
                column: "id_type");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_types_depenses_id_nature",
                table: "tbl_types_depenses",
                column: "id_nature");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_wishlist_id_type",
                table: "tbl_wishlist",
                column: "id_type");
        }
    }
}
