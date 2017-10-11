using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BallingMaskiner.Models;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class QuotationsController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Quotations
        public ActionResult Index()
        {
            return View(db.Quotations.AsNoTracking().ToList());
        }

        // GET: Quotations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // GET: Quotations/Create
        public ActionResult Create(int? id)
        {
            Customer customer = db.Customers.SingleOrDefault(x => x.Id == id);

            QuotationViewModel model =  new QuotationViewModel();
            model.CustomerId = customer.Id;
            model.CustomerName = customer.Name;

            return View(model);
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] QuotationViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Quotation quotation = new Quotation
                {
                    Name = vm.Name,
                    FileName = vm.File.FileName,
                    Category = vm.Category,
                    IsTemplate = vm.IsTemplate
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

                quotation.FileData = data;

                Customer customer = db.Customers.SingleOrDefault(x => x.Id == vm.CustomerId);

                customer.Quotations.Add(quotation);
                db.Customers.AddOrUpdate(customer);
                db.Quotations.Add(quotation);
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new {id = vm.CustomerId});
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
            Quotation quotation = db.Quotations.SingleOrDefault(x => x.Id == id);

            if (quotation == null)
            {
                return HttpNotFound();
            }

            QuotationViewModel vm = new QuotationViewModel()
            {
                Name = quotation.Name,
                Category = quotation.Category,
                IsTemplate = quotation.IsTemplate,
                CustomerId = customerId
            };

            return View(vm);
        }

        // POST: Contacts/Edit/5
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

                db.Quotations.AddOrUpdate(quotation);
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new { id = vm.CustomerId });
            }
            return View(vm);
        }

        // GET: Quotations/Delete/5
        public ActionResult Delete(int? id, int customerId)
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
                CustomerId = customerId
            };

            return View(vm);
        }

        // POST: Quotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int customerId)
        {
            Quotation quotation = db.Quotations.Include("FileData").SingleOrDefault(x => x.Id == id);

            QuotationViewModel vm = new QuotationViewModel
            {
                Id = quotation.Id,
                Name = quotation.Name,
                CustomerId = customerId
            };

            db.FileDatas.Remove(quotation.FileData);
            db.Quotations.Remove(quotation);
            db.SaveChanges();
            return RedirectToAction("Details", "Customers", new { id = customerId });
        }

        public FileResult Download(int? id)
        {
            Quotation quotation = db.Quotations.Include("FileData").SingleOrDefault(x => x.Id == id);
            return File(quotation.FileData.Data, System.Net.Mime.MediaTypeNames.Application.Octet, quotation.FileName);
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
