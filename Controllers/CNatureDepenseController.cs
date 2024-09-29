using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/nature-depense")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Nature des dépenses")]

    public class CNatureDepenseController : ControllerBase
    {
        private readonly CNatureDepenseService m_oNatureDepenseService;


        public CNatureDepenseController(CMyBudgetManagerApiDbContext a_oContext)
        {
            m_oNatureDepenseService = new CNatureDepenseService(a_oContext);
        }

        // GET: api/nature-depense/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CNatureDepense>>> aoGetAllNaturesDepenses()
        {
            return await m_oNatureDepenseService.aoGetAllNaturesDepenses();
        }

        // GET: api/nature-depense/5
        [HttpGet("{nId}")]
        public async Task<ActionResult<CNatureDepense>> oGetNatureDepense(byte nId)
        {
            CNatureDepense? l_oNatureDepense = await m_oNatureDepenseService.oGetNatureDepense(nId);
            if (l_oNatureDepense == null)
            {
                return NotFound();
            }
            else
            {
                return l_oNatureDepense;
            }
        }

        // PUT: api/nature-depense/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutNatureDepense(byte nId, CNatureDepense oNatureDepense)
        {
            int l_nState;

            if (ModelState.IsValid)
            {
                l_nState = await m_oNatureDepenseService.nUpdateDepense(nId, oNatureDepense);
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
