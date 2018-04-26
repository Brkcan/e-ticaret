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
    public class SepeteEkleController : Controller
    {
       private SepeteEkleManeger sepetekle = new SepeteEkleManeger();
        private KategoriManeger km = new KategoriManeger();

        public ActionResult Index()
        {

            var sepeteEkles = sepetekle.List();
            return View(sepeteEkles.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SepeteEkle sepeteEkle = sepetekle.Find(x => x.Id == id);
            if (sepeteEkle == null)
            {
                return HttpNotFound();
            }
            return View(sepeteEkle);
        }

        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(km.List(), "Id", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( SepeteEkle sepeteEkle)
        {
            if (ModelState.IsValid)
            {
                sepetekle.Insert(sepeteEkle);
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(km.List(), "Id", "Title", sepeteEkle.KategoriId);
            return View(sepeteEkle);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SepeteEkle sepeteEkle = sepetekle.Find(x => x.Id == id);
            if (sepeteEkle == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(km.List(), "Id", "Title", sepeteEkle.KategoriId);
            return View(sepeteEkle);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( SepeteEkle sepeteEkle)
        {
            if (ModelState.IsValid)
            {
                SepeteEkle db_sepet = sepetekle.Find(x => x.Id == sepeteEkle.Id);
                db_sepet.KategoriId = sepeteEkle.KategoriId;
                db_sepet.UrununKategorisi = sepeteEkle.UrununKategorisi;
                db_sepet.Isim = sepeteEkle.Isim;

                sepetekle.Update(db_sepet);

                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(km.List(), "Id", "Title", sepeteEkle.KategoriId);
            return View(sepeteEkle);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SepeteEkle sepeteEkle = sepetekle.Find(x => x.Id == id);
            if (sepeteEkle == null)
            {
                return HttpNotFound();
            }
            return View(sepeteEkle);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SepeteEkle sepeteEkle = sepetekle.Find(x => x.Id == id);
            sepetekle.Delete(sepeteEkle);

            return RedirectToAction("Index");
        }

      
    }
}
