using System;
using Moq;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Repository;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Tests.ServiceTests;

public partial class CDepenseServiceTests
{
    private readonly Mock<CDepenseRepository> m_oMockDepenseRepository;
    private readonly CDepenseService m_oDepenseService;

    public CDepenseServiceTests()
    {
        m_oMockDepenseRepository = new Mock<CDepenseRepository>(new CMyBudgetManagerApiDbContext());
        m_oDepenseService = new CDepenseService(m_oMockDepenseRepository.Object);
    }
}
