using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Repository;

public class CNatureDepenseRepository
{
    private readonly CMyBudgetManagerApiDbContext m_oContext;

    public CNatureDepenseRepository(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oContext = a_oContext;
    }

    public async Task<ActionResult<IEnumerable<CNatureDepense>>> aoGetAllNaturesDepenses()
    {
        return await m_oContext.p_oNaturesDepenses.ToListAsync();
    }

    public async Task<CNatureDepense?> oGetNatureDepense(byte a_nId)
    {
        return await m_oContext.p_oNaturesDepenses.FindAsync(a_nId);
    }

    internal bool bNatureDepenseExiste(byte a_nId)
    {
        return m_oContext.p_oNaturesDepenses.Any(e => e.p_nIdNature == a_nId);
    }

    internal async Task UpdateNatureDepense(object a_oNatureDepense)
    {
        m_oContext.Entry(a_oNatureDepense).State = EntityState.Modified;

        try
        {
            await m_oContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }
}
