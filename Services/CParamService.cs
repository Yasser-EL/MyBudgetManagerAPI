using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Repository;

namespace MyBudgetManagerAPI.Services;

public class CParamService
{

    public enum eSection
    {
        PARAM,
        CHARGES_FIXES,
        CHARGES_VARIABLES,
        CREDITS
    }

    public enum eChargesFixes
    {
        LOYER,
        INTERNET,
        TELEPHONIE
    }

    public enum eChargesVariables
    {
        EAU,
        ELECTRICITE,
        TRANSPORT
    }

    public enum eCredits
    {
        VOITURE
    }

    public enum eParam
    {
        SEMAINE_EN_COURS,
        MOIS_EN_COURS
    }
    private readonly CParamRepository m_oParamRepository;

    public CParamService(CMyBudgetManagerApiDbContext a_oContext)
    {
        m_oParamRepository = new CParamRepository(a_oContext);
    }

    internal async Task<decimal?> dGetSommeCharges(string a_sSection)
    {
        decimal? l_dSomme = default;

        if (bSectionChargeEstValide(a_sSection))
        {
            l_dSomme = await m_oParamRepository.dGetSommeCharges(a_sSection);
        }

        return l_dSomme;
    }

    internal async Task<int> nUpdateParam(string a_sSection, string a_sParamName, decimal a_dValue)
    {
        //state 1 = everything is ok
        //state 0 : depense not found
        //state -1: parameter id different than body id
        //state -2: internal error 
        int l_nState = 1;
        CParam? l_oParam;

        try
        {
            if (!bSectionEstValide(a_sSection) || !bParamNameEstValide(a_sSection, a_sParamName))
            {
                l_nState = -1;
            }
            else
            {
                l_oParam = await oGetParam(a_sSection, a_sParamName);

                // Check if the entity exists
                if (l_oParam == null)
                {
                    l_nState = 0;
                }
                else
                {
                    l_oParam.p_rValue = a_dValue;
                    await m_oParamRepository.UpdateNatureDepense(l_oParam);
                }
            }
        }
        catch (Exception ex)
        {
            l_nState = -2;
        }

        return l_nState;
    }

    internal async Task<ActionResult<CParam?>> oGetParam(string a_sSection, string a_sParamName)
    {
        CParam? l_oParam = default;

        if (bSectionEstValide(a_sSection) && bParamNameEstValide(a_sParamName, a_sSection))
        {
            l_oParam = await m_oParamRepository.oGetParam(a_sSection, a_sParamName);
        }

        return l_oParam;
    }

    private static bool bSectionEstValide(string a_sSection)
    {
        return Enum.IsDefined(typeof(eSection), a_sSection);
    }

    private static bool bSectionChargeEstValide(string _asSection)
    {
        return (Enum.IsDefined(typeof(eChargesFixes), _asSection) || Enum.IsDefined(typeof(eChargesVariables), _asSection));
    }

    private static bool bParamNameEstValide(string a_sParamName, string a_sSection)
    {
        eSection l_eSection = (eSection)Enum.Parse(typeof(eSection), a_sSection);


        switch (l_eSection)
        {
            case eSection.CHARGES_FIXES:
                return Enum.IsDefined(typeof(eChargesFixes), a_sParamName);
            case eSection.CHARGES_VARIABLES:
                return Enum.IsDefined(typeof(eChargesVariables), a_sParamName);
            case eSection.CREDITS:
                return Enum.IsDefined(typeof(eCredits), a_sParamName);
            case eSection.PARAM:
                return Enum.IsDefined(typeof(eParam), a_sParamName);
            default:
                return false;
        }
    }
}
