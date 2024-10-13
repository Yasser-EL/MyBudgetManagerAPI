using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Wishlist")]
public partial class CWishlist
{
    [JsonProperty("IdWishlist")]
    [Required(ErrorMessage = "Le champ IdWishlist est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdWishlist doit être supérieur à 0.")]
    public int p_nIdWishlist { get; set; }

    [JsonProperty("IdType")]
    [Required(ErrorMessage = "Le champ IdType est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdType doit être supérieur à 0.")]
    public int p_nIdType { get; set; }

    [JsonProperty("Libelle")]
    [Required(ErrorMessage = "Le champ Libelle est obligatoire.")]
    [MaxLength(50, ErrorMessage = "Le champs Libelle doit avoir un maximum de 50 caractères.")]
    public string p_sLibelle { get; set; } = null!;

    [JsonProperty("Prix")]
    [DataType(DataType.Currency)]
    public decimal p_rPrix { get; set; }
    public virtual CTypeDepense p_oIdTypeNavigation { get; set; } = null!;
}
