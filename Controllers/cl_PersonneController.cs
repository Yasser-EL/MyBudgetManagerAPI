using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/personne")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Personnes")]
    public class cl_PersonneController : ControllerBase
    {
        private readonly cl_MyBudgetManagerApiDbContext _context;

        public cl_PersonneController(cl_MyBudgetManagerApiDbContext context)
        {
            _context = context;
        }

        // GET: api/personne/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<cl_Personne>>> GetAllPersonnes()
        {
            return await _context.p_clPersonnes.ToListAsync();
        }
    }
}
