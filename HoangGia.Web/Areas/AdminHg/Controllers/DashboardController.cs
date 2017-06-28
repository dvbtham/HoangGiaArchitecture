using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class DashboardController : Controller
    {
        // GET: AdminHg/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}