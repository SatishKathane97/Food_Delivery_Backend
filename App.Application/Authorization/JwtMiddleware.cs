using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext context, ClaimsBaseService claims)
        {
            var user = claims.GetUser();
            if (user != null)
            {
                context.Items["User"] = user;
            }
            await _next(context);
        }
    }
}
