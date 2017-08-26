using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Model.Enums;
using HoangGia.Service.Services;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class SettingsController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public PartialViewResult Address()
        {
            var setting = _settingService.FindById((int)SettingValue.User);
            var settingMapper = Mapper.Map<Setting, SettingViewModel>(setting);
            return PartialView("_AddressPartial", settingMapper);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public PartialViewResult CompanyInfoTop()
        {
            var setting = _settingService.FindById((int)SettingValue.User);
            var settingMapper = Mapper.Map<Setting, SettingViewModel>(setting);
            return PartialView("_CompanyInfoPartial", settingMapper);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public PartialViewResult CompanyInfoFooter()
        {
            var setting = _settingService.FindById((int)SettingValue.User);
            var settingMapper = Mapper.Map<Setting, SettingViewModel>(setting);
            return PartialView("_CompanyInfoFooterPartial", settingMapper);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400, VaryByParam = "none")]
        public PartialViewResult ServiceDescription()
        {
            var setting = _settingService.FindById((int)SettingValue.User);
            var settingMapper = Mapper.Map<Setting, SettingViewModel>(setting);
            return PartialView("_ServiceDescriptionPartial", settingMapper);
        }
    }
}