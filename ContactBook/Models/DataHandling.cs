using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactBook.Models
{
    public class DataHandling
    {
        public bool CreateContact(Contact user)
        {
            using (var Db = new ApplicationDbContext())
            {
                Db.Contacts.Add(user);
                return SaveDbChanges(Db);
            }
        }

        public bool CreatePhone(Telephone phone)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Telephones.Add(phone);
                return SaveDbChanges(db);
            }
        }

        public bool DeleteContact(int deletedUser)
        {
            using (var db = new ApplicationDbContext())
            {
                var DeletingPhones = db.Telephones.Where(i => i.contact.ContactId == deletedUser).ToList();
                db.Telephones.RemoveRange(DeletingPhones);
                db.Contacts.Remove(db.Contacts.FirstOrDefault(u => u.ContactId == deletedUser));
                return SaveDbChanges(db);
            }
        }

        public bool DeletePhone(int deletedPhone)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Telephones.Remove(db.Telephones.Single(m => m.TelephoneId == deletedPhone));
                return SaveDbChanges(db);
            }
        }

        public IEnumerable<Telephone> GetPhones()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Telephones
                    .Include(t => t.contact)                   
                    .ToList();
            }
        }

        public IEnumerable<Contact> GetContacts()
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Contacts
                    .Include(c => c.Telephones)
                    .ToList();
            }
        }

        public bool UpdateContact(Contact user)
        {
            using (var db = new ApplicationDbContext())
            {
                var toUpdate = db.Contacts.Single(u => u.ContactId == user.ContactId);
                toUpdate.FirstName = user.FirstName;
                toUpdate.LastName = user.LastName;
                toUpdate.Email = user.Email;
                toUpdate.Address = user.Address;
                return SaveDbChanges(db);
            }
        }

        public bool EmailExists(string mail)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Contacts.Any(u => u.Email == mail);
            }
        }

        public bool SaveDbChanges(ApplicationDbContext db)
        {
            return db.SaveChanges() > 0;                      
        }
    }
}