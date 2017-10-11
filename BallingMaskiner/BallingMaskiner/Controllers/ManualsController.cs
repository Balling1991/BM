using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BallingMaskiner.Models;
using System.IO;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class ManualsController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Manuals
        public ActionResult Index()
        {
            return View(db.Manuals.AsNoTracking().ToList());
        }

        // GET: Manuals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manual manual = db.Manuals.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (manual == null)
            {
                return HttpNotFound();
            }
            return View(manual);
        }

        // GET: Manuals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Manuals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] ManualViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Manual manual = new Manual
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

                manual.FileData = data;

                db.Manuals.Add(manual);
                db.SaveChanges();
                return RedirectToAction("Index", "Manuals");
            }

            return View(vm);
        }

        // GET: Manuals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manual manual = db.Manuals.Find(id);
            if (manual == null)
            {
                return HttpNotFound();
            }
            return View(manual);
        }

        // POST: Manuals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Manual manual)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manual).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manual);
        }

        // GET: Manuals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manual manual = db.Manuals.SingleOrDefault(x => x.Id == id);
            if (manual == null)
            {
                return HttpNotFound();
            }

            ManualViewModel vm = new ManualViewModel
            {
                Id = manual.Id,
                Name = manual.Name,
            };
            return View(vm);
        }

        // POST: Manuals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manual manual = db.Manuals.Include("FileData").SingleOrDefault(x => x.Id == id);

            ManualViewModel vm = new ManualViewModel
            {
                Id = manual.Id,
                Name = manual.Name
            };

            db.FileDatas.Remove(manual.FileData);
            db.Manuals.Remove(manual);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult Download(int? id)
        {
            Manual manual = db.Manuals.Include("FileData").SingleOrDefault(x => x.Id == id);
            return File(manual.FileData.Data, System.Net.Mime.MediaTypeNames.Application.Octet, manual.FileName);
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
