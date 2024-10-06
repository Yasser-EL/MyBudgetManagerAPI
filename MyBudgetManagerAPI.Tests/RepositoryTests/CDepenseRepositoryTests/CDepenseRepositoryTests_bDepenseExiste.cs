using System;

namespace MyBudgetManagerAPI.Tests.RepositoryTests;

public partial class CDepenseRepositoryTests
{
    //Tests : bDepenseExiste
    [Fact]
    public void bDepenseExiste_ShouldReturnTrue_WhenDepenseExists()
    {
        // Arrange
        int l_nTestId = 1;

        // Act
        var result = m_oDepenseRepository.bDepenseExiste(l_nTestId);

        // Assert
        Assert.True(result);  // Should return true because a Depense with Id 1 exists
    }

    [Fact]
    public void bDepenseExiste_ShouldReturnFalse_WhenDepenseDoesNotExist()
    {
        // Arrange
        int l_nTestId = 99;

        // Act
        var result = m_oDepenseRepository.bDepenseExiste(l_nTestId);

        // Assert
        Assert.False(result);  // Should return false because no Depense exists
    }
}
