using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("NatureDepense")]
public partial class cl_NatureDepense
{
    [JsonPropertyName("IdNature")]
    public byte p_nIdNature { get; set; }

    [JsonPropertyName("Pourcentage")]
    public byte p_rPourcentage { get; set; }

    [JsonPropertyName("Libelle")]
    public string? p_sLibelle { get; set; }

    [JsonIgnore]
    public virtual ICollection<cl_TypeDepense> p_aclTypesDepenses { get; set; } = new List<cl_TypeDepense>();
}
