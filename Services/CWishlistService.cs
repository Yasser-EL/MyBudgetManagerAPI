using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;

namespace MyBudgetManagerAPI.Services;

public class CWishlistService
{
    private readonly CWishlistRepository m_oWishlistRepository;
    public CWishlistService(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oWishlistRepository = new CWishlistRepository(a_oContext);
    }

    internal async Task<ActionResult<IEnumerable<CWishlist>>> aoGetAllWishlists()
    {
        return await m_oWishlistRepository.aoGetAllWishlists();
    }

    internal async Task<bool> bAddWishlist(CWishlist oWishlist)
    {
        return await m_oWishlistRepository.bAddWishlist(oWishlist);
    }

    internal async Task<bool> bDeleteWishlist(int a_nId)
    {
        return await m_oWishlistRepository.bDeleteWishlist(a_nId);
    }

    internal async Task<int> nUpdateWishlist(int a_nId, CWishlist a_oWishlist)
    {
        //state 1 = everything is ok
        //state 0 : depense not found
        //state -1: parameter id different than body id
        //state -2: internal error 

        int l_nState = 1;

        try
        {
            if (a_nId != a_oWishlist.p_nIdWishlist)
            {
                l_nState = -1;
            }
            else if (!m_oWishlistRepository.bWishlistExiste(a_nId))
            {
                l_nState = 0;
            }
            else
            {
                await m_oWishlistRepository.UpdateWishlist(a_oWishlist);
            }
        }
        catch (Exception ex)
        {
            l_nState = -2;
        }

        return l_nState;
    }

    internal async Task<CWishlist?> oGetWishlist(int a_nId)
    {
        return await m_oWishlistRepository.oGetWishlist(a_nId);
    }
}
