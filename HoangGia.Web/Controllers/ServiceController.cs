using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Service.Services;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IServService _servService;

        public ServiceController(IServService servService)
        {
            _servService = servService;
        }
        
        public ActionResult Index()
        {
            var services = _servService.GetActivedServices();
            var serviceMapper =
                Mapper.Map<IEnumerable<Model.Entities.Service>, IEnumerable<ServiceViewModel>>(services);
            return View("List",serviceMapper);
        }

        public ActionResult Details(int id)
        {
            var service = _servService.FindById(id);
            var serviceMapper = Mapper.Map<Model.Entities.Service, ServiceViewModel>(service);
            return View(serviceMapper);
        }
    }
}