using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EventsMVC.Models;
using EventsMVC.ViewModel;

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

        [Authorize(Roles ="Admin")]
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

        [Authorize(Roles = "Admin")]
        // GET: TIN_Wydarzenie/Create
        public ActionResult Create()
        {
            ViewBag.TIN_Kategoria_TIN_Kategoria_ID = new SelectList(db.TIN_Kategoria, "TIN_Kategoria_ID", "Nazwa");
            return View();
        }

        // POST: TIN_Wydarzenie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        // GET: TIN_Wydarzenie/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            //TIN_Wydarzenie tIN_Wydarzenie = db.TIN_Wydarzenie.Find(id);

            var wydTagViewModel = new WydTagViewModel
            {
                TIN_Wydarzenie = db.TIN_Wydarzenie.Include(i => i.TIN_Tagi).First(i => i.TIN_Wydarzenie_ID == id)
            };

            if (wydTagViewModel.TIN_Wydarzenie == null)
            {
                return HttpNotFound();
            }
            var allWydTagList = db.TIN_Tagi.ToList();
            wydTagViewModel.AllTags = allWydTagList.Select(o => new SelectListItem
            {
                Text = o.Nazwa,
                Value = o.TIN_Tagi_ID.ToString()

            });
            ViewBag.TIN_Kategoria_TIN_Kategoria_ID = new SelectList(db.TIN_Kategoria, "TIN_Kategoria_ID", "Nazwa", wydTagViewModel.TIN_Wydarzenie.TIN_Kategoria_TIN_Kategoria_ID);
            return View(wydTagViewModel);
        }

        // POST: TIN_Wydarzenie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken] //Bind(Include = "Nazwa,WiekOd,WiekDo,Cena,DataOd,DataDo,Adres,Organizator,TIN_Wydarzenie_ID,TIN_Kategoria_TIN_Kategoria_ID")]
        public ActionResult Edit(WydTagViewModel wydTagView)
        {
            if (wydTagView == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                var wydToUpdate = db.TIN_Wydarzenie
                    .Include(i => i.TIN_Tagi).First(i => i.TIN_Wydarzenie_ID == wydTagView.TIN_Wydarzenie.TIN_Wydarzenie_ID);

                if(TryUpdateModel(wydToUpdate,"TIN_Wydarzenie", new string[] { "Nazwa,WiekOd,WiekDo,Cena,DataOd,DataDo,Adres,Organizator,TIN_Wydarzenie_ID,TIN_Kategoria_TIN_Kategoria_ID"} ))
                {
                    var newWydTag = db.TIN_Tagi.Where(
                        m => wydTagView.SelectedTags.Contains(m.TIN_Tagi_ID)).ToList();
                    var updatedWydTag = new HashSet<decimal>(wydTagView.SelectedTags);
                    foreach (TIN_Tagi tin_Tagi in db.TIN_Tagi)
                    {
                        if(!updatedWydTag.Contains(tin_Tagi.TIN_Tagi_ID))
                        {
                            wydToUpdate.TIN_Tagi.Remove(tin_Tagi);
                        }
                        else
                        {
                            wydToUpdate.TIN_Tagi.Add((tin_Tagi));
                        }
                    }
                   /* var updatedUser = db.Users.SingleOrDefault(x => x.id == id);

                    updatedUser.FirstName = user.FirstName;
                    updatedUser.LastName = user.LastName;

                    db.Entry(updatedUser).State = EntityState.Modified;
                    db.SaveChanges(); */


                    db.Entry(wydToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                }
                
                return RedirectToAction("Index");
            }
            ViewBag.TIN_Kategoria_TIN_Kategoria_ID = new SelectList(db.TIN_Kategoria, "TIN_Kategoria_ID", "Nazwa", wydTagView.TIN_Wydarzenie.TIN_Kategoria_TIN_Kategoria_ID);
            return View(wydTagView);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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
