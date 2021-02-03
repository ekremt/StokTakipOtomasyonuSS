using StokOtomasyonu.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace StokOtomasyonu.Controllers
{
    public class UrunController:Controller
    {
        StokTakipOtomasyonuEntities1 context = new StokTakipOtomasyonuEntities1();
        [HttpGet]  [Route("Urun/Index")]
        public ActionResult Index()
        {

            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        [Route("Urun/Create")]
        public ActionResult Create(Urun urun)
        {
            try
            {
                context.Urun.Add(urun);
                context.SaveChanges();
                return Json(urun, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("kayitYapilmadi", JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [Route("Urun/Edit")]
        public ActionResult Edit(Urun urun)
        {
            /* var dbEditKullanici = context.Kullanici.Where(k => k.id ==kullanici.id).FirstOrDefault();
             dbEditKullanici = kullanici;*/
            context.Entry(urun).State = EntityState.Modified;
            context.SaveChanges();
            return Json(urun, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [Route("Urun/Delete")]
        public ActionResult Delete(Urun urun)
        {
            try
            {
                var dbDeleteUrun = context.Urun.Where(k => k.id == urun.id).FirstOrDefault();
                context.Urun.Remove(dbDeleteUrun);
                context.SaveChanges();
                return Json(dbDeleteUrun, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("silinemedi", JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        [Route("Urun/List")]
        public ActionResult List()
        {

            if (Session["UserID"] != null && Session["isAdmin"] == null)
            {
                var sid = Convert.ToInt32(Session["UserID"]);
                var dbUrun = from kul in context.Kullanici
                             join isyeri in context.Isyeri on kul.isyeri_id equals isyeri.id
                             join urun in context.Urun on isyeri.id equals urun.isyeri_id
                             where (kul.id == sid)
                             select new
                             {
                                 urun.id,
                                 urun.isyeri_id,
                                 urun.created_at,
                                 urun.urun_adi,
                                 urun.urun_fiyati,
                                 urun.urun_model,
                                 urun.urun_seri_no,
                                 urun.urun_tarihi
                             };
                var dbUrunList = dbUrun.ToList();
                return Json(dbUrunList, JsonRequestBehavior.AllowGet);
            }
            else if (Session["isAdmin"] != null && Session["UserID"] != null)
            {


                var dbUrun = context.Urun.ToArray();
                return Json(dbUrun, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();

            }



        }
    }
}