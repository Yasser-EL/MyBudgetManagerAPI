using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;

namespace MyBudgetManagerAPI.Services;

public class CDepenseService
{

    public const string MSG_ERRUR_MOIS_INVALIDE = "Valeur invalide du mois";
    public const string MSG_ERRUR_SEMAINE_INVALIDE = "Valeur invalide de la semaine";
    public const string MSG_ERRUR_ANNEE_INVALIDE = "Valeur invalide de l'année";
    public const string MSG_ERRUR_ID_TYPE_DEPENSE_INVALIDE = "Id invalide du type de dépense";
    public const string MSG_ERRUR_ID_PERSONNE_INVALIDE = "Id invalide de la personne";


    private readonly CDepenseRepository m_oDepenseRepository;

    public CDepenseService(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oDepenseRepository = new CDepenseRepository(a_oContext);
    }

    //constructor for mock repository
    public CDepenseService(CDepenseRepository a_oDepenseRepository)
    {
        m_oDepenseRepository = a_oDepenseRepository;
    }

    public virtual async Task<List<CDepense>> aoGetDepenses(int a_nMois, int a_nAnnee, int? a_nSemaine)
    {
        List<CDepense> l_aoDepenses;
        //validation de l'input

        //->validation du mois
        if (!bMoisEstValide(a_nMois))
        {
            throw new ArgumentException(MSG_ERRUR_MOIS_INVALIDE);
        }
        //-> validation de l'année

        if (!(a_nAnnee >= 2020 && a_nAnnee <= 2100))
        {
            throw new ArgumentException(MSG_ERRUR_ANNEE_INVALIDE);
        }

        //-> validation de la semaine (input facultatif)
        //sauter la validation si le paramètre n'est pas passé
        if (a_nSemaine != null)
        {
            //-> validation de la semaine
            if (!bSemaineEstValide((int)a_nSemaine))
            {
                throw new ArgumentException(MSG_ERRUR_SEMAINE_INVALIDE);
            }
        }

        //si validation ok, lancer l'opération
        l_aoDepenses = await m_oDepenseRepository.aoGetDepenses(a_nMois, a_nAnnee, a_nSemaine);

        return l_aoDepenses;
    }

    public virtual async Task<decimal> dGetTotalDepenses(int a_nIdTypeDepense, int? a_nIdPersonne, int? a_nSemaine, int? a_nMois)
    {
        int l_nMois;
        int l_nAnnee;

        //validation des inputs

        //-> validation du mois
        if (a_nMois == null)
        {
            //sauter la validation du mois s'il n'est pas passé
            l_nMois = DateTime.Now.Month;
        }
        else
        {
            l_nMois = (int)a_nMois;
            if (!bMoisEstValide(l_nMois))
            {
                throw new ArgumentException(MSG_ERRUR_MOIS_INVALIDE);
            }
        }

        //-> validation du type de dépense
        if (!(a_nIdTypeDepense > 0))
        {
            throw new ArgumentException(MSG_ERRUR_ID_TYPE_DEPENSE_INVALIDE);
        }

        //validation de l'id personne
        //sauter la validation de l'id s'il n'est pas passé
        if (a_nIdPersonne != null)
        {
            //validation de l'id de la personne
            if (!(a_nIdPersonne > 0))
            {
                throw new ArgumentException(MSG_ERRUR_ID_PERSONNE_INVALIDE);
            }
        }

        //-> validation de la semaine (input facultatif)
        //sauter la validation si le paramètre n'est pas passé
        if (a_nSemaine != null)
        {
            //-> validation de la semaine
            if (!bSemaineEstValide((int)a_nSemaine))
            {
                throw new ArgumentException(MSG_ERRUR_SEMAINE_INVALIDE);
            }
        }

        l_nAnnee = DateTime.Now.Year;
        return await m_oDepenseRepository.dGetTotalDepenses(a_nIdTypeDepense, a_nIdPersonne, a_nSemaine, l_nMois, l_nAnnee);
    }

    public virtual async Task<int> nUpdateDepense(int a_nId, CDepense a_oDepense)
    {
        //state 1 = everything is ok
        //state 0 : depense not found
        //state -1: parameter id different than body id
        //state -2: internal error 

        int l_nState = 1;

        try
        {
            if (a_nId != a_oDepense.p_nIdDepense)
            {
                l_nState = -1;
            }
            else if (!m_oDepenseRepository.bDepenseExiste(a_nId))
            {
                l_nState = 0;
            }
            else
            {
                if (!(await m_oDepenseRepository.bUpdateDepense(a_oDepense)))
                {
                    l_nState = -2;
                }
            }
        }
        catch (Exception ex)
        {
            l_nState = -2;
        }

        return l_nState;
    }
    public virtual async Task<bool> bAddDepense(CDepense a_oDepense)
    {
        return await m_oDepenseRepository.bAddDepense(a_oDepense);
    }

    public virtual async Task<bool> bDeleteDepense(int a_nId)
    {
        return await m_oDepenseRepository.bDeleteDepense(a_nId);
    }

    //validation methods
    private static bool bMoisEstValide(int a_nMois)
    {
        return (a_nMois >= 1 && a_nMois <= 12);
    }

    private static bool bSemaineEstValide(int a_nSemaine)
    {
        return (a_nSemaine >= 1 && a_nSemaine <= 4);
    }
}
