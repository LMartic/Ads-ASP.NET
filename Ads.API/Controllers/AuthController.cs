using Ads.API.ViewModels;
using Ads.Application.Interfaces;
using Ads.DataAccess.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ads.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtFactory _jwtFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(SignInManager<ApplicationUser> signInManager,
            IJwtFactory jwtFactory,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _jwtFactory = jwtFactory;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginViewModel viewModel)
        {
            var user = _userManager.FindByNameAsync(viewModel.Email);
            if (user == null)
                return BadRequest("Korsnik ne postoji.");

            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, false, false);
            if (!result.Succeeded)
                BadRequest("Pogresno korisnicko ime ili sifra");

            var token = _jwtFactory.GenerateEncodeToken(user.Result.Id);
            return Ok(token);
        }
    }
}