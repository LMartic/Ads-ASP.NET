using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Exceptions;
using Ads.Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Ads.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICreateCategoryCommand _command;
        private readonly IGetCategoriesCommand _getCategoriesCommand;

        public CategoryController(ICreateCategoryCommand command, IGetCategoriesCommand getCategoriesCommand)
        {
            _command = command;
            _getCategoriesCommand = getCategoriesCommand;
        }


        [HttpGet]
        [Authorize(Roles = "Admin,Member")]
        public IActionResult GetAll([FromQuery]CategorySearch request)
        {
            try
            {
                var response = _getCategoriesCommand.Execute(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateCategoryDto viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.Name))
                return BadRequest("Name polje mora imate vrednost");

            try
            {
                _command.Execute(viewModel);
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
    }
}