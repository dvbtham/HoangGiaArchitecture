using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Model.Enums;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectController(IProjectService projectService, IProjectCategoryService projectCategoryService)
        {
            _projectService = projectService;
            _projectCategoryService = projectCategoryService;
        }

        public ActionResult Index()
        {
            var projects = _projectService.GetInclude(null, new[] { "ProjectCategory" });
            var projectsMapper = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);

            return View(projectsMapper);
        }

        public IEnumerable<ProjectCategoryViewModel> Categories()
        {
            var categories = _projectCategoryService.GetWorkingProjectCategories();
            var categoriesMapper =
                Mapper.Map<IEnumerable<ProjectCategory>, IEnumerable<ProjectCategoryViewModel>>(categories);
            return categoriesMapper;
        }
        public SelectList ProjectStatusList()
        {
            var list = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "Chưa khởi tạo", Value = ((int) ProjectStatus.UnActived).ToString()},
                    new SelectListItem {Text = "Đang thi hành", Value = ((int) ProjectStatus.Working).ToString()},
                    new SelectListItem {Text = "Trì hoãn", Value = ((int) ProjectStatus.Delay).ToString()},
                    new SelectListItem {Text = "Hoàn thành", Value = ((int) ProjectStatus.Done).ToString()},
                }, "Value", "Text");
            return list;
        }
        public ActionResult Create()
        {

            var viewModel = new ProjectViewModel
            {
                Heading = "Thêm mới dự án",
                Categories = new SelectList(Categories(), "Id", "Name"),
                ProjectStatusList = ProjectStatusList()
            };
            return View("ProjectForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var project = new Project();
                project.ProjectMapper(viewModel);
                _projectService.Add(project);
                try
                {
                    _projectCategoryService.SaveChanges();
                }
                catch (DbEntityValidationException dbEnValEx)
                {
                    foreach (var eve in dbEnValEx.EntityValidationErrors)
                    {
                        string mess = "Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validations erros.";
                        Trace.WriteLine(mess);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            string mess2 = "Property: \"" + ve.PropertyName + "\", Errors: \"" + ve.ErrorMessage + "\"";
                            Trace.WriteLine(mess2);
                        }
                    }

                }
                return RedirectToAction("Index", "Project");
            }
            viewModel.Categories = new SelectList(Categories(), "Id", "Name");
            viewModel.ProjectStatusList = ProjectStatusList();
            viewModel.Heading = "Thêm mới dự án";
            return View("ProjectForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var project = _projectService.FindById(id);
            if (project == null) return HttpNotFound("Không tìm thấy dự án.");
            var viewModelMapper = Mapper.Map<Project, ProjectViewModel>(project);
            viewModelMapper.Heading = "Cập nhật dự án";
            viewModelMapper.Categories = new SelectList(Categories(), "Id", "Name");
            viewModelMapper.ProjectStatusList = ProjectStatusList();
            return View("ProjectForm", viewModelMapper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var project = _projectService.FindById(viewModel.Id);
                project.ProjectMapper(viewModel);
                _projectService.Update(project);
                _projectService.SaveChanges();
                return RedirectToAction("Index", "Project");
            }
            viewModel.ProjectStatusList = ProjectStatusList();
            viewModel.Categories = new SelectList(Categories(), "Id", "Name");
            viewModel.Heading = "Cập nhật dự án";
            return View("ProjectForm", viewModel);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _projectService.Delete(id);
            _projectService.SaveChanges();
            return Json(new { response = "Xóa thành công" });
        }
    }
}