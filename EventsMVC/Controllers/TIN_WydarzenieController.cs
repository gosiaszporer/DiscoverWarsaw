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
    public class TIN_WydarzenieController : Controller
    {
        private s12667Entities db = new s12667Entities();

        // GET: TIN_Wydarzenie
        public ActionResult Index()
        {
            var tIN_Wydarzenie = db.TIN_Wydarzenie.Include(t => t.TIN_Kategoria);
            return View(tIN_Wydarzenie.ToList());
        }

        // GET: TIN_Wydarzenie/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Wydarzenie tIN_Wydarzenie = db.TIN_Wydarzenie.Find(id);
            if (tIN_Wydarzenie == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Wydarzenie);
        }

        // GET: TIN_Wydarzenie/Create
        public ActionResult Create()
        {
            ViewBag.TIN_Kategoria_TIN_Kategoria_ID = new SelectList(db.TIN_Kategoria, "TIN_Kategoria_ID", "Nazwa");
            return View();
        }

        // POST: TIN_Wydarzenie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nazwa,WiekOd,WiekDo,Cena,DataOd,DataDo,Adres,Organizator,TIN_Wydarzenie_ID,TIN_Kategoria_TIN_Kategoria_ID")] TIN_Wydarzenie tIN_Wydarzenie)
        {
            if (ModelState.IsValid)
            {
                db.TIN_Wydarzenie.Add(tIN_Wydarzenie);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TIN_Kategoria_TIN_Kategoria_ID = new SelectList(db.TIN_Kategoria, "TIN_Kategoria_ID", "Nazwa", tIN_Wydarzenie.TIN_Kategoria_TIN_Kategoria_ID);
            return View(tIN_Wydarzenie);
        }

        // GET: TIN_Wydarzenie/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Wydarzenie tIN_Wydarzenie = db.TIN_Wydarzenie.Find(id);
            if (tIN_Wydarzenie == null)
            {
                return HttpNotFound();
            }
            ViewBag.TIN_Kategoria_TIN_Kategoria_ID = new SelectList(db.TIN_Kategoria, "TIN_Kategoria_ID", "Nazwa", tIN_Wydarzenie.TIN_Kategoria_TIN_Kategoria_ID);
            return View(tIN_Wydarzenie);
        }

        // POST: TIN_Wydarzenie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nazwa,WiekOd,WiekDo,Cena,DataOd,DataDo,Adres,Organizator,TIN_Wydarzenie_ID,TIN_Kategoria_TIN_Kategoria_ID")] TIN_Wydarzenie tIN_Wydarzenie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIN_Wydarzenie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TIN_Kategoria_TIN_Kategoria_ID = new SelectList(db.TIN_Kategoria, "TIN_Kategoria_ID", "Nazwa", tIN_Wydarzenie.TIN_Kategoria_TIN_Kategoria_ID);
            return View(tIN_Wydarzenie);
        }

        // GET: TIN_Wydarzenie/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Wydarzenie tIN_Wydarzenie = db.TIN_Wydarzenie.Find(id);
            if (tIN_Wydarzenie == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Wydarzenie);
        }

        // POST: TIN_Wydarzenie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TIN_Wydarzenie tIN_Wydarzenie = db.TIN_Wydarzenie.Find(id);
            db.TIN_Wydarzenie.Remove(tIN_Wydarzenie);
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
