using Ads.API.ViewModels;
using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
using Ads.Application.Searches;
using Ads.DataAccess.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ads.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin, Member")]
    [EnableCors("AllowAll")]
    public class FollowerController : ControllerBase
    {
        private readonly ICreateRemoveFollowerCommand _createRemoveFollowerCommand;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGetFollowersCommand _getFollowersCommand;

        public FollowerController(
            ICreateRemoveFollowerCommand createRemoveFollowerCommand,
            UserManager<ApplicationUser> userManager,
            IGetFollowersCommand getFollowersCommand
            )
        {
            _createRemoveFollowerCommand = createRemoveFollowerCommand;
            _userManager = userManager;
            _getFollowersCommand = getFollowersCommand;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AdFollowerViewModel viewModel)
        {
            try
            {
                _createRemoveFollowerCommand.Execute(new CreateFollowerDto()
                {
                    AdId = viewModel.AdId,
                    UserId = GetUserId().Result
                });

                return StatusCode(201);
            }
            catch (EntityNotFoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("following")]
        public IActionResult Following()
        {
            try
            {
                var response = _getFollowersCommand.Execute(new FollowerSearch()
                {
                    UserId = GetUserId().Result
                });
                return Ok(response);
            }
            catch (EntityNotFoundException e)
            {
                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        private async Task<string> GetUserId()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return user.Id;
        }
    }
}