using System;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.RepositoryTests;

public partial class CDepenseRepositoryTests
{
    [Fact]
    public async Task dGetTotalDepenses_NullFilters_ReturnsCorrectTotal()
    {
        // Arrange

        // Act
        decimal result = await m_oDepenseRepository.dGetTotalDepenses(1, null, null, null, 2024);

        // Assert
        Assert.Equal(300, result);
    }

    [Fact]
    public async Task dGetTotalDepenses_SpecificMonthAndYear_ReturnsCorrectTotal()
    {
        // Arrange

        // Act
        decimal result = await m_oDepenseRepository.dGetTotalDepenses(2, null, null, 1, 2024);

        // Assert
        Assert.Equal(300, result);
    }

    [Fact]
    public async Task dGetTotalDepenses_SpecificWeekAndMonthAndYear_ReturnsCorrectTotal()
    {
        // Arrange

        // Act
        decimal result = await m_oDepenseRepository.dGetTotalDepenses(1, null, 1, 3, 2024);

        // Assert
        Assert.Equal(100, result);
    }

    [Fact]
    public async Task dGetTotalDepenses_SpecificPersonne_ReturnsCorrectTotal()
    {
        // Arrange

        // Act
        decimal result = await m_oDepenseRepository.dGetTotalDepenses(1, 1, null, null, 2024);

        // Assert
        Assert.Equal(300, result);
    }

    [Fact]
    public async Task dGetTotalDepenses_ValidDepensesWithAllFilters_ReturnsCorrectTotal()
    {
        // Arrange
        // Act
        decimal result = await m_oDepenseRepository.dGetTotalDepenses(1, 1, 2, 3, 2024);

        // Assert
        Assert.Equal(300, result); // 100 + 200
    }

    [Fact]
    public async Task dGetTotalDepenses_NoMatchingDepenses_ReturnsZero()
    {
        // Act
        decimal result = await m_oDepenseRepository.dGetTotalDepenses(1, 3, 5, 6, 2025);

        // Assert
        Assert.Equal(0, result); // No depenses match these filters
    }
}
