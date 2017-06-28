using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IServService _servService;

        public ServicesController(IServService servService)
        {
            _servService = servService;
        }

        public ActionResult Index()
        {
            var services = _servService.GetAll();
            var serviceMapper =
                Mapper.Map<IEnumerable<Model.Entities.Service>, IEnumerable<ServiceViewModel>>(services);
            return View(serviceMapper);
        }

        public ActionResult Create()
        {
            var viewModel = new ServiceViewModel
            {
                Heading = "Thêm mới dịch vụ"
            };
            return View("ServiceForm", viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var service = new Model.Entities.Service();
                service.ServiceMapper(viewModel);
                _servService.Add(service);
                _servService.SaveChanges();
                return RedirectToAction("Index", "Services");
            }
            viewModel.Heading = "Thêm mới dịch vụ";
            return View("ServiceForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var service = _servService.FindById(id);
            if (id == 0 || service == null)
                return HttpNotFound("Không tìm thấy dịch vụ");
            var serviceMapper = Mapper.Map<Model.Entities.Service, ServiceViewModel>(service);
            serviceMapper.Heading = "Cập nhật dịch vụ";
            return View("ServiceForm", serviceMapper);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ServiceViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var service = _servService.FindById(viewModel.Id);
                service.ServiceMapper(viewModel);
                _servService.Update(service);
                _servService.SaveChanges();
                return RedirectToAction("Index", "Services");
            }
            viewModel.Heading = "Cập nhật dịch vụ";
            return View("ServiceForm", viewModel);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _servService.Delete(id);
            _servService.SaveChanges();
            return Json(new { response = "Xóa thành công" }, JsonRequestBehavior.AllowGet);
        }
    }
}