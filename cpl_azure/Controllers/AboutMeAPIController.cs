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
    public class AboutMeAPIController : ApiController
    {
        private cpl_azureContext db = new cpl_azureContext();

        //// GET api/AboutMeAPI
        //public IEnumerable<AboutMe> GetAboutMes()
        //{
        //    return db.AboutMes.AsEnumerable();
        //}

        // GET api/AboutMeAPI
        public AboutMe GetAboutMe()
        {
            //Return only the first biography
            AboutMe aboutme = db.AboutMes.FirstOrDefault();
            if (aboutme == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return aboutme;
        }

        // GET api/AboutMeAPI/5
        public AboutMe GetAboutMe(int id)
        {
            AboutMe aboutme = db.AboutMes.Find(id);
            if (aboutme == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return aboutme;
        }

        // PUT api/AboutMeAPI/5
        public HttpResponseMessage PutAboutMe(int id, AboutMe aboutme)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != aboutme.AboutMeId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(aboutme).State = EntityState.Modified;

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

        // POST api/AboutMeAPI
        public HttpResponseMessage PostAboutMe(AboutMe aboutme)
        {
            if (ModelState.IsValid)
            {
                db.AboutMes.Add(aboutme);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, aboutme);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = aboutme.AboutMeId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/AboutMeAPI/5
        public HttpResponseMessage DeleteAboutMe(int id)
        {
            AboutMe aboutme = db.AboutMes.Find(id);
            if (aboutme == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.AboutMes.Remove(aboutme);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, aboutme);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}