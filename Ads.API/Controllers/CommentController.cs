using Ads.API.ViewModels;
using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
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
    [EnableCors("AllowAll")]
    [Authorize(Roles = "Admin,Member")]
    public class CommentController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICreateCommentCommand _createCommentCommand;
        private readonly IEditCommentCommand _editCommentCommand;

        public CommentController(
            UserManager<ApplicationUser> userManager,
            ICreateCommentCommand createCommentCommand,
            IEditCommentCommand editCommentCommand)
        {
            _userManager = userManager;
            _createCommentCommand = createCommentCommand;
            _editCommentCommand = editCommentCommand;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCommentViewModel viewModel)
        {
            try
            {
                _createCommentCommand.Execute(new CreateCommandDto()
                {
                    AdId = viewModel.AdId,
                    Comment = viewModel.Comment,
                    UserId = GetUserId().Result

                });
                return StatusCode(201, "Komentar uspesno kreiran");
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

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditCommentsViewModel viewModel)
        {
            try
            {
                _editCommentCommand.Execute(new EditCommentDto()
                {
                    UserId = GetUserId().Result,
                    Comment = viewModel.Comment,
                    Id = id
                });
                return StatusCode(201, "Uspesno azuriran komentar");
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