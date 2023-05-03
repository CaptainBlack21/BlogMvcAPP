using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BlogMvcAPP.Models;

namespace BlogMvcAPP.Controllers
{
    public class CatagoryController : Controller
    {
        private BlogContext db = new BlogContext();
        
        public PartialViewResult katagorilistesi()
        {
            return PartialView(db.Katagoriler.ToList());
        }
        // GET: Catagory
        public ActionResult Index()
        {
            var katagoriler = db.
                Katagoriler.Select(i => new CatagoryModel()
                {
                    Id= i.Id,
                    KatagoriAdi=i.KatagoriAdi,
                    BlogSayisi=i.Bloglar.Count(),
                });

            return View(katagoriler.ToList());
        }

        // GET: Catagory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catagory catagory = db.Katagoriler.Find(id);
            if (catagory == null)
            {
                return HttpNotFound();
            }
            return View(catagory);
        }

        // GET: Catagory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catagory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KatagoriAdi")] Catagory catagory)
        {
            if (ModelState.IsValid)
            {
                db.Katagoriler.Add(catagory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(catagory);
        }

        // GET: Catagory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catagory catagory = db.Katagoriler.Find(id);
            if (catagory == null)
            {
                return HttpNotFound();
            }
            return View(catagory);
        }

        // POST: Catagory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,KatagoriAdi")] Catagory catagory)
        {
            if (ModelState.IsValid)
            {
                var cat = db.Katagoriler.Find(catagory.Id);
                if (cat!=null)
                {
                    cat.Id=catagory.Id;
                    cat.KatagoriAdi=catagory.KatagoriAdi;
                    db.SaveChanges();

                    TempData["catagory1"] = cat;
                    return RedirectToAction("Index");
                }
               
                
            }
            return View(catagory);
        }

        // GET: Catagory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Catagory catagory = db.Katagoriler.Find(id);
            if (catagory == null)
            {
                return HttpNotFound();
            }
            return View(catagory);
        }

        // POST: Catagory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catagory catagory = db.Katagoriler.Find(id);
            db.Katagoriler.Remove(catagory);
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
