using System.Web.Mvc;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Utilities.Helpers;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(ContactViewModel viewModel)
        {
            var contact = new Contact();
            contact.ContactMapper(viewModel);
            _contactService.Add(contact);
            _contactService.SaveChanges();
            MailHelper.SendMail("thamdv96@gmail.com", viewModel.Title, viewModel.Message);
            return Json(new { data =  contact }, JsonRequestBehavior.AllowGet);
        }
    }
}