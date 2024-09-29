using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/types-depenses")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Types des dépenses")]
    public class CTypeDepenseController : ControllerBase
    {
        private readonly CTypeDepenseService m_oTypeDepenseService;

        public CTypeDepenseController(CMyBudgetManagerApiDbContext m_oContext)
        {
            m_oTypeDepenseService = new CTypeDepenseService(m_oContext);
        }

        // GET: api/types-depenses/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CTypeDepense>>> GetAllTypesDepenses()
        {
            return await m_oTypeDepenseService.aoGetAllTypesDepenses();
        }

        // GET: api/types-depensese/5
        [HttpGet("{nId}")]
        public async Task<ActionResult<CTypeDepense>> GetTypeDepense(int nId)
        {
            CTypeDepense? l_oTypeDepense = await m_oTypeDepenseService.oGetTypeDepense(nId);

            if (l_oTypeDepense == null)
            {
                return NotFound();
            }
            else
            {
                return l_oTypeDepense;
            }
        }

        // PUT: api/types-depenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutTypeDepense(int nId, [FromBody] CTypeDepense oTypeDepense)
        {
            int l_nState;

            if (ModelState.IsValid)
            {
                l_nState = await m_oTypeDepenseService.nUpdateTypeDepense(nId, oTypeDepense);
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
    }
}
