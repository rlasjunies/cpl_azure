using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace cpl_azure.Models
{
    public class Pictures
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PictureId { get; set; }

        [Display(Name = "Nom")]
        public string Name { get; set; }
        [Display(Name = "Taille")]
        public int Size { get; set; }
        [Display(Name = "adresse")]
        public string url { get; set; }
        [Display(Name = "adresse de suppression")]
        public string delete_url { get; set; }
        [Display(Name = "adresse vignette")]
        public string thumbnail_url { get; set; }
        [Display(Name = "Nom de sauvegarde")]
        public string savedFileName { get; set; }
        [Display(Name = "Path&Name de sauvegarde")]
        public string FullFileName { get; set; }
    }
}