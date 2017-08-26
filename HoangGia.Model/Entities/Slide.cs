using System.ComponentModel.DataAnnotations.Schema;

namespace HoangGia.Model.Entities
{
    [Table("Slides")]
    public class Slide
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public bool IsActived { get; set; }
    }
}
