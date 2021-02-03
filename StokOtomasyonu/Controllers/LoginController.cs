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
        StokTakipOtomasyonuEntities1 context = new StokTakipOtomasyonuEntities1();
        [HttpGet] [Route("Login/Index")]
        public ActionResult Index()
        {
            if (Session["UserID"] == null)
            {
                return View();
            }
            else
            {
               
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost] [Route("Login/Index")]
        public ActionResult Index(Kullanici kullanici)
        {
            string kullanici_adi = kullanici.kullanici_adi;
            string sifre = kullanici.sifre;
           
            var dbKullanici = context.Kullanici
                    .Where(k=>k.kullanici_adi==kullanici_adi && k.sifre==sifre).FirstOrDefault()  ;
            if (dbKullanici != null)
            {
                Session["UserID"] = dbKullanici.id.ToString();
                Session["UserName"] = dbKullanici.kullanici_adi.ToString();

                if(dbKullanici.is_admin!=null)
                {
                    Session["isAdmin"] = dbKullanici.is_admin.ToString();
                }
                

                return View();

            }
            else
            {
                return View();
            }
        }
        [HttpGet][Route("Login/SignOut")]
        public ActionResult SignOut()
        {
            
                Session["UserID"] = null;
                Session["UserName"] = null;
                Session["isAdmin"] = null;
                return RedirectToAction("Index", "Login");

               
            
        }
    }
}