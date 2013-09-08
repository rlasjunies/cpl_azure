using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using T4TS;

namespace cpl_azure.Models
{
    [TypeScriptInterfaceAttribute(Module = "cpl_azure")]
    public class Paints
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PaintId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        [Display(Name = "Nom")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(255, ErrorMessage = "La chaine ne pas dépasser {0} caractère")]
        public string Description { get; set; }

        [Display(Name = "Année")]
        [StringLength(4, ErrorMessage = "La chaine doit être au format YYYY", MinimumLength = 4)]
        public string Year { get; set; }

        [Display(Name = "Image")]
        [StringLength(255, ErrorMessage = "La chaine ne pas dépasser {0} caractère")]
        public string Picture { get; set; }

        [Display(Name = "Taille")]
        [StringLength(255, ErrorMessage = "La chaine ne pas dépasser {0} caractère")]
        public string Size { get; set; }

        [Display(Name = "Ordre")]
        public int Order { get; set; }

    }
}