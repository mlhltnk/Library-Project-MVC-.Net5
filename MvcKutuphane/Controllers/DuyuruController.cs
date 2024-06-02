using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class DuyuruController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Duyuru
        public ActionResult Index()
        {
            var dyr = db.TBLDUYURULAR.ToList();
            return View(dyr);
        }

        [HttpGet]
        public ActionResult Yeniduyuru()
        { 
            return View(); 
        }

        [HttpPost]
        public ActionResult YeniDuyuru(TBLDUYURULAR p)
        {
            db.TBLDUYURULAR.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");    
        }
        public ActionResult DuyuruSil(int id)
        {
           var dyr= db.TBLDUYURULAR.Find(id);
            db.TBLDUYURULAR.Remove(dyr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DuyuruDetay(TBLDUYURULAR p)
        {
            var duyuru = db.TBLDUYURULAR.Find(p.ID);
            return View("DuyuruDetay",duyuru); 
        }

        public ActionResult DuyuruGüncelle(TBLDUYURULAR p)
        {
            var prs = db.TBLDUYURULAR.Find(p.ID);
            prs.ICERIK = p.ICERIK;  
            prs.KATEGORI = p.KATEGORI;
            prs.TARIH = p.TARIH;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}