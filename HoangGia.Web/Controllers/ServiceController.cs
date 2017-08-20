using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Model.Enums;
using HoangGia.Service.Services;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServService _servService;
        private readonly IProjectService _projectService;
        private readonly ISettingService _settingService;

        public ServiceController(IServService servService,
            IProjectService projectService, ISettingService settingService)
        {
            _servService = servService;
            _projectService = projectService;
            _settingService = settingService;
        }

        public ActionResult Index()
        {
            var services = _servService.GetActivedServices();
            var serviceMapper =
                Mapper.Map<IEnumerable<Model.Entities.Service>, IEnumerable<ServiceViewModel>>(services);
            return View("List", serviceMapper);
        }

        public ActionResult Details(int id)
        {
            var service = _servService.FindById(id);
            var recentProjects = _projectService.ActivingProjects(new[] { "ProjectImages", "ProjectCategory" }).Take(5).OrderByDescending(x => x.CreatedDate);

            var recentProjectsMapper = Mapper.Map<IEnumerable<Project>, IEnumerable<ProjectViewModel>>(recentProjects);
            var serviceMapper = Mapper.Map<Model.Entities.Service, ServiceViewModel>(service);
            serviceMapper.RecenProjects = recentProjectsMapper;
            return View(serviceMapper);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 84600, VaryByParam = "none")]
        public PartialViewResult ServiceSchema()
        {
            var setting = _settingService.FindById((int)SettingValue.Default);
            var settingMapper = Mapper.Map<Setting, SettingViewModel>(setting);
            return PartialView("_ServiceSchemaPartial", settingMapper);
        }
    }
}