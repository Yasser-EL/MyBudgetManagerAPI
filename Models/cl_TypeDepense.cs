using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("TypeDepense")]
public partial class cl_TypeDepense
{
    [JsonPropertyName("IdType")]
    public int p_nIdType { get; set; }

    [JsonPropertyName("Libelle")]
    public string p_sLibelle { get; set; } = null!;

    [JsonPropertyName("IdNature")]
    public byte p_nIdNature { get; set; }

    [JsonPropertyName("Repartition")]
    public byte? p_nRepartition { get; set; }

    [JsonPropertyName("EquallyDivisible")]
    public bool? p_bEquallyDivisible { get; set; }

    [JsonPropertyName("Hebdo")]
    public bool p_bHebdo { get; set; }

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
    public virtual cl_NatureDepense p_clIdNatureNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<cl_Depense> p_aclDepenses { get; set; } = new List<cl_Depense>();

    [JsonIgnore]
    public virtual ICollection<cl_Wishlist> p_aclWishlists { get; set; } = new List<cl_Wishlist>();
}
