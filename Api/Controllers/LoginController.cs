using System.Threading.Tasks;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api")]
    public class LoginController : Controller
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpPost("sign-up")]
        public async Task SignUpAsync([FromBody] SignUpRequest request)
        {
            await _service.SignUpAsync(request);
        }
        
        [HttpPost("sign-in")]
        public async Task SignInAsync([FromBody] SignInRequest request)
        {
            await _service.SignInAsync(request);
        }
        
        [HttpPost("sign-out")]
        public async Task SignOutAsync()
        {
            await _service.SignOutAsync();
        }
    }
}