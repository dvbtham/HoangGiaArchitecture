using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using HoangGia.Web.Areas.AdminHg.Controllers;
using HoangGia.Entities;

namespace HoangGia.Web.ViewModels
{
    public class MenuViewModel 
    {
        public short Id { get; set; }

        [Display(Name = "Danh mục cha")]
        public short? ParentId { get; set; }

        [Display(Name = "Tên menu")]
        [Required(ErrorMessage = "Bạn chưa nhập tên menu")]
        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên Action name")]
        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string ActionName { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên Controller name")]
        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string ControllerName { get; set; }

        [Display(Name = "Thứ tự")]
        public int? Order { get; set; }
        
        [Display(Name = "Trạng thái")]
        public bool IsActived { get; set; } = true;
        
        public IList<SelectListItem> Menus { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<MenuController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<MenuController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
        public string Heading { get; set; }
    }
}