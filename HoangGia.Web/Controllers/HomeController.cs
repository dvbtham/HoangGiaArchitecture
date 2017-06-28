using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Entities;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IServService _serveService;
        private readonly IProjectService _projectService;

        public HomeController(IMenuService menuService, IServService serveService, IProjectService projectService)
        {
            _menuService = menuService;
            _serveService = serveService;
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var menus = _menuService.GetAll();
            var menuViewModel = Mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(menus);
            return PartialView("_HeaderPartial", menuViewModel);
        }


        [ChildActionOnly]
        public PartialViewResult Services()
        {
            var services = _serveService.ShowOnHomeServices().Take(3);
            var serviceMapper =
                Mapper.Map<IEnumerable<Model.Entities.Service>, IEnumerable<ServiceViewModel>>(services);
            return PartialView("_ServiceHomePartial", serviceMapper);
        }

        [ChildActionOnly]
        public PartialViewResult Projects()
        {
            var projects = _projectService.ActivingProjects(new[] { "ProjectCategory", "ProjectImages" });
            var projectsMapper =
                Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(projects);
            return PartialView("_ProjectsHomePartial", projectsMapper);
        }
    }
}