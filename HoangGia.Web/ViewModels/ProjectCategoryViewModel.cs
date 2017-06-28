using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HoangGia.Model.Entities;

namespace HoangGia.Web.ViewModels
{
    public class ProjectCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa nhập tên danh mục dự án")]
        [MaxLength(256, ErrorMessage = "Chỉ nhập 256 ký tự")]
        [Display(Name = "Tên danh mục dự án")]
        public string Name { get; set; }

        [Display(Name = "Xóa")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<Project> Projects { get;  set; }
       
    }
}