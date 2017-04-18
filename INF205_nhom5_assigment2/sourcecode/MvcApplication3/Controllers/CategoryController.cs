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
    public class CategoryController : Controller
    {
        private INF205_ASS1_NHOM_5Entities db = new INF205_ASS1_NHOM_5Entities();

        //
        // GET: /Category/

        public ActionResult Index(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var categories = db.categories.Include(c => c.category2).OrderBy(i => i.id);
            
            return View(categories.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id = 0)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            ViewBag.parent_category_id = new SelectList(db.categories, "id", "name");
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(category category)
        {
            if (ModelState.IsValid)
            {
                db.categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.parent_category_id = new SelectList(db.categories, "id", "name", category.parent_category_id);
            return View(category);
        }

        //
        // GET: /Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            ViewBag.parent_category_id = new SelectList(db.categories, "id", "name", category.parent_category_id);
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.parent_category_id = new SelectList(db.categories, "id", "name", category.parent_category_id);
            return View(category);
        }

        //
        // GET: /Category/Delete/5

        public ActionResult Delete(int id = 0)
        {
            category category = db.categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Category/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            category category = db.categories.Find(id);
            db.categories.Remove(category);
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