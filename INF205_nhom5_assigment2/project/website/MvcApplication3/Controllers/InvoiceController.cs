using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace MvcApplication3.Controllers
{
    public class InvoiceController : Controller
    {
        private INF205_ASS1_NHOM_5Entities db = new INF205_ASS1_NHOM_5Entities();

        //
        // GET: /Invoice/

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var invoices = db.invoices.Include(i => i.customer).OrderBy(i=>i.id);
            return View(invoices.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Invoice/Details/5

        public ActionResult Details(int id = 0)
        {
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Create

        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.customers, "id", "name");
            return View();
        }

        //
        // POST: /Invoice/Create

        [HttpPost]
        public ActionResult Create(invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.customers, "id", "name", invoice.customer_id);
            return View(invoice);
        }

        //
        // GET: /Invoice/Edit/5

        public ActionResult Edit(int id = 0)
        {
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.customers, "id", "name", invoice.customer_id);
            return View(invoice);
        }

        //
        // POST: /Invoice/Edit/5

        [HttpPost]
        public ActionResult Edit(invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.customers, "id", "name", invoice.customer_id);
            return View(invoice);
        }

        //
        // GET: /Invoice/Delete/5

        public ActionResult Delete(int id = 0)
        {
            invoice invoice = db.invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        //
        // POST: /Invoice/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            invoice invoice = db.invoices.Find(id);
            db.invoices.Remove(invoice);
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