using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoangGia.Model.Entities
{
    [Table("Settings")]
    public class Setting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string PageTitle { get; set; }
        public string MetaDesc { get; set; }
        public string AdminEmailAddress { get; set; }
        public string NotificationReplyEmail { get; set; }
        public string ServiceScheme { get; set; }
        public string ServiceDescription { get; set; }
        public string WhyChooseUsTitles { get; set; }
        public string Smtp { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpPort { get; set; }
        public bool? SmtpEnableSsl { get; set; }
        public string Tel { get; set; }
        public string WorkingDay { get; set; }
        public string CompanyAddress { get; set; }
        public string Long { get; set; }
        public string Lat { get; set; }
    }
}
