using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace cpl_azure.Models
{
    public class Pictures
    {
        public int PictureId { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}