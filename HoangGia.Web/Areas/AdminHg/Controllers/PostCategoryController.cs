using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.Infrastructure.Helpers;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class PostCategoryController : Controller
    {
        private readonly IPostCategoryService _postCategoryService;

        public PostCategoryController(IPostCategoryService postCategoryService)
        {
            _postCategoryService = postCategoryService;
        }
        public PartialViewResult Index()
        {
            ViewBag.TrashButton = true;
            var categories = _postCategoryService.ActivingCategories();
            var categoriesMapper =
                Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(categories);
            
            return PartialView("_PostCategoriesPartial",categoriesMapper);
        }
        public PartialViewResult Trash()
        {
            ViewBag.TrashButton = false;
            var categories = _postCategoryService.TrashedCategories();
            var categoriesMapper =
                Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(categories);

            return PartialView("_PostCategoriesPartial", categoriesMapper);
        }
        public ActionResult Create()
        {
            var viewModel = new PostCategoryViewModel
            {
                Heading = "Thêm mới danh mục bài viết"
            };
            PrepareCategoriesModel(viewModel);
            return View("PostCategoryForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PostCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var postCategory = new PostCategory();
                postCategory.PostCategoryMapper(viewModel);
                _postCategoryService.Add(postCategory);
                _postCategoryService.SaveChanges();
                return RedirectToAction("Index", "PostCategory");
            }
            PrepareCategoriesModel(viewModel);
            viewModel.Heading = "Thêm mới danh mục bài viết";
            return View("PostCategoryForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var postCategory = _postCategoryService.FindById(id);
            var viewModelMapper = Mapper.Map<PostCategory, PostCategoryViewModel>(postCategory);
            viewModelMapper.Heading = "Cập nhật danh mục bài viết";
            PrepareCategoriesModel(viewModelMapper);
            return View("PostCategoryForm", viewModelMapper);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PostCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var postCategory = _postCategoryService.FindById(viewModel.Id);
                postCategory.PostCategoryMapper(viewModel);
                _postCategoryService.Update(postCategory);
                _postCategoryService.SaveChanges();
                return RedirectToAction("Index", "PostCategory");
            }
            PrepareCategoriesModel(viewModel);
            viewModel.Heading = "Cập nhật danh mục bài viết";
            return View("PostCategoryForm", viewModel);
        }

        [NonAction]
        private void PrepareCategoriesModel(PostCategoryViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(PostCategoryViewModel));
            if (viewModel.Categories == null)
                viewModel.Categories = new List<SelectListItem>();
            viewModel.Categories.Add(new SelectListItem
            {
                Text = @" -- Chưa chọn danh mục cha -- ",
                Value = ""
            });
            var categories = PostCategoryHelper.GetCategoryList(_postCategoryService);
            foreach (var c in categories)
                viewModel.Categories.Add(c);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _postCategoryService.IsDeleted(id);
            _postCategoryService.SaveChanges();
            return Json(new { response = "Xóa thành công" });
        }
    }
}