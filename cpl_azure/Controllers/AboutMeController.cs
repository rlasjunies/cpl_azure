using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cpl_azure.Models;

namespace cpl_azure.Controllers
{
    public class AboutMeController : Controller
    {
        private cpl_azureContext db = new cpl_azureContext();

        //
        // GET: /AboutMe/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.AboutMes.ToList());
        }

        //
        // GET: /AboutMe/Details/5

        public ActionResult Details(int id = 0)
        {
            AboutMe aboutme = db.AboutMes.Find(id);
            if (aboutme == null)
            {
                return HttpNotFound();
            }
            return View(aboutme);
        }

        //
        // GET: /AboutMe/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /AboutMe/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AboutMe aboutme)
        {
            if (ModelState.IsValid)
            {
                db.AboutMes.Add(aboutme);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aboutme);
        }

        //
        // GET: /AboutMe/Edit/5

        public ActionResult Edit(int id = 0)
        {
            AboutMe aboutme = db.AboutMes.Find(id);
            if (aboutme == null)
            {
                return HttpNotFound();
            }
            return View(aboutme);
        }

        //
        // POST: /AboutMe/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AboutMe aboutme)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aboutme).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aboutme);
        }

        //
        // GET: /AboutMe/Delete/5

        public ActionResult Delete(int id = 0)
        {
            AboutMe aboutme = db.AboutMes.Find(id);
            if (aboutme == null)
            {
                return HttpNotFound();
            }
            return View(aboutme);
        }

        //
        // POST: /AboutMe/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AboutMe aboutme = db.AboutMes.Find(id);
            db.AboutMes.Remove(aboutme);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}