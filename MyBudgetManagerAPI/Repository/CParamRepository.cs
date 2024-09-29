using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Repository;

public class CParamRepository
{
    private readonly CMyBudgetManagerApiDbContext m_oContext;
    public CParamRepository(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oContext = a_oContext;
    }

    internal async Task<decimal> dGetSommeCharges(string sSection)
    {
        decimal? l_rSum = await m_oContext.p_oParams.Where(p => p.p_sSection == sSection).SumAsync(p => p.p_rValue);

        return l_rSum ?? 0;
    }

    internal async Task<ActionResult<CParam?>> oGetParam(string sSection, string sParamName)
    {
        return await m_oContext.p_oParams.SingleOrDefaultAsync(p => p.p_sSection == sSection
                                                                && p.p_sParameter == sParamName);
    }

    internal async Task UpdateNatureDepense(CParam l_oParam)
    {
        try
        {
            m_oContext.Entry(l_oParam).State = EntityState.Modified;
            await m_oContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }
}
