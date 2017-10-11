using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class QuotationsTemplateController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: QuotationsTemplate
        public ActionResult Index()
        {
            return View(db.Quotations.ToList());
        }

        // GET: QuotationsTemplate/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // GET: QuotationsTemplate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuotationsTemplate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,FileName,Category")] Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                db.Quotations.Add(quotation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(quotation);
        }

        // GET: QuotationsTemplate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.SingleOrDefault(x => x.Id == id);
            if (quotation == null)
            {
                return HttpNotFound();
            }

            QuotationViewModel vm = new QuotationViewModel
            {
                Id = quotation.Id,
                Name = quotation.Name,
                Category = quotation.Category,
                IsTemplate = quotation.IsTemplate,   
            };

            return View(vm);
        }

        // POST: QuotationsTemplate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuotationViewModel vm)
        {
            Quotation qt = db.Quotations.SingleOrDefault(x => x.Id == vm.Id);

            if (ModelState.IsValid && qt != null)
            {

                Quotation quotation = new Quotation
                {
                    Id = qt.Id,
                    Name = vm.Name,
                    Category = vm.Category,
                    IsTemplate = vm.IsTemplate,
                    FileName = qt.FileName,
                    FileData = qt.FileData
                            
                };

                //db.Entry(quotation).State = EntityState.Modified;
                db.Quotations.AddOrUpdate(quotation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        // GET: QuotationsTemplate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // POST: QuotationsTemplate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotation quotation = db.Quotations.Find(id);
            db.Quotations.Remove(quotation);
            db.SaveChanges();
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
