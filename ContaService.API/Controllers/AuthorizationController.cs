using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ContaService.API.Infrastructure.Auth;
using ContaService.API.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ContaService.API.Controllers
{
    [Route("api/v1/authorization")]
    public class AuthorizationController : Controller
    {
        [HttpPost]
        public async Task<ActionResult> GetToken([FromForm] LoginInfo loginInfo)
        {
            string uri = "https://www.googleapis.com/identitytoolkit/v3/relyingparty/verifyPassword?key=AIzaSyCTcSSm6gjFL2R3dS7P1J5cc2jfyHTS-n0";

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
