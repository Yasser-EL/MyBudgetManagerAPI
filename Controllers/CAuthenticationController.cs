using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using MyBudgetManagerAPI.Services;

namespace MyBudgetManagerAPI.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Authentification")]
    public class CAuthenticationController : ControllerBase
    {
        private readonly IConfiguration m_oConfig;

        public CAuthenticationController(IConfiguration a_oConfig)
        {
            m_oConfig = a_oConfig;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] CUserLogin a_oLogin)
        {
            string l_sToken;

            //TODO: check user in DATABASE

            // Validate the user credentials here (e.g., check with the database)
            if (a_oLogin.p_sUsername == "test" && a_oLogin.p_sPassword == "password")
            {
                // Generate JWT Token
                l_sToken = CAuthenticationService.GenerateJWTToken(a_oLogin, m_oConfig);
                return Ok(new { token = l_sToken });
            }
            else
            {
                return Unauthorized();
            }
        }
    }

    public class CUserLogin
    {
        [JsonPropertyName("Username")]
        public string p_sUsername { get; set; }

        [JsonPropertyName("Password")]
        public string p_sPassword { get; set; }
    }
}