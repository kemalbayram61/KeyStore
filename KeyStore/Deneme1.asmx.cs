using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using KeyStore.DataAccess;
using KeyStore.Entities;
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
    // [System.Web.Script.Services.ScriptService

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
        AuthorityDataAccess ada = new AuthorityDataAccess();
        KeyDataAccess kda = new KeyDataAccess();
        ConverterClass cc = new ConverterClass();

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

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateAuthority(int authority_id, string authority_type)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Authority authority = new Authority();
            authority.id = authority_id;
            authority.authority_type = authority_type;
            ada.AddAuthority(authority);

            Context.Response.Write(js.Serialize(authority));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void DeleteAuthority(int authority_id)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            bool delete_case = ada.DeleteAuthority(authority_id);

            Context.Response.Write(js.Serialize(delete_case));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void UpdateAuthority(int authority_id, string authority_type)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Authority authority = new Authority();
            authority.id = authority_id;
            authority.authority_type = authority_type;
            ada.UpdateAuthority(authority);

            Context.Response.Write(js.Serialize(authority));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetAuthoryById(int authority_id)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Authority authority = new Authority();
            authority = ada.GetAuthorityById(authority_id);

            Context.Response.Write(js.Serialize(authority));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetAllAuthory()
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            List<PackageObject> authority_list = ada.GetAllAuthority();

            Context.Response.Write(js.Serialize(authority_list));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void CreateKey(int key_id, string creators_mac_address, string sent_mac_address, string get_date, string sent_date, int key_seccurity_degree, string key_value)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Key key = new Key();
            byte[] key_value_array = kda.StringToKeyArray(key_value);
            byte[] random_array = cc.GetRandomByteArray();
            key_value_array = cc.XORArrays(key_value_array, random_array);

            key.id = key_id;
            key.creators_mac_address = creators_mac_address;
            key.sent_mac_address = sent_mac_address;
            key.get_date = get_date;
            key.sent_date = sent_date;
            key.key_seccurity_degree = key_seccurity_degree;
            key.key_value = key_value_array;
            kda.AddKey(key);

            Context.Response.Write(js.Serialize(key));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void DeleteKey(int key_id)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            bool delete_case = kda.DeleteKey(key_id);

            Context.Response.Write(js.Serialize(delete_case));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void UpdateKey(int key_id, string creators_mac_address, string sent_mac_address, string get_date, string sent_date, int key_seccurity_degree, string key_value)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Key key = new Key();
            byte[] key_value_array = kda.StringToKeyArray(key_value);
            if (key_value != null)
            {
                byte[] random_array = cc.GetRandomByteArray();
                key_value_array = cc.XORArrays(key_value_array, random_array);
            }

            key.id = key_id;
            key.creators_mac_address = creators_mac_address;
            key.sent_mac_address = sent_mac_address;
            key.get_date = get_date;
            key.sent_date = sent_date;
            key.key_seccurity_degree = key_seccurity_degree;
            key.key_value = key_value_array;
            kda.UpdateKey(key);

            Context.Response.Write(js.Serialize(key));
        }

    }
}
