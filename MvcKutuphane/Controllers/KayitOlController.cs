using MvcKutuphane.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcKutuphane.Controllers
{
    [AllowAnonymous]
    public class KayitOlController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: KayitOl

        [HttpPost]
        public ActionResult Kayit(TBLUYELER p)
        {
            if (!ModelState.IsValid)
            {
                return View("Kayit");
               
            }
            db.TBLUYELER.Add(p);
            db.SaveChanges();
            return View();
        }

        [HttpGet]
        public ActionResult Kayit()
        {
            return  View();
        }
    }
}