using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Repository;

public class CTypeDepenseRepository
{
    private CMyBudgetManagerApiDbContext m_oContext;

    public CTypeDepenseRepository(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oContext = a_oContext;
    }

    internal async Task<ActionResult<IEnumerable<CTypeDepense>>> aoGetAllTypesDepenses()
    {
        return await m_oContext.p_oTypesDepenses.ToListAsync();
    }

    internal bool bTypeDepenseExiste(int a_nId)
    {
        return m_oContext.p_oTypesDepenses.Any(e => e.p_nIdType == a_nId);
    }

    internal async Task<CTypeDepense?> oGetTypeDepense(int a_nId)
    {
        return await m_oContext.p_oTypesDepenses.FindAsync(a_nId);
    }

    internal async Task UpdateTypeDepense(CTypeDepense a_oTypeDepense)
    {
        m_oContext.Entry(a_oTypeDepense).State = EntityState.Modified;
        await m_oContext.SaveChangesAsync();
    }
}
