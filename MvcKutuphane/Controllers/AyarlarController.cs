using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class AyarlarController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Ayarlar
        public ActionResult Index()
        {
            var kullanicilar = db.TBLADMIN.ToList();
            return View(kullanicilar);
        }


        public ActionResult Index2()
        {
            var kullanicilar = db.TBLADMIN.ToList();
            return View(kullanicilar);
        }



        [HttpGet]
        public ActionResult YeniAdmin()
        {
            return View();
        }



        [HttpPost]
        public ActionResult YeniAdmin(TBLADMIN t)
        {
            db.TBLADMIN.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }

        public ActionResult AdminSil(int id)
        {
            var admin = db.TBLADMIN.Find(id);
            db.TBLADMIN.Remove(admin);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }

        [HttpGet]
        public ActionResult AdminGuncelle(int id)
        {
            var adm = db.TBLADMIN.Find(id);

            return View(adm);
        }

     
        [HttpPost]
        public ActionResult AdminGuncelle(TBLADMIN p)
        {
            var adm = db.TBLADMIN.Find(p.ID);
            adm.Kullanici = p.Kullanici;   //kategorinin yeni adı=indexten yeni giriş yapılan ad
            adm.Sifre = p.Sifre;   //kategorinin yeni adı=indexten yeni giriş yapılan ad
            adm.Yetki = p.Yetki;   //kategorinin yeni adı=indexten yeni giriş yapılan ad
            db.SaveChanges();
            return RedirectToAction("Index2");
        }
    }
}