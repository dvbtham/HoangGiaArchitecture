using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class SlidesController : Controller
    {
        private readonly ISlideService _slideService;

        public SlidesController(ISlideService slideService)
        {
            _slideService = slideService;
        }
        public ActionResult Index()
        {
            var slides = _slideService.GetAll();
            var slidesMapper = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slides);
            return View(slidesMapper);
        }
        public ActionResult Create()
        {

            var viewModel = new SlideViewModel
            {
                Heading = "Thêm mới slide",
            };
            return View("SlideForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(SlideViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var slide = new Slide();
                slide.SlideMapper(viewModel);
                _slideService.Add(slide);

                _slideService.SaveChanges();

                return RedirectToAction("Index", "Slides");
            }
            viewModel.Heading = "Thêm mới slide";
            return View("SlideForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var slide = _slideService.FindById(id);
            if (slide == null) return HttpNotFound("Không tìm thấy slide.");
            var viewModelMapper = Mapper.Map<Slide, SlideViewModel>(slide);
            viewModelMapper.Heading = "Cập nhật slide";
            return View("SlideForm", viewModelMapper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SlideViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var slide = _slideService.FindById(viewModel.Id);
                slide.SlideMapper(viewModel);
                _slideService.Update(slide);
                _slideService.SaveChanges();
                return RedirectToAction("Index", "Slides");
            }
            viewModel.Heading = "Cập nhật slide";
            return View("SlideForm", viewModel);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _slideService.Delete(id);
            _slideService.SaveChanges();
            return Json(new { response = "Xóa thành công" });
        }
    }
}