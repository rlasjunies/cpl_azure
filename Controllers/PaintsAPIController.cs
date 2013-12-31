using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using cpl_azure.Models;

namespace cpl_azure.Controllers
{
    public class PaintsAPIController : ApiController
    {
        private cpl_azureContext db = new cpl_azureContext();

        // GET api/PaintsAPI
        public IEnumerable<Paints> GetPaints()
        {
            return db.Paints.AsEnumerable();
        }

        // GET api/PaintsAPI/5
        public Paints GetPaints(int id)
        {
            Paints paints = db.Paints.Find(id);
            if (paints == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return paints;
        }

        // PUT api/PaintsAPI/5
        public HttpResponseMessage PutPaints(int id, Paints paints)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != paints.PaintId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(paints).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/PaintsAPI
        public HttpResponseMessage PostPaints(Paints paints)
        {
            if (ModelState.IsValid)
            {
                db.Paints.Add(paints);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, paints);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = paints.PaintId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/PaintsAPI/5
        public HttpResponseMessage DeletePaints(int id)
        {
            Paints paints = db.Paints.Find(id);
            if (paints == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Paints.Remove(paints);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, paints);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}