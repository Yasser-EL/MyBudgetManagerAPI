using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Personne")]
public partial class cl_Personne
{
    [JsonPropertyName("IdPersonne")]
    public byte p_nIdPersonne { get; set; }

    [JsonPropertyName("Nom")]
    public string? p_sNom { get; set; }

    [JsonPropertyName("Dettes")]
    public decimal? p_rDettes { get; set; }

    [JsonPropertyName("Salaire")]
    public decimal? p_rSalaire { get; set; }

    [JsonIgnore]
    public virtual ICollection<cl_Depense> p_aclDepenses { get; set; } = new List<cl_Depense>();
}
