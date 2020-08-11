using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ContaService.API.Infrastructure.Auth;
using ContaService.API.Infrastructure.Extensions;
using ContaService.API.Infrastructure.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ContaService.API.Controllers
{
    [Route("api/v1/authorization")]
    public class AuthorizationController : Controller
    {
        private readonly FireBaseOptions _options;
        public AuthorizationController(IOptions<FireBaseOptions> options)
        {
            _options = options.Value;
        }

        [HttpPost]
        public async Task<ActionResult> GetToken([FromForm] LoginInfo loginInfo)
        {
            string uri = $"{_options.VerifyPasswordUri}{_options.ProjectKey}";

            using (HttpClient client = new HttpClient())
            {
                FireBaseLoginInfo fireBaseLoginInfo = new FireBaseLoginInfo
                {
                    Email = loginInfo.Username,
                    Password = loginInfo.Password
                };
                var result = await client.PostAsJsonAsync<FireBaseLoginInfo, GoogleToken>(uri, fireBaseLoginInfo);
                Token token = new Token
                {
                    token_type = "Bearer",
                    access_token = result.idToken,
                    id_token = result.idToken,
                    expires_in = int.Parse(result.expiresIn),
                    refresh_token = result.refreshToken

                };

                return Ok(token);
            }
        }
    }
}
