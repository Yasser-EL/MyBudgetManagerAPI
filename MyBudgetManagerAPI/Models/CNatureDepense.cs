using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyBudgetManagerAPI.Models;

[DisplayName("NatureDepense")]
public partial class CNatureDepense
{
    [JsonProperty("IdNature")]
    [Required(ErrorMessage = "Le champ IdNature est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdNature doit être supérieur à 0.")]
    public byte p_nIdNature { get; set; }

    [JsonProperty("Pourcentage")]
    [Range(0, 100, ErrorMessage = "Le Pourcentage doit être entre {1} et {2}.")]
    public byte p_rPourcentage { get; set; }

    [JsonProperty("Libelle")]
    [Required(ErrorMessage = "Le champ Libelle est obligatoire.")]
    [MaxLength(50, ErrorMessage = "Le champs Libelle doit avoir un maximum de 50 caractères.")]
    public required string p_sLibelle { get; set; }

    [JsonIgnore]
    public virtual ICollection<CTypeDepense> p_aoTypesDepenses { get; set; } = new List<CTypeDepense>();
}
