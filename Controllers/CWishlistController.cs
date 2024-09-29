using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/wishlist")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Wishlist")]
    public class CWishlistController : ControllerBase
    {
        private readonly CWishlistService m_oWishlistService;

        public CWishlistController(CMyBudgetManagerApiDbContext a_oContext)
        {
            m_oWishlistService = new CWishlistService(a_oContext);
        }

        // GET: api/wishlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CWishlist>>> GetAllWishlists()
        {
            return await m_oWishlistService.aoGetAllWishlists();
        }

        // GET: api/wishlist/5
        [HttpGet("{nId}")]
        public async Task<ActionResult<CWishlist>> GetWishlist(int nId)
        {
            CWishlist? l_oWishlist = await m_oWishlistService.oGetWishlist(nId);
            if (l_oWishlist == null)
            {
                return NotFound();
            }
            else
            {
                return l_oWishlist;
            }
        }

        // PUT: api/wishlist/5
        [HttpPut("{nId}")]
        public async Task<IActionResult> PutWishlist(int nId, CWishlist oWishlist)
        {
            int l_nState;

            if (ModelState.IsValid)
            {
                l_nState = await m_oWishlistService.nUpdateWishlist(nId, oWishlist);
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

        // POST: api/CWishlist
        [HttpPost]
        public async Task<ActionResult<CWishlist>> PostWishlist(CWishlist oWishlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                if (await m_oWishlistService.bAddWishlist(oWishlist))
                {
                    return NoContent();
                }
                else
                {
                    throw new Exception();
                }
            }
        }

        // DELETE: api/Wishlist/5
        [HttpDelete("{nId}")]
        public async Task<IActionResult> DeleteWishlist(int nId)
        {
            if (await m_oWishlistService.bDeleteWishlist(nId))
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
