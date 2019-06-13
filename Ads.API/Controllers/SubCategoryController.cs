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
    public class SubCategoryController : ControllerBase
    {
        private readonly ICreateSubCategoryCommand _createSubCategoryCommand;
        private readonly IListSubCategoryCommand _listSubCategoryCommand;

        public SubCategoryController(ICreateSubCategoryCommand createSubCategoryCommand, IListSubCategoryCommand listSubCategoryCommand)
        {
            _createSubCategoryCommand = createSubCategoryCommand;
            _listSubCategoryCommand = listSubCategoryCommand;
        }

        [HttpPost]
        public IActionResult CreateSubCategory([FromBody] CreateSubCategoryDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("Name polje mora imati vrednost");

            try
            {
                _createSubCategoryCommand.Execute(request);
                return StatusCode(201, "Uspesno dodato");
            }
            catch (EntityAlreadyExistsException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllSubCategories([FromQuery] EmptySearch request)
        {
            try
            {
                var response = _listSubCategoryCommand.Execute(request);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}