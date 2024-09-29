using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/param")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Paramètres")]
    public class CParamController : ControllerBase
    {
        private readonly CParamService m_oParamService;

        public CParamController(CMyBudgetManagerApiDbContext a_oContext)
        {
            m_oParamService = new CParamService(a_oContext);
        }

        // GET: api/param/SECTION/PARAM
        [HttpGet("{sSection}/{sParamName}")]
        public async Task<ActionResult<CParam>> GetParam(string sSection, string sParamName)
        {
            CParam? l_oParam = await m_oParamService.oGetParam(sSection, sParamName);

            if (l_oParam == null)
            {
                return NotFound();
            }
            else
            {
                return l_oParam;
            }
        }

        // PUT: api/param/SECTION/PARAM?value=50
        [HttpPut("{sSection}/{sParamName}")]
        public async Task<IActionResult> PutParam(string sSection, string sParamName, [FromQuery(Name = "value")] decimal rValue)
        {
            int l_nState;


            l_nState = await m_oParamService.nUpdateParam(sSection, sParamName, rValue);
            //state 1 = everything is ok
            //state 0 : depense not found
            //state -1: parameter id different than body id OR invalid model
            //state -2: internal error 

            switch (l_nState)
            {
                case -2:
                    throw new Exception();
                case -1:
                    return BadRequest();
                case 0:
                    return NotFound();
                default:
                    return NoContent();
            }
        }

        // GET: api/param/somme-charges/SECTION
        [HttpGet("somme-charges/{sSection}")]
        public async Task<ActionResult> GetSommeCharges(string sSection)
        {
            decimal? l_rSomme = await m_oParamService.dGetSommeCharges(sSection);

            if (l_rSomme == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(new { somme = l_rSomme });
            }
        }

        // GET: api/param/charge-fixe/NOM_CHARGE
        [HttpGet("charge-fixe/{sChargeFixe}")]
        public async Task<ActionResult> GetChargeFixe(string sChargeFixe)
        {
            return Ok(new { value = await GetParam(CParamService.eSection.CHARGES_FIXES.ToString(), sChargeFixe) });
        }

        // PUT: api/param/charge-fixe/NOM_CHARGE?value=50
        [HttpPut("charge-fixe/{sChargeFixe}")]
        public async Task<IActionResult> PutChargeFixe(string sChargeFixe, [FromQuery(Name = "value")] decimal rValue)
        {
            return await PutParam(CParamService.eSection.CHARGES_FIXES.ToString(), sChargeFixe, rValue);
        }

        // GET: api/param/charge-variable/NOM_CHARGE
        [HttpGet("charge-variable/{sChargeVariable}")]
        public async Task<ActionResult> GetChargeVariable(string sChargeVariable)
        {
            return Ok(new { value = await GetParam(CParamService.eSection.CHARGES_VARIABLES.ToString(), sChargeVariable) });
        }

        // PUT: api/param/charge-variable/NOM_CHARGE?value=50
        [HttpPut("charge-variable/{sChargeVariable}")]
        public async Task<IActionResult> PutChargeVariable(string sChargeVariable, [FromQuery(Name = "value")] decimal rValue)
        {
            return await PutParam(CParamService.eSection.CHARGES_VARIABLES.ToString(), sChargeVariable, rValue);
        }


        // GET: api/param/credit-voiture/NOM_CHARGE
        [HttpGet("credit-voiture/{sChargeVariable}")]
        public async Task<ActionResult> GetCreditVoiture()
        {
            return Ok(new { value = await GetParam(CParamService.eSection.CHARGES_VARIABLES.ToString(), CParamService.eCredits.VOITURE.ToString()) });
        }

        // GET: api/param/semaine-en-cours
        [HttpGet("semaine-en-cours")]
        public async Task<ActionResult> GetSemaineEnCours()
        {
            return Ok(new { value = await GetParam(CParamService.eSection.PARAM.ToString(), CParamService.eParam.SEMAINE_EN_COURS.ToString()) });
        }

        // GET: api/param/mois-en-cours
        [HttpGet("mois-en-cours")]
        public async Task<ActionResult> GetMoisEnCours()
        {
            return Ok(new { value = await GetParam(CParamService.eSection.PARAM.ToString(), CParamService.eParam.MOIS_EN_COURS.ToString()) });
        }
    }
}
