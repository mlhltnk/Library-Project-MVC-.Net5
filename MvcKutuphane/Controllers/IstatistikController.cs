using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcKutuphane.Models.Entity;

namespace MvcKutuphane.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        public ActionResult Index()
        {
            var deger1 = db.TBLUYELER.Count();
            var deger2 = db.TBLKITAP.Count();
            var deger3 = db.TBLKITAP.Where(x => x.DURUM == false).Count();
            var deger4 = db.TBLCEZALAR.Sum(x => x.PARA);
            ViewBag.dgr1 =deger1;
            ViewBag.dgr2 =deger2;
            ViewBag.dgr3 =deger3;
            ViewBag.dgr4 =deger4;
            
            return View();
        }

        public ActionResult Hava() 
        {
            return View();
        }
        public ActionResult HavaKart() 
        {
            return View();
        }
        public ActionResult Galeri() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult resimyükle(HttpPostedFileBase dosya)
            //dışarıdan resim yükleme işlemi
        {
            if (dosya.ContentLength > 0)
            {
                string dosyayolu = Path.Combine(Server.MapPath("~/web2/resimler"), Path.GetFileName(dosya.FileName));
                dosya.SaveAs(dosyayolu);
            }
            return RedirectToAction("Galeri");
        }

        public ActionResult Linqkart()
        {
            var deger1 = db.TBLKITAP.Count();
            var deger2 = db.TBLUYELER.Count();
            var deger3 = db.TBLCEZALAR.Sum(x => x.PARA);
            var deger4 = db.TBLKITAP.Where(x => x.DURUM==false).Count();
            var deger5 = db.TBLKATEGORI.Count();

            var deger6 = db.TBLHAREKET.GroupBy(x=>x.TBLUYELER.AD).OrderByDescending(z=>z.Count()).
                Select(y=>y.Key).FirstOrDefault();

            var deger8 = db.EnfazlaKitapYazar().FirstOrDefault();

            //-----------------------------------------------------------------------------------------------------------------
            var deger9 = db.TBLKITAP.GroupBy(x => x.YAYINEVI).OrderByDescending(z=>z.Count()).
                Select(y=>y.Key).FirstOrDefault();

            //GroupBy(x => x.YAYINEVI):aynı yayınevine sahip olan kitapları aynı grup içinde toplar.

            //OrderByDescending(z => z.Count()):Grupları grup içindeki kitap sayısına göre azalan sırada sıralar. En fazla kitaba sahip yayınevinden başlayarak azalan sırada sıralama yapar.

            //Select(y =>  y.Key ): Her bir grup için yalnızca "YAYINEVI" alanını içeren yeni bir anonim nesne koleksiyonu oluşturur.
            //Bu, gruplandırılmış verinin yalnızca yayınevi bilgilerini içeren bir sonuç koleksiyonunu döndürür.

            //FirstOrDefault(): Sıralanmış sonuçlardan yalnızca ilk sıradaki sonucu alır. Yani, en fazla kitaba sahip olan yayınevinin bilgilerini alır.

            //veritabanındaki kitapların yayınevlerine göre gruplandırılmasını, gruplar arasında kitap sayısına göre sıralanmasını ve en fazla kitaba sahip olan yayınevinin bilgilerini almayı amaçlar.
            //Bu bilgiler bir anonim nesne içinde saklanır ve en fazla kitaba sahip olan yayınevinin bilgilerini içeren bu nesne döndürülür.
            //-------------------------------------------------------------------------------------------------------------------





            var deger11 = db.TBLILETISIM.Count();
            ViewBag.dgr1 =deger1;
            ViewBag.dgr2 =deger2;
            ViewBag.dgr3 =deger3;
            ViewBag.dgr4 =deger4;
            ViewBag.dgr5 =deger5;
            ViewBag.dgr6 =deger6;
            ViewBag.dgr11 =deger11;
            ViewBag.dgr8 = deger8;
            ViewBag.dgr9 =deger9;
            return View();
        }

    }
}