using StokOtomasyonu.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace StokOtomasyonu.Controllers
{
    public class IsyeriController:Controller
    {
        StokTakipOtomasyonuEntities1 context = new StokTakipOtomasyonuEntities1();
        [HttpGet]
        [Route("Isyeri/Index")]
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
        [Route("Isyeri/Create")]
        public ActionResult Create(Isyeri isyeri)
        {
            try
            {
                context.Isyeri.Add(isyeri);
                context.SaveChanges();
                return Json(isyeri, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("kayitYapilmadi", JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [Route("Isyeri/Edit")]
        public ActionResult Edit(Isyeri isyeri)
        {
            /* var dbEditKullanici = context.Kullanici.Where(k => k.id ==kullanici.id).FirstOrDefault();
             dbEditKullanici = kullanici;*/
            context.Entry(isyeri).State = EntityState.Modified;
            context.SaveChanges();
            return Json(isyeri, JsonRequestBehavior.AllowGet);
        }
       

        
        [HttpPost]
        [Route("Isyeri/Delete")]
        public ActionResult Delete(Isyeri isyeri)
        {
            try
            {
                var dbDeleteIsyeri = context.Isyeri.Where(k => k.id == isyeri.id).FirstOrDefault();
                context.Isyeri.Remove(dbDeleteIsyeri);
                context.SaveChanges();
                return Json(dbDeleteIsyeri, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("silinemedi", JsonRequestBehavior.AllowGet);
            }

        }
        
        [HttpGet] [Route("Isyeri/List")]
        public ActionResult List()
        {
            if (Session["UserID"] != null && Session["isAdmin"] == null)
            {
                var sid = Convert.ToInt32(Session["UserID"]);
                var dbIsyeriUser = from isy in context.Isyeri
                                   join kul in context.Kullanici on isy.id equals kul.isyeri_id
                                   where (kul.id == sid)
                                   orderby isy.id
                                   select isy  ;
               
                return Json(dbIsyeriUser, JsonRequestBehavior.AllowGet);
            }
           else if(Session["isAdmin"] != null && Session["UserID"] != null)
            {
                 var dbIsyeriAdi = context.Isyeri.ToArray();
                return Json(dbIsyeriAdi, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();

            }
           
            
        }
        [HttpPost]
        [Route("Isyeri/IsyeriUser")]
        public ActionResult IsyeriUser(Isyeri isyeri)
        {
            var dbIsyeriUser = from isy in context.Isyeri
                         join kul in context.Kullanici on isy.id equals kul.isyeri_id
                         where (isy.id == isyeri.id)
                         orderby isy.id
                         select new
                         {
                             kul.kullanici_adi,
                             kul.soyadi,
                             kul.email,
                             kul.adi
                          
                         };
            var dbList = dbIsyeriUser.ToArray();
            return Json(dbList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Route("Isyeri/IsyeriUrun")]
        public ActionResult IsyeriUrun(Isyeri isyeri)
        {
            var dbIsyeriUrun = from isy in context.Isyeri
                               join urun in context.Urun on isy.id equals urun.isyeri_id
                               where (isy.id == isyeri.id)
                               orderby isy.id
                               select new
                               {
                                   urun.urun_adi,
                                   urun.urun_fiyati,
                                   urun.urun_model,
                                   urun.urun_seri_no,
                                   urun.urun_tarihi

                               };
            var dbList = dbIsyeriUrun.ToArray();
            return Json(dbList, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [Route("Isyeri/IsyeriStok")]
        public ActionResult IsyeriStok(Isyeri isyeri)
        {

            var dbIsyeriStok = from isyer in context.Isyeri
                         join urun in context.Urun on isyer.id equals urun.isyeri_id
                         join st in context.Stok on urun.id equals st.urun_id
                         join depo in context.Depo on st.depo_id equals depo.id
                         where (isyer.id == isyeri.id)
                               orderby isyer.id
                         select new
                         {
                             st.id,
                             isyer.isyeri_adi,
                             urun.urun_adi,
                             st.stok_adedi,
                             st.stok_kodu,
                             depo.depo_adi,
                             depo.depo_lokasyon
                         };
            
            var dbList = dbIsyeriStok.ToArray();
            return Json(dbList, JsonRequestBehavior.AllowGet);
        }
    }
}