using System;
using Moq;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.RepositoryTests;

public partial class CDepenseRepositoryTests
{

    //Tests bDeleteDepense
    [Fact]
    public async Task bDeleteDepense_ExistingDepense_RemovesDepenseAndReturnsTrue()
    {
        // Arrange
        int l_nTestId = 1;

        // Act
        bool result = await m_oDepenseRepository.bDeleteDepense(l_nTestId);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task bDeleteDepense_NonExistingDepense_ReturnsFalse()
    {
        // Arrange
        int l_nTestId = 99;
        // Act
        bool result = await m_oDepenseRepository.bDeleteDepense(l_nTestId);

        // Assert
        Assert.False(result);
    }
}
