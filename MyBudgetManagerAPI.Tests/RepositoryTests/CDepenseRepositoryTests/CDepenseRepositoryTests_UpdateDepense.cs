using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Tests.RepositoryTests;

public partial class CDepenseRepositoryTests
{
    [Fact]
    public async Task UpdateDepense_ValidDepense_UpdatesSuccessfully()
    {
        // Arrange
        var depense = new CDepense { p_nIdDepense = 1, p_sLibelle = "Test Depense" };

        // Act
        // Detach any existing tracked entity with the same primary key
        var existingEntity = await m_oTestContext.p_oDepenses.FindAsync(depense.p_nIdDepense);
        if (existingEntity != null)
        {
            m_oTestContext.Entry(existingEntity).State = EntityState.Detached;
        }
        bool result = await m_oDepenseRepository.bUpdateDepense(depense);

        // Assert
        Assert.True(result); // Verify the state was changed
    }

    [Fact]
    public async Task UpdateDepense_NonExistentDepense_ReturnsFalse()
    {
        // Arrange
        var nonExistentDepense = new CDepense { p_nIdDepense = 999, p_sLibelle = "Non-existent Depense" };

        // Act
        bool result = await m_oDepenseRepository.bUpdateDepense(nonExistentDepense);

        // Assert
        Assert.False(result); // It should return false as the entity does not exist
    }
}
