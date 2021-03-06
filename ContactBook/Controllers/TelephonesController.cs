﻿using System;
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

        // GET: Telephones for each Contact
        public ActionResult Index(string searching)
        {
            return View(searching == null ? data.GetPhones() : data.GetPhones().Where(x => x.contact.FirstName.Contains(searching) || x.contact.LastName.Contains(searching)).ToList());
        }

        // POST: Telephones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Telephone telephone)
        {
            if (ModelState.IsValid)
            {
                data.CreatePhone(telephone);
                return RedirectToAction("Index");
            }
            TempData["ErrorMessage"] = "Phone number is not valid";
            return RedirectToAction("Index", "Contacts");
        }

        // POST: Telephones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Telephone telephone)
        {
            if (ModelState.IsValid)
            {
                db.Entry(telephone).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            TempData["Message"] = "Phone number is not valid";
            return RedirectToAction("Index");
        }

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
