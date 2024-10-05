using Moq;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;
using MyBudgetManagerAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MyBudgetManagerAPI.Tests;

public class CDepenseRepositoryTests
{
    private Mock<DbSet<CDepense>> m_oMockSet;
    private Mock<CMyBudgetManagerApiDbContext> m_oMockContext;
    private CDepenseRepository m_oDepenseRepository;
    private IQueryable<CDepense> m_aoDepenses;

    //refactoring
    public CDepenseRepositoryTests()
    {
        m_oMockSet = new Mock<DbSet<CDepense>>();
        m_oMockContext = new Mock<CMyBudgetManagerApiDbContext>();
        m_oDepenseRepository = new CDepenseRepository(m_oMockContext.Object);
        PrepareMockData();
        PrepareMockSet();
    }

    private void PrepareMockData()
    {
        m_aoDepenses = new List<CDepense>
        {
            new CDepense { p_nIdDepense = 1 , p_sLibelle = "Depense 1" },
            new CDepense { p_nIdDepense = 2 , p_sLibelle = "Depense 2" }
        }.AsQueryable();
    }

    private void PrepareMockSet()
    {
        // Setup the DbSet to return the mock data
        m_oMockSet.As<IQueryable<CDepense>>().Setup(m => m.Provider).Returns(m_aoDepenses.Provider);
        m_oMockSet.As<IQueryable<CDepense>>().Setup(m => m.Expression).Returns(m_aoDepenses.Expression);
        m_oMockSet.As<IQueryable<CDepense>>().Setup(m => m.ElementType).Returns(m_aoDepenses.ElementType);
        m_oMockSet.As<IQueryable<CDepense>>().Setup(m => m.GetEnumerator()).Returns(m_aoDepenses.GetEnumerator());


        // Set up FindAsync to return the correct depense based on the id
        m_oMockSet.Setup(m => m.FindAsync(It.IsAny<int>()))
            .ReturnsAsync((object[] ids) =>
            {
                int id = (int)ids[0];
                return m_aoDepenses.FirstOrDefault(d => d.p_nIdDepense == id);
            });

        m_oMockContext.Setup(m => m.p_oDepenses).Returns(m_oMockSet.Object);
    }

    //Tests : bDepenseExiste
    [Fact]
    public void bDepenseExiste_ShouldReturnTrue_WhenDepenseExists()
    {
        // Arrange

        // Setup the context to return the mocked DbSet
        m_oMockContext.Setup(c => c.p_oDepenses).Returns(m_oMockSet.Object);

        // Act
        var result = m_oDepenseRepository.bDepenseExiste(1);

        // Assert
        Assert.True(result);  // Should return true because a Depense with Id 1 exists
    }

    [Fact]
    public void bDepenseExiste_ShouldReturnFalse_WhenDepenseDoesNotExist()
    {
        // Arrange
        // Setup the context to return the mocked DbSet
        m_oMockContext.Setup(c => c.p_oDepenses).Returns(m_oMockSet.Object);

        // Act
        var result = m_oDepenseRepository.bDepenseExiste(3);

        // Assert
        Assert.False(result);  // Should return false because no Depense exists
    }

    //Tests bDeleteDepense

    [Fact]
    public async Task bDeleteDepense_ExistingDepense_RemovesDepenseAndReturnsTrue()
    {
        // Arrange
        int depenseId = 1;
        CDepense l_oDepense = m_aoDepenses.First();
        m_oMockSet.Setup(m => m.FindAsync(depenseId))
                        .ReturnsAsync(l_oDepense);

        // Act
        bool result = await m_oDepenseRepository.bDeleteDepense(depenseId);

        // Assert
        Assert.True(result);
        m_oMockSet.Verify(m => m.Remove(l_oDepense), Times.Once);
        m_oMockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task bDeleteDepense_NonExistingDepense_ReturnsFalse()
    {
        // Arrange
        int depenseId = 3;
        m_oMockSet.Setup(m => m.FindAsync(depenseId))
            .ReturnsAsync((CDepense)null); // Simulate that the depense doesn't exist

        // Act
        bool result = await m_oDepenseRepository.bDeleteDepense(depenseId);

        // Assert
        Assert.False(result);
        m_oMockSet.Verify(m => m.Remove(It.IsAny<CDepense>()), Times.Never);
        m_oMockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task bDeleteDepense_ExceptionThrown_ReturnsFalse()
    {
        // Arrange
        int depenseId = 3;
        var depense = new CDepense { p_nIdDepense = depenseId, p_sLibelle = "Depense test" };
        m_oMockSet.Setup(m => m.FindAsync(depenseId))
            .ReturnsAsync(depense);

        m_oMockContext.Setup(m => m.SaveChangesAsync(CancellationToken.None)).ThrowsAsync(new Exception("Error saving changes"));

        // Act
        bool result = await m_oDepenseRepository.bDeleteDepense(depenseId);

        // Assert
        Assert.False(result);
        m_oMockSet.Verify(m => m.Remove(depense), Times.Once);
        m_oMockContext.Verify(m => m.SaveChangesAsync(CancellationToken.None), Times.Once);
    }
}
