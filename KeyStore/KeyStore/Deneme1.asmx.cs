using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;

namespace KeyStore
{
    /// <summary>
    /// Summary description for Deneme1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]

    [Serializable]
    public class Ogrenci
    {
        public string ogrenciAdi;
        public string ogrenciNumara;

        public Ogrenci(string ogrenciAdi, string ogrenciNumara)
        {
            this.ogrenciAdi = ogrenciAdi;
            this.ogrenciNumara = ogrenciNumara;
        }
    }
    public class Deneme1 : System.Web.Services.WebService
    {
        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void OgrencileriYazdir()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            List<Ogrenci> ogrenci = new List<Ogrenci>();
            ogrenci.Add(new Ogrenci("Mert", "1234242"));
            ogrenci.Add(new Ogrenci("Mert", "1234242"));
            ogrenci.Add(new Ogrenci("Mert", "1234242"));

            Context.Response.Write(js.Serialize(ogrenci));

        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void OgrenciKaydet(string ogrenciNumara, string ogrenciAdi)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            // ogrenci kaydetme yapılabilir

            var veri = "Başarılı";

            Context.Response.Write(js.Serialize(veri));

        }

    }
}
