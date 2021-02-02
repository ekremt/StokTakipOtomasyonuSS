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

            return View();
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
            var dbUrun = context.Urun.ToArray();
            return Json(dbUrun, JsonRequestBehavior.AllowGet);
        }
    }
}