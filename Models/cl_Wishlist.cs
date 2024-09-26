using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Wishlist")]
public partial class cl_Wishlist
{
    [JsonPropertyName("IdType")]
    public int p_nIdType { get; set; }

    [JsonPropertyName("IdWishlist")]
    public int p_nIdWishlist { get; set; }

    [JsonPropertyName("Libelle")]
    public string p_sLibelle { get; set; } = null!;

    [JsonPropertyName("Prix")]
    public decimal p_rPrix { get; set; }

    [JsonIgnore]
    public virtual cl_TypeDepense p_clIdTypeNavigation { get; set; } = null!;
}
