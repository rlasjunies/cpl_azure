using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cpl_azure.Controllers
{
    public class StaticFilesController : Controller
    {
        //
        // GET: /Content/Uploads
        public ActionResult StaticFileDownload(string fileUrl, string mimetype)
        {
            var filePath = Server.MapPath("~" + fileUrl);

            if (System.IO.File.Exists(filePath))
                return File(filePath, mimetype);
            else
            {
                return new HttpNotFoundResult("File not found");
            }
        }

    }
}
