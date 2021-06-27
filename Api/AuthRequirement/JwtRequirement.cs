using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.AuthRequirement
{
    public class JwtRequirement:IAuthorizationRequirement
    {

    }
    public class JwtRequirementHandler : AuthorizationHandler<JwtRequirement>
    {
        private readonly HttpClient _client;
        private readonly HttpContext _httpContext;

        public JwtRequirementHandler(IHttpClientFactory httpClientFactory,
                                    HttpContextAccessor httpContextAccessor)
        {
            _client = httpClientFactory.CreateClient();
            _httpContext = httpContextAccessor.HttpContext;
        }


        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, JwtRequirement requirement)
        {
            if (_httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString().Split(' ')[1];
                var response = await _client.GetAsync($"https://localhost:44326/Auth/Validate?access_token={token}");
                if (response.StatusCode ==System.Net.HttpStatusCode.OK)
                {
                    context.Succeed(requirement);
                }
            }
           
        }
    }
}
