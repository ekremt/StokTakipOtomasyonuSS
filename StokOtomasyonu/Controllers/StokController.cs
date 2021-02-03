using StokOtomasyonu.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokOtomasyonu.Controllers
{
    public class StokController:Controller
    {
        StokTakipOtomasyonuEntities1 context = new StokTakipOtomasyonuEntities1();
        [HttpGet]
        [Route("Stok/Index")]
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
        [Route("Stok/Create")]
        public ActionResult Create(Stok stok)
        {
            try
            {
                context.Stok.Add(stok);
                context.SaveChanges();
                return Json(stok, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("kayitYapilmadi", JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [Route("Stok/Edit")]
        public ActionResult Edit(Stok stok)
        {
            /* var dbEditKullanici = context.Kullanici.Where(k => k.id ==kullanici.id).FirstOrDefault();
             dbEditKullanici = kullanici;*/
            context.Entry(stok).State = EntityState.Modified;
            context.SaveChanges();
            return Json(stok, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [Route("Stok/Delete")]
        public ActionResult Delete(Stok stok)
        {
            try
            {
                var dbDeleteStok = context.Stok.Where(k => k.id == stok.id).FirstOrDefault();
                context.Stok.Remove(dbDeleteStok);
                context.SaveChanges();
                return Json(dbDeleteStok, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("silinemedi", JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        [Route("Stok/List")]
        public ActionResult List()
        {


            if (Session["UserID"] != null && Session["isAdmin"] == null)
            {
                var sid = Convert.ToInt32(Session["UserID"]);
                var dbStok = from kul in context.Kullanici
                             join isyeri in context.Isyeri on kul.isyeri_id equals isyeri.id
                             join urun in context.Urun on isyeri.id equals urun.isyeri_id
                             join st in context.Stok on urun.id equals st.urun_id
                             join depo in context.Depo on st.depo_id equals depo.id
                             orderby st.id
                             where (kul.id == sid)
                             select new
                             {
                                 st.id,
                                 isyeri.isyeri_adi,
                                 urun.urun_adi,
                                 urun.urun_seri_no,
                                 st.stok_adedi,
                                 st.stok_kodu,
                                 depo.depo_adi,
                                 depo.depo_lokasyon
                             };
                var dbStokList = dbStok.ToList();
                return Json(dbStokList, JsonRequestBehavior.AllowGet);
            }
            else if (Session["isAdmin"] != null && Session["UserID"] != null)
            {


                var dbStok = from isyeri in context.Isyeri
                             join urun in context.Urun on isyeri.id equals urun.isyeri_id
                             join st in context.Stok on urun.id equals st.urun_id
                             join depo in context.Depo on st.depo_id equals depo.id
                             orderby st.id
                             select new
                             {
                                 st.id,
                                 isyeri.isyeri_adi,
                                 urun.urun_adi,
                                 urun.urun_seri_no,
                                 st.stok_adedi,
                                 st.stok_kodu,
                                 depo.depo_adi,
                                 depo.depo_lokasyon
                             };

                var dbStokList = dbStok.ToList();
                return Json(dbStokList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return View();

            }






        }
    }
}