using KeyStore.DataAccess;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace KeyStore
{
    /// <summary>
    /// KetStoreService için özet açıklama
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Bu Web Hizmeti'nin, ASP.NET AJAX kullanılarak komut dosyasından çağrılmasına, aşağıdaki satırı açıklamadan kaldırmasına olanak vermek için.
    // [System.Web.Script.Services.ScriptService]
    public class KetStoreService : System.Web.Services.WebService
    {
        AuthorityDataAccess ada = new AuthorityDataAccess();
        KeyDataAccess kda = new KeyDataAccess();
        ConverterClass cc = new ConverterClass();
        UserDataAccess uda = new UserDataAccess();
        MacAddressDataAccess mda = new MacAddressDataAccess();
        ImageDataAccess ida = new ImageDataAccess();

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetAllAuthority(int token)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Package package = new Package();
            List<PackageObject> authority_list = new List<PackageObject>();

            if (uda.UserActive(token))
            {
                if (uda.GetUserAuthorityByToken(token) == 1)
                {
                    authority_list = ada.GetAllAuthority();
                    package.process_code = 1;
                }
                else
                {
                    package.process_code = 14;
                }
            }
            else
            {
                package.process_code = 13;
            }

            package.package_object_list = authority_list;
            Context.Response.Write(js.Serialize(package));
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
        public void UserRegister(string name, string surname, string password, string mac_address_value)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Package package = new Package();
            User user = new User();
            if (password != name)
            {
                if (uda.GetUserId(name, surname) == -1)
                {
                    int max_id = uda.GetLastId();
                    user.id = max_id + 1;
                    user.name = name;
                    user.surname = surname;
                    user.password = password;
                    user.authority_id = 2;
                    MacAddress mac_address = new MacAddress();
                    int mac_max_id = mda.GetLastId();

                    mac_address.id = mac_max_id + 1;
                    mac_address.value = mac_address_value;
                    mac_address.user_id = max_id + 1;

                    if (uda.AddUser(user).id != -1 && mda.AddMacAddress(mac_address).id != -1)
                    {
                        package.process_code = 1;
                    }
                    else
                    {
                        package.process_code = 20;
                    }
                }
                else
                {
                    package.process_code = 12;
                }
            }
            else
            {
                package.process_code = 19;
            }
            package.package_object = user;

            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void UserLogin(string name, string surname, string password)
        {
            Package package = new Package();
            int user_id = uda.GetUserId(name, surname);
            User user = uda.GetUserById(user_id);
            if (user_id != -1)
            {
                if (password == user.password)
                {
                    User n_user = new User();
                    n_user.name = user.name;
                    n_user.surname = user.surname;
                    n_user.id = user.id;
                    n_user.authority_id = user.authority_id;
                    n_user.password = user.password;
                    n_user.token = uda.GetRandomForToken();
                    if (uda.UpdateUser(n_user).id != -1)
                    {
                        package.process_code = 1;
                        package.user_token = n_user.token;
                    }
                    else
                    {
                        package.process_code = 3;
                    }
                }
                else
                {
                    package.process_code = 18;
                }
            }
            else
            {
                package.process_code = 11;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void UserLogout(int token)
        {
            Package package = new Package();
            User user = uda.GetUserByToken(token);
            if (user != null)
            {
                user.token = -1;
                if (uda.UpdateUser(user).id != -1)
                {
                    package.process_code = 1;
                }
                else
                {
                    package.process_code = 3;
                }
            }
            else
            {
                package.process_code = 21;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void ForgotPassword(string name, string surname, string new_password)
        {
            Package package = new Package();
            int user_id = uda.GetUserId(name, surname);
            if (user_id != -1)
            {
                User user = uda.GetUserById(user_id);
                user.password = new_password;
                if (uda.UpdateUser(user).id != -1)
                {
                    package.process_code = 1;
                }
                else
                {
                    package.process_code = 3;
                }

            }
            else
            {
                package.process_code = 18;
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetKey(int token, string date, string mac_address)
        {
            Package package = new Package();
            int user_authority = uda.GetUserAuthorityByToken(token);

            if (user_authority != -1)
            {
                Key key = kda.GetKey();
                if (key.id != -1)
                {
                    key.sent_date = date;
                    key.sent_mac_address = mac_address;
                    if (kda.UpdateKey(key).id != -1)
                    {
                        package.process_code = 1;
                        package.package_object = key;
                    }
                    else
                    {
                        package.process_code = 3;
                    }

                }
                else
                {
                    package.process_code = 22;
                }
            }
            else
            {
                package.process_code = 13;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void PostKey(int token, string get_date, string creators_mac_address, string key_value)
        {
            Package package = new Package();
            int user_authority = uda.GetUserAuthorityByToken(token);

            if (user_authority != -1)
            {
                Key key = new Key();
                int max_id = kda.GetLastId();
                key.id = max_id + 1;
                key.get_date = get_date;
                key.creators_mac_address = creators_mac_address;
                byte[] key_array = kda.StringToKeyArray(key_value);
                byte[] bit_array = cc.GetRandomByteArray(key_array.Length);
                key_array = cc.XORArrays(key_array, bit_array);
                key.key_value = key_array;
                key.key_seccurity_degree = 1;
                if (kda.AddKey(key).id != -1)
                {
                    package.process_code = 1;
                }
                else
                {
                    package.process_code = 20;
                }
            }
            else
            {
                package.process_code = 13;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetDynamicKey(int token, string date, string mac_address, string key_value)
        {
            Package package = new Package();
            int user_authority = uda.GetUserAuthorityByToken(token);

            if (user_authority != -1)
            {
                Key key = new Key();
                int max_id = kda.GetLastId();
                key.id = max_id + 1;
                key.get_date = date;
                key.creators_mac_address = mac_address;
                byte[] key_array = kda.StringToKeyArray(key_value);
                byte[] bit_array = cc.GetRandomByteArray(key_array.Length);
                key_array = cc.XORArrays(key_array, bit_array);
                key.key_value = key_array;
                key.key_seccurity_degree = 2;

                if (kda.AddKey(key).id != -1)
                {
                    key = kda.GetKeyById(key.id);
                    key.sent_date = date;
                    key.sent_mac_address = mac_address;
                    if (kda.UpdateKey(key).id != -1)
                    {
                        package.process_code = 1;
                        package.package_object = key;
                    }
                    else
                    {
                        package.process_code = 3;
                    }
                }
                else
                {
                    package.process_code = 20;
                }
            }
            else
            {
                package.process_code = 13;
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void PostImage(int token, string image_values)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Package package = new Package();
            User user = uda.GetUserByToken(token);
            package.user_token = token;
            Imagee image = new Imagee();
            if (user.id != -1)
            {
                image.user_id = user.id;
                int max_id = ida.GetLastId();
                image.id = max_id + 1;
                image.image_values = ida.StringToByteArray(image_values);
                if (ida.AddImage(image).id != -1)
                {
                    package.process_code = 1;
                    package.package_object = image;
                }
                else
                {
                    package.process_code = 20;
                }
            }
            else
            {
                package.process_code = 13;
            }

            Context.Response.Write(js.Serialize(package));
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void GetAllImage(int token)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";

            Package package = new Package();
            User user = uda.GetUserByToken(token);
            package.user_token = token;
            List<PackageObject> image_list = ida.GetAllImage();
            if (user.id != -1)
            {
                if (image_list != null)
                {
                    package.process_code = 1;
                    package.package_object_list = image_list;
                }
                else
                {
                    package.process_code = 6;
                }
            }
            else
            {
                package.process_code = 13;
            }

            Context.Response.Write(js.Serialize(package));
        }

    }

}

