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
    public class PaintsController : Controller
    {
        private cpl_azureContext db = new cpl_azureContext();

        //
        // GET: /Paints/

        public ActionResult Index()
        {
            return View(db.Paints.ToList());
        }

        //
        // GET: /Paints/Details/5

        public ActionResult Details(string id = null)
        {
            Paints paints = db.Paints.Find(id);
            if (paints == null)
            {
                return HttpNotFound();
            }
            return View(paints);
        }

        //
        // GET: /Paints/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Paints/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Paints paints)
        {
            if (ModelState.IsValid)
            {
                db.Paints.Add(paints);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paints);
        }

        //
        // GET: /Paints/Edit/5

        public ActionResult Edit(string id = null)
        {
            Paints paints = db.Paints.Find(id);
            if (paints == null)
            {
                return HttpNotFound();
            }
            return View(paints);
        }

        //
        // POST: /Paints/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Paints paints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paints).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paints);
        }

        //
        // GET: /Paints/Delete/5

        public ActionResult Delete(string id = null)
        {
            Paints paints = db.Paints.Find(id);
            if (paints == null)
            {
                return HttpNotFound();
            }
            return View(paints);
        }

        //
        // POST: /Paints/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Paints paints = db.Paints.Find(id);
            db.Paints.Remove(paints);
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