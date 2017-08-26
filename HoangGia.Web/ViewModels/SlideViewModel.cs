using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using HoangGia.Web.Areas.AdminHg.Controllers;

namespace HoangGia.Web.ViewModels
{
    public class SlideViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Bạn chưa nhập Địa chỉ hình ảnh!")]
        [StringLength(255, ErrorMessage = "Không nhập quá 255 ký tự")]
        public string Image { get; set; }

        [Url(ErrorMessage = "Vui lòng nhập đúng định dạng địa chỉ!")]
        public string Url { get; set; }

        public bool IsActived { get; set; } = true;

        [IgnoreMap]
        public string Action
        {
            get
            {
                Expression<Func<SlidesController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<SlidesController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
        [IgnoreMap]
        public string Heading { get; set; }
    }
}