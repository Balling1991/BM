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
    public class MachinesController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Machines
        public ActionResult Index()
        {
            return View(db.Machines.AsNoTracking().ToList());
        }

        // GET: Machines/Details/5
        public ActionResult Details(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (machine == null)
            {
                return HttpNotFound();
            }

            MachineViewModel vm = new MachineViewModel
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Name = machine.Name,
                SpareParts = machine.SpareParts,
                Services = machine.Services,
                IsSold = machine.IsSold,
                DateCreated = machine.DateCreated,
                CustomerId = customerId
            };

            return View(vm);
        }

        // GET: Machines/Create
        public ActionResult Create(int? id)
        {
            Customer customer = db.Customers.SingleOrDefault(x => x.Id == id);

            MachineViewModel model = new MachineViewModel
            {
                CustomerName = customer.Name,
                CustomerId = customer.Id
            };

            return View(model);
        }

        // POST: Machines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MachineViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Machine machine = new Machine
                {
                    DateCreated = DateTime.Now,
                    IsSold = vm.IsSold,
                    Name = vm.Name,
                    SerialNumber = vm.SerialNumber,
                };

                Customer customer = db.Customers.SingleOrDefault(x => x.Id == vm.CustomerId);
                customer.Machines.Add(machine);
                db.Customers.AddOrUpdate(customer);
                db.Machines.Add(machine);
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new {id = vm.CustomerId});
            }

            return View(vm);
        }

        // GET: Machines/Edit/5
        public ActionResult Edit(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.SingleOrDefault(x => x.Id == id);
            if (machine == null)
            {
                return HttpNotFound();
            }

            MachineViewModel vm = new MachineViewModel
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Name = machine.Name,
                IsSold = machine.IsSold,
                CustomerId = customerId
            };
            return View(vm);
        }

        // POST: Machines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MachineViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Machine machine = new Machine
                {
                    Id = vm.Id,
                    SerialNumber = vm.SerialNumber,
                    Name = vm.Name,
                    IsSold = vm.IsSold
                };

                db.Entry(machine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Machines", new { id = vm.Id, customerId = vm.CustomerId });
            }
            return View(vm);
        }

        // GET: Machines/Delete/5
        public ActionResult Delete(int? id, int customerId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Machine machine = db.Machines.SingleOrDefault(x => x.Id == id);
            if (machine == null)
            {
                return HttpNotFound();
            }

            MachineViewModel vm = new MachineViewModel
            {
                Id = machine.Id,
                SerialNumber = machine.SerialNumber,
                Name = machine.Name,
                IsSold = machine.IsSold,
                CustomerId = customerId
            };
            return View(vm);
        }

        // POST: Machines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int customerId)
        {
            Machine machine = db.Machines.Find(id);
            db.Machines.Remove(machine);
            db.SaveChanges();
            return RedirectToAction("Details", "Customers", new { id = customerId });
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
