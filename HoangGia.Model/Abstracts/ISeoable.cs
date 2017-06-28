using System;
using System.ComponentModel.DataAnnotations;

namespace HoangGia.Abstracts
{
    public interface ISeoable
    {
        DateTime? CreatedDate { get; set; }

        string CreatedBy { get; set; }

        DateTime? UpdatedDate { get; set; }

        string UpdatedBy { get; set; }

        string MetaKeyword { get; set; }

        string MetaDescription { get; set; }
        
    }

    public class Seoable : ISeoable
    {
        public DateTime? CreatedDate { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        [MaxLength(150)]
        public string MetaKeyword { get; set; }

        [MaxLength(150)]
        public string MetaDescription { get; set; }
    }
}
