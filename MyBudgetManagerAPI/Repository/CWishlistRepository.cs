using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Repository;

public class CWishlistRepository
{
    private CMyBudgetManagerApiDbContext m_oContext;

    public CWishlistRepository(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oContext = a_oContext;
    }

    internal async Task<ActionResult<IEnumerable<CWishlist>>> aoGetAllWishlists()
    {
        return await m_oContext.p_oWishlists.ToListAsync();
    }

    internal async Task<bool> bAddWishlist(CWishlist oWishlist)
    {
        bool l_bOk = true;

        try
        {
            m_oContext.p_oWishlists.Add(oWishlist);
            await m_oContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            l_bOk = false;
        }

        return l_bOk;
    }

    internal async Task<bool> bDeleteWishlist(int a_nId)
    {
        bool l_bOk = true;

        try
        {
            CWishlist? l_oWishlist = await m_oContext.p_oWishlists.FindAsync(a_nId);
            if (l_oWishlist != null)
            {
                m_oContext.p_oWishlists.Remove(l_oWishlist);
                await m_oContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }
        catch (Exception ex)
        {
            l_bOk = false;
        }

        return l_bOk;
    }

    internal bool bWishlistExiste(int a_nId)
    {
        return m_oContext.p_oWishlists.Any(e => e.p_nIdWishlist == a_nId);
    }

    internal async Task<CWishlist?> oGetWishlist(int a_nId)
    {
        return await m_oContext.p_oWishlists.FindAsync(a_nId);
    }

    internal async Task UpdateWishlist(CWishlist a_oWishlist)
    {
        m_oContext.Entry(a_oWishlist).State = EntityState.Modified;
        await m_oContext.SaveChangesAsync();
    }
}
