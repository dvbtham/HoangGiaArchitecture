using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Utilities.Helpers;
using HoangGia.Web.Infrastructure;
using HoangGia.Web.Infrastructure.Core;
using HoangGia.Web.Infrastructure.Helpers;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Areas.AdminHg.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPostCategoryService _postCategoryService;

        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            _postService = postService;
            _postCategoryService = postCategoryService;
        }
        public ActionResult Index(string q, int page = 1)
        {
            ViewBag.SearchString = q;
            int totalRow = 0;
            var pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            var posts = _postService.PagingPosts(q, page, pageSize, out totalRow);
            var totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            var postsMapper = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(posts);
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postsMapper,
                MaxPage = int.Parse(ConfigHelper.GetByKey("maxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };
            return View("Index", paginationSet);
        }

        public ActionResult Create()
        {
            var viewModel = new PostViewModel
            {
                Heading = "Thêm mới tin tức"
            };
            PrepareCategoriesModel(viewModel);
            return View("PostForm", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var post = new Post();
                post.PostMapper(viewModel);
                post.CreatedBy = User.Identity.Name;
                _postService.Add(post);
                _postService.SaveChanges();
                return RedirectToAction("Index", "Post");
            }
            PrepareCategoriesModel(viewModel);
            viewModel.Heading = "Thêm mới tin tức";
            return View("PostForm", viewModel);
        }
        public ActionResult Edit(int id)
        {
            var post = _postService.FindById(id);
            var postMapper = Mapper.Map<Post, PostViewModel>(post);
            postMapper.Heading = "Cập nhật " + post.Name;
            PrepareCategoriesModel(postMapper);
            return View("PostForm", postMapper);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(PostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var post = _postService.FindById(viewModel.Id);
                viewModel.UpdatedDate = DateTime.Now;
                viewModel.UpdatedBy = User.Identity.Name;
                post.PostMapper(viewModel);
                _postService.Update(post);
                _postService.SaveChanges();

                return RedirectToAction("Index", "Post");
            }
            PrepareCategoriesModel(viewModel);
            viewModel.Heading = "Cập nhật " + viewModel.Name;
            return View("PostForm", viewModel);
        }

        [NonAction]
        private void PrepareCategoriesModel(PostViewModel viewModel)
        {
            if (viewModel == null)
                throw new ArgumentNullException(nameof(PostViewModel));
            if (viewModel.Categories == null)
                viewModel.Categories = new List<SelectListItem>();
            viewModel.Categories.Add(new SelectListItem
            {
                Text = @" -- Chưa chọn danh mục bài viết -- ",
                Value = string.Empty
            });
            var categories = PostCategoryHelper.GetCategoryList(_postCategoryService);
            foreach (var c in categories)
                viewModel.Categories.Add(c);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _postService.MoveToTrash(id);
            _postService.SaveChanges();
            return Json(new { response = "Xóa thành công" });
        }
    }
}