using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoangGia.Model.Entities
{
    [Table("ProjectCategories")]
    public class ProjectCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Project> Projects { get; private set; }

        public ProjectCategory()
        {
            Projects = new Collection<Project>();
        }
    }
}
