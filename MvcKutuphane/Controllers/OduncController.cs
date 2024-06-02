using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;
using System.Security.Cryptography;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Odunc




        [Authorize(Roles = "A")]   
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x=>x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }






        [HttpGet]
        public ActionResult OduncVer()
        {
           

            List<SelectListItem> deger1 = (from x in db.TBLUYELER.ToList()                       
                                           select new SelectListItem
                                           {   Text = x.AD +" " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
         

            ViewBag.dgr1 = deger1;

            List<SelectListItem> deger2 = (from i in db.TBLKITAP.Where(x=>x.DURUM==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.AD,
                                               Value = i.ID.ToString()
                                           }).ToList();

            ViewBag.dgr2 = deger2;


            List<SelectListItem> deger3 = (from t in db.TBLPERSONEL.ToList()
                                           select new SelectListItem
                                           {
                                               Text = t.PERSONEL,
                                               Value = t.ID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;

            return View();
        }







        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            

            var d1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();               
            var d2 = db.TBLKITAP.Where(x => x.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(Z => Z.ID == p.TBLPERSONEL.ID).FirstOrDefault();

            p.TBLUYELER = d1;  
            p.TBLKITAP = d2;
            p.TBLPERSONEL = d3;

            db.TBLHAREKET.Add(p);  
            db.SaveChanges();
            return RedirectToAction("Index");
        }








        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odnc = db.TBLHAREKET.Find(p.ID);


             
            DateTime d1 = odnc.IADETARIH.Value.Date;

            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());     
                                                                                   
            TimeSpan d3 = d2 - d1;                
            ViewBag.dgr = d3.TotalDays;

            return View("Odunciade",odnc);
        }







        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETİRTARİH = p.UYEGETİRTARİH;    
            hrk.ISLEMDURUM = true;                  
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}