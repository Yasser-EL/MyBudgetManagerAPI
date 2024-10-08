using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Repository;

public class CDepenseRepository
{
    private readonly CMyBudgetManagerApiDbContext m_oContext;

    public CDepenseRepository(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oContext = a_oContext;
    }

    public virtual async Task<List<CDepense>> aoGetDepenses(int a_nMois, int a_nAnnee, int? a_nSemaine)
    {
        List<CDepense> l_aoDepenses = new List<CDepense>();

        l_aoDepenses = await m_oContext.p_oDepenses
                                    .Where(item => item.p_nMois == a_nMois
                                                && item.p_nAnnee == a_nAnnee
                                                && (!a_nSemaine.HasValue || item.p_nSemaine == a_nSemaine))
                                    .ToListAsync();

        return l_aoDepenses;
    }

    public virtual async Task<decimal> dGetTotalDepenses(int a_nIdTypeDepense, int? a_nIdPersonne, int? a_nSemaine, int? a_nMois, int a_nAnnee)
    {
        decimal? l_rTotal = await m_oContext.p_oDepenses
                            .Where(item => item.p_nIdType == a_nIdTypeDepense
                                        && (!a_nMois.HasValue || item.p_nMois == a_nMois)
                                        && item.p_nAnnee == a_nAnnee
                                        && (!a_nIdPersonne.HasValue || item.p_nIdPersonne == a_nIdPersonne)
                                        && (!a_nSemaine.HasValue || item.p_nSemaine == a_nSemaine))
                            .SumAsync(item => item.p_rMontant);
        return (l_rTotal ?? 0);
    }

    public virtual async Task<bool> bUpdateDepense(CDepense a_oDepense)
    {
        bool lbOk = true;

        try
        {
            m_oContext.Entry(a_oDepense).State = EntityState.Modified;
            await m_oContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            lbOk = false;
            Console.WriteLine(ex.ToString());
        }

        return lbOk;
    }

    public virtual bool bDepenseExiste(int a_nId)
    {
        return m_oContext.p_oDepenses.Any(e => e.p_nIdDepense == a_nId);
    }

    public async Task<bool> bAddDepense(CDepense a_oDepense)
    {
        bool l_bOk = true;

        try
        {
            if (a_oDepense != null)
            {
                m_oContext.p_oDepenses.Add(a_oDepense);
                await m_oContext.SaveChangesAsync();
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        catch (Exception ex)
        {
            l_bOk = false;
        }

        return l_bOk;
    }

    public async Task<bool> bDeleteDepense(int a_nId)
    {
        bool l_bOk = true;

        try
        {
            CDepense? l_oDepense = await m_oContext.p_oDepenses.FindAsync(a_nId);
            if (l_oDepense != null)
            {
                m_oContext.p_oDepenses.Remove(l_oDepense);
                await m_oContext.SaveChangesAsync(CancellationToken.None);
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        catch (Exception ex)
        {
            l_bOk = false;
        }

        return l_bOk;
    }
}
