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
    public class TIN_OpiniaController : Controller
    {
        private s12667Entities db = new s12667Entities();
        private static int globalCount;

        // GET: TIN_Opinia
        public ActionResult Index()
        {
            var tIN_Opinia = db.TIN_Opinia.Include(t => t.TIN_Wydarzenie);
            return View(tIN_Opinia.ToList());
        }

        // GET: TIN_Opinia/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Opinia tIN_Opinia = db.TIN_Opinia.Find(id);
            if (tIN_Opinia == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Opinia);
        }

        // GET: TIN_Opinia/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TIN_Wydarzenie_TIN_Wydarzenie_ID = new SelectList(db.TIN_Wydarzenie, "TIN_Wydarzenie_ID", "Nazwa");
            return View();
        }

        // POST: TIN_Opinia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TIN_Opinia_ID,Tresc,TIN_Wydarzenie_TIN_Wydarzenie_ID")] TIN_Opinia tIN_Opinia)
        {
            tIN_Opinia.TIN_Opinia_ID = ++globalCount;

            if (ModelState.IsValid)
            {
                db.TIN_Opinia.Add(tIN_Opinia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TIN_Wydarzenie_TIN_Wydarzenie_ID = new SelectList(db.TIN_Wydarzenie, "TIN_Wydarzenie_ID", "Nazwa", tIN_Opinia.TIN_Wydarzenie_TIN_Wydarzenie_ID);
            return View(tIN_Opinia);
        }

        // GET: TIN_Opinia/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Opinia tIN_Opinia = db.TIN_Opinia.Find(id);
            if (tIN_Opinia == null)
            {
                return HttpNotFound();
            }
            ViewBag.TIN_Wydarzenie_TIN_Wydarzenie_ID = new SelectList(db.TIN_Wydarzenie, "TIN_Wydarzenie_ID", "Nazwa", tIN_Opinia.TIN_Wydarzenie_TIN_Wydarzenie_ID);
            return View(tIN_Opinia);
        }

        // POST: TIN_Opinia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TIN_Opinia_ID,Tresc,TIN_Wydarzenie_TIN_Wydarzenie_ID")] TIN_Opinia tIN_Opinia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIN_Opinia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TIN_Wydarzenie_TIN_Wydarzenie_ID = new SelectList(db.TIN_Wydarzenie, "TIN_Wydarzenie_ID", "Nazwa", tIN_Opinia.TIN_Wydarzenie_TIN_Wydarzenie_ID);
            return View(tIN_Opinia);
        }

        // GET: TIN_Opinia/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Opinia tIN_Opinia = db.TIN_Opinia.Find(id);
            if (tIN_Opinia == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Opinia);
        }

        // POST: TIN_Opinia/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TIN_Opinia tIN_Opinia = db.TIN_Opinia.Find(id);
            db.TIN_Opinia.Remove(tIN_Opinia);
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
