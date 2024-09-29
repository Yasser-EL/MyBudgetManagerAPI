using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Param")]
public partial class CParam
{
    [JsonPropertyName("IdParam")]
    [Required(ErrorMessage = "Le champ IdParam est obligatoire.")]
    [Range(1, int.MaxValue, ErrorMessage = "Le champ IdParam doit être supérieur à 0.")]
    public int p_nIdParam { get; set; }

    [JsonPropertyName("Section")]
    [Required(ErrorMessage = "Le champ Section est obligatoire.")]
    [MaxLength(50, ErrorMessage = "Le champs Section doit avoir un maximum de 50 caractères.")]
    public required string p_sSection { get; set; }

    [JsonPropertyName("Parameter")]
    [Required(ErrorMessage = "Le champ Parameter est obligatoire.")]
    [MaxLength(50, ErrorMessage = "Le champs Parameter doit avoir un maximum de 50 caractères.")]
    public required string p_sParameter { get; set; }

    [JsonPropertyName("Value")]
    public decimal? p_rValue { get; set; }

    public static implicit operator CParam(ActionResult<CParam?> v)
    {
        throw new NotImplementedException();
    }
}
