using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebApplication2.Models;
using WebApplication2.Database;

namespace WebApplication2.Controllers
{
    public class ProductController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //GET: List of Products
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        //POST: Create a Product
        [HttpPost]
        public ActionResult Create([Bind(Include = "ProductName, Brand, Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Products.Add(product);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    Console.WriteLine($"Error saving product: {ex.Message}");
                }
            }

            return View(product);
        }

        public ActionResult Edit(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        //PUT: Edit a Product
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ProductID, ProductName, Brand, Price")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                    Console.WriteLine($"Error editing product: {ex.Message}");
                }
            }
                
            return View(product);
        }

        //DELETE: Delete a Product
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Product product = db.Products.Find(id);
                if (product == null)
                {
                    return Json(new { success = false, message = "Product not found." });
                }

                db.Products.Remove(product);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting product: {ex.Message}");
                return Json(new { success = false, message = "Unable to delete product. Please try again." });
            }
        }

        //VIEW: View the Product
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        // Dispose: Performs application-defined tasks associated with freeing, releasing, or resetting managed resources.
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