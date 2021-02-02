using StokOtomasyonu.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StokOtomasyonu.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet] [Route("Login/Index")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost] [Route("Login/Index")]
        public ActionResult Index(Kullanici kullanici)
        {
            string kullanici_adi = kullanici.kullanici_adi;
            string sifre = kullanici.sifre;
            StokTakipOtomasyonuEntities1 context = new StokTakipOtomasyonuEntities1();
            var dbKullanici = context.Kullanici
                    .Where(k=>k.kullanici_adi==kullanici_adi && k.sifre==sifre).FirstOrDefault()  ;
            if (dbKullanici != null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                return View();
            }
        }
    }
}