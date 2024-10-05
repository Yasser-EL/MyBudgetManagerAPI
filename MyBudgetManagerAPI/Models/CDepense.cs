using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Depense")]
public partial class CDepense
{
    [JsonPropertyName("IdDepense")]
    [Required(ErrorMessage = "Le champ IdDepense est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdDepense doit être supérieur à 0.")]
    public int p_nIdDepense { get; set; }

    [JsonPropertyName("Libelle")]
    [Required(ErrorMessage = "Le champ Libelle est obligatoire.")]
    [MaxLength(50, ErrorMessage = "Le champs Libelle doit avoir un maximum de 50 caractères.")]
    public required string p_sLibelle { get; set; }

    [JsonPropertyName("IdType")]
    [Required(ErrorMessage = "Le champ IdType est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdType doit être supérieur à 0.")]
    public int p_nIdType { get; set; }

    [JsonPropertyName("Montant")]
    [DataType(DataType.Currency)]
    public decimal p_rMontant { get; set; }

    [JsonPropertyName("Semaine")]
    [Range(1, 4, ErrorMessage = "La Semaine doit être entre {1} et {2}.")]
    [Required(ErrorMessage = "Le champ Semaine est obligatoire.")]
    public byte p_nSemaine { get; set; }

    [JsonPropertyName("Mois")]
    [Range(1, 12, ErrorMessage = "Le Mois doit être entre {1} et {2}.")]
    [Required(ErrorMessage = "Le champ Mois est obligatoire.")]
    public byte p_nMois { get; set; }

    [JsonPropertyName("Annee")]
    [Range(2000, 2100, ErrorMessage = "L'Année doit être entre {1} et {2}.")]
    [Required(ErrorMessage = "Le champ Annee est obligatoire.")]
    public short p_nAnnee { get; set; }

    [JsonPropertyName("Date")]
    [DataType(DataType.Date)]
    public DateOnly p_dDate { get; set; }

    [JsonPropertyName("IdPersonne")]
    [Required(ErrorMessage = "Le champ IdPersonne est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdPersonne doit être supérieur à 0.")]
    public byte p_nIdPersonne { get; set; }

    [JsonIgnore]
    public virtual CPersonne p_oIdPersonneNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual CTypeDepense? p_oIdTypeNavigation { get; set; }
}
