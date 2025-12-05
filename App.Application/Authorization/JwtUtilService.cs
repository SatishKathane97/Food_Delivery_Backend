using APP.Domain.Enums;
using APP.Domain.FactoryService;
using Lib.Config.JWTAuth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.Application.Authorization
{
    public interface IJwtService
    {
        string GenerateToken(ClaimsModel model);
        ClaimsModel? ReadClaims(string? token);
    }

    public class JwtUtilService : IJwtService
    {
        private readonly AuthToken _opts;
        private readonly JwtSecurityTokenHandler _handler;
        private readonly SymmetricSecurityKey _key;

        public JwtUtilService(IOptions<AuthToken> options)
        {
            _opts = options.Value ?? throw new Exception("AuthToken missing in config");
            _handler = new JwtSecurityTokenHandler();
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_opts.Key!));
        }

        public string GenerateToken(ClaimsModel model)
        {
            var claims = new List<Claim>
            {
                new Claim("id", model.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString())
            };

            // Add Email if exists
            if (!string.IsNullOrEmpty(model.Email))
                claims.Add(new Claim(ClaimTypes.Email, model.Email));

            // Add Role if exists (ONLY once)
            if (!string.IsNullOrEmpty(model.Role))
                claims.Add(new Claim(ClaimTypes.Role, model.Role));

            var token = new JwtSecurityToken(
                issuer: _opts.Issuer,
                audience: _opts.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256)
            );

            return _handler.WriteToken(token);
        }

        public ClaimsModel? ReadClaims(string? token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            var principal = _handler.ReadJwtToken(token);

            // Extract ID claim safely
            var idClaim = principal.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            if (string.IsNullOrEmpty(idClaim)) return null;

            return new ClaimsModel
            {
                UserId = long.Parse(idClaim),
                Email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                Role = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value
            };
        }
    }
}
