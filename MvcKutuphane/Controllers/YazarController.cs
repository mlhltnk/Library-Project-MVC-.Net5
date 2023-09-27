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
        DBKUTUPHANEEntities2 db = new DBKUTUPHANEEntities2();

        // GET: Yazar
        public ActionResult Index()
        {
            return View();
        }
    }
}