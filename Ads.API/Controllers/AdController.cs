using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Searches;
using Ads.DataAccess.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace Ads.API.Controllers
{
    [Route("api/[controller]")]
    public class AdController : ControllerBase
    {
        private readonly ICreateAdCommand _createAdCommand;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IListAdCommand _command;
        private readonly IEditAdCommand _editAdCommand;
        private readonly IDeleteAdCommand _deleteAdCommand;

        public AdController(ICreateAdCommand createAdCommand,
            UserManager<ApplicationUser> userManager,
            IListAdCommand command,
            IEditAdCommand editAdCommand,
            IDeleteAdCommand deleteAdCommand)
        {
            _createAdCommand = createAdCommand;
            _userManager = userManager;
            _command = command;
            _editAdCommand = editAdCommand;
            _deleteAdCommand = deleteAdCommand;
        }

        [HttpGet]
        public IActionResult GetAllAds([FromQuery] AdSearch model)
        {
            try
            {
                var response = _command.Execute(model);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateAd([FromBody] CreateAdDto request)
        {
            if (string.IsNullOrEmpty(request.Subject))
                return BadRequest("Subject mora imati vrednost");
            request.UserId = GetUserId();
            try
            {
                _createAdCommand.Execute(request);
                return StatusCode(200, "Uspesno dodato");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult EditAd(int id, [FromBody]EditAdDto model)
        {
            model.Id = id;
            model.UserId = GetUserId();
            try
            {
                _editAdCommand.Execute(model);
                return StatusCode(200, "Uspesno azurirano");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAd(int id, [FromBody] DeleteAdDto model)
        {
            model.Id = id;
            model.UserId = GetUserId();
            try
            {
                _deleteAdCommand.Execute(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private string GetUserId()
        {
            var user = _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value).Result;
            return user.Id;
        }
    }
}