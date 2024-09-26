using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Controllers
{

    public enum eSection
    {
        PARAM,
        SALAIRES,
        DETTES,
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

    [Authorize]
    [Route("api/param")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Paramètres")]
    public class cl_ParamController : ControllerBase
    {
        private readonly cl_MyBudgetMangerApiDbContext m_clContext;

        public cl_ParamController(cl_MyBudgetMangerApiDbContext pclContext)
        {
            m_clContext = pclContext;
        }

        // GET: api/param/SECTION/PARAM
        [HttpGet("{sSection}/{sParamName}")]
        public async Task<ActionResult<decimal>> GetParam(string sSection, string sParamName)
        {
            cl_Param? pclParam = await m_clContext.p_clParams.SingleOrDefaultAsync(p => p.p_sSection == sSection
                                                                                    && p.p_sParameter == sParamName);

            if (pclParam == null)
            {
                return NotFound();
            }

            return pclParam.p_rValue ?? 0;
        }

        // PUT: api/param/SECTION/PARAM?value=50
        [HttpPut("{sSection}/{sParamName}")]
        public async Task<IActionResult> PutParam(string sSection, string sParamName, [FromQuery(Name = "value")] decimal rValue)
        {
            bool lbOk;
            cl_Param? pclParam;
            try
            {
                pclParam = await m_clContext.p_clParams
                                .SingleOrDefaultAsync(p => p.p_sSection == sSection
                                                        && p.p_sParameter == sParamName);

                // Check if the entity exists
                lbOk = (pclParam != null);
                if (!lbOk)
                {
                    return NotFound();
                }
                else
                {
                    pclParam.p_rValue = rValue;
                    await m_clContext.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // GET: api/param/somme-charges/SECTION
        [HttpGet("somme-charges/{sSection}")]
        public async Task<ActionResult<decimal>> GetSommeCharges(string sSection)
        {
            decimal? lrSum = await m_clContext.p_clParams.Where(p => p.p_sSection == sSection).SumAsync(p => p.p_rValue);

            return lrSum ?? 0;
        }

        // GET: api/param/charge-fixe/NOM_CHARGE
        [HttpGet("charge-fixe/{sChargeFixe}")]
        public async Task<ActionResult<decimal>> GetChargeFixe(string sChargeFixe)
        {
            return await GetParam(eSection.CHARGES_FIXES.ToString(), sChargeFixe);
        }

        // PUT: api/param/charge-fixe/NOM_CHARGE?value=50
        [HttpPut("charge-fixe/{sChargeFixe}")]
        public async Task<IActionResult> PutChargeFixe(string sChargeFixe, [FromQuery(Name = "value")] decimal rValue)
        {
            return await PutParam(eSection.CHARGES_FIXES.ToString(), sChargeFixe, rValue);
        }

        // GET: api/param/charge-variable/NOM_CHARGE
        [HttpGet("charge-variable/{sChargeVariable}")]
        public async Task<ActionResult<decimal>> GetChargeVariable(string sChargeVariable)
        {
            return await GetParam(eSection.CHARGES_VARIABLES.ToString(), sChargeVariable);
        }

        // PUT: api/param/charge-variable/NOM_CHARGE?value=50
        [HttpPut("charge-variable/{sChargeVariable}")]
        public async Task<IActionResult> PutChargeVariable(string sChargeVariable, [FromQuery(Name = "value")] decimal rValue)
        {
            return await PutParam(eSection.CHARGES_VARIABLES.ToString(), sChargeVariable, rValue);
        }


        // GET: api/param/credit-voiture/NOM_CHARGE
        [HttpGet("credit-voiture/{sChargeVariable}")]
        public async Task<ActionResult<decimal>> GetCreditVoiture()
        {
            return await GetParam(eSection.CHARGES_VARIABLES.ToString(), eCredits.VOITURE.ToString());
        }

        // GET: api/param/semaine-en-cours
        [HttpGet("semaine-en-cours")]
        public async Task<ActionResult<decimal>> GetSemaineEnCours()
        {
            return await GetParam(eSection.PARAM.ToString(), eParam.SEMAINE_EN_COURS.ToString());
        }

        // GET: api/param/mois-en-cours
        [HttpGet("mois-en-cours")]
        public async Task<ActionResult<decimal>> GetMoisEnCours()
        {
            return await GetParam(eSection.PARAM.ToString(), eParam.MOIS_EN_COURS.ToString());
        }


        private bool bParamExists(int nId)
        {
            return m_clContext.p_clParams.Any(e => e.p_nIdParam == nId);
        }
    }
}
