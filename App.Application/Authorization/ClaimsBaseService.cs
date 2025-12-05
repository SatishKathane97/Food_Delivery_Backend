using APP.Domain.Enums;
using APP.Domain.FactoryService;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;   // 👈 needed for FirstOrDefault / Last

namespace App.Application.Authorization
{
    public class ClaimsBaseService
    {
        private readonly IJwtService _jwt;
        private readonly IHttpContextAccessor _http;

        public ClaimsBaseService(IJwtService jwt, IHttpContextAccessor http)
        {
            _jwt = jwt;
            _http = http;
        }

        public ClaimsModel? GetUser()
        {
            var token = _http.HttpContext?
                .Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            return _jwt.ReadClaims(token);
        }

        public long? GetUserId() => GetUser()?.UserId;

        // 🔹 Get raw string role ("admin" / "user")
        public string? GetRole() => GetUser()?.Role;

        // 🔹 Convert string role → RoleTypes? safely
        public RoleTypes? GetRoleEnum()
        {
            var roleStr = GetRole();
            if (string.IsNullOrWhiteSpace(roleStr))
                return null;

            return Enum.TryParse<RoleTypes>(roleStr, ignoreCase: true, out var parsed)
                ? parsed
                : null;
        }

        // 🔹 Helper: check if current user is in a RoleTypes enum value
        public bool IsInRole(RoleTypes role)
        {
            var currentRole = GetRole();
            return string.Equals(currentRole, role.ToString(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
