using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MvcApplication1.Controllers
{
    public class CustomerController : Controller
    {
        private INF205_ASS1_NHOM_5Entities db = new INF205_ASS1_NHOM_5Entities();

        //
        // GET: /Customer/

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(db.customers.OrderBy(i => i.id).ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id = 0)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        public ActionResult Create(customer customer)
        {
            if (ModelState.IsValid)
            {
                db.customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        //
        // GET: /Customer/Edit/5

        public ActionResult Edit(int id = 0)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        public ActionResult Edit(customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5

        public ActionResult Delete(int id = 0)
        {
            customer customer = db.customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            customer customer = db.customers.Find(id);
            var invoicesd = db.invoices.Where(i => i.customer_id == id).ToList();
            foreach (invoice detail1 in invoicesd)
            {
                var detail = db.invoice_detail.Where(i => i.invoice_id == detail1.id).ToList();
                foreach (invoice_detail detail2 in detail)
                {
                    db.invoice_detail.Remove(detail2);
                    db.SaveChanges();
                }
                db.invoices.Remove(detail1);
                db.SaveChanges();
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}