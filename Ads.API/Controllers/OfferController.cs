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
    [Authorize(Roles = "Admin,Member")]
    [EnableCors("AllowAll")]
    public class OfferController : ControllerBase
    {
        private readonly ICreateOfferCommand _createOfferCommand;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDeleteOfferCommand _deleteOfferCommand;
        private readonly IEditOfferCommand _editOfferCommand;
        private readonly IGetOfferCommand _getOfferCommand;

        public OfferController(
            ICreateOfferCommand createOfferCommand,
            UserManager<ApplicationUser> userManager,
            IDeleteOfferCommand deleteOfferCommand,
            IEditOfferCommand editOfferCommand,
            IGetOfferCommand getOfferCommand
            )
        {
            _createOfferCommand = createOfferCommand;
            _userManager = userManager;
            _deleteOfferCommand = deleteOfferCommand;
            _editOfferCommand = editOfferCommand;
            _getOfferCommand = getOfferCommand;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var response = _getOfferCommand.Execute(new OfferSearch()
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateOfferViewModel viewModel)
        {
            try
            {
                _createOfferCommand.Execute(new CreateOfferDto()
                {
                    AdId = viewModel.AdId,
                    Amount = viewModel.Amount,
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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteOfferCommand.Execute(new DeleteOfferDto()
                {
                    Id = id,
                    UserId = GetUserId().Result
                });
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EditOfferViewModel viewModel)
        {
            try
            {
                _editOfferCommand.Execute(new EditOfferDto()
                {
                    Amount = viewModel.Amount,
                    Id = id,
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

        private async Task<string> GetUserId()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return user.Id;
        }
    }
}