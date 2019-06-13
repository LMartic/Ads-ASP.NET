using Ads.Application.Commands;
using Ads.Application.Dto;
using Ads.Application.Searches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Ads.MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubCategoryController : Controller
    {
        private readonly IListSubCategoryCommand _listSubCategoryCommand;
        private readonly IListCategoryCommand _listCategoryCommand;
        private readonly ICreateSubCategoryCommand _createSubCategoryCommand;

        public SubCategoryController(IListSubCategoryCommand listSubCategoryCommand, IListCategoryCommand listCategoryCommand, ICreateSubCategoryCommand createSubCategoryCommand)
        {
            _listSubCategoryCommand = listSubCategoryCommand;
            _listCategoryCommand = listCategoryCommand;
            _createSubCategoryCommand = createSubCategoryCommand;
        }

        [HttpGet]
        public ActionResult Index(EmptySearch model)
        {
            return View(GetSubCategories(model));
        }

        [HttpGet]
        public ActionResult Add(EmptySearch request)
        {
            ViewData["Categories"] = new SelectList(GetCategoryList(request), "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Add(CreateSubCategoryDto request)
        {
            try
            {
                _createSubCategoryCommand.Execute(request);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            return View();
        }

        private IEnumerable<CreateSubCategoryDto> GetSubCategories(EmptySearch model)
        {
            return _listSubCategoryCommand.Execute(model);
        }

        private IEnumerable<CreateCategoryDto> GetCategoryList(EmptySearch request)
        {
            return _listCategoryCommand.Execute(request);
        }
    }
}
