
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();

        // GET: Uye
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLUYELER.ToList();

            var degerler = db.TBLUYELER.ToList().ToPagedList(sayfa, 3);   
       

            return View(degerler);
        }

        public ActionResult UyeEkle(TBLUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
           
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult UyeEkle()
        {
            return View();
        }

        public ActionResult UyeSil(int id)
        {
            var uye = db.TBLUYELER.Find(id);
            db.TBLUYELER.Remove(uye);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeGetir(int id)
        {
            var uye = db.TBLUYELER.Find(id);

            return View(uye);
        }

        public ActionResult UyeGuncelle(TBLUYELER p)
        {
            var uye = db.TBLUYELER.Find(p.ID);
            uye.AD = p.AD;  
            uye.SOYAD = p.SOYAD;
            uye.MAIL= p.MAIL;
            uye.KULLANICIADI= p.KULLANICIADI;
            uye.SIFRE= p.SIFRE;
            uye.OKUL= p.OKUL;
            uye.TELEFON= p.TELEFON;
            uye.FOTOGRAF= p.FOTOGRAF;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UyeKitapGecmis(int id)
        {
            
            var kitapgecmis = db.TBLHAREKET.Where(x=>x.UYE==id).ToList();
            var uyebilgisi = db.TBLUYELER.Where(y => y.ID == id).Select(z => z.AD + " " + z.SOYAD).FirstOrDefault();
            ViewBag.uyeblgs = uyebilgisi;
            return View(kitapgecmis);
        }

    }
}