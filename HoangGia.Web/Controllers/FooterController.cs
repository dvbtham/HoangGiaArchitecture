using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Model.Entities;
using HoangGia.Service.Services;
using HoangGia.Web.ViewModels;

namespace HoangGia.Web.Controllers
{
    public class FooterController : Controller
    {
        private readonly IPostCategoryService _postCategoryService;
        private readonly IPostService _postService;

        public FooterController(IPostCategoryService postCategoryService, IPostService postService)
        {
            _postCategoryService = postCategoryService;
            _postService = postService;
        }
        
        [ChildActionOnly]
        public PartialViewResult NewPosts()
        {
            var popularPosts = _postService.GetAll()
                .Where(x => x.IsDeleted == false && x.HotFlag == true)
                .OrderByDescending(x => x.CreatedDate).Take(2);
            var popPostsMapper = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(popularPosts);
            return PartialView("_NewPostsPartial", popPostsMapper);
        }

        [ChildActionOnly]
        public ActionResult CategoryFooter()
        {
            var categories = _postCategoryService.GetAll()
                .Where(x => x.IsDeleted == false);
            var pcategoriesMapper = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(categories);
            return PartialView("_CategoryFooterPartial", pcategoriesMapper);
        }
    }
}