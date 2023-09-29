
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;


namespace MvcKutuphane.Controllers
{
    public class UyeController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();

        // GET: Uye
        public ActionResult Index()
        {
            var degerler = db.TBLUYELER.ToList();
            return View(degerler);
        }
    }
}