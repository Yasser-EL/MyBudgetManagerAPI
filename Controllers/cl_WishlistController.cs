using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/wishlist")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Wishlist")]
    public class cl_WishlistController : ControllerBase
    {
        private readonly cl_MyBudgetManagerApiDbContext _context;

        public cl_WishlistController(cl_MyBudgetManagerApiDbContext context)
        {
            _context = context;
        }

        // GET: api/wishlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<cl_Wishlist>>> GetAllWishlists()
        {
            return await _context.p_clWishlists.ToListAsync();
        }

        // GET: api/wishlist/5
        [HttpGet("{nId}")]
        public async Task<ActionResult<cl_Wishlist>> GetWishlist(int nId)
        {
            var pclWishlist = await _context.p_clWishlists.FindAsync(nId);

            if (pclWishlist == null)
            {
                return NotFound();
            }

            return pclWishlist;
        }

        // PUT: api/wishlist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutWishlist(int nId, cl_Wishlist pclWishlist)
        {
            if (nId != pclWishlist.p_nIdWishlist)
            {
                return BadRequest();
            }

            _context.Entry(pclWishlist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bWishlistExists(nId))
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

        // POST: api/cl_Wishlist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<cl_Wishlist>> PostWishlist(cl_Wishlist pclWishlist)
        {
            _context.p_clWishlists.Add(pclWishlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWishlist", new { nId = pclWishlist.p_nIdWishlist }, pclWishlist);
        }

        // DELETE: api/cl_Wishlist/5
        [HttpDelete("{nId}")]
        public async Task<IActionResult> DeleteWishlist(int nId)
        {
            var pclWishlist = await _context.p_clWishlists.FindAsync(nId);
            if (pclWishlist == null)
            {
                return NotFound();
            }

            _context.p_clWishlists.Remove(pclWishlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool bWishlistExists(int nId)
        {
            return _context.p_clWishlists.Any(e => e.p_nIdWishlist == nId);
        }
    }
}
