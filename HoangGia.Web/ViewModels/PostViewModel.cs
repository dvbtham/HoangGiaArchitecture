using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Abstracts;
using HoangGia.Model.Entities;
using HoangGia.Utilities.Helpers;
using HoangGia.Web.Areas.AdminHg.Controllers;

namespace HoangGia.Web.ViewModels
{
    public class PostViewModel : Seoable
    {
        public long Id { get; set; }

        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề bài viết")]
        [Display(Name = "Tiêu đề")]
        public string Name { get; set; }

        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        [Required(ErrorMessage = "Bạn chưa nhập tiêu đề SEO")]
        [Display(Name = "Tiêu đề SEO")]
        public string Alias => AliasHelper.Alias(Name);

        [Required(ErrorMessage = "Bạn chưa chọn danh mục bài viết")]
        [Display(Name = "Danh mục bài viết")]
        public int CategoryId { get; set; }

        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [MaxLength(750, ErrorMessage = "Mô tả chỉ nhập 750 ký tự")]
        [Display(Name = "Mô tả")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập nội dung")]
        [Display(Name = "Nội dung")]
        [AllowHtml]
        public string Content { get; set; }

        [Display(Name = "Thẻ gán")]
        [MaxLength(256, ErrorMessage = "Thẻ gán chỉ nhập 256 ký tự ")]
        public string Tags { get; set; }

        [Display(Name = "Hiển thị trang chủ")]
        public bool? HomeFlag { get; set; }

        [Display(Name = "Bài viết Hot")]
        public bool? HotFlag { get; set; } = false;

        [Display(Name = "Lượt xem")]
        public int? ViewCount { get; set; } = 0;

        [Display(Name = "Xóa")]
        public bool IsDeleted { get; set; } = false;
        
        public PostCategory PostCategory { get; set; }

        [IgnoreMap]
        public Post Next { get; set; }
        
        [IgnoreMap]
        public Post Prev { get; set; }

        [IgnoreMap]
        public IList<SelectListItem> Categories { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<PostController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<PostController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
        public string Heading { get; set; }
    }
}