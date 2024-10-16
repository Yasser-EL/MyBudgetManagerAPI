﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Personne")]
public partial class CPersonne
{
    [JsonProperty("IdPersonne")]
    [Required(ErrorMessage = "Le champ IdPersonne est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdPersonne doit être supérieur à 0.")]
    public byte p_nIdPersonne { get; set; }

    [JsonProperty("Nom")]
    [Required(ErrorMessage = "Le champ Nom est obligatoire.")]
    [MaxLength(50, ErrorMessage = "Le champs Nom doit avoir un maximum de 50 caractères.")]
    public required string p_sNom { get; set; }

    [JsonProperty("Dettes")]
    public decimal? p_rDettes { get; set; }

    [JsonProperty("Salaire")]
    public decimal? p_rSalaire { get; set; }

    [JsonIgnore]
    public virtual ICollection<CDepense> p_aoDepenses { get; set; } = new List<CDepense>();
}
