using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
	public class JwtService
	{
		private readonly IConfiguration _configuration;
		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Generate(AppUser user, IList<string> roles)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier,user.Id),
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim("FullName",user.FullName),
				new Claim(ClaimTypes.Email,user.Email)
			};
			claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));
			string secret = _configuration.GetSection("JWT:Secret").Value;

			SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
			SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken token = new JwtSecurityToken(
			claims: claims,
				signingCredentials: creds,
				expires: DateTime.Now.AddDays(5),
				issuer: _configuration.GetSection("JWT:Issuer").Value,
				audience: _configuration.GetSection("JWT:Audience").Value
				);
			var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);
			return tokenStr;
		}
	}
}
