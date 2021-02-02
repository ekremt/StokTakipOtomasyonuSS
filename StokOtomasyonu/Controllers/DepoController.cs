using StokOtomasyonu.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokOtomasyonu.Controllers
{
    public class DepoController:Controller
    {
        StokTakipOtomasyonuEntities1 context = new StokTakipOtomasyonuEntities1();
        [HttpGet]
        [Route("Depo/Index")]
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        [Route("Depo/Create")]
        public ActionResult Create(Depo depo)
        {
            try
            {
                context.Depo.Add(depo);
                context.SaveChanges();
                return Json(depo, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("kayitYapilmadi", JsonRequestBehavior.AllowGet);
            }

        }
        [HttpPost]
        [Route("Depo/Edit")]
        public ActionResult Edit(Depo depo)
        {
            /* var dbEditKullanici = context.Kullanici.Where(k => k.id ==kullanici.id).FirstOrDefault();
             dbEditKullanici = kullanici;*/
            context.Entry(depo).State = EntityState.Modified;
            context.SaveChanges();
            return Json(depo, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        [Route("Depo/Delete")]
        public ActionResult Delete(Depo depo)
        {
            try
            {
                var dbDeleteDepo = context.Depo.Where(k => k.id == depo.id).FirstOrDefault();
                context.Depo.Remove(dbDeleteDepo);
                context.SaveChanges();
                return Json(dbDeleteDepo, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("silinemedi", JsonRequestBehavior.AllowGet);
            }

        }

        [HttpGet]
        [Route("Depo/List")]
        public ActionResult List()
        {
            
            var dbDepo = context.Depo.ToArray();
            return Json(dbDepo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("Depo/DepoUrun")]
        public ActionResult DepoUrun(Depo depo)
        {
            var dbDepoUrun = from isyeri in context.Isyeri
                         join urun in context.Urun on isyeri.id equals urun.isyeri_id
                         join st in context.Stok on urun.id equals st.urun_id
                         join dep in context.Depo on st.depo_id equals dep.id
                             where (dep.id == depo.id)
                             orderby st.id
                         select new
                         {
                             st.id,
                             isyeri.isyeri_adi,
                             urun.urun_adi,
                             urun.urun_seri_no,
                             st.stok_adedi,
                             st.stok_kodu,
                             dep.depo_adi,
                             dep.depo_lokasyon
                         };
            var dbList = dbDepoUrun.ToArray();
            return Json(dbList, JsonRequestBehavior.AllowGet);
        }
    }
}