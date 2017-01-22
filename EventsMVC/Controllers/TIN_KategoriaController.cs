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
    public class TIN_KategoriaController : Controller
    {
        private s12667Entities db = new s12667Entities();

        [Authorize(Roles="Admin")]
        // GET: TIN_Kategoria
        public ActionResult Index()
        {
            return View(db.TIN_Kategoria.ToList());
        }

        [Authorize(Roles = "Admin")]
        // GET: TIN_Kategoria/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Kategoria tIN_Kategoria = db.TIN_Kategoria.Find(id);
            if (tIN_Kategoria == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Kategoria);
        }

        [Authorize(Roles = "Admin")]
        // GET: TIN_Kategoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TIN_Kategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nazwa,TIN_Kategoria_ID")] TIN_Kategoria tIN_Kategoria)
        {
            if (ModelState.IsValid)
            {
                db.TIN_Kategoria.Add(tIN_Kategoria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tIN_Kategoria);
        }

        [Authorize(Roles = "Admin")]
        // GET: TIN_Kategoria/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Kategoria tIN_Kategoria = db.TIN_Kategoria.Find(id);
            if (tIN_Kategoria == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Kategoria);
        }

        // POST: TIN_Kategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nazwa,TIN_Kategoria_ID")] TIN_Kategoria tIN_Kategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tIN_Kategoria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tIN_Kategoria);
        }

        [Authorize(Roles = "Admin")]
        // GET: TIN_Kategoria/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TIN_Kategoria tIN_Kategoria = db.TIN_Kategoria.Find(id);
            if (tIN_Kategoria == null)
            {
                return HttpNotFound();
            }
            return View(tIN_Kategoria);
        }

        [Authorize(Roles = "Admin")]
        // POST: TIN_Kategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            TIN_Kategoria tIN_Kategoria = db.TIN_Kategoria.Find(id);
            db.TIN_Kategoria.Remove(tIN_Kategoria);
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
