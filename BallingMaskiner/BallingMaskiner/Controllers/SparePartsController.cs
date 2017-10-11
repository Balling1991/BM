using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BallingMaskiner.Models;
using System.Data.Entity.Migrations;
using System.IO;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class SparePartsController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: SpareParts
        public ActionResult Index()
        {
            return View(db.SpareParts.AsNoTracking().ToList());
        }

        // GET: SpareParts/Details/5
        public ActionResult Details(int? id, int customerId, int machineId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SparePart sparePart = db.SpareParts.Find(id);
            if (sparePart == null)
            {
                return HttpNotFound();
            }
            SparePartViewModel model = new SparePartViewModel
            {
                Id = sparePart.Id,
                Name = sparePart.Name,
                MachineId = machineId,
                CustomerId = customerId
            };

            return View(model);
        }

        // GET: SpareParts/Create
        public ActionResult Create(int? id, int customerId)
        {
            Machine machine = db.Machines.SingleOrDefault(x => x.Id == id);

            SparePartViewModel model = new SparePartViewModel
            {
                MachineId = machine.Id,
                MachineName = machine.Name,
                CustomerId = customerId
            };

            return View(model);
        }

        // POST: SpareParts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SparePartViewModel vm)
        {
            if (ModelState.IsValid)
            {
                SparePart sparepart = new SparePart
                {
                    Name = vm.Name,
                    FileName = vm.File.FileName
                };

                FileData data = new FileData();
                using (Stream inputStream = vm.File.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    data.Data = memoryStream.ToArray();
                }

                sparepart.FileData = data;

                Machine machine = db.Machines.SingleOrDefault(x => x.Id == vm.MachineId);

                machine.SpareParts.Add(sparepart);
                db.Machines.AddOrUpdate(machine);
                db.SpareParts.Add(sparepart);

                db.SaveChanges();
                return RedirectToAction("Details", "Machines", new { Id = vm.MachineId, customerId = vm.CustomerId });
            }

            return View(vm);
        }

        // GET: SpareParts/Edit/5
        public ActionResult Edit(int? id, int customerId, int machineId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SparePart sparePart = db.SpareParts.Find(id);

            ViewBag.machines = db.Machines.ToList();

            if (sparePart == null)
            {
                return HttpNotFound();
            }

            SparePartViewModel model = new SparePartViewModel
            {
                Id = sparePart.Id,
                Name = sparePart.Name,
                MachineId = machineId,
                CustomerId = customerId
            };

            return View(model);
        }

        // POST: SpareParts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SparePartViewModel vm)
        {
            if (ModelState.IsValid)
            {
                SparePart sparepart = db.SpareParts.SingleOrDefault(x => x.Id == vm.Id);

                sparepart.Name = vm.Name;

                db.SpareParts.AddOrUpdate(sparepart);
                db.SaveChanges();
                return RedirectToAction("Details", "Machines", new { id = vm.MachineId, customerId = vm.CustomerId });
            }
            return View(vm);
        }

        // GET: SpareParts/Delete/5
        public ActionResult Delete(int? id, int customerId, int machineId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SparePart sparePart = db.SpareParts.Find(id);
            if (sparePart == null)
            {
                return HttpNotFound();
            }

            SparePartViewModel vm = new SparePartViewModel
            {
                Id = sparePart.Id,
                Name = sparePart.Name,
                CustomerId = customerId,
                MachineId = machineId
            };

            return View(vm);
        }

        // POST: SpareParts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int customerId, int machineId)
        {
            SparePart sparePart = db.SpareParts.Include("FileData").SingleOrDefault(x => x.Id == id);

            db.FileDatas.Remove(sparePart.FileData);
            db.SpareParts.Remove(sparePart);
            db.SaveChanges();
            return RedirectToAction("Details", "Machines", new { id = machineId, customerId = customerId });
        }

        public FileResult Download(int? id)
        {
            SparePart sparePart = db.SpareParts.Include("FileData").SingleOrDefault(x => x.Id == id);
            return File(sparePart.FileData.Data, System.Net.Mime.MediaTypeNames.Application.Octet, sparePart.FileName);
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
