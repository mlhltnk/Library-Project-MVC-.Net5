using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class OduncController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Odunc
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x=>x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult OduncVer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OduncVer(TBLHAREKET p)
        {
            db.TBLHAREKET.Add(p);
            db.SaveChanges();
            return View();
        }

        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odnc = db.TBLHAREKET.Find(p.ID);

            DateTime d1 = DateTime.Parse(odnc.IADETARIH.ToString());                //d1;başlangıçtaki kitapın verilmesi gereken tarih 

            DateTime d2 = Convert.ToDateTime(DateTime.Now.ToShortDateString());     //d2; bugünün tarihini alıyoruz. 
                                                                                    //Bugünün tarihini string olarak alıyor
            TimeSpan d3 = d2 - d1;                //ZAMANLARI BİRBİRİNDEN ÇIKARMAK için Timespan kullanılır
            ViewBag.dgr = d3.TotalDays;

            return View("Odunciade",odnc);
        }

        public ActionResult OduncGuncelle(TBLHAREKET p)
        {
            var hrk = db.TBLHAREKET.Find(p.ID);
            hrk.UYEGETİRTARİH = p.UYEGETİRTARİH;    //kategorinin yeni adı=indexten yeni giriş yapılan ad
            hrk.ISLEMDURUM = true;                  //geri alınca true'ya dönsün
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}