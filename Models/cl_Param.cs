using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MyBudgetManagerAPI.Models;

[DisplayName("Param")]
public partial class cl_Param
{
    [JsonPropertyName("IdParam")]
    public int p_nIdParam { get; set; }

    [JsonPropertyName("Section")]
    public string? p_sSection { get; set; }

    [JsonPropertyName("Parameter")]
    public string? p_sParameter { get; set; }

    [JsonPropertyName("Value")]
    public decimal? p_rValue { get; set; }
}
