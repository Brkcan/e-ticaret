using BusinessLayer;
using BusinessLayer.result;
using Entities;
using Entities.ModelNesneleriBurada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Taki_ve_Mucevher.Controllers
{
    public class HomeController : Controller
    {
        private SepeteEkleManeger SepeteEkleManeger = new SepeteEkleManeger();
        private UserMenegar UserMenegar = new UserMenegar();
        // GET: Home
        public ActionResult Index()
        {
            //BusinessLayer.Test test = new BusinessLayer.Test();
            //test.InsertTest();
            //test.CommentTest();

            if (TempData["Sepet"]!= null)
            {
                return View(TempData["Sepet"] as List<SepeteEkle>);
            }
            return View(SepeteEkleManeger.List().OrderByDescending(x => x.DuzenleyenKullanici).ToList());
        }
        //public ActionResult ByKategori(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    KategoriManeger cm = new KategoriManeger();
        //    Kategori kat = cm.Find(x => x.Id == id.Value);

        //    if (kat == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View("Index", kat.SepeteEkle);

        //}

         

        public ActionResult MostLiked()
        {
            return View("Index" , SepeteEkleManeger.List().OrderByDescending(x => x.BegeniSayısı).ToList());
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult ShowProfile()
        {
            kullanici datauser = Session["login"] as kullanici;


            BusinessLayerResult<kullanici> res = UserMenegar.GetUserById(datauser.Id);
            return View(res.Result);
        }
        public ActionResult EditProfile()
        {
            kullanici datauser = Session["login"] as kullanici;

          
            BusinessLayerResult<kullanici> res = UserMenegar.GetUserById(datauser.Id);
            return View(res.Result);
           
        }
        [HttpPost]
        public ActionResult EditProfile(kullanici model , HttpPostedFileBase ProfileImage)
        {
            if(ProfileImage != null && (ProfileImage.ContentType == "Resim/jpeg" || ProfileImage.ContentType == "Resim/jpg"|| ProfileImage.ContentType == "Resim/png"))
            {
                string filename = $"user_{model.Id}.{ProfileImage.ContentType.Split('/')[1]}";
                ProfileImage.SaveAs(Server.MapPath($"~/Resim/{filename}"));
                model.ProfilefileImagename = filename;
            }
           
            BusinessLayerResult<kullanici> res = UserMenegar.UpdateProfile(model);

            Session["login"] = res.Result;
            return RedirectToAction("ShowProfile");
                }
        public ActionResult DeleteProfile()
        {
            kullanici datauser = Session["login"] as kullanici;
            BusinessLayerResult<kullanici> res = UserMenegar.RemoveUserById(datauser.Id);
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
            HataMesajlariMirasAlma<kullanici> htma = UserMenegar.LoginUser(model);

            if(htma.Errors.Count > 0)
            {
                htma.Errors.ForEach(x => ModelState.AddModelError("", x));


                    return View(model);
                }
                Session["login"] = htma.Result;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                HataMesajlariMirasAlma<kullanici> htma = UserMenegar.RegisterUser(model);

                if(htma.Errors.Count > 0)
                {
                    htma.Errors.ForEach(x => ModelState.AddModelError("", x));
                    return View(model);
                }
                return RedirectToAction("RegisterOk");
            }

            
            return View(model);
        }
        public ActionResult RegisterOk()
        {
            return View();
        }
        //public ActionResult UserActivate(Guid Activate_id)
        //{
        //    return View();
        //}
        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }
    }
}