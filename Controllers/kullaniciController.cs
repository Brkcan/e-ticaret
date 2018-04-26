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
using BusinessLayer.result;

namespace Taki_ve_Mucevher.Controllers
{
    public class kullaniciController : Controller
    {

        private UserMenegar UserMenegar = new UserMenegar();
        
        public ActionResult Index()
        {
            return View(UserMenegar.List());
        }

       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kullanici kullanici = UserMenegar.Find(x => x.Id == id.Value);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<kullanici> res = UserMenegar.Insert(kullanici);

                if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(kullanici);
                }

                
                return RedirectToAction("Index");
            }

            return View(kullanici);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kullanici kullanici = UserMenegar.Find(x => x.Id == id.Value);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( kullanici kullanici)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<kullanici> res = UserMenegar.Update(kullanici);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(kullanici);
                }

                return RedirectToAction("Index");
            }
            return View(kullanici);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            kullanici kullanici = UserMenegar.Find(x => x.Id == id.Value);
            if (kullanici == null)
            {
                return HttpNotFound();
            }
            return View(kullanici);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            kullanici kullanici = UserMenegar.Find(x => x.Id == id);
            UserMenegar.Delete(kullanici);
            return RedirectToAction("Index");
        }

       
    }
}
