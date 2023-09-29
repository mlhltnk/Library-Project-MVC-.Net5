using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class PersonelController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();

        // GET: Personel
        public ActionResult Index()
        {
            var degerler = db.TBLPERSONEL.ToList();
            return View(degerler);
        }

        [HttpPost]
        public ActionResult PersonelEkle(TBLPERSONEL p)
        {
            if(!ModelState.IsValid)
            {
                return View("PersonelEkle");  
                //arka taraftaki dataannotation sağlanamadıysa personelekleye geri dön
            }
            db.TBLPERSONEL.Add(p);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            return View();
        }

        

        public ActionResult PersonelSil(int id)
        {
            var person = db.TBLPERSONEL.Find(id);
            db.TBLPERSONEL.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            var prs = db.TBLPERSONEL.Find(id);

            return View(prs);
        }

        public ActionResult PersonelGuncelle(TBLPERSONEL p)
        {
            var prs = db.TBLPERSONEL.Find(p.ID);
            prs.PERSONEL = p.PERSONEL;   //kategorinin yeni adı=indexten yeni giriş yapılan ad
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}