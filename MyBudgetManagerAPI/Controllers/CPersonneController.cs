using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBudgetManagerAPI.Data;
using MyBudgetManagerAPI.Models;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Controllers
{
    [Authorize]
    [Route("api/personne")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Personnes")]
    public class CPersonneController : ControllerBase
    {
        private readonly CPersonneService m_oPersonneService;

        public CPersonneController(CMyBudgetManagerApiDbContext a_oContext)
        {
            m_oPersonneService = new CPersonneService(a_oContext);
        }

        // GET: api/personne/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<CPersonne>>> GetAllPersonnes()
        {
            return await m_oPersonneService.GetAllPersonnes();
        }
    }
}
