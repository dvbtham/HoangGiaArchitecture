using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;
using HoangGia.Abstracts;
using HoangGia.Model.Enums;
using HoangGia.Web.Areas.AdminHg.Controllers;
using System;
using System.Linq;

namespace HoangGia.Web.ViewModels
{
    public class ProjectViewModel : Seoable
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên dự án")]
        [Display(Name = "Tên dự án")]
        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn danh mục dự án")]
        [Display(Name = "Danh mục dự án")]
        public int ProjectCategoryId { get; set; }

        [Display(Name = "Mô tả ")]
        [MaxLength(700, ErrorMessage = "Chỉ nhập 700 ký tự")]
        public string ProjectDescription { get; set; }

        [Display(Name = "Khách hàng")]
        public string Client { get; set; }

        [Display(Name = "Trạng thái")]
        public ProjectStatus Status { get; set; } = ProjectStatus.UnActived;

        [Display(Name = "Xóa")]
        public bool IsDeleted { get; set; } = false;

        public ProjectCategoryViewModel ProjectCategory { get; set; }

        public IEnumerable<ProjectImageViewModel> ProjectImages { get; set; }

        public IEnumerable<ProjectViewModel> RelatedProjects { get; set; }

        public string Action
        {
            get
            {
                Expression<Func<ProjectController, ActionResult>> update = (c => c.Update(this));
                Expression<Func<ProjectController, ActionResult>> create = (c => c.Create(this));
                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression)?.Method.Name;
            }
        }
        public string Heading { get; set; }
        
        public SelectList Categories { get; set; }

        public string Image => ProjectImages.FirstOrDefault()?.ImageUrl;

        public SelectList ProjectStatusList { get; set; }
        
    }

    
   
}