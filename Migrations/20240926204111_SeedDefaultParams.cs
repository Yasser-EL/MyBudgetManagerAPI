using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyBudgetManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDefaultParams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tbl_natures_depenses",
                columns: new[] { "id_nature", "pourcentage", "libelle" },
                values: new object[,]
                {
                    { (byte)1, (byte)50, "Primaire" },
                    { (byte)2, (byte)30, "Secondaire" },
                    { (byte)3, (byte)20, "Epargnes" }
                });

            migrationBuilder.InsertData(
                table: "tbl_param",
                columns: new[] { "id_param", "value", "parameter", "section" },
                values: new object[,]
                {
                    { 1, 1m, "SEMAINE_EN_COURS", "PARAM" },
                    { 2, 1m, "MOIS_EN_COURS", "PARAM" },
                    { 3, null, "LOYER", "CHARGES_FIXES" },
                    { 4, null, "INTERNET", "CHARGES_FIXES" },
                    { 5, null, "TELEPHONIE", "CHARGES_FIXES" },
                    { 6, null, "EAU", "CHARGES_VARIABLES" },
                    { 7, null, "ELECTRICITE", "CHARGES_VARIABLES" },
                    { 8, null, "TRANSPORT", "CHARGES_VARIABLES" },
                    { 9, null, "VOITURE", "CREDITS" }
                });

            migrationBuilder.InsertData(
                table: "tbl_personne",
                columns: new[] { "id_personne", "dettes", "salaire", "nom" },
                values: new object[,]
                {
                    { (byte)1, null, null, "PERE" },
                    { (byte)2, null, null, "MERE" }
                });

            migrationBuilder.InsertData(
                table: "tbl_types_depenses",
                columns: new[] { "id_type", "equally_divisible", "hebdo", "id_nature", "repartition", "semaine_1", "semaine_2", "semaine_3", "semaine_4", "total", "libelle" },
                values: new object[,]
                {
                    { 1, false, true, (byte)1, (byte)38, 0m, 0m, 0m, 0m, 0m, "Nourriture et sanitaire" },
                    { 2, false, false, (byte)1, (byte)24, 0m, 0m, 0m, 0m, 0m, "Santé" },
                    { 3, false, false, (byte)2, (byte)24, 0m, 0m, 0m, 0m, 0m, "Equipements" },
                    { 4, false, true, (byte)1, (byte)38, 0m, 0m, 0m, 0m, 0m, "Essence" },
                    { 5, true, false, (byte)2, (byte)32, 0m, 0m, 0m, 0m, 0m, "Vêtements" },
                    { 6, false, false, (byte)2, (byte)26, 0m, 0m, 0m, 0m, 0m, "Voyage" },
                    { 7, false, false, (byte)2, (byte)9, 0m, 0m, 0m, 0m, 0m, "Resto" },
                    { 8, false, false, (byte)2, (byte)9, 0m, 0m, 0m, 0m, 0m, "Loisirs" }
                });
        }
    }
}
