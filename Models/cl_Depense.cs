using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Depense")]
public partial class cl_Depense
{
    [JsonPropertyName("IdDepense")]
    public int p_nIdDepense { get; set; }

    [JsonPropertyName("Libelle")]
    public string? p_sLibelle { get; set; }

    [JsonPropertyName("IdType")]
    public int? p_nIdType { get; set; }

    [JsonPropertyName("Montant")]
    public decimal? p_rMontant { get; set; }

    [JsonPropertyName("Semaine")]
    public byte? p_nSemaine { get; set; }

    [JsonPropertyName("Mois")]
    public byte? p_nMois { get; set; }

    [JsonPropertyName("Annee")]
    public short? p_nAnnee { get; set; }

    [JsonPropertyName("Date")]
    public DateOnly? p_dDate { get; set; }

    [JsonPropertyName("IdPersonne")]
    public byte p_nIdPersonne { get; set; }

    //Hide Navigation Properties from API Responses
    [JsonIgnore]
    public virtual cl_Personne p_clIdPersonneNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual cl_TypeDepense? p_clIdTypeNavigation { get; set; }
}
