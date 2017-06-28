using System.Web.Mvc;

namespace HoangGia.Web.Areas.AdminHg
{
    public class AdminHgAreaRegistration : AreaRegistration 
    {
        public override string AreaName => "AdminHg";

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminHg",
                "AdminHg",
                new { action = "Index", controller = "Dashboard", id = UrlParameter.Optional }
            );
            context.MapRoute(
                "AdminHg_default",
                "AdminHg/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}