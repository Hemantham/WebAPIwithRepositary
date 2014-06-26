using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWorking.Models;
using PagedList;

namespace MvcWorking.Controllers
{
    public class ProductsController : Controller
    {
        private APIContext db = new APIContext();

        //
        // GET: /Products/

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var products = db.Products.Include(p => p.Category);        

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DescSortParm = sortOrder == "Desc" ? "desc_desc" : "Desc";
            ViewBag.CurrentSort = sortOrder;
            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Desc":
                    products = products.OrderBy(s => s.Description);
                    break;
                case "desc_desc":
                    products = products.OrderByDescending(s => s.Description);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
           

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));

        }

        //
        // GET: /Products/Details/5

        public ActionResult Details(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Products/Create

        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategryName");
            return View();
        }

        //
        // POST: /Products/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategryName", product.CategoryID);
            return View(product);
        }

        //
        // GET: /Products/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategryName", product.CategoryID);
            return View(product);
        }

        //
        // POST: /Products/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "CategryName", product.CategoryID);
            return View(product);
        }

        //
        // GET: /Products/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Products/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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