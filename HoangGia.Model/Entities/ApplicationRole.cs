using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace HoangGia.Model.Entities
{
    public class ApplicationRole : IdentityRole
    {
        [MaxLength(500)]
        public string Description { get; set; }

        public bool? IsDeleted { get; set; }
    }
}