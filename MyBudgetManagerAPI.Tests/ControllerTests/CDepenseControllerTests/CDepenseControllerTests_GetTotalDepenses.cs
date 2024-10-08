using System;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MyBudgetManagerAPI.Tests.ControllerTests;

public partial class CDepenseControllerTests
{
    [Fact]
    public async Task GetTotalDepenses_ShouldReturnOk_WithTotalAmount_WhenInputsAreValid()
    {
        // Arrange
        int nIdTypeDepense = 1;
        int? nIdPersonne = 2;
        int? nSemaine = 3;
        int? nMois = 4;
        decimal expectedTotal = 100.50m;

        m_oMockDepenseService
            .Setup(service => service.dGetTotalDepenses(nIdTypeDepense, nIdPersonne, nSemaine, nMois))
            .ReturnsAsync(expectedTotal);

        // Act
        var result = await m_oDepenseController.GetTotalDepenses(nIdTypeDepense, nIdPersonne, nSemaine, nMois);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        // Access the total value from the anonymous type by using a dynamic object
        var returnValue = actionResult.Value;
        var total = (decimal)returnValue.GetType().GetProperty("total").GetValue(returnValue, null);

        Assert.Equal(expectedTotal, total);
    }

    [Fact]
    public async Task GetTotalDepenses_ShouldReturnBadRequest_WhenArgumentExceptionIsThrown()
    {
        // Arrange
        int nIdTypeDepense = 1;
        int? nIdPersonne = null;
        int? nSemaine = 3;
        int? nMois = 13;  // Invalid month

        m_oMockDepenseService
            .Setup(service => service.dGetTotalDepenses(nIdTypeDepense, nIdPersonne, nSemaine, nMois))
            .ThrowsAsync(new ArgumentException("Invalid month"));

        // Act
        var result = await m_oDepenseController.GetTotalDepenses(nIdTypeDepense, nIdPersonne, nSemaine, nMois);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        var returnValue = Assert.IsType<string>(actionResult.Value);

        Assert.Contains("Invalid month", returnValue);
    }

    [Fact]
    public async Task GetTotalDepenses_ShouldReturnOk_WithDefaultValues_WhenOptionalParametersAreNull()
    {
        // Arrange
        int nIdTypeDepense = 1;
        int? nIdPersonne = null;  // Optional parameter
        int? nSemaine = null;     // Optional parameter
        int? nMois = null;        // Optional parameter
        decimal expectedTotal = 150.00m;

        m_oMockDepenseService
            .Setup(service => service.dGetTotalDepenses(nIdTypeDepense, nIdPersonne, nSemaine, nMois))
            .ReturnsAsync(expectedTotal);

        // Act
        var result = await m_oDepenseController.GetTotalDepenses(nIdTypeDepense, nIdPersonne, nSemaine, nMois);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = actionResult.Value;
        var total = (decimal)returnValue.GetType().GetProperty("total").GetValue(returnValue, null);

        Assert.Equal(expectedTotal, total);
    }
}
