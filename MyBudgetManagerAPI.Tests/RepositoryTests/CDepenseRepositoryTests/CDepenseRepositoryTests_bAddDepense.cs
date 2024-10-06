using System;
using Microsoft.EntityFrameworkCore;
using Moq;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.RepositoryTests;

public partial class CDepenseRepositoryTests
{
    [Fact]
    public async Task bAddDepense_ValidDepense_ReturnsTrue()
    {
        // Arrange
        var depense = new CDepense { p_sLibelle = "Test Depense" };

        // Act
        m_oTestContext.Entry(depense).State = EntityState.Detached;
        bool result = await m_oDepenseRepository.bAddDepense(depense);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task bAddDepense_SaveChangesThrowsException_ReturnsFalse()
    {
        // Arrange
        var depense = new CDepense { p_nIdDepense = 2, p_sLibelle = "Test Depense 2" };
        // Act
        bool result = await m_oDepenseRepository.bAddDepense(depense);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task bAddDepense_NullDepense_ReturnsFalse()
    {
        // Act
        bool result = await m_oDepenseRepository.bAddDepense(null);

        // Assert
        Assert.False(result);
    }
}
