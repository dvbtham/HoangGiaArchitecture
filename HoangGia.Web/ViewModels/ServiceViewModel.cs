using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using HoangGia.Abstracts;
using HoangGia.Web.Areas.AdminHg.Controllers;

namespace HoangGia.Web.ViewModels
{
    public class ServiceViewModel : Seoable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên dịch vụ")]
        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        [Display(Name = "Tên dịch vụ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng mô tả về dịch vụ")]
        [MaxLength(500, ErrorMessage = "Chỉ nhập 500 ký tự")]
        [Display(Name = "Mô tả tổng quan")]
        public string Overview { get; set; }

        [Required(ErrorMessage = "Vui lòng mô tả chi tiết về dịch vụ")]
        [Display(Name = "Mô tả chi tiết")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn hình ảnh")]
        [Display(Name = "Ảnh")]
        public string Images { get; set; }

        [Display(Name = "Tại sao chọn chúng tôi?")]
        public string ChooseUsContent { get; set; }

        [Display(Name = "Giá trị mang lại")]
        public string ChooseUsValues { get; set; }

        [Display(Name = "Kích hoạt")]
        public bool IsActived { get; set; } = true;

        [Display(Name = "Xóa")]
        public bool IsDeleted { get; set; } = false;

        [Display(Name = "Hiển thị trang chủ")]
        public bool ShowOnHome { get; set; } = false;

        public string Action
        {
            get
            {
                Expression<Func<ServicesController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<ServicesController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }

        public string Heading { get; set; }

        public string CoverImage => ImageUrls?[0];
        public List<string> ValueList => ChooseUsValues?.Split('/').ToList();
        public List<string> ImageUrls => Images?.Split(',').ToList();
    }
}