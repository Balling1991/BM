﻿using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BallingMaskiner.Models;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Contacts
        public ActionResult Index()
        {
            return View(db.Contacts.AsNoTracking().ToList());
        }

        // GET: Contacts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            return View(contact);
        }

        // GET: Contacts/Create
        public ActionResult Create(int? id)
        {
            var customer = db.Customers.SingleOrDefault(x => x.Id == id);
            ContactViewModel model = new ContactViewModel { CustomerId = customer.Id, CustomerName = customer.Name };
            return View(model);
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind(Include = "Id,Name,PhoneNumber,Email")]
        public ActionResult Create(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact
                {
                    Email = vm.Email,
                    Name = vm.Name,
                    PhoneNumber = vm.PhoneNumber
                };

                Customer customer = db.Customers.SingleOrDefault(x => x.Id == vm.CustomerId);
                customer.Contacts.Add(contact);
                db.Customers.AddOrUpdate(customer);
                db.Contacts.Add(contact);

                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new { id = customer.Id });
            }

            return View(vm);
        }

        // GET: Contacts/Edit/5
        public ActionResult Edit(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.SingleOrDefault(x => x.Id == id);

            if (contact == null)
            {
                return HttpNotFound();
            }

            ContactViewModel vm = new ContactViewModel
            {
                Email = contact.Email,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                CustomerId = customerId
            };

            return View(vm);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact
                {
                    Id = vm.Id,
                    Name = vm.Name,
                    PhoneNumber = vm.PhoneNumber,
                    Email = vm.Email
                };

                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new {id = vm.CustomerId});
            }
            return View(vm);
        }

        // GET: Contacts/Delete/5
        public ActionResult Delete(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.SingleOrDefault(x => x.Id == id);
            if (contact == null)
            {
                return HttpNotFound();
            }

            ContactViewModel vm = new ContactViewModel
            {
                Id = contact.Id,
                Email = contact.Email,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                CustomerId = customerId
            };

            return View(vm);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int customerId)
        {
            Contact contact = db.Contacts.Find(id);

            ContactViewModel vm = new ContactViewModel
            {
                Id = contact.Id,
                Email = contact.Email,
                Name = contact.Name,
                PhoneNumber = contact.PhoneNumber,
                CustomerId = customerId
            };

            db.Contacts.Remove(contact);
            db.SaveChanges();
            return RedirectToAction("Details", "Customers", new { id = vm.CustomerId });
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
