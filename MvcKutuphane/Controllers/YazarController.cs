using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{

    public class YazarController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();

        // GET: Yazar
        public ActionResult Index()
        {
            var degerler = db.TBLYAZAR.ToList();
            return View(degerler);
        }


        [HttpGet]
        public ActionResult YazarEkle()
        {
            return View();    
        }

        public ActionResult YazarEkle(TBLYAZAR p)
        {
            if(!ModelState.IsValid)
            {
                return View("YazarEkle");
            }
            db.TBLYAZAR.Add(p);
            db.SaveChanges();
            return View();
        }


        public ActionResult YazarSil(int id)
        {
            var yazar = db.TBLYAZAR.Find(id);
            db.TBLYAZAR.Remove(yazar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult YazarGetir(int id)           
        {
            var yzr = db.TBLYAZAR.Find(id);  
            //yazargetire verileri getirme işlemi
            return View("YazarGetir", yzr);
        }


        public ActionResult YazarGuncelle(TBLYAZAR p)
        {
            var yzr = db.TBLYAZAR.Find(p.ID);
            yzr.AD = p.AD;   //kategorinin yeni adı=indexten yeni giriş yapılan ad
            yzr.SOYAD = p.SOYAD;
            yzr.DETAY = p.DETAY;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}