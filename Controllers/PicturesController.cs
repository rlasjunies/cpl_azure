using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using cpl_azure.Models;
using MvcFileUploader;
using MvcFileUploader.Models;
using System;

namespace cpl_azure.Controllers
{
    public class PicturesController : Controller
    {
        private cpl_azureContext db = new cpl_azureContext();

        //
        // GET: /Pictures/
        [Authorize(Roles = "picturesAll")]
        public ActionResult Index()
        {
            return View(db.Pictures.ToList());
        }

        //
        // GET: /Pictures/Details/5
        [Authorize(Roles = "picturesAll")]
        public ActionResult Details(int id)
        {
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        //
        // GET: /Pictures/Create
        [Authorize(Roles = "picturesAll")]
        public ActionResult Create()
        {
            return View();
        }

        ////
        //// POST: /Pictures/Create
        //[Authorize(Roles = "picturesAll")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Pictures pictures)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Pictures.Add(pictures);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(pictures);
        //}

        //
        // GET: /Pictures/Edit/5
        [Authorize(Roles = "picturesAll")]
        public ActionResult Edit(int id)
        {
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        //
        // POST: /Pictures/Edit/5
        [Authorize(Roles = "picturesAll")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pictures pictures)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pictures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pictures);
        }

        //
        // GET: /Pictures/Delete/5
        [Authorize(Roles = "picturesAll")]
        public ActionResult Delete(int id)
        {
            Pictures pictures = db.Pictures.Find(id);
            if (pictures == null)
            {
                return HttpNotFound();
            }
            return View(pictures);
        }

        //
        // POST: /Pictures/Delete/5
        [Authorize(Roles = "picturesAll")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Pictures pictures = db.Pictures.Find(id);
            //db.Pictures.Remove(pictures);
            //db.SaveChanges();
            this.DeleteFileAndRecord(id);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "picturesAll")]
        public ActionResult UploadFile(int? entityId) // optionally receive values specified with Html helper
        {                      
            //Guid id = 
            
            // here we can send in some extra info to be included with the delete url 
            var statuses = new List<ViewDataUploadFileResult>();
            for (var i = 0; i < Request.Files.Count; i++)
            {
                var st = FileSaver.StoreFile(x =>
                {
                    x.File = Request.Files[i];
                    //note how we are adding an additional value to be posted with delete request
                    //and giving it the same value posted with upload
                    x.DeleteUrl = Url.Action("DeleteFile", new { entityId = entityId });
                    x.StorageDirectory = Server.MapPath("~/Content/uploads");
                    x.UrlPrefix = "/Content/uploads";


                    //overriding defaults
                    //x.FileName = Guid.NewGuid().ToString(); //Request.Files[i].FileName;// default is filename suffixed with filetimestamp
                    //x.FileName = Guid.NewGuid().ToString() + Request.Files[i].ContentType;
                    x.ThrowExceptions = true;//default is false, if false exception message is set in error property
                });

                if (st != null)
                {
                    statuses.Add(st);
                }
            }

            //statuses contains all the uploaded files details (if error occurs then check error property is not null or empty)
            //todo: add additional code to generate thumbnail for videos, associate files with entities etc

            //adding thumbnail url for jquery file upload javascript plugin
            statuses.ForEach(x => x.thumbnail_url = x.url + "?width=80&height=80"); // uses ImageResizer httpmodule to resize images from this url

            //setting custom download url instead of direct url to file which is default
            //statuses.ForEach(x => x.url = Url.Action("DownloadFile", new { fileUrl = x.url, mimetype = x.type }));

            foreach ( var status in statuses)
            {

                if (ModelState.IsValid && status.size != 0)
                {
                    var picture = new Models.Pictures()
                    {
                        //PictureId = status.SavedFileName,
                        Name = status.name,
                        delete_url = status.delete_url,
                        savedFileName = status.SavedFileName,
                        Size = status.size,
                        thumbnail_url = status.thumbnail_url,
                        url = status.url,
                        FullFileName = status.FullPath
                    };
                    db.Pictures.Add(picture);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                }
            }

            //return Json(new { files = statuses });
            //return RedirectToAction("Index");           ko
            //return null;                                ko
            //return RedirectToAction("/admin/pictures"); ko
            //return View("Index", db.Pictures.ToList());  ko
            //return Redirect("~/admin/pictures/create"); ok

            string acceptHeader = Request.Headers["Accept"];
            if (string.IsNullOrEmpty(acceptHeader) || !acceptHeader.Contains("application/json"))
            {
                return Json(new { file = statuses }, "text/plain");
            }
            else
            {
                return Json(new { files = statuses } );
            }

        }

        //here i am receving the extra info injected
        [HttpPost] // should accept only post
        [Authorize(Roles = "picturesAll")]
        public ActionResult DeleteFile(int? entityId, string fileUrl)
        {
            //var filePath = Server.MapPath("~" + fileUrl);

            //if (System.IO.File.Exists(filePath))
            //    System.IO.File.Delete(filePath);
            Pictures picture = db.Pictures.Where(p => p.url == fileUrl).SingleOrDefault();
            //Pictures picture = db.Pictures.Find(Id);
            if (picture != null)
            {
                return this.DeleteFileAndRecord(picture.PictureId);
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "picturesAll")]
        private ActionResult DeleteFileAndRecord(int Id)
        {

            // delete the record
            //Pictures picture = db.Pictures.Where(p => p.savedFileName == Id).SingleOrDefault();
            Pictures picture = db.Pictures.Find(Id);
            if (picture != null)
            {

                //remove the db record
                db.Pictures.Remove(picture);
                db.SaveChanges();

                //delete the file
                var filePath = Server.MapPath("~/Content/uploads/" + picture.savedFileName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
                }

                return new HttpStatusCodeResult(200); // trigger success
            }
            else
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
            }
            
        }

        [Authorize(Roles = "picturesAll")]
        public ActionResult DownloadFile(string fileUrl, string mimetype)
        {
            var filePath = Server.MapPath("~" + fileUrl);

            if (System.IO.File.Exists(filePath))
                return File(filePath, mimetype);
            else
            {
                return new HttpNotFoundResult("File not found");
            }
        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}