using System;
using System.ComponentModel.DataAnnotations;

namespace HoangGia.Web.ViewModels
{
    [Serializable]
    public class ContactViewModel
    {
        public int Id { get; set; }

        [StringLength(255, ErrorMessage = "Không nhập quá 255 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề")]
        public string Title { get; set; }

        [StringLength(255, ErrorMessage = "Không nhập quá 255 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Không nhập quá 255 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Vui lòng nhập đúng định dạng Email!")]
        public string Email { get; set; }

        [StringLength(3000, ErrorMessage = "Không nhập quá 3000 ký tự")]
        [Required(ErrorMessage = "Vui lòng nhập nội dung")]
        public string Message { get; set; }
    }
}