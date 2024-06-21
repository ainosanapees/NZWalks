using Microsoft.AspNetCore.Mvc;
using NZWalks.Api.Repositories;

namespace NZWalks.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {

        private readonly IUserRepository userRepository;
        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
         }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            var isAuthenticated = await userRepository.AuthenticateAsync(loginRequest.userName, loginRequest.password);


            if (!isAuthenticated)
            {

            }
            return BadRequest(" Username and password not valid");
        }
    }
}
