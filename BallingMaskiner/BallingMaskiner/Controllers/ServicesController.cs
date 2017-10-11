using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BallingMaskiner.Models;
using System.IO;
using System.Data.Entity.Migrations;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Services
        public ActionResult Index()
        {
            return View(db.Services.AsNoTracking().ToList());
        }

        // GET: Services/Details/5
        public ActionResult Details(int? id, int customerId, int machineId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }
            ServiceViewModel vm = new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                MachineId = machineId,
                CustomerId = customerId
            };
            return View(vm);
        }

        // GET: Services/Create
        public ActionResult Create(int? id, int customerId)
        {
            Machine machine = db.Machines.SingleOrDefault(x => x.Id == id);

            ServiceViewModel vm = new ServiceViewModel
            {
                MachineId = machine.Id,
                MachineName = machine.Name,
                CustomerId = customerId
            };

            return View(vm);
        }

        // POST: Services/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] ServiceViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Service service = new Service
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

                service.FileData = data;

                Machine machine = db.Machines.SingleOrDefault(x => x.Id == vm.MachineId);

                machine.Services.Add(service);
                db.Machines.AddOrUpdate(machine);
                db.Services.Add(service);

                db.SaveChanges();
                return RedirectToAction("Details", "Machines", new { Id = vm.MachineId, customerId = vm.CustomerId });
            }

            return View(vm);
        }

        // GET: Services/Edit/5
        public ActionResult Edit(int? id, int customerId, int machineId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            ServiceViewModel vm = new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                MachineId = machineId,
                CustomerId = customerId
            };
            return View(vm);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Service service = db.Services.SingleOrDefault(x => x.Id == vm.Id);

                service.Name = vm.Name;

                db.Services.AddOrUpdate(service);
                db.SaveChanges();
                return RedirectToAction("Details", "Machines", new { id = vm.MachineId, customerId = vm.CustomerId });
            }
            return View(vm);
        }

        // GET: Services/Delete/5
        public ActionResult Delete(int? id, int customerId, int machineId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Service service = db.Services.SingleOrDefault(x => x.Id == id);
            if (service == null)
            {
                return HttpNotFound();
            }

            ServiceViewModel vm = new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                CustomerId = customerId,
                MachineId = machineId
            };
            return View(vm);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int customerId, int machineId)
        {
            Service service = db.Services.Include("FileData").SingleOrDefault(x => x.Id == id);

            ServiceViewModel vm = new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name
            };

            db.FileDatas.Remove(service.FileData);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Details", "Machines", new { id = machineId, customerId = customerId });
        }

        public FileResult Download(int? id)
        {
            Service service = db.Services.Include("FileData").SingleOrDefault(x => x.Id == id);
            return File(service.FileData.Data, System.Net.Mime.MediaTypeNames.Application.Octet, service.FileName);
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
