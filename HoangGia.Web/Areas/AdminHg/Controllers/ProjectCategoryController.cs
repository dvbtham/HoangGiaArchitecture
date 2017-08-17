using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class ProjectCategoryController : Controller
    {
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectCategoryController(IProjectCategoryService projectCategoryService)
        {
            _projectCategoryService = projectCategoryService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult List()
        {
            var data = _projectCategoryService.GetWorkingProjectCategories();
            return Json(new { response = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var projectCategory = _projectCategoryService.FindById(id);
            return Json(projectCategory, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(ProjectCategoryViewModel model)
        {
            var category = new ProjectCategory();
            category.ProjectCategoryMapper(model);
            var data = _projectCategoryService.Add(category);
            _projectCategoryService.SaveChanges();
            return Json(new { response = data }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(ProjectCategoryViewModel model)
        {
            var category = _projectCategoryService.FindById(model.Id);
            category.ProjectCategoryMapper(model);
            _projectCategoryService.Update(category);
            _projectCategoryService.SaveChanges();
            return Json(new { response = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            //_projectCategoryService.Delete(id);
            _projectCategoryService.Trash(id);
            _projectCategoryService.SaveChanges();
            return Json(new { response = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
        }
    }
}