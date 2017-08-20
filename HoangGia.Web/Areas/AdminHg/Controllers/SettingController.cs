using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Model.Enums;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }
        
        public ActionResult Index()
        {
            return View(GetSetting());
        }
        public PartialViewResult WebSetting()
        {
            return PartialView("_WebSettingPartial", GetSetting());
        }
        public PartialViewResult ServiceSetting()
        {
            return PartialView("_ServiceSettingPartial", GetSetting());
        }
        public PartialViewResult MailSetting()
        {
            return PartialView("_MailSettingPartial", GetSetting());
        }

        [HttpGet]
        public JsonResult Edit(int id)
        {
            var setting = _settingService.FindById(id);
            return Json(setting, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(SettingViewModel viewModel)
        {
            var setting = _settingService.FindById(viewModel.Id);
            setting.SettingMapper(viewModel);
            _settingService.SaveChanges();
            return Json(new
            {
                status = true
            });
        }
        [NonAction]
        public SettingViewModel GetSetting()
        {
            var setting = _settingService.FindById((int)SettingValue.Default);
            var settingMapper = Mapper.Map<Setting, SettingViewModel>(setting);
            return settingMapper;
        }
    }
}