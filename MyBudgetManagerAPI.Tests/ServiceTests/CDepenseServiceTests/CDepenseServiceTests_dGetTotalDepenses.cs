using System;
using Moq;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Tests.ServiceTests;

public partial class CDepenseServiceTests
{
    [Fact]
    public async Task dGetTotalDepenses_ShouldThrowArgumentException_WhenMoisIsInvalid()
    {
        // Arrange
        int invalidMois = 13;
        int validTypeDepense = 1;
        int? validPersonne = 1;
        int? validSemaine = 1;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            m_oDepenseService.dGetTotalDepenses(validTypeDepense, validPersonne, validSemaine, invalidMois));

        Assert.Equal(CDepenseService.MSG_ERRUR_MOIS_INVALIDE, ex.Message);
    }

    [Fact]
    public async Task dGetTotalDepenses_ShouldThrowArgumentException_WhenIdTypeDepenseIsInvalid()
    {
        // Arrange
        int invalidIdTypeDepense = -1; // Invalid
        int? validPersonne = 1;
        int? validSemaine = 1;
        int? validMois = null;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            m_oDepenseService.dGetTotalDepenses(invalidIdTypeDepense, validPersonne, validSemaine, validMois));

        Assert.Equal(CDepenseService.MSG_ERRUR_ID_TYPE_DEPENSE_INVALIDE, ex.Message);
    }

    [Fact]
    public async Task dGetTotalDepenses_ShouldThrowArgumentException_WhenIdPersonneIsInvalid()
    {
        // Arrange
        int validIdTypeDepense = 1;
        int? invalidIdPersonne = -1; // Invalid
        int? validSemaine = 1;
        int? validMois = null;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            m_oDepenseService.dGetTotalDepenses(validIdTypeDepense, invalidIdPersonne, validSemaine, validMois));

        Assert.Equal(CDepenseService.MSG_ERRUR_ID_PERSONNE_INVALIDE, ex.Message);
    }

    [Fact]
    public async Task dGetTotalDepenses_ShouldThrowArgumentException_WhenSemaineIsInvalid()
    {
        // Arrange
        int validIdTypeDepense = 1;
        int? validIdPersonne = 1;
        int? invalidSemaine = 54; // Invalid semaine number
        int? validMois = 1;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            m_oDepenseService.dGetTotalDepenses(validIdTypeDepense, validIdPersonne, invalidSemaine, validMois));

        Assert.Equal(CDepenseService.MSG_ERRUR_SEMAINE_INVALIDE, ex.Message);
    }

    [Fact]
    public async Task dGetTotalDepenses_ShouldCallRepository_WhenInputsAreValid()
    {
        // Arrange
        int validIdTypeDepense = 1;
        int? validIdPersonne = 1;
        int? validSemaine = 1;
        int validMois = 5; // Valid month
        int validAnnee = DateTime.Now.Year; // Current year
        decimal expectedTotalDepenses = 100.5M;

        m_oMockDepenseRepository
            .Setup(repo => repo.dGetTotalDepenses(validIdTypeDepense, validIdPersonne, validSemaine, validMois, validAnnee))
            .ReturnsAsync(expectedTotalDepenses);

        // Act
        var result = await m_oDepenseService.dGetTotalDepenses(validIdTypeDepense, validIdPersonne, validSemaine, validMois);

        // Assert
        Assert.Equal(expectedTotalDepenses, result);
        m_oMockDepenseRepository.Verify(repo => repo.dGetTotalDepenses(validIdTypeDepense, validIdPersonne, validSemaine, validMois, validAnnee), Times.Once);
    }

    [Fact]
    public async Task dGetTotalDepenses_ShouldUseCurrentMonth_WhenMoisIsNull()
    {
        // Arrange
        int validIdTypeDepense = 1;
        int? validIdPersonne = 1;
        int? validSemaine = null;
        int validMois = DateTime.Now.Month; // Should use current month
        int validAnnee = DateTime.Now.Year; // Current year
        decimal expectedTotalDepenses = 100.5M;

        m_oMockDepenseRepository
            .Setup(repo => repo.dGetTotalDepenses(validIdTypeDepense, validIdPersonne, validSemaine, validMois, validAnnee))
            .ReturnsAsync(expectedTotalDepenses);

        // Act
        var result = await m_oDepenseService.dGetTotalDepenses(validIdTypeDepense, validIdPersonne, validSemaine, null); // Passing null for month

        // Assert
        Assert.Equal(expectedTotalDepenses, result);
        m_oMockDepenseRepository.Verify(repo => repo.dGetTotalDepenses(validIdTypeDepense, validIdPersonne, validSemaine, validMois, validAnnee), Times.Once);
    }
}
