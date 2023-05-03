using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using BlogMvcAPP.Models;

namespace BlogMvcAPP.Controllers
{
    public class BlogController : Controller
    {
        private BlogContext db = new BlogContext();

        public ActionResult list(int? id,string q)
        {
            var bloglar = db.Bloglar
                 .Where(i => i.Onay == true && i.AnaSayfa == true)
    .Select(i => new BlogModel()
    {
        Id = i.Id,
        Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
        Aciklama = i.Aciklama,
        EklenmeTarihi = i.EklenmeTarihi,
        AnaSayfa = i.AnaSayfa,
        Onay = i.Onay,
        Resim = i.Resim,
        CatagoryId = i.CatagoryId
    }).AsQueryable();

            if (string.IsNullOrEmpty("q")==false)
            {
                bloglar=bloglar.Where(i=>i.Baslik.Contains(q) || i.Aciklama.Contains(q));
            }

            if (id != null)
            {
                bloglar=bloglar.Where(i=>i.CatagoryId==id);
            }
   

            return View(bloglar.ToList());
        }
        // GET: Blog
        public ActionResult Index()
        {
            var bloglar = db.Bloglar.Include(b => b.Catagory).OrderByDescending(i=>i.EklenmeTarihi);
            return View(bloglar.ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.CatagoryId = new SelectList(db.Katagoriler, "Id", "KatagoriAdi");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Baslik,Aciklama,Resim,İcerik,CatagoryId")] Blog blog)
        {
            blog.EklenmeTarihi = DateTime.Now.Date;
            
            if (ModelState.IsValid)
            {
                db.Bloglar.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CatagoryId = new SelectList(db.Katagoriler, "Id", "KatagoriAdi", blog.CatagoryId);
            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatagoryId = new SelectList(db.Katagoriler, "Id", "KatagoriAdi", blog.CatagoryId);
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Baslik,Aciklama,Resim,İcerik,Onay,AnaSayfa,CatagoryId")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                var entity=db.Bloglar.Find(blog.Id);
                if (entity!=null)
                {
                    entity.Baslik = blog.Baslik;
                    entity.Aciklama= blog.Aciklama;
                    entity.Resim=blog.Resim;
                    entity.İcerik = blog.İcerik;
                    entity.Onay=blog.Onay;
                    entity.AnaSayfa=blog.AnaSayfa;
                    entity.CatagoryId=blog.CatagoryId;

                    db.SaveChanges();

                    TempData["Blog"] = entity;
                    return RedirectToAction("Index");
                }
               
            }
            ViewBag.CatagoryId = new SelectList(db.Katagoriler, "Id", "KatagoriAdi", blog.CatagoryId);
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Bloglar.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Bloglar.Find(id);
            db.Bloglar.Remove(blog);
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
