using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;

namespace MyBudgetManagerAPI.Services;

public class CTypeDepenseService
{
    private readonly CTypeDepenseRepository m_oTypeDepenseRepository;
    public CTypeDepenseService(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oTypeDepenseRepository = new CTypeDepenseRepository(a_oContext);
    }

    internal async Task<ActionResult<IEnumerable<CTypeDepense>>> aoGetAllTypesDepenses()
    {
        return await m_oTypeDepenseRepository.aoGetAllTypesDepenses();
    }

    internal async Task<int> nUpdateTypeDepense(int a_nId, CTypeDepense a_oTypeDepense)
    {
        //state 1 = everything is ok
        //state 0 : depense not found
        //state -1: parameter id different than body id
        //state -2: internal error 

        int l_nState = 1;
        try
        {
            if (a_nId != a_oTypeDepense.p_nIdType)
            {
                l_nState = -1;
            }
            else if (!m_oTypeDepenseRepository.bTypeDepenseExiste(a_nId))
            {
                l_nState = 0;
            }
            else
            {
                await m_oTypeDepenseRepository.UpdateTypeDepense(a_oTypeDepense);
            }
        }
        catch (Exception ex)
        {
            l_nState = -2;
        }

        return l_nState;
    }

    internal async Task<CTypeDepense?> oGetTypeDepense(int a_nId)
    {
        return await m_oTypeDepenseRepository.oGetTypeDepense(a_nId);
    }
}
