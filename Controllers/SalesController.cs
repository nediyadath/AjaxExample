using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCAjaxExample.Models;

namespace MVCAjaxExample.Controllers
{
    public class SalesController : Controller
    {
        private EcomContext db = new EcomContext();

        // GET: Sales
        public ActionResult Index()
        {
            var sale = db.sale.Include(s => s.prod);
            return View(sale.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.prodID = new SelectList(db.product, "id", "name");
            return View();
        }

        public JsonResult UpdateQty(int prodID, float quantity)
        {
            string msg = "Success";
            Inventory p = db.inv.Single(x => x.prodID == prodID);
            float q = p.qty-quantity;
            p.qty = q;
            try
            {
                db.Entry(p).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch
            {
                msg = "Something went wrong please try again";
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetPrice(int prodID)
        {
            float price= db.product.Single(x => x.id == prodID).price;
            return Json(price, JsonRequestBehavior.AllowGet);

        }
        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,SalesID,prodID,qty,price")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.sale.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.prodID = new SelectList(db.product, "id", "name", sale.prodID);
            return View(sale);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.prodID = new SelectList(db.product, "id", "name", sale.prodID);
            return View(sale);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,SalesID,prodID,qty,price")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sale).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.prodID = new SelectList(db.product, "id", "name", sale.prodID);
            return View(sale);
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.sale.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.sale.Find(id);
            db.sale.Remove(sale);
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
