using Ads.DataAccess.Domain;
using Ads.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ads.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind("FirstName,LastName,Email,Password,Role")]
            RegisterViewModel model)
        {
            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email

            };
            var identityResult = _userManager.CreateAsync(user, model.Password).Result;

            _userManager.AddToRoleAsync(user, model.Role);
            return RedirectToAction("Index", "Home");
        }
    }
}
