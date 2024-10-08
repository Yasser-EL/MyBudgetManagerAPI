using System;
using Moq;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.ServiceTests;
public partial class CDepenseServiceTests
{
    [Fact]
    public async Task nUpdateDepense_ShouldReturnNegativeOne_WhenIdsDoNotMatch()
    {
        // Arrange
        int id = 1;
        var depense = new CDepense { p_nIdDepense = 2, p_sLibelle = "Test dépense" }; // Different ID

        // Act
        var result = await m_oDepenseService.nUpdateDepense(id, depense);

        // Assert
        Assert.Equal(-1, result); // IDs do not match
    }

    [Fact]
    public async Task nUpdateDepense_ShouldReturnZero_WhenDepenseDoesNotExist()
    {
        // Arrange
        int id = 1;
        var depense = new CDepense { p_nIdDepense = id, p_sLibelle = "Dépense test" }; // Matching ID

        m_oMockDepenseRepository
            .Setup(repo => repo.bDepenseExiste(id))
            .Returns(false); // Depense does not exist

        // Act
        var result = await m_oDepenseService.nUpdateDepense(id, depense);

        // Assert
        Assert.Equal(0, result); // Depense not found
    }

    [Fact]
    public async Task nUpdateDepense_ShouldReturnNegativeTwo_WhenUpdateFails()
    {
        // Arrange
        int id = 1;
        var depense = new CDepense { p_nIdDepense = id, p_sLibelle = "Test test" }; // Matching ID

        m_oMockDepenseRepository
            .Setup(repo => repo.bDepenseExiste(id))
            .Returns(true); // Depense exists

        m_oMockDepenseRepository
            .Setup(repo => repo.bUpdateDepense(depense))
            .ReturnsAsync(false); // Update fails

        // Act
        var result = await m_oDepenseService.nUpdateDepense(id, depense);

        // Assert
        Assert.Equal(-2, result); // Update failure
    }

    [Fact]
    public async Task nUpdateDepense_ShouldReturnOne_WhenUpdateIsSuccessful()
    {
        // Arrange
        int id = 1;
        var depense = new CDepense { p_nIdDepense = id, p_sLibelle = "Dépense dépense" }; // Matching ID

        m_oMockDepenseRepository
            .Setup(repo => repo.bDepenseExiste(id))
            .Returns(true); // Depense exists

        m_oMockDepenseRepository
            .Setup(repo => repo.bUpdateDepense(depense))
            .ReturnsAsync(true); // Update successful

        // Act
        var result = await m_oDepenseService.nUpdateDepense(id, depense);

        // Assert
        Assert.Equal(1, result); // Everything is OK
    }

    [Fact]
    public async Task nUpdateDepense_ShouldReturnNegativeTwo_WhenExceptionIsThrown()
    {
        // Arrange
        int id = 1;
        var depense = new CDepense { p_nIdDepense = id, p_sLibelle = "Test dépense" }; // Matching ID

        m_oMockDepenseRepository
            .Setup(repo => repo.bDepenseExiste(id))
            .Throws(new Exception("Some internal error")); // Simulate an exception

        // Act
        var result = await m_oDepenseService.nUpdateDepense(id, depense);

        // Assert
        Assert.Equal(-2, result); // Internal error
    }
}
