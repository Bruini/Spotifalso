using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotifalso.Aplication.Inputs;
using Spotifalso.Aplication.Interfaces.Services;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Spotifalso.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<dynamic> Login()
        {
            var loginInput = BasicAuthToLoginInput();
            return await _authService.Login(loginInput);           
        }

        private LoginInput BasicAuthToLoginInput()
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                var username = credentials.FirstOrDefault();
                var password = credentials.LastOrDefault();

                return new LoginInput
                {
                    Username = username,
                    Password = password
                };
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
