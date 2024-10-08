using System;
using Moq;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Tests.ServiceTests;

public partial class CDepenseServiceTests
{
    [Fact]
    public async Task aoGetDepenses_ShouldThrowArgumentException_WhenMoisIsInvalid()
    {
        // Arrange
        int invalidMois = 13;
        int validAnnee = 2024;
        int validSemaine = 1;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            m_oDepenseService.aoGetDepenses(invalidMois, validAnnee, validSemaine));

        Assert.Equal(CDepenseService.MSG_ERRUR_MOIS_INVALIDE, ex.Message);
    }

    [Fact]
    public async Task aoGetDepenses_ShouldThrowArgumentException_WhenAnneeIsInvalid()
    {
        // Arrange
        int validMois = 1;
        int invalidAnnee = 2019; // out of valid range
        int? validSemaine = 1;

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            m_oDepenseService.aoGetDepenses(validMois, invalidAnnee, validSemaine));

        Assert.Equal(CDepenseService.MSG_ERRUR_ANNEE_INVALIDE, ex.Message);
    }

    [Fact]
    public async Task aoGetDepenses_ShouldThrowArgumentException_WhenSemaineIsInvalid()
    {
        // Arrange
        int validMois = 1;
        int validAnnee = 2022;
        int? invalidSemaine = 54; // Invalid semaine number

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(() =>
            m_oDepenseService.aoGetDepenses(validMois, validAnnee, invalidSemaine));

        Assert.Equal(CDepenseService.MSG_ERRUR_SEMAINE_INVALIDE, ex.Message);
    }

    [Fact]
    public async Task aoGetDepenses_ShouldCallRepository_WhenInputsAreValid()

    {
        // Arrange
        int validMois = 5;
        int validAnnee = 2023;
        int? validSemaine = null;

        var expectedDepenses = new List<CDepense> { new CDepense() { p_sLibelle = "Depense test" } };
        m_oMockDepenseRepository
            .Setup(repo => repo.aoGetDepenses(validMois, validAnnee, validSemaine))
            .ReturnsAsync(expectedDepenses);

        // Act
        var result = await m_oDepenseService.aoGetDepenses(validMois, validAnnee, validSemaine);

        // Assert
        Assert.Equal(expectedDepenses, result);
        m_oMockDepenseRepository.Verify(repo => repo.aoGetDepenses(validMois, validAnnee, validSemaine), Times.Once);
    }
}
