using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoangGia.Entities
{
    [Table("Menus")]
    public class Menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short Id { get; set; }

        public short? ParentId { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        [MaxLength(256)]
        public string ActionName { get; set; }

        [Required]
        [MaxLength(256)]
        public string ControllerName { get; set; }

        public int? Order { get; set; }
        public bool IsActived { get; set; }
    }
}
