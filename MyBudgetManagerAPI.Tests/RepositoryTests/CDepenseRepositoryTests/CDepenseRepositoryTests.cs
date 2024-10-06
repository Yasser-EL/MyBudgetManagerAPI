using Moq;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;
using MyBudgetManagerAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace MyBudgetManagerAPI.Tests.RepositoryTests;

public partial class CDepenseRepositoryTests
{
    private CMyBudgetManagerApiDbContext m_oTestContext;
    private CDepenseRepository m_oDepenseRepository;
    private IQueryable<CDepense> m_aoDepenses;

    //refactoring
    public CDepenseRepositoryTests()
    {
        PrepareMockData();
        //PrepareMockSet();
        PrepareMockContext();

        m_oDepenseRepository = new CDepenseRepository(m_oTestContext);
    }

    private void PrepareMockData()
    {
        m_aoDepenses = new List<CDepense>
        {
            new CDepense { p_nIdDepense = 1, p_sLibelle = "Depense 1", p_nMois = 10, p_nAnnee = 2024, p_nSemaine = 1 },
            new CDepense { p_nIdDepense = 2, p_sLibelle = "Depense 2", p_nMois = 9,  p_nAnnee = 2024, p_nSemaine = 1 },
            new CDepense { p_nIdDepense = 3, p_sLibelle = "Depense 3", p_nMois = 10, p_nAnnee = 2024, p_nSemaine = 2 },
            new CDepense { p_nIdDepense = 4, p_sLibelle = "Depense 4", p_nIdType = 1, p_nIdPersonne = 1, p_nSemaine = 1, p_nMois = 3, p_nAnnee = 2024, p_rMontant = 100 },
            new CDepense { p_nIdDepense = 5, p_sLibelle = "Depense 5", p_nIdType = 1, p_nIdPersonne = 1, p_nSemaine = 2, p_nMois = 3, p_nAnnee = 2024, p_rMontant = 200 },
            new CDepense { p_nIdDepense = 6, p_sLibelle = "Depense 6", p_nIdType = 2, p_nIdPersonne = 2, p_nSemaine = 3, p_nMois = 3, p_nAnnee = 2024, p_rMontant = 300 },
            new CDepense { p_nIdDepense = 7, p_sLibelle = "Depense 7", p_nIdType = 2, p_nIdPersonne = 2, p_nSemaine = 2, p_nMois = 1, p_nAnnee = 2024, p_rMontant = 300 } // Different type
        }.AsQueryable();
    }

    private void PrepareMockContext()
    {
        DbContextOptions<CMyBudgetManagerApiDbContext> l_oOptions = new DbContextOptionsBuilder<CMyBudgetManagerApiDbContext>()
                                                                        .UseInMemoryDatabase(databaseName: "TestDatabase")
                                                                        .Options;

        m_oTestContext = new CMyBudgetManagerApiDbContext(l_oOptions);
        m_oTestContext.Database.EnsureDeleted();
        m_oTestContext.Database.EnsureCreated();

        m_oTestContext.p_oDepenses.AddRange(m_aoDepenses);
        m_oTestContext.SaveChanges();
    }
}
