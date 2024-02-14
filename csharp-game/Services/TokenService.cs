using csharp_game.Data;
using csharp_game.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly GameContext _context;

    public TokenService(IConfiguration configuration , UserManager<ApplicationUser> userManager , GameContext context)
    {
        _configuration = configuration;
        _userManager = userManager;
        _context = context;
    }

    public string CreateToken(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            // Add additional claims here
        };

        return GenerateToken(claims);
    }

    public async Task<string> CreatePasswordResetToken(ApplicationUser user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        // Save the token in the tokens table
        _context.Tokens.Add(new Token { UserId = user.Id , Value = token , ExpiryDate = DateTime.UtcNow.AddDays(1) });
        await _context.SaveChangesAsync();

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim("reset", token)
        };
        return GenerateToken(claims);
    }

    private string GenerateToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtTokenSettings:SymmetricSecurityKey"]));
        var creds = new SigningCredentials(key , SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtTokenSettings:ExpirationDays"]));
        var token = new JwtSecurityToken(
            _configuration["JwtTokenSettings:ValidIssuer"] ,
            _configuration["JwtTokenSettings:ValidAudience"] ,
            claims ,
            expires: expires ,
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
