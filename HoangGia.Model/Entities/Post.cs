using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HoangGia.Abstracts;

namespace HoangGia.Model.Entities
{
    [Table("Posts")]
    public class Post : Seoable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [MaxLength(256)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(256)]
        public string Alias { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [MaxLength(256)]
        public string Image { get; set; }

        [MaxLength(750)]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public bool? HomeFlag { get; set; }
        public bool? HotFlag { get; set; }
        public int? ViewCount { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("CategoryId")]
        public virtual PostCategory PostCategory { get; set; }

        public  ICollection<PostTag> PostTags { get; private set; }

        public Post()
        {
            PostTags = new Collection<PostTag>();
        }
    }
}
