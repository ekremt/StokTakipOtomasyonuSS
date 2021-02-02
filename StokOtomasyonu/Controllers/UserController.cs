using StokOtomasyonu.Models;
using StokOtomasyonu.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokOtomasyonu.Controllers
{
    public class UserController:Controller
    {
        StokTakipOtomasyonuEntities1 context = new StokTakipOtomasyonuEntities1();

        [HttpGet] [Route("User/Index")]
        public ActionResult Index()
        {
            
             return View(); 
        }
        [HttpPost] [Route("User/Create")]
        public ActionResult Create(Kullanici kullanici)
        {
            try
            {
                context.Kullanici.Add(kullanici);
                context.SaveChanges();
                return Json(kullanici, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("kayitYapilmadi", JsonRequestBehavior.AllowGet);
            }
           
        }
        [HttpPost]
        [Route("User/Edit")]
        public ActionResult Edit(Kullanici kullanici)
        {
           /* var dbEditKullanici = context.Kullanici.Where(k => k.id ==kullanici.id).FirstOrDefault();
            dbEditKullanici = kullanici;*/
            context.Entry(kullanici).State = EntityState.Modified;
            context.SaveChanges(); 
            return Json(kullanici, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Route("User/EditPassword")]
        public ActionResult EditPassword(EditPassword sifre)
        {
            var dbEditKullanici = context.Kullanici.Where(k => k.id == sifre.id).FirstOrDefault();
            if (dbEditKullanici != null)
            {
                var dbEditKullaniciSifre = context.Kullanici.Where(k => k.sifre == sifre.sifreOld).FirstOrDefault();

                if (dbEditKullaniciSifre != null)
                {
                    dbEditKullaniciSifre.sifre = sifre.sifreNew;
                    context.Entry(dbEditKullaniciSifre).State = EntityState.Modified;
                    context.SaveChanges();
                    return Json(sifre, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("sifreYanlis", JsonRequestBehavior.AllowGet);

                }
            }
            else
            {
                return Json("kullaniciYok", JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet][Route("User/List")]
        public ActionResult List()
        {
            var dbKullanici = context.Kullanici.ToArray();
          
            return Json(dbKullanici, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("User/Delete")]
        public ActionResult Delete(Kullanici kullanici)
        {
            try
            {
                var dbDeleteKullanici = context.Kullanici.Where(k => k.id == kullanici.id).FirstOrDefault();
                context.Kullanici.Remove(dbDeleteKullanici);
                context.SaveChanges();
                return Json(dbDeleteKullanici, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("silinemedi", JsonRequestBehavior.AllowGet);
            }

        }
    }
}