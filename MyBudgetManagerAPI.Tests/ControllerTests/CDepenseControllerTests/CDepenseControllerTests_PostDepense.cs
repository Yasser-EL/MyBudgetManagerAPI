using System;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.ControllerTests;

public partial class CDepenseControllerTests
{
    [Fact]
    public async Task PostDepense_ShouldReturnNoContent_WhenAddSucceeds()
    {
        // Arrange
        var oDepense = new CDepense() { p_sLibelle = "Test dépense" };
        m_oMockDepenseService.Setup(service => service.bAddDepense(oDepense))
                           .ReturnsAsync(true);

        // Act
        var result = await m_oDepenseController.PostDepense(oDepense);

        // Assert
        var actionResult = Assert.IsType<ActionResult<CDepense>>(result); // Assert that it's the correct ActionResult type
        Assert.IsType<NoContentResult>(actionResult.Result);  // Now check the actual result type (NoContentResult)
    }

    [Fact]
    public async Task PostDepense_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        var oDepense = new CDepense() { p_sLibelle = "Test dépense" };

        // Set ModelState to invalid
        m_oDepenseController.ModelState.AddModelError("Error", "Model is invalid");

        // Act
        var result = await m_oDepenseController.PostDepense(oDepense);

        // Assert
        var actionResult = Assert.IsType<ActionResult<CDepense>>(result); // Assert that it's the correct ActionResult type
        Assert.IsType<BadRequestResult>(actionResult.Result);
    }

    [Fact]
    public async Task PostDepense_ShouldThrowException_WhenAddFails()
    {
        // Arrange
        var oDepense = new CDepense() { p_sLibelle = "Test dépense" };
        m_oMockDepenseService.Setup(service => service.bAddDepense(oDepense))
                           .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => m_oDepenseController.PostDepense(oDepense));
    }
}
