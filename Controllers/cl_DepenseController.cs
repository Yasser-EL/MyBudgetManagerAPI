using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/depenses")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Dépenses")]
    public class cl_DepenseController : ControllerBase
    {
        private readonly cl_MyBudgetMangerApiDbContext _context;

        public cl_DepenseController(cl_MyBudgetMangerApiDbContext context)
        {
            _context = context;
        }

        //GET: api/depenses/12/2024/4
        [HttpGet("{nMois}/{nAnnee}")]
        public async Task<ActionResult<IEnumerable<cl_Depense>>> GetDepenses(int nMois, int nAnnee,
                                                                    [FromQuery(Name = "semaine")] int? nSemaine)
        {
            var items = await _context.p_clDepenses
                                        .Where(item => item.p_nMois == nMois
                                                    && item.p_nAnnee == nAnnee
                                                    && (!nSemaine.HasValue || item.p_nSemaine == nSemaine))
                                        .ToListAsync();

            return Ok(items);
        }

        //GET: api/depenses/total/12/2024/4
        [HttpGet("total/{nIdTypeDepense}")]
        public async Task<ActionResult<IEnumerable<cl_Depense>>> GetTotalDepenses(int nIdTypeDepense,
                                                                    [FromQuery(Name = "id_personne")] int? nIdPersonne,
                                                                    [FromQuery(Name = "semaine")] int? nSemaine,
                                                                    [FromQuery(Name = "mois")] int? nMois)
        {
            int lnMois;
            int lnAnnee;

            if (nMois > 0)
                lnMois = (int)nMois;
            else
                lnMois = DateTime.Now.Month;

            lnAnnee = DateTime.Now.Year;

            var items = await _context.p_clDepenses
                                        .Where(item => item.p_nIdType == nIdTypeDepense
                                                    && item.p_nMois == lnMois
                                                    && item.p_nAnnee == lnAnnee
                                                    && (!nIdPersonne.HasValue || item.p_nIdPersonne == nIdPersonne)
                                                    && (!nSemaine.HasValue || item.p_nSemaine == nSemaine))
                                        .ToListAsync();

            return Ok(items);
        }

        // PUT: api/depenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutDepense(int nId, cl_Depense pclDepense)
        {
            if (nId != pclDepense.p_nIdDepense)
            {
                return BadRequest();
            }

            _context.Entry(pclDepense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bDepenseExists(nId))
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

        // POST: api/depenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<cl_Depense>> PostDepense(cl_Depense pclDepense)
        {
            _context.p_clDepenses.Add(pclDepense);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDepense", new { nId = pclDepense.p_nIdDepense }, pclDepense);
            return NoContent();
        }

        // DELETE: api/depenses/5
        [HttpDelete("{nId}")]
        public async Task<IActionResult> DeleteDepense(int nId)
        {
            var pclDepense = await _context.p_clDepenses.FindAsync(nId);
            if (pclDepense == null)
            {
                return NotFound();
            }

            _context.p_clDepenses.Remove(pclDepense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //GET: 

        private bool bDepenseExists(int nId)
        {
            return _context.p_clDepenses.Any(e => e.p_nIdDepense == nId);
        }
    }
}
