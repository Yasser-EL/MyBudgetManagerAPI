using Microsoft.AspNetCore.Mvc;
using Moq;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.ControllerTests;

public partial class CDepenseControllerTests
{
    [Fact]
    public async Task PutDepense_ShouldReturnNoContent_WhenUpdateSucceeds()
    {
        // Arrange
        int nId = 1;
        var oDepense = new CDepense { p_nIdDepense = nId, p_sLibelle = "Dépense test" };
        m_oMockDepenseService.Setup(service => service.nUpdateDepense(nId, oDepense))
                           .ReturnsAsync(1);

        // Act
        var result = await m_oDepenseController.PutDepense(nId, oDepense);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task PutDepense_ShouldReturnNotFound_WhenDepenseNotFound()
    {
        // Arrange
        int nId = 1;
        var oDepense = new CDepense { p_nIdDepense = nId, p_sLibelle = "Test dépense" };
        m_oMockDepenseService.Setup(service => service.nUpdateDepense(nId, oDepense))
                           .ReturnsAsync(0);

        // Act
        var result = await m_oDepenseController.PutDepense(nId, oDepense);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutDepense_ShouldReturnBadRequest_WhenIdsMismatch()
    {
        // Arrange
        int nId = 1;
        var oDepense = new CDepense { p_nIdDepense = 2, p_sLibelle = "Dépense test" }; // Mismatched IDs
        m_oMockDepenseService.Setup(service => service.nUpdateDepense(nId, oDepense))
                           .ReturnsAsync(-1);

        // Act
        var result = await m_oDepenseController.PutDepense(nId, oDepense);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutDepense_ShouldReturnBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        int nId = 1;
        var oDepense = new CDepense { p_nIdDepense = nId, p_sLibelle = "Test dépense" };

        // Set ModelState to invalid
        m_oDepenseController.ModelState.AddModelError("Error", "Model is invalid");

        // Act
        var result = await m_oDepenseController.PutDepense(nId, oDepense);

        // Assert
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutDepense_ShouldThrowException_WhenInternalErrorOccurs()
    {
        // Arrange
        int nId = 1;
        var oDepense = new CDepense { p_nIdDepense = nId, p_sLibelle = "Test dépense" };
        m_oMockDepenseService.Setup(service => service.nUpdateDepense(nId, oDepense))
                           .ReturnsAsync(-2); // Internal error state

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => m_oDepenseController.PutDepense(nId, oDepense));
    }
}
