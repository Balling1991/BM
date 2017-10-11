using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BallingMaskiner.Models;
using System.IO;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class ProspectsController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Prospects
        public ActionResult Index()
        {
            return View(db.Prospects.AsNoTracking().ToList());
        }

        // GET: Prospects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospect prospect = db.Prospects.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (prospect == null)
            {
                return HttpNotFound();
            }
            return View(prospect);
        }

        // GET: Prospects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prospects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind] ProspectsViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Prospect prospect = new Prospect
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

                prospect.FileData = data;

                db.Prospects.Add(prospect);
                db.SaveChanges();
                return RedirectToAction("Index", "Prospects");
            }

            return View(vm);
        }

        // GET: Prospects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospect prospect = db.Prospects.Find(id);
            if (prospect == null)
            {
                return HttpNotFound();
            }
            return View(prospect);
        }

        // POST: Prospects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id")] Prospect prospect)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prospect).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prospect);
        }

        // GET: Prospects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prospect prospect = db.Prospects.SingleOrDefault(x => x.Id == id);
            if (prospect == null)
            {
                return HttpNotFound();
            }

            ProspectsViewModel vm = new ProspectsViewModel
            {
                Id = prospect.Id,
                Name = prospect.Name,
            };

            return View(vm);
        }

        // POST: Prospects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prospect prospect = db.Prospects.Include("FileData").SingleOrDefault(x => x.Id == id);

            ProspectsViewModel vm = new ProspectsViewModel
            {
                Id = prospect.Id,
                Name = prospect.Name
            };

            db.FileDatas.Remove(prospect.FileData);
            db.Prospects.Remove(prospect);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult Download(int? id)
        {
            Prospect prospect = db.Prospects.Include("FileData").SingleOrDefault(x => x.Id == id);
            return File(prospect.FileData.Data, System.Net.Mime.MediaTypeNames.Application.Octet, prospect.FileName);
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
