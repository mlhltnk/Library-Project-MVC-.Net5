using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;
using MvcKutuphane.Models.Entity;
using static System.Collections.Specialized.BitVector32;

namespace MvcKutuphane.Controllers
{


    
    public class PanelimController : Controller
    {
        DBKUTUPHANEEntities4 db = new DBKUTUPHANEEntities4();
        // GET: Panelim

        
        [HttpGet]
        public ActionResult Index()  //listeleme işlemi
        {          
          var uyemail = (string)Session["Mail"];
            //Kullanıcının oturum(Session) verileri kullanılarak, oturumdan kullanıcının e-posta adresini alır.
            //Oturum,web uygulamalarında kullanıcıya özgü geçici verileri depolamaktadır. e posta verisini uyemail'e atadık.

           // var degerler = db.TBLUYELER.FirstOrDefault(z => z.MAIL == uyemail);  //TBLUYELER adlı tablodan uyemail değişkenine eşit olan e-posta adresine sahip ilk kaydı seçer(bu kayıt tüm sutunları içerir)
                                                                                  //bunu degerlere atar

            var degerler = db.TBLDUYURULAR.ToList();

            var d1 = db.TBLUYELER.Where(x=>x.MAIL==uyemail).Select(y=>y.AD).FirstOrDefault();  //sessiondan gelen ad bilgisini d1 e atadık
            ViewBag.d1 = d1;

            var d2 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.SOYAD).FirstOrDefault();  //d1'e Soyadı'ı atadık.
            ViewBag.d2 = d2;

            var d3 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.FOTOGRAF).FirstOrDefault();  //d1'e fotograf'ı atadık.
            ViewBag.d3 = d3;

            var d4 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.KULLANICIADI).FirstOrDefault();  
            ViewBag.d4 = d4;

            var d5 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.OKUL).FirstOrDefault();  
            ViewBag.d5 = d5;

            var d6 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.TELEFON).FirstOrDefault();
            ViewBag.d6 = d6;

            var d7 = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.MAIL).FirstOrDefault();
            ViewBag.d7 = d7;

            var uyeid = db.TBLUYELER.Where(x => x.MAIL == uyemail).Select(y => y.ID).FirstOrDefault();  
            ViewBag.d8 = db.TBLHAREKET.Where(x => x.UYE == uyeid).Count();  //hareket tablosunda üyeid değeri sayısını saydırdık

            var d9 = db.TBLMESAJLAR.Where(x => x.ALICI == uyemail).Count();  //mesajlar tablom içerisinde sisteme authentica olan üye mail kaç tane geçiyorsa onu bize verecek
            ViewBag.d9 = d9;

            var d10 = db.TBLDUYURULAR.Count();
            ViewBag.d10 = d10;

            return View(degerler);    //kullanıcının oturum bilgilerini kullanarak veritabanından kullanıcının profiliyle ilgili bilgileri çeker
        }




        [HttpPost]
        public ActionResult Index2(TBLUYELER p)  //SIFRE Resetleme İŞLEMİ  burada
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


        
        public ActionResult Kitaplarım() 
        {
            var kullanici = (string)Session["Mail"];
            var id=db.TBLUYELER.Where(x=>x.MAIL==kullanici.ToString()).Select(x=>x.ID).FirstOrDefault();  //sessiondan gelen kullanıcının id'sini seçme işlemi
            var degerler = db.TBLHAREKET.Where(x => x.UYE==id).ToList();
            return View(degerler);
        }


        
        public ActionResult Duyurular()
        {
            var duyurulistesi = db.TBLDUYURULAR.ToList();
            return View(duyurulistesi);
        }

        
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();                //çıkış yap işlemi için kullanılır.

            return RedirectToAction("GirisYap","login");  //logincontrollerdaki giriş yapa yönlendir
        }

        public PartialViewResult Partial1()
        {
            return PartialView();
        }

        public PartialViewResult Partial2()
        {
            var kullanici = (string)Session["Mail"];
            var id = db.TBLUYELER.Where(x => x.MAIL == kullanici).Select(y => y.ID).FirstOrDefault();   //sisteme giriş yapan kullanıcı id bilgisini aldık
                                                         //kullanıcının e-posta adresini kullanarak veritabanından kullanıcının kimliğini (ID) alınır. Bu ID bu kullanıcının diğer bilgilerini çekmek için kullanılacaktır.
            var uyebul = db.TBLUYELER.Find(id);         //veritabanından kullanıcının ID'sine karşılık gelen kullanıcı kaydını alır.
            return PartialView(uyebul);     //partial2yi döndür üyebul'dan gelen değerle döndür
        }

    }

}