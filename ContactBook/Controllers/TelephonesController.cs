using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ContactBook.Models;

namespace ContactBook.Controllers
{
    public class TelephonesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        DataHandling data = new DataHandling();

        // GET: Telephones
        public ActionResult Index()
        {
            var telephones = data.GetPhones();
            return View(telephones);
        }

        //// GET: Telephones/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Telephone telephone = db.Telephones.Find(id);
        //    if (telephone == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(telephone);
        //}

        // GET: Telephones/Create
        public ActionResult Create()
        {           
            return View();
        }

        // POST: Telephones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Telephone telephone)
        {
            if (ModelState.IsValid)
            {
                data.CreatePhone(telephone);
                return RedirectToAction("Index", new { id = telephone.contact.ContactId });
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
        }

        //// GET: Telephones/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Telephone telephone = db.Telephones.Find(id);
        //    if (telephone == null)
        //    {
        //        return HttpNotFound();
        //    }         
        //    return View(telephone);
        //}

        //// POST: Telephones/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Telephone telephone)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(telephone).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ContactId = new SelectList(db.Contacts, "ContactId", "FirstName", telephone.ContactID);
        //    return View(telephone);
        //}

        //// GET: Telephones/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Telephone telephone = db.Telephones.Find(id);
        //    if (telephone == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(telephone);
        //}

        //// POST: Telephones/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            data.DeletePhone(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
