using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
namespace MvcKutuphane.Controllers
{
    public class KategoriController : Controller
    {
        DBKUTUPHANEEntities2 db = new DBKUTUPHANEEntities2();


        // GET: Kategori
        public ActionResult Index()
        {
            var degerler=db.TBLKATEGORI.ToList();
            return View(degerler);
        }

        [HttpPost]
        public ActionResult KategoriEkle(TBLKATEGORI p)
        {
            db.TBLKATEGORI.Add(p);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        { return View(); 
        }

        public ActionResult KategoriSil(int id)
        {
            var kategori = db.TBLKATEGORI.Find(id);
            db.TBLKATEGORI.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktg = db.TBLKATEGORI.Find(id);

            return View(ktg);
        }
    
        public ActionResult KategoriGuncelle(TBLKATEGORI p)
        {
            var ktg = db.TBLKATEGORI.Find(p.ID);
            ktg.AD=p.AD;   //kategorinin yeni adı=indexten yeni giriş yapılan ad
            db.SaveChanges();
            return RedirectToAction("Index");
        }

       
    }
}