using System;

namespace MyBudgetManagerAPI.Tests.RepositoryTests;

public partial class CDepenseRepositoryTests
{
    //Tests : aoGetDepenses
    [Fact]
    public async Task aoGetDepenses_FilterByMonthAndYear_ReturnsMatchingDepenses()
    {
        // Arrange
        int a_nMois = 10;
        int a_nAnnee = 2024;
        int? a_nSemaine = null;

        // Act
        var result = await m_oDepenseRepository.aoGetDepenses(a_nMois, a_nAnnee, a_nSemaine);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count); // Expecting 2 matching entries for the month of October 2024
        Assert.Contains(result, d => d.p_sLibelle == "Depense 1");
        Assert.Contains(result, d => d.p_sLibelle == "Depense 3");
    }

    [Fact]
    public async Task aoGetDepenses_FilterByMonthYearAndWeek_ReturnsMatchingDepenses()
    {
        // Arrange
        int a_nMois = 10;
        int a_nAnnee = 2024;
        int a_nSemaine = 1;

        // Act
        var result = await m_oDepenseRepository.aoGetDepenses(a_nMois, a_nAnnee, a_nSemaine);

        // Assert
        Assert.Single(result);
        Assert.Equal(a_nMois, result.First().p_nMois);
        Assert.Equal(a_nAnnee, result.First().p_nAnnee);
        Assert.Equal(a_nSemaine, result.First().p_nSemaine);
    }

    [Fact]
    public async Task aoGetDepenses_NoMatchingDepenses_ReturnsEmptyList()
    {
        // Arrange
        int a_nMois = 10;
        int a_nAnnee = 2024;
        int a_nSemaine = 3;
        // Act
        var result = await m_oDepenseRepository.aoGetDepenses(a_nMois, a_nAnnee, a_nSemaine);

        // Assert
        Assert.Empty(result);
    }
}
