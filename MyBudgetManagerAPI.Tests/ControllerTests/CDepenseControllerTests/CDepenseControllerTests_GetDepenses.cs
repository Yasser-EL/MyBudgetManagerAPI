using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.ControllerTests;

public partial class CDepenseControllerTests
{
    [Fact]
    public async Task GetDepenses_ShouldReturnOk_WithListOfDepenses_WhenInputsAreValid()
    {
        // Arrange
        int nMois = 5;
        int nAnnee = 2024;
        int? nSemaine = 2;

        var depenses = new List<CDepense> { new CDepense() { p_sLibelle = "Depense 1" }, new CDepense() { p_sLibelle = "Depense 2" } };

        m_oMockDepenseService
                        .Setup(service => service.aoGetDepenses(nMois, nAnnee, nSemaine))
                        .ReturnsAsync(depenses);

        // Act
        var result = await m_oDepenseController.GetDepenses(nMois, nAnnee, nSemaine);

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<CDepense>>>(result);
        var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
        var returnValue = Assert.IsType<List<CDepense>>(okResult.Value);

        Assert.Equal(depenses.Count, returnValue.Count);
    }

    [Fact]
    public async Task GetDepenses_ShouldReturnBadRequest_WhenServiceThrowsException()
    {
        // Arrange
        int nMois = 5;
        int nAnnee = 2024;
        int? nSemaine = null;

        m_oMockDepenseService
            .Setup(service => service.aoGetDepenses(nMois, nAnnee, nSemaine))
            .ThrowsAsync(new ArgumentException("Invalid parameters"));

        // Act
        var result = await m_oDepenseController.GetDepenses(nMois, nAnnee, nSemaine);

        // Assert
        var actionResult = Assert.IsType<ActionResult<IEnumerable<CDepense>>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task GetDepenses_ShouldCallService_WithCorrectParameters()
    {
        // Arrange
        int nMois = 7;
        int nAnnee = 2023;
        int? nSemaine = null;

        m_oMockDepenseService
            .Setup(service => service.aoGetDepenses(nMois, nAnnee, nSemaine))
            .ReturnsAsync(new List<CDepense>());

        // Act
        var result = await m_oDepenseController.GetDepenses(nMois, nAnnee, nSemaine);

        // Assert
        m_oMockDepenseService.Verify(s => s.aoGetDepenses(nMois, nAnnee, nSemaine), Times.Once);
    }
}
