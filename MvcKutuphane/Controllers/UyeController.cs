
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
            //sayfa numarası sayfa değerinden başlasın, her sayfada 3 değer göstersin

            return View(degerler);
        }

        public ActionResult UyeEkle(TBLUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("UyeEkle");
                //arka taraftaki dataannotation sağlanamadıysa personelekleye geri dön
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
            uye.AD = p.AD;   //kategorinin yeni adı=indexten giriş yapılan ad
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
            //kullanıcının kitap geçmişini veritabanından çeker ve kullanıcıya görüntüler. 
            var gecmis = db.TBLHAREKET.Where(x=>x.UYE==id).ToList();
            return View(gecmis);
        }

    }
}