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




        [Authorize(Roles = "A")]    //YETKİ TANIMLAMA İŞLEMİ-->sadece yetkisi A olanlar buraya girebilir!!!
        public ActionResult Index()
        {
            var degerler = db.TBLHAREKET.Where(x=>x.ISLEMDURUM == false).ToList();
            return View(degerler);
        }






        [HttpGet]
        public ActionResult OduncVer()
        {
           

            List<SelectListItem> deger1 = (from x in db.TBLUYELER.ToList()                       //DROPDOWN LİST UYGULAMASI
                                           select new SelectListItem
                                           {   Text = x.AD +" " + x.SOYAD,
                                               Value = x.ID.ToString()
                                           }).ToList();
            //List<SelectListItem> türünde bir liste oluşturuluyor ve deger1 adında bir değişkene atanıyor
            //from x in db.TBLUYELER.ToList(): TBLUYELER tablosundan veriler bir liste haline getirilir ve bu liste üzerinde bir LINQ sorgusu yürütebilmek için x adında bir döngü değişkeni tanımlar. 
            //select new SelectListItem: LINQ sorgusunun sonucunda hangi verilerin seçileceğini belirtir. Burada yeni bir SelectListItem nesnesi oluşturulur.
            //SelectListItem nesnesinin Text özelliğine değer atanır. x.AD ve x.SOYAD, TBLUYELER tablosundan gelen her kaydın "AD" ve "SOYAD" alanlarına karşılık gelir.
            //Bu iki değer boşlukla birleştirilir ve Text özelliğine atanır. Böylece her SelectListItem, kullanıcı adı ve soyadı bir arada içeren bir metin içerir.
            //SelectListItem nesnesinin Value özelliğine değer atar. x.ID, her kaydın "ID" alanına karşılık gelir.
            //Bu değer bir metin olarak alınır ve Value özelliğine atanır. ToString() metodu, bu tamsayı değerini metin bir ifadeye dönüştürür.
            //ToList(); LINQ sorgusunun sonlandığını ve sonuçların bir liste olarak döndüğünü belirtir
            //SONUÇ OLARAK: deger1 listesi, TBLUYELER tablosundan alınan verileri SelectListItem nesnelerine dönüştürerek kullanıcı adı ve soyadını metin olarak görüntüleyen bir liste içerir. 

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
            //Ödünç verme işlemi gerçekleştirmeden önce yukarıda almış olduğumuz değerleri(dropdowndan çekmiş olduğumuz ID değerleri) birer değişken olarak Hareket tablosunun içine atamamız gerekiyor
            //yoksa her seferinde en son eklediğimiz değerin verilerini tablolara ekliyor

            var d1 = db.TBLUYELER.Where(x => x.ID == p.TBLUYELER.ID).FirstOrDefault();            //TBLUYELER adlı bir veritabanı tablosundan p.TBLUYELER.ID ile belirtilen ID'ye sahip TBLUYELER nesnesini alır     
            var d2 = db.TBLKITAP.Where(x => x.ID == p.TBLKITAP.ID).FirstOrDefault();
            var d3 = db.TBLPERSONEL.Where(Z => Z.ID == p.TBLPERSONEL.ID).FirstOrDefault();

            p.TBLUYELER = d1;  //p adlı TBLHAREKET nesnesinin TBLUYELER alanını, yukarıda çekilen veritabanı nesnesi(d1) ile doldurur. Bu işlemle ödünç verilen üye bilgisi TBLHAREKET nesnesine atanır.
            p.TBLKITAP = d2;
            p.TBLPERSONEL = d3;

            db.TBLHAREKET.Add(p);  //TBLHAREKET adlı veritabanı tablosuna p nesnesini ekler. Yani, ödünç verme işlemiyle ilgili bilgileri içeren TBLHAREKET nesnesi veritabanına eklenir
            db.SaveChanges();
            return RedirectToAction("Index");
        }








        public ActionResult Odunciade(TBLHAREKET p)
        {
            var odnc = db.TBLHAREKET.Find(p.ID);


            //DateTime d1 = DateTime.Parse(odnc.IADETARIH.ToString());                //d1;başlangıçtaki kitapın verilmesi gereken tarih 
            DateTime d1 = odnc.IADETARIH.Value.Date;

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