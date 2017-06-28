using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class ProjectImageController : Controller
    {
        private readonly IProjectImageService _projectImageService;
        private readonly IProjectService _projectService;

        public ProjectImageController(IProjectImageService projectImageService, IProjectService projectService)
        {
            _projectImageService = projectImageService;
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            var projects = _projectService.GetInclude(null, new[] { "ProjectImages" });
            var projectsMapper = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);
            return View(projectsMapper);
        }

        public JsonResult Add(ProjectImageViewModel viewModel)
        {
            var projectImage = new ProjectImage();
            projectImage.ProjectImageMapper(viewModel);

            var urls = viewModel.ImageUrl.Split(',').ToList();
            if (urls.Count > 0)
            {
                foreach (var url in urls)
                {
                    projectImage.ImageUrl = url;
                    _projectImageService.Add(projectImage);
                    _projectImageService.SaveChanges();
                }
                return Json(new { res = viewModel }, JsonRequestBehavior.AllowGet);
            }
            
            _projectImageService.Add(projectImage);
            _projectImageService.SaveChanges();
            return Json(new { res = viewModel }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            _projectImageService.Delete(id);
            _projectImageService.SaveChanges();
            return Json(new { res = "deleted" }, JsonRequestBehavior.AllowGet);
        }
    }
}