using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BallingMaskiner.Models;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class VisitsController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Visits
        public ActionResult Index()
        {
            return View(db.Visits.ToList());
        }

        // GET: Visits/Details/5
        public ActionResult Details(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visits = db.Visits.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (visits == null)
            {
                return HttpNotFound();
            }

            VisitsViewModel vm = new VisitsViewModel
            {
                Id = visits.Id,
                ContactPerson = visits.ContactPerson,
                Comment = visits.Comment,
                Date = visits.Date,
                NextAppointment = visits.NextAppointment,
                CustomerId = customerId
            };

            return View(vm);
        }

        // GET: Visits/Create
        public ActionResult Create(int? id)
        {
            Customer customer = db.Customers.SingleOrDefault(x => x.Id == id);

            VisitsViewModel vm = new VisitsViewModel
            {
                CustomerName = customer.Name,
                CustomerId = customer.Id
            };

            return View(vm);
        }

        // POST: Visits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VisitsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Visits visit = new Visits
                {
                    ContactPerson = vm.ContactPerson,
                    Comment = vm.Comment,
                    Date = vm.Date,
                    NextAppointment = vm.NextAppointment
                };

                Customer customer = db.Customers.SingleOrDefault(x => x.Id == vm.CustomerId);
                customer.Visits.Add(visit);
                db.Customers.AddOrUpdate(customer);
                db.Visits.Add(visit);
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new {id = vm.CustomerId});
            }

            return View(vm);
        }

        // GET: Visits/Edit/5
        public ActionResult Edit(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visits = db.Visits.SingleOrDefault(x => x.Id == id);
            if (visits == null)
            {
                return HttpNotFound();
            }

            VisitsViewModel vm = new VisitsViewModel
            {
                Id = visits.Id,
                ContactPerson = visits.ContactPerson,
                Comment = visits.Comment,
                Date = visits.Date,
                NextAppointment = visits.NextAppointment,
                CustomerId = customerId
            };

            return View(vm);
        }

        // POST: Visits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VisitsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Visits visit = new Visits
                {
                    Id = vm.Id,
                    ContactPerson = vm.ContactPerson,
                    Comment = vm.Comment,
                    Date = vm.Date,
                    NextAppointment = vm.NextAppointment
                };

                db.Entry(visit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new { id = vm.CustomerId });
            }
            return View(vm);
        }

        // GET: Visits/Delete/5
        public ActionResult Delete(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visits visits = db.Visits.SingleOrDefault(x => x.Id == id);
            if (visits == null)
            {
                return HttpNotFound();
            }

            VisitsViewModel vm = new VisitsViewModel
            {
                Id = visits.Id,
                ContactPerson = visits.ContactPerson,
                Comment = visits.Comment,
                Date = visits.Date,
                NextAppointment = visits.NextAppointment,
                CustomerId = customerId
            };

            return View(vm);
        }

        // POST: Visits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int customerId)
        {
            Visits visits = db.Visits.Find(id);
            db.Visits.Remove(visits);
            db.SaveChanges();
            return RedirectToAction("Details", "Customers", new { id = customerId});
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
