using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class KitapController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Kitap
        public ActionResult Index(string p) //"p", kullanıcının arama yapmak için girdiği metni almak için kullanılır.
        {

            var kitaplar = from k in db.TBLKITAP select k;  //k değişkeniyle tblkitap tablosuna ulaş ve k'yı(yani tüm tabloyu) seç

            if(!string.IsNullOrEmpty(p))
            {
                kitaplar = kitaplar.Where(x => x.AD.Contains(p));
            }
            //var kitaplar = db.TBLKITAP.ToList();
            return View(kitaplar.ToList());

           /* {
                if (!string.IsNullOrEmpty(p))
                {
                    var ktp = db.TBLKITAP.Where(x => x.AD.Contains(p));
                 
                    return View(ktp.ToList());
                }
                var kitaplar = db.TBLKITAP.ToList();
                return View(kitaplar);
            } */
        }


        [HttpGet]
        public ActionResult KitapEkle()
        {
            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()  //linq sorgu  //KATEGORİLERİ GETİRME İŞLEMİ
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()

                                           }).ToList();

            ViewBag.dgr1 = deger1;



            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList() select new SelectListItem
                                                                             {
                                                                                Text = i.AD + ' ' + i.SOYAD,
                                                                                 Value = i.ID.ToString()
                                                                               }).ToList();
            ViewBag.dgr2 = deger2;

            return View();
        }

        [HttpPost]
        public ActionResult KitapEkle(TBLKITAP p)
        {
            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();  //ilişkisel tablo
            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault();   //ilişkisel tablo
            p.TBLKATEGORI = ktg;
            p.TBLYAZAR = yzr;
            db.TBLKITAP.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapSil(int id)
        {
            var kitap = db.TBLKITAP.Find(id);
            db.TBLKITAP.Remove(kitap);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KitapGetir(int id)
        {
            var ktp = db.TBLKITAP.Find(id);
            //yazargetire verileri getirme işlemi

            List<SelectListItem> deger1 = (from i in db.TBLKATEGORI.ToList()  //linq sorgu  //KATEGORİLERİ GETİRME İŞLEMİ
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()

                                           }).ToList();

            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLYAZAR.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD + ' ' + i.SOYAD,
                                               Value = i.ID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;


            return View("KitapGetir", ktp);
        }


        public ActionResult KitapGuncelle(TBLKITAP p)
        { 
            var ktp = db.TBLKITAP.Find(p.ID);          
            ktp.AD = p.AD;
            ktp.BASIMYIL = p.BASIMYIL;
            ktp.YAYINEVI = p.YAYINEVI;
            ktp.SAYFASAYISI = p.SAYFASAYISI;
            ktp.DURUM = true;  //güncellemede ilk durum true gelsin

            var ktg = db.TBLKATEGORI.Where(k => k.ID == p.TBLKATEGORI.ID).FirstOrDefault();
            //k olarak adlandırılan her kayıt için ID alanının p.TBLKATEGORI.ID ile eşleşen ilk değeri getir.
            //TBLKATEGORI tablosundan ID değeri p.TBLKATEGORI.ID ile eşleşen kayıtları seçip ktg'ye atar.

            var yzr = db.TBLYAZAR.Where(y => y.ID == p.TBLYAZAR.ID).FirstOrDefault(); //ilişkisel tablo

            ktp.KATEGORI = ktg.ID;

            ktp.YAZAR = yzr.ID;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}