using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/nature-depense")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Nature des dépenses")]

    public class cl_NatureDepenseController : ControllerBase
    {
        private readonly cl_MyBudgetManagerApiDbContext _context;

        public cl_NatureDepenseController(cl_MyBudgetManagerApiDbContext context)
        {
            _context = context;
        }

        // GET: api/nature-depense/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<cl_NatureDepense>>> GetAllNaturesDepenses()
        {
            return await _context.p_clNaturesDepenses.ToListAsync();
        }

        // GET: api/nature-depense/5
        [HttpGet("{nId}")]
        public async Task<ActionResult<cl_NatureDepense>> GetNatureDepense(byte nId)
        {
            var pclNatureDepense = await _context.p_clNaturesDepenses.FindAsync(nId);

            if (pclNatureDepense == null)
            {
                return NotFound();
            }

            return pclNatureDepense;
        }

        // PUT: api/nature-depense/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutNatureDepense(byte nId, cl_NatureDepense pclNatureDepense)
        {
            if (nId != pclNatureDepense.p_nIdNature)
            {
                return BadRequest();
            }

            _context.Entry(pclNatureDepense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bNatureDepenseExists(nId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool bNatureDepenseExists(byte nId)
        {
            return _context.p_clNaturesDepenses.Any(e => e.p_nIdNature == nId);
        }
    }
}
