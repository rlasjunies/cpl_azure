using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Mvc;

namespace cpl_azure.Models
{
    public class AboutMe
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AboutMeId { get; set; }

        [Required]
        [Display(Name = "Biographie")]
        [AllowHtml]
        public string Biographie { get; set; }
    }
}
