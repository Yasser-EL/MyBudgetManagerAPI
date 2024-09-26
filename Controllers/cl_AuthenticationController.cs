using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

[Route("api/authentication")]
[ApiController]
[ApiExplorerSettings(GroupName = "Authentification")]
public class cl_AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    public cl_AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLogin model)
    {
        //TODO: check user in DATABASE

        // Validate the user credentials here (e.g., check with the database)
        if (model.p_sUsername == "test" && model.p_sPassword == "password")
        {
            // Generate JWT Token
            var tokenString = GenerateJWTToken(model);
            return Ok(new { token = tokenString });
        }

        return Unauthorized();
    }

    private string GenerateJWTToken(UserLogin userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.p_sUsername),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(int.Parse(_config["JwtSettings:ExpirationMinutes"])),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class UserLogin
{
    [JsonPropertyName("Username")]
    public string p_sUsername { get; set; }

    [JsonPropertyName("Password")]
    public string p_sPassword { get; set; }
}
