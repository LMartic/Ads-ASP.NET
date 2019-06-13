using Ads.API.ViewModels;
using Ads.Application.Interfaces;
using Ads.DataAccess.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ads.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(UserManager<ApplicationUser> userManager, IJwtFactory jwtFactory, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            _signInManager = signInManager;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel viewModel)
        {
            var user = _userManager.FindByEmailAsync(viewModel.Email).Result;
            if (user == null)
                return BadRequest("Korisnik ne postoji");

            var signIn = _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, false).Result;

            if (!signIn.Succeeded)
                return BadRequest("Poresan email ili lozinka");

            var token = _jwtFactory.GenerateEncodeToken(user.Id);
            return Ok(token);

        }
    }
}
