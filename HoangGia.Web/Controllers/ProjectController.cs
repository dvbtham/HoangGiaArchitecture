using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var project = _projectService.FindById(id, new[]{ "ProjectImages" });
            if (project == null) return new HttpNotFoundResult("project not found.");

            var relatedProjects = _projectService.GetRelatedProjects(project.Id, project.ProjectCategoryId, new[] { "ProjectImages", "ProjectCategory" });

            var projectMapper = Mapper.Map<Project, ProjectViewModel>(project);
            var relatedProjectMapper = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(relatedProjects);

            projectMapper.Heading = projectMapper.Name;
            projectMapper.RelatedProjects = relatedProjectMapper;

            return View(projectMapper);
        }
    }
}