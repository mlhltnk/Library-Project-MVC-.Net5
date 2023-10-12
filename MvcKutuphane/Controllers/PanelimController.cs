using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using MvcKutuphane.Models.Entity;
using static System.Collections.Specialized.BitVector32;

namespace MvcKutuphane.Controllers
{
    public class PanelimController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Panelim

        [Authorize]
        [HttpGet]
        public ActionResult Index()  //listeleme işlemi
        {          
          var uyemail = (string)Session["Mail"];
            //Kullanıcının oturum(Session) verileri kullanılarak, oturumdan kullanıcının e-posta adresini alır.
            //Oturum,web uygulamalarında kullanıcıya özgü geçici verileri depolamaktadır. e posta verisini uyemail'e atadık.

            var degerler = db.TBLUYELER.FirstOrDefault(z => z.MAIL == uyemail);  //TBLUYELER adlı tablodan uyemail değişkenine eşit olan e-posta adresine sahip ilk kaydı seçer(bu kayıt tüm sutunları içerir)
                                                                                  //bunu degerlere atar

            return View(degerler);    //kullanıcının oturum bilgilerini kullanarak veritabanından kullanıcının profiliyle ilgili bilgileri çeker
        }




        [HttpPost]
        public ActionResult Index2(TBLUYELER p)  //SIFRE Resetleme İŞLEMİ
        { 
            
            var kullanici = (string)Session["Mail"];  //session içerisindeki mail adlı kullanıcı bilgisini kullanici adlı değişkene atadım(stringe çevirip)

            var uye = db.TBLUYELER.FirstOrDefault(x => x.MAIL == kullanici);  //TBLUYELER adlı tablodan kullanıcı değişkenine eşit olan e-posta adresine sahip ilk kaydı seçer (bu kayıt tüm sutunları içerir) bunu uye'ye atar
            
            uye.SIFRE = p.SIFRE;  //uyeden gelen şifre, p ile indexten giriş yapılan şifre ile değiştirilir

            uye.AD = p.AD;

            uye.FOTOGRAF = p.FOTOGRAF;

            uye.OKUL = p.OKUL;

            uye.KULLANICIADI = p.KULLANICIADI;

            db.SaveChanges();

            return RedirectToAction("Index"); 
        }
    }

}