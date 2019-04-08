using DatabaseIntegration.Model;
using DatabaseIntegration.Services;
using DatabaseIntegration.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseIntegration.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TokensController : ControllerBase
    {
        private readonly IAuthenticationService _authorizationService;

        public TokensController(IAuthenticationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserViewModel userViewModel)
        {
            var authentication = _authorizationService.Authenticate(MapTo(userViewModel));

            if(authentication.IsValid)
                return Ok(authentication.Created);

            return StatusCode(401);
        }

        private User MapTo(UserViewModel userViewModel)
        {
            return new User()
            {
                Login = userViewModel.Login,
                Password = userViewModel.Password
            };
        }
    }
}
