using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("TypeDepense")]
public partial class CTypeDepense
{
    [JsonPropertyName("IdType")]
    [Required(ErrorMessage = "Le champ IdType est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdType doit être supérieur à 0.")]
    public int p_nIdType { get; set; }

    [JsonPropertyName("Libelle")]
    [Required(ErrorMessage = "Le champ Libelle est obligatoire.")]
    [MaxLength(50, ErrorMessage = "Le champs Libelle doit avoir un maximum de 50 caractères.")]
    public string p_sLibelle { get; set; } = null!;

    [JsonPropertyName("IdNature")]
    [Required(ErrorMessage = "Le champ IdNature est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdNature doit être supérieur à 0.")]
    public byte p_nIdNature { get; set; }

    [JsonPropertyName("Repartition")]
    [Range(0, 100, ErrorMessage = "La Repartition doit être entre {1} et {2}.")]
    public byte? p_nRepartition { get; set; }

    [JsonPropertyName("EquallyDivisible")]
    [Required(ErrorMessage = "Le champ EquallyDivisible est obligatoire.")]
    public bool p_bEquallyDivisible { get; set; }

    [JsonPropertyName("Hebdo")]
    [Required(ErrorMessage = "Le champ Hebdo est obligatoire.")]
    public bool? p_bHebdo { get; set; }

    [JsonPropertyName("Semaine1")]
    public decimal p_rSemaine1 { get; set; }

    [JsonPropertyName("Semaine2")]
    public decimal p_rSemaine2 { get; set; }

    [JsonPropertyName("Semaine3")]
    public decimal p_rSemaine3 { get; set; }

    [JsonPropertyName("Semaine4")]
    public decimal p_rSemaine4 { get; set; }

    [JsonPropertyName("Total")]
    public decimal p_rTotal { get; set; }

    [JsonIgnore]
    public virtual CNatureDepense p_oIdNatureNavigation { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<CDepense> p_aoDepenses { get; set; } = new List<CDepense>();
    [JsonIgnore]
    public virtual ICollection<CWishlist> p_aoWishlists { get; set; } = new List<CWishlist>();
}
