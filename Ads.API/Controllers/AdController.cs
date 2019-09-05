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
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin,Member")]
    [EnableCors("AllowAll")]
    public class AdController : ControllerBase
    {
        private readonly ICreateAdCommand _createAdCommand;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGetAdsCommand _getAdsCommand;
        private readonly IDeleteAdCommand _deleteAdCommand;
        private readonly IEditAdCommand _editAdCommand;
        private readonly IGetAdOffersCommand _getAdOffersCommand;
        private readonly IGetAdCommentsCommand _getAdCommentsCommand;


        public AdController(ICreateAdCommand createAdCommand,
            UserManager<ApplicationUser> userManager,
            IGetAdsCommand getAdsCommand,
            IDeleteAdCommand deleteAdCommand,
            IEditAdCommand editAdCommand,
            IGetAdOffersCommand getAdOffersCommand,
            IGetAdCommentsCommand getAdCommentsCommand)
        {
            _createAdCommand = createAdCommand;
            _userManager = userManager;
            _getAdsCommand = getAdsCommand;
            _deleteAdCommand = deleteAdCommand;
            _editAdCommand = editAdCommand;
            _getAdOffersCommand = getAdOffersCommand;
            _getAdCommentsCommand = getAdCommentsCommand;
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll([FromQuery] AdSearch request)
        {
            try
            {
                var response = _getAdsCommand.Execute(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOffers(int id)
        {
            try
            {
                var response = _getAdOffersCommand.Execute(new AdOfferSearch()
                {
                    AdId = id,
                    UserId = GetUserId().Result
                });
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        [ActionName("GetAllComments")]
        [AllowAnonymous]
        public IActionResult GetComments(int id)
        {
            try
            {
                var response = _getAdCommentsCommand.Execute(id);
                return Ok(response);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] AdViewModel viewModel)
        {
            try
            {
                _createAdCommand.Execute(new CreateAdDto()
                {
                    Subject = viewModel.Subject,
                    UserId = GetUserId().Result,
                    Description = viewModel.Description,
                    CategoryId = viewModel.CategoryId,
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
            var request = new DeleteAdDto();
            request.Id = id;
            request.UserId = GetUserId().Result;
            try
            {
                _deleteAdCommand.Execute(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditAdDto request)
        {
            request.Id = id;
            try
            {
                _editAdCommand.Execute(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        private async Task<string> GetUserId()
        {
            var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return user.Id;
        }
    }
}