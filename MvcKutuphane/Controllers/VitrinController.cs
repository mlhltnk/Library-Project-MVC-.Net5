using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using MvcKutuphane.Models.Siniflarim;

namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class VitrinController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Vitrin

        [HttpGet]
        public ActionResult Index()
        {
            //İLİŞKİSİ OLMAYAN 2 VERİTABANINI TEK BİR VİEW'DE GÖSTERME İŞLEMİ (ya bu şekilde yapılır ya da Partialview ile yapılır)                   

            Class1 cs = new Class1();           //Class1 modelimizin  yeni bir örneği(cs) oluşturduk. 

            cs.Deger1 = db.TBLKITAP.ToList();    //cs adlı Class1 nesnesinin Deger1 adlı bir özelliği ayarlanıyor.
                                                 //Deger1 özelliği, veritabanından alınan TBLKITAP tablosunun tüm verilerini içerecektir.
            cs.Deger2 = db.TBLHAKKIMIZDA.ToList();

            //var degerler = db.TBLKITAP.ToList();

            return View(cs);
        }

        [HttpPost]
        public ActionResult Index(TBLILETISIM t) 
        {
            db.TBLILETISIM.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}