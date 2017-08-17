using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Utilities.Helpers;
using HoangGia.Web.Infrastructure.Core;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly IPostCategoryService _postCategoryService;
        private readonly ITagService _tagService;

        public PostsController(IPostService postService,
            IPostCategoryService postCategoryService, ITagService tagService)
        {
            _postService = postService;
            _postCategoryService = postCategoryService;
            _tagService = tagService;
        }

        public ActionResult Index(string q, int page = 1)
        {
            int totalRow;
            ViewBag.q = q;
            var pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            var posts = _postService.PagingPosts(q, page, pageSize, out totalRow, new[] { "PostCategory" });
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

        public ActionResult PostByCategory(string q, int id, int page = 1)
        {
            int totalRow;
            ViewBag.q = q;
            var pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            var posts = _postService.PagingPosts(q, page, pageSize, out totalRow, new[] { "PostCategory" })
                .Where(x => x.CategoryId == id);
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

        public ActionResult PostByTag(string q, string tag, int page = 1)
        {
            int totalRow;
            ViewBag.q = q;
            var pageSize = int.Parse(ConfigHelper.GetByKey("pageSize"));
            var posts = _postService.PagingPosts(q, page, pageSize, out totalRow, new[] { "PostCategory" })
            .Where(x => x.Tags.ToLower().Contains(tag.ToLower()));
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
        public ActionResult Details(int id)
        {
            var post = _postService.FindById(id);
            if (Request.Cookies["ViewedPost"] != null)
            {
                if (Request.Cookies["ViewedPost"][$"pId_{id}"] == null)
                {
                    var cookie = Request.Cookies["ViewedPost"];
                    cookie[$"pId_{id}"] = id.ToString();
                    cookie.Expires = DateTime.Now.AddHours(2);
                    Response.Cookies.Add(cookie);
                    _postService.IncreaseView(id);
                }
            }
            else
            {
                var cookie = new HttpCookie("ViewedPost");
                cookie[$"pId_{id}"] = id.ToString();
                cookie.Expires = DateTime.Now.AddHours(2);
                Response.Cookies.Add(cookie);
                _postService.IncreaseView(id);
            }
            var postMapper = Mapper.Map<Post, PostViewModel>(post);

            postMapper.Next = _postService.GetAll().OrderBy(x => x.Id).FirstOrDefault(x => x.Id > id);
            postMapper.Prev = _postService.GetAll().OrderByDescending(x => x.Id).FirstOrDefault(x => x.Id < id);

            return View(postMapper);
        }

        [ChildActionOnly]
        public PartialViewResult PopularPost()
        {
            var popularPosts = _postService.GetAll()
                .Where(x => x.IsDeleted == false && x.HotFlag == true)
                .OrderByDescending(x => x.CreatedDate).Take(10);
            var popPostsMapper = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(popularPosts);
            return PartialView("_PopularPostsPartial", popPostsMapper);
        }

        [ChildActionOnly]
        public PartialViewResult Categories()
        {
            var categories = _postCategoryService.ActivingCategories();
            var categoriesMapper = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(categories);
            return PartialView("_CategoriesPartial", categoriesMapper);
        }
        [ChildActionOnly]
        public PartialViewResult TagsWidget()
        {
            var tags = _tagService.GetAll();
            return PartialView("_TagPartial", tags);
        }
    }
}