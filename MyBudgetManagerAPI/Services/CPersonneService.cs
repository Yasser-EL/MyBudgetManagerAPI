using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;

namespace MyBudgetManagerAPI.Services;

public class CPersonneService
{
    private readonly CPersonneRepository m_oPersonneRepository;
    public CPersonneService(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oPersonneRepository = new CPersonneRepository(a_oContext);
    }

    internal async Task<ActionResult<IEnumerable<CPersonne>>> GetAllPersonnes()
    {
        return await m_oPersonneRepository.GetAllPersonnes();
    }
}
