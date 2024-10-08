using Microsoft.AspNetCore.Mvc;
using Moq;

namespace MyBudgetManagerAPI.Tests.ControllerTests;

public partial class CDepenseControllerTests
{
    [Fact]
    public async Task DeleteDepense_ShouldReturnNoContent_WhenDeleteSucceeds()
    {
        // Arrange
        var depenseId = 1;
        m_oMockDepenseService.Setup(service => service.bDeleteDepense(depenseId))
                           .ReturnsAsync(true);

        // Act
        var result = await m_oDepenseController.DeleteDepense(depenseId);

        // Assert
        var actionResult = Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteDepense_ShouldReturnBadRequest_WhenDeleteFails()
    {
        // Arrange
        var depenseId = 1;
        m_oMockDepenseService.Setup(service => service.bDeleteDepense(depenseId))
                           .ReturnsAsync(false);

        // Act
        var result = await m_oDepenseController.DeleteDepense(depenseId);

        // Assert
        var actionResult = Assert.IsType<BadRequestResult>(result);
    }
}
