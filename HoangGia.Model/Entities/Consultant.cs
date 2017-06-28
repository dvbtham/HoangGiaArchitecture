using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoangGia.Model.Entities
{
    [Table("Consultants")]
    public class Consultant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Fullname { get; set; }

        [Required]
        [MaxLength(500)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(500)]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        public string Phone { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public int ProjectCategoryId { get; set; }

        public DateTime CreatedAt { get; set; }
        
        [ForeignKey("ProjectCategoryId")]
        public virtual ProjectCategory ProjectCategory { get; set; }
    }
}
