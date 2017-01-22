using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventsMVC.Models;

namespace EventsMVC.Controllers
{
    public class TIN_TagiController : Controller
    {
        private s12667Entities db = new s12667Entities();

        [Authorize(Roles = "Admin")]
        // GET: TIN_Tagi
        public ActionResult Index()
        {
            return View(db.TIN_Tagi.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: TIN_Tagi/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Tagi tIN_Tagi = db.TIN_Tagi.Find(id);
            if (tIN_Tagi == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Tagi);
        }

        [Authorize(Roles = "Admin")]
        // GET: TIN_Tagi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIN_Tagi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nazwa,TIN_Tagi_ID")] TIN_Tagi tIN_Tagi)
        {
            if (ModelState.IsValid)
            {
                db.TIN_Tagi.Add(tIN_Tagi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIN_Tagi);
        }

        // GET: TIN_Tagi/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Tagi tIN_Tagi = db.TIN_Tagi.Find(id);
            if (tIN_Tagi == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Tagi);
        }

        // POST: TIN_Tagi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nazwa,TIN_Tagi_ID")] TIN_Tagi tIN_Tagi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIN_Tagi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIN_Tagi);
        }

        // GET: TIN_Tagi/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Tagi tIN_Tagi = db.TIN_Tagi.Find(id);
            if (tIN_Tagi == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Tagi);
        }

        // POST: TIN_Tagi/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TIN_Tagi tIN_Tagi = db.TIN_Tagi.Find(id);
            db.TIN_Tagi.Remove(tIN_Tagi);
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
