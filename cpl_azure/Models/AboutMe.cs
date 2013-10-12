using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Mvc;
using T4TS;

namespace cpl_azure.Models
{
    [TypeScriptInterfaceAttribute(Module = "cpla.models")]
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
