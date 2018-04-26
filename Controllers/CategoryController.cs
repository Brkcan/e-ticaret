using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Entities;
using BusinessLayer;

namespace Taki_ve_Mucevher.Controllers
{
    public class CategoryController : Controller
    {
        private KategoriManeger km = new KategoriManeger();

        public ActionResult Index()
        {
            return View(km.List());
        }

    
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = km.Find(x => x.Id == id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

    
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                km.Insert(kategori);
                return RedirectToAction("Index");
            }

            return View(kategori);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = km.Find(x => x.Id == id.Value);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Kategori kategori)
        {

            Kategori cat = km.Find(x => x.Id == kategori.Id);
            cat.Title = kategori.Title;
            cat.Aciklama = kategori.Aciklama;

            km.Update(cat);
            if (ModelState.IsValid)
            {
                km.Update(kategori);
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = km.Find(x => x.Id == id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = km.Find(x => x.Id == id);
            km.Delete(kategori);
            return RedirectToAction("Index");
        }

       
    }
}
