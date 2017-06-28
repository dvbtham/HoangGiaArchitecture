using System.ComponentModel.DataAnnotations.Schema;

namespace HoangGia.Model.Entities
{
    [Table("Follower")]
    public class Follower
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool IsBanned { get; set; }
    }
}
