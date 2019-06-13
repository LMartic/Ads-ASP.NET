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
        private readonly ICreateCategoryCommand _createCategoryCommand;
        private readonly IListCategoryCommand _listCategoryCommand;
        private readonly IDeleteCategoryCommand _deleteCategoryCommand;

        public CategoryController(ICreateCategoryCommand createCategoryCommand,
            IListCategoryCommand listCategoryCommand,
            IDeleteCategoryCommand deleteCategoryCommand)
        {
            _createCategoryCommand = createCategoryCommand;
            _listCategoryCommand = listCategoryCommand;
            _deleteCategoryCommand = deleteCategoryCommand;
        }
        [HttpGet]
        public IActionResult GetAllCategories(EmptySearch request)
        {
            try
            {
                var response = _listCategoryCommand.Execute(request);
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id, [FromBody]CreateCategoryDto request)
        {
            request.Id = id;
            try
            {
                _deleteCategoryCommand.Execute(request);
                return Ok();
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

        [HttpPost]
        public IActionResult AddCategory([FromBody] CreateCategoryDto categoryDto)
        {
            if (string.IsNullOrEmpty(categoryDto.Name))
                return BadRequest("Name polje mora imate vrednost");

            try
            {
                _createCategoryCommand.Execute(categoryDto);
                return StatusCode(200);
            }
            catch (EntityNotFoundException ex)
            {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}