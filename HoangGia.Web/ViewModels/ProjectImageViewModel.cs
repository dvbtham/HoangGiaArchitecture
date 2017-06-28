using System.ComponentModel.DataAnnotations;

namespace HoangGia.Web.ViewModels
{
    public class ProjectImageViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn dự án")]
        [Display(Name = "Dự án")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Bạn chưa chọn hình ảnh")]
        [Display(Name = "Hình ảnh")]
        public string ImageUrl { get; set; }

        public ProjectViewModel Project { get; set; }
        
    }
}