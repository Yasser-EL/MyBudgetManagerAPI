using Moq;
using MyBudgetManagerAPI.Controllers;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Tests.ControllerTests;

public partial class CDepenseControllerTests
{
    private readonly CDepenseController m_oDepenseController;
    private readonly Mock<CDepenseService> m_oMockDepenseService;

    public CDepenseControllerTests()
    {
        m_oMockDepenseService = new Mock<CDepenseService>(new CMyBudgetManagerApiDbContext());
        m_oDepenseController = new CDepenseController(m_oMockDepenseService.Object);
    }
}
