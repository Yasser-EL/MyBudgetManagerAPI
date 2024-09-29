using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Repository;

public class CPersonneRepository
{
    private readonly CMyBudgetManagerApiDbContext m_oContext;
    public CPersonneRepository(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oContext = a_oContext;
    }

    internal async Task<ActionResult<IEnumerable<CPersonne>>> GetAllPersonnes()
    {
        return await m_oContext.p_oPersonnes.ToListAsync();
    }
}
