using Ads.API.ViewModels;
using Ads.DataAccess.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ads.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel viewModel)
        {
            var userExist = await _userManager.FindByEmailAsync(viewModel.Email);
            if (userExist != null)
                return BadRequest("Korisnik vec postoji");

            var user = new ApplicationUser()
            {
                UserName = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Email = viewModel.Email
            };

            var register = await _userManager.CreateAsync(user, viewModel.Password);
            if (register.Succeeded)
            {

                var addRole = await _userManager.AddToRoleAsync(user, viewModel.Role);
                if (!addRole.Succeeded)
                    return BadRequest("Greska u dodeljivanju rolova");
                return Ok("Korsnik je uspesno kreiran");
            }

            return BadRequest("Problem u kreiranju korisnika");
        }
    }
}
