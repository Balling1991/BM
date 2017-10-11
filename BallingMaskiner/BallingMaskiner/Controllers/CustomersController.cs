using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using BallingMaskiner.Models;
using PagedList;

namespace BallingMaskiner.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private IntraDbContext db = new IntraDbContext();

        // GET: Customers
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var customers = from s in db.Customers select s;

            ViewBag.CurrentSort = sortOrder;
            ViewBag.Search = searchString;
            ViewBag.Customers = customers;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }


            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(s => s.Cvr.Contains(searchString) 
                                       || s.Name.Contains(searchString)
                                       || s.Address.Contains(searchString)
                                       || s.City.Contains(searchString)
                                       || s.PostalCode.Contains(searchString)
                                       || s.PhoneNumber.Contains(searchString)
                                       || s.Email.Contains(searchString)
                                       || s.Homepage.Contains(searchString));
            }

            ViewBag.CvrSort = sortOrder == "Cvr" ? "cvr_desc" : "Cvr";
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AddressSort = sortOrder == "Address" ? "address_desc" : "Address";
            ViewBag.CitySort = sortOrder == "City" ? "city_desc" : "City";
            ViewBag.PostalCodeSort = sortOrder == "PostalCode" ? "postalcode_desc" : "PostalCode";
            ViewBag.PhoneNumberSort = sortOrder == "PhoneNumber" ? "phonenumber_desc" : "PhoneNumber";
            ViewBag.EmailSort = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.HomepageSort = sortOrder == "Homepage" ? "homepage_desc" : "Homepage";

            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "cvr_desc":
                    customers = customers.OrderByDescending(s => s.Cvr);
                    break;
                case "Cvr":
                    customers = customers.OrderBy(s => s.Cvr);
                    break;
                case "name_desc":
                    customers = customers.OrderByDescending(s => s.Name);
                    break;
                case "Address":
                    customers = customers.OrderBy(s => s.Address);
                    break;
                case "address_desc":
                    customers = customers.OrderByDescending(s => s.Address);
                    break;
                case "City":
                    customers = customers.OrderBy(s => s.City);
                    break;
                case "city_desc":
                    customers = customers.OrderByDescending(s => s.City);
                    break;
                case "PostalCode":
                    customers = customers.OrderBy(s => s.PostalCode);
                    break;
                case "postalcode_desc":
                    customers = customers.OrderByDescending(s => s.PostalCode);
                    break;
                case "PhoneNumber":
                    customers = customers.OrderBy(s => s.PhoneNumber);
                    break;
                case "phonenumber_desc":
                    customers = customers.OrderByDescending(s => s.PhoneNumber);
                    break;
                case "Email":
                    customers = customers.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    customers = customers.OrderByDescending(s => s.Email);
                    break;
                case "Homepage":
                    customers = customers.OrderBy(s => s.Homepage);
                    break;
                case "homepage_desc":
                    customers = customers.OrderByDescending(s => s.Homepage);
                    break;
                default:
                    customers = customers.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(customers.ToPagedList(pageNumber, pageSize));
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.AsNoTracking().SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Address,PhoneNumber,Email,Homepage,PostalCode,City,Cvr")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(customer.Cvr))
                {
                    if (!CheckCvr(customer.Cvr, customer.Id))
                    {
                        ModelState.AddModelError("Cvr", "Cvr findes allerede");
                        return View(customer);
                    }
                }

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(customer.Cvr))
                {
                    if (!CheckCvr(customer.Cvr, customer.Id))
                    {
                        ModelState.AddModelError("Cvr", "Cvr findes allerede");
                        return View(customer);
                    }
                }
                db.Customers.AddOrUpdate(customer);
                db.SaveChanges();
                return RedirectToAction("Details", "Customers", new { id = customer.Id });
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Include("Quotations").Include("Machines").SingleOrDefault(x => x.Id == id);

            if (customer != null)
            {
                foreach (Quotation quotation in customer.Quotations.ToList())
                {
                    db.FileDatas.Remove(quotation.FileData);
                }

                foreach (SparePart sparePart in customer.Machines.ToList().SelectMany(machine => machine.SpareParts.ToList()))
                {
                    db.FileDatas.Remove(sparePart.FileData);
                }

                db.Customers.Remove(customer);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public bool CheckCvr(string cvr, int id)
        {
            Customer tag = db.Customers.SingleOrDefault(m => m.Cvr == cvr);

            if (tag == null)
            {
                return true;
            }

            if (tag.Id == id)
            {
                return true;
            }

            return false;
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
