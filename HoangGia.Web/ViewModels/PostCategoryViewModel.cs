using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using HoangGia.Service.Services;
using HoangGia.Utilities.Helpers;
using HoangGia.Web.Areas.AdminHg.Controllers;

namespace HoangGia.Web.ViewModels
{
    public class PostCategoryViewModel
    {
        
        public int Id { get; set; }

        [MaxLength(256, ErrorMessage = "Tên danh mục bài viết chỉ nhập 256 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập tên danh mục bài viết")]
        [Display(Name = "Tên danh mục bài viết")]
        public string Name { get; set; }
        
        public string Alias => AliasHelper.Alias(Name);

        [MaxLength(500, ErrorMessage = "Đường dẫn chỉ nhập 500 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Display(Name = "Danh mục cha")]
        public int? ParentId { get; set; }

        [Display(Name = "Thứ tự")]
        public int? DisplayOrder { get; set; }

        [MaxLength(256)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        public string ParentName => ServiceFactory.Get<IPostCategoryService>().GetAll()
            .SingleOrDefault(x => x.Id == ParentId)?.Name;

        [Display(Name = "Hiển thị trang chủ")]
        public bool? HomeFlag { get; set; }

        [Display(Name = "Xóa")]
        public bool IsDeleted { get; set; } = false;
        
        public IEnumerable<PostViewModel> Posts { get; set; }
        
        public IList<SelectListItem> Categories { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<PostCategoryController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<PostCategoryController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
        public string Heading { get; set; }

    }
}