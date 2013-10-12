using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Mvc;
using T4TS;

namespace cpl_azure.Models
{
    [TypeScriptInterfaceAttribute( Module="cpla.models" )]
    public class News
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Titre")]
        public string Title { get; set; }

        [Display(Name = "Synthèse")]
        [StringLength(255, ErrorMessage = "La chaine ne pas dépasser {0} caractère")]
        public string Summary { get; set; }

        [Display(Name = "Contenu")]
        [AllowHtml]
        public string Content { get; set; }

    }
}
