using System.Collections.Generic;

namespace MvcFileUploader.Models
{
    public class FileUploadViewModel
    {        
        public string ReturnUrl { get; set; }
        public bool ReturnOnClose
        {
            get { return "#".Equals(ReturnUrl); }
        }
        public string FileTypes { get; set; } // jquery-image-upload format expression string
        public long MaxFileSizeInBytes { get; set; }
        public string UploadUrl { get; set; }

        public bool ShowPopUpClose { get; set; }
        public bool RenderSharedScript { get; set; }
     

        public UploadUI UIStyle { get; set; }

        public IDictionary<string, string> PostValuesWithUpload { get; set; } 
    }

    public enum UploadUI
    {
        Bootstrap,
        JQueryUI
    }
}