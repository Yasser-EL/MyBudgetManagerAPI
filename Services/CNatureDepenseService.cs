using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;

namespace MyBudgetManagerAPI.Services;

public class CNatureDepenseService
{
    private readonly CNatureDepenseRepository m_oNatureDepenseRepository;

    public CNatureDepenseService(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oNatureDepenseRepository = new CNatureDepenseRepository(a_oContext);
    }

    public async Task<ActionResult<IEnumerable<CNatureDepense>>> aoGetAllNaturesDepenses()
    {
        return await m_oNatureDepenseRepository.aoGetAllNaturesDepenses();
    }

    internal async Task<int> nUpdateDepense(byte a_nId, CNatureDepense oNatureDepense)
    {
        //state 1 = everything is ok
        //state 0 : depense not found
        //state -1: parameter id different than body id
        //state -2: internal error 

        int l_nState = 1;

        try
        {
            if (a_nId != oNatureDepense.p_nIdNature)
            {
                l_nState = -1;
            }
            else if (!m_oNatureDepenseRepository.bNatureDepenseExiste(a_nId))
            {
                l_nState = 0;
            }
            else
            {
                await m_oNatureDepenseRepository.UpdateNatureDepense(oNatureDepense);
            }
        }
        catch (Exception ex)
        {
            l_nState = -2;
        }

        return l_nState;
    }

    internal async Task<CNatureDepense?> oGetNatureDepense(byte a_nId)
    {
        return await m_oNatureDepenseRepository.oGetNatureDepense(a_nId);
    }
}
