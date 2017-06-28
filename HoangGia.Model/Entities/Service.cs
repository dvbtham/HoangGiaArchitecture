using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HoangGia.Abstracts;

namespace HoangGia.Model.Entities
{
    [Table("Services")]
    public class Service : Seoable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string Overview { get; set; }

        [Required]
        public string Description { get; set; }
        
        public string Images { get; set; }

        public string ChooseUsContent { get; set; }
        
        public string ChooseUsValues { get; set; }
        
        public bool IsActived { get; set; }

        public bool ShowOnHome { get; set; }

        public bool IsDeleted { get; set; }
    }
}
