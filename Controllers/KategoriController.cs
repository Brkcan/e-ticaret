using BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Taki_ve_Mucevher.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        public ActionResult Select(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KategoriManeger cm = new KategoriManeger();
            Kategori kat = cm.Find(x => x.Id == id.Value);

            if(kat == null)
            {
                return HttpNotFound();
            }
            TempData["Sepet"] = kat.SepeteEkle.OrderByDescending(x => x.DuzenleyenKullanici).ToList();
            return RedirectToAction("Index" , "Home");
        }
    }
}