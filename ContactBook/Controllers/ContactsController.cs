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
    public class ContactsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        DataHandling data = new DataHandling();

        public ActionResult ViewPhones(int id)
        {
            return RedirectToAction("Index", "Telephones");
        }

        // GET: Contacts
        public ActionResult Index(string searching)
        {
            return View(searching == null ? data.GetContacts() : data.GetContacts().Where(x => x.FirstName.Contains(searching) || x.LastName.Contains(searching)).ToList());
        }

        // GET: Contacts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                bool checkMail = data.EmailExists(contact.Email);

                if (checkMail)
                {
                    ModelState.AddModelError(string.Empty, "Email already exists.");
                    return View(contact);
                }

                data.CreateContact(contact);
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: Contacts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                data.UpdateContact(contact);
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        public ActionResult Delete(int id)
        {
            data.DeleteContact(id);
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
