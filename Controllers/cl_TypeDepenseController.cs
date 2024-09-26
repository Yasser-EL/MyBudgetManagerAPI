using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/types-depenses")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Types des dépenses")]
    public class cl_TypeDepenseController : ControllerBase
    {
        private readonly cl_MyBudgetManagerApiDbContext _context;

        public cl_TypeDepenseController(cl_MyBudgetManagerApiDbContext context)
        {
            _context = context;
        }

        // GET: api/types-depenses/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<cl_TypeDepense>>> GetAllTypesDepenses()
        {
            return await _context.p_clTypesDepenses.ToListAsync();
        }

        // GET: api/types-depensese/5
        [HttpGet("{nId}")]
        public async Task<ActionResult<cl_TypeDepense>> GetTypeDepense(int nId)
        {
            var pclTypeDepense = await _context.p_clTypesDepenses.FindAsync(nId);

            if (pclTypeDepense == null)
            {
                return NotFound();
            }

            return pclTypeDepense;
        }

        // PUT: api/types-depenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutTypeDepense(int nId, cl_TypeDepense pclTypeDepense)
        {
            if (nId != pclTypeDepense.p_nIdType)
            {
                return BadRequest();
            }

            _context.Entry(pclTypeDepense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bTypeDepenseExists(nId))
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

        // POST: api/types-depenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        private bool bTypeDepenseExists(int nId)
        {
            return _context.p_clTypesDepenses.Any(e => e.p_nIdType == nId);
        }
    }
}
