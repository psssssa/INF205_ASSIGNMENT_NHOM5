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
    public class InvoiceDetailController : Controller
    {
        private INF205_ASS1_NHOM_5Entities db = new INF205_ASS1_NHOM_5Entities();

        //
        // GET: /InvoiceDetail/

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var invoice_detail = db.invoice_detail.Include(i => i.invoice).Include(i => i.product).OrderBy(i => i.id);
            return View(invoice_detail.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /InvoiceDetail/Details/5

        public ActionResult Details(int id = 0)
        {
            invoice_detail invoice_detail = db.invoice_detail.Find(id);
            if (invoice_detail == null)
            {
                return HttpNotFound();
            }
            return View(invoice_detail);
        }

        //
        // GET: /InvoiceDetail/Create

        public ActionResult Create()
        {
            ViewBag.invoice_id = new SelectList(db.invoices, "id", "invoice_name");
            ViewBag.product_id = new SelectList(db.products, "id", "sku");
            return View();
        }

        //
        // POST: /InvoiceDetail/Create

        [HttpPost]
        public ActionResult Create(invoice_detail invoice_detail)
        {
            if (ModelState.IsValid)
            {
                db.invoice_detail.Add(invoice_detail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.invoice_id = new SelectList(db.invoices, "id", "invoice_name", invoice_detail.invoice_id);
            ViewBag.product_id = new SelectList(db.products, "id", "sku", invoice_detail.product_id);
            return View(invoice_detail);
        }

        //
        // GET: /InvoiceDetail/Edit/5

        public ActionResult Edit(int id = 0)
        {
            invoice_detail invoice_detail = db.invoice_detail.Find(id);
            if (invoice_detail == null)
            {
                return HttpNotFound();
            }
            ViewBag.invoice_id = new SelectList(db.invoices, "id", "invoice_name", invoice_detail.invoice_id);
            ViewBag.product_id = new SelectList(db.products, "id", "sku", invoice_detail.product_id);
            return View(invoice_detail);
        }

        //
        // POST: /InvoiceDetail/Edit/5

        [HttpPost]
        public ActionResult Edit(invoice_detail invoice_detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice_detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.invoice_id = new SelectList(db.invoices, "id", "invoice_name", invoice_detail.invoice_id);
            ViewBag.product_id = new SelectList(db.products, "id", "sku", invoice_detail.product_id);
            return View(invoice_detail);
        }

        //
        // GET: /InvoiceDetail/Delete/5

        public ActionResult Delete(int id = 0)
        {
            invoice_detail invoice_detail = db.invoice_detail.Find(id);
            if (invoice_detail == null)
            {
                return HttpNotFound();
            }
            return View(invoice_detail);
        }

        //
        // POST: /InvoiceDetail/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            invoice_detail invoice_detail = db.invoice_detail.Find(id);
            db.invoice_detail.Remove(invoice_detail);
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