using System;
using System.Web.Mvc;
using HoangGia.Service.Services;
using HoangGia.Entities;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class AdminBaseController : Controller
    {
        private readonly IErrorService _errorService;

        public AdminBaseController(IErrorService errorService)
        {
            _errorService = errorService;
        }

        protected void LogError(Exception ex)
        {
            try
            {
                var error = new Error
                {
                    Message = ex.Message,
                    CreatedDate = DateTime.Now,
                    StackTrace = ex.StackTrace
                };

                _errorService.LogError(error);
                _errorService.SaveChange();
            }
            catch(Exception ex2)
            {
                LogError(ex2);
            }
        }
    }
}