using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;


namespace MvcKutuphane.Controllers
{
    public class MesajlarController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Mesajlar
        public ActionResult Index()
        {
            var uyemail = (string)Session["MAIL"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail.ToString()).ToList();  
            return View(mesajlar);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        { 

            return View(); 
        }

        [HttpPost]
        public ActionResult YeniMesaj(TBLMESAJLAR t)
        {
            var uyemail = (string)Session["MAIL"].ToString();
            t.GONDEREN = uyemail.ToString();                              
            t.TARIH = DateTime.Parse(DateTime.Now.ToShortDateString());     
            db.TBLMESAJLAR.Add(t);                                         
            db.SaveChanges();

            return RedirectToAction("Giden","Mesajlar");
        }

        public ActionResult Giden()
        {
            var uyemail = (string)Session["MAIL"].ToString();
            var mesajlar = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail.ToString()).ToList();  //sadece gönderdiğimiz mailleri görmek için linq
            return View(mesajlar);
        }

        public PartialViewResult Partial1()
        {
            var uyemail = (string)Session["MAIL"].ToString();
            var gelensayisi = db.TBLMESAJLAR.Where(x=>x.ALICI==uyemail).Count();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = db.TBLMESAJLAR.Where(x => x.GONDEREN == uyemail).Count();
            ViewBag.d2 = gidensayisi;
            return PartialView();
        }

    }
}