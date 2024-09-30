using Moq;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;
using MyBudgetManagerAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MyBudgetManagerAPI.Tests;

public class CDepenseRepositoryTests
{
    [Fact]
    public void bDepenseExiste_ShouldReturnTrue_WhenDepenseExists()
    {
        // Arrange
        var mockSet = new Mock<DbSet<CDepense>>();
        var mockContext = new Mock<CMyBudgetManagerApiDbContext>();

        // Simulate the DbSet data using IQueryable
        var data = new List<CDepense>
        {
            new CDepense { p_nIdDepense = 1 , p_sLibelle = "Depense 1" },
            new CDepense { p_nIdDepense = 2 , p_sLibelle = "Depense 2" }
        }.AsQueryable();

        // Setup the DbSet to return the mock data
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        // Setup the context to return the mocked DbSet
        mockContext.Setup(c => c.p_oDepenses).Returns(mockSet.Object);

        // Create an instance of the repository
        var repository = new CDepenseRepository(mockContext.Object);

        // Act
        var result = repository.bDepenseExiste(1);

        // Assert
        Assert.True(result);  // Should return true because a Depense with Id 1 exists
    }

    [Fact]
    public void bDepenseExiste_ShouldReturnFalse_WhenDepenseDoesNotExist()
    {
        // Arrange
        var mockSet = new Mock<DbSet<CDepense>>();
        var mockContext = new Mock<CMyBudgetManagerApiDbContext>();

        // Simulate empty DbSet data
        var data = new List<CDepense>().AsQueryable();

        // Setup the DbSet to return the mock data
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<CDepense>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        // Setup the context to return the mocked DbSet
        mockContext.Setup(c => c.p_oDepenses).Returns(mockSet.Object);

        // Create an instance of the repository
        var repository = new CDepenseRepository(mockContext.Object);

        // Act
        var result = repository.bDepenseExiste(1);

        // Assert
        Assert.False(result);  // Should return false because no Depense exists
    }
}
