using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HoangGia.Abstracts;
using HoangGia.Model.Enums;

namespace HoangGia.Model.Entities
{
    [Table("Projects")]
    public class Project : Seoable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Location { get; set; }

        [Required]
        public int ProjectCategoryId { get; set; }

        [MaxLength(700)]
        public string ProjectDescription{ get; set; }
        
        [MaxLength(256)]
        public string Client { get; set; }
        
        public ProjectStatus Status { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<ProjectImage> ProjectImages { get; private set; }

        public Project()
        {
            ProjectImages = new Collection<ProjectImage>();
        }
        
        [ForeignKey("ProjectCategoryId")]
        public virtual ProjectCategory ProjectCategory { get; set; }
    }
}
