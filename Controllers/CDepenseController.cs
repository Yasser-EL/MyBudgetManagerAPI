using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/depenses")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Dépenses")]
    public class CDepenseController : ControllerBase
    {
        private readonly CDepenseService m_oDepenseService;


        public CDepenseController(CMyBudgetManagerApiDbContext a_oContext)
        {
            m_oDepenseService = new CDepenseService(a_oContext);
        }

        //GET: api/depenses/12/2024/4
        [Authorize]
        [HttpGet("{nMois}/{nAnnee}")]
        public async Task<ActionResult<IEnumerable<CDepense>>> GetDepenses(int nMois, int nAnnee,
                                                                    [FromQuery(Name = "semaine")] int? nSemaine)
        {
            return await m_oDepenseService.aoGetDepenses(nMois, nAnnee, nSemaine);
        }

        //GET: api/depenses/total/12/2024/4
        [HttpGet("total/{nIdTypeDepense}")]
        public async Task<ActionResult> GetTotalDepenses(int nIdTypeDepense,
                                                                    [FromQuery(Name = "id_personne")] int? nIdPersonne,
                                                                    [FromQuery(Name = "semaine")] int? nSemaine,
                                                                    [FromQuery(Name = "mois")] int? nMois)
        {
            decimal l_rTotal = await m_oDepenseService.dGetTotalDepenses(nIdTypeDepense, nIdPersonne, nSemaine, nMois);

            return Ok(new { total = l_rTotal });
        }

        // PUT: api/depenses/5
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutDepense(int nId, [FromBody] CDepense oDepense)
        {
            int l_nState;

            if (ModelState.IsValid)
            {
                l_nState = await m_oDepenseService.nUpdateDepense(nId, oDepense);
            }
            else
            {
                l_nState = -1;
            }

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

        // POST: api/depenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CDepense>> PostDepense([FromBody] CDepense oDepense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                if (await m_oDepenseService.bAddDepense(oDepense))
                {
                    return NoContent();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        // DELETE: api/depenses/5
        [HttpDelete("{nId}")]
        public async Task<IActionResult> DeleteDepense(int nId)
        {
            if (await m_oDepenseService.bDeleteDepense(nId))
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
