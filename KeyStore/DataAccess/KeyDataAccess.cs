using KeyStore.Abstract;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KeyStore.DataAccess
{
    [Serializable]
    public class KeyDataAccess : IKeyDataAccess
    {
        private string key_db_path = @"C:\\Users\\Melih\\Desktop\\KeyStore-master\\KeyStore\\KeyStore\\DataAccess\\Database\\DBKey.txt";

        private string KeyArrayToString(byte[] key_array)
        {
            string result = "";
            for (int i = 0; i < key_array.Length-1; i++)
            {
                result = result + key_array[i].ToString() + ":";
            }
            result = result + key_array[key_array.Length - 1].ToString();
            return result;
        }

        public byte[] StringToKeyArray(string key_string)
        {
            string[] values = key_string.Split(':');
            byte[] key_value = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                key_value[i] = (byte)Convert.ToInt32(values[i]);
            }
            return key_value;
        }

        public int GetLastId()
        {
            int max_id = -1;
            List<PackageObject> key_list = GetAllKey();
            if (key_list != null)
            {
                foreach(Key element in key_list)
                {
                    if (max_id < element.id) max_id = element.id;
                }
            }
            return max_id;
        }

        public Key AddKey(Key key)
        {
            List<PackageObject> key_list = GetAllKey();
            if (key_list != null)
            {
                foreach (Key element in key_list)
                {
                    if (element.id == key.id)
                    {
                        return new Key();
                    }
                }
            }

            if (File.Exists(key_db_path))
            {
                File.Delete(key_db_path);
            }

            if (!File.Exists(key_db_path))
            {
                using (StreamWriter sw = File.CreateText(key_db_path))
                {
                    if (key_list != null)
                    {
                        foreach (Key element in key_list)
                        {
                            sw.WriteLine(element.id.ToString() + ";" + element.creators_mac_address + ";" + element.sent_mac_address + ";" + element.get_date + ";" + element.sent_date + ";" + element.key_seccurity_degree.ToString() + ";" + KeyArrayToString(element.key_value));
                        }
                    }
                    sw.WriteLine(key.id.ToString() + ";" + key.creators_mac_address + ";" + key.sent_mac_address + ";" + key.get_date + ";" + key.sent_date + ";" + key.key_seccurity_degree.ToString() + ";" + KeyArrayToString(key.key_value));
                }
            }
            else
            {
                return new Key();
            }
            return key;
        }

        public bool DeleteKey(int key_id)
        {
            bool is_element_find = false;

            List<PackageObject> keyy_list = GetAllKey();
            if (File.Exists(key_db_path))
            {
                File.Delete(key_db_path);
            }
            using (StreamWriter sw = File.CreateText(key_db_path))
            {
                foreach (Key element in keyy_list)
                {
                    if (element.id != key_id)
                    {
                        sw.WriteLine(element.id.ToString() + ";" + element.creators_mac_address + ";" + element.sent_mac_address + ";" + element.get_date + ";" + element.sent_date + ";" + element.key_seccurity_degree.ToString() + ";" + KeyArrayToString(element.key_value));
                    }
                    else
                    {
                        is_element_find = true;
                    }
                }
            }

            return is_element_find;
        }

        public List<PackageObject> GetAllKey()
        {
            List<PackageObject> key_list = new List<PackageObject>();
            if (File.Exists(key_db_path))
            {
                using (StreamReader sr = File.OpenText(key_db_path))
                {
                    string line = "";
                    string[] line_element;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_element = line.Split(';');
                        Key key = new Key();
                        key.id = Convert.ToInt32(line_element[0]);
                        key.creators_mac_address = line_element[1];
                        key.sent_mac_address = line_element[2];
                        key.get_date = line_element[3];
                        key.sent_date = line_element[4];
                        key.key_value = StringToKeyArray(line_element[6]);
                        key.key_seccurity_degree = Convert.ToInt32(line_element[5]);
                        key_list.Add(key);
                    }
                }
                return key_list;
            }
            else
            {
                return null;
            }
        }

        public Key GetKey()
        {
            List<PackageObject> key_list = GetAllKey();

            if (key_list != null)
            {
                foreach (Key element in key_list)
                {
                    if(element.sent_date=="..") return element;
                }
                return new Key();
            }
            else
            {
                return new Key() ;
            }

        }

        public Key GetKeyById(int key_id)
        {
            Key key = new Key();
            List<PackageObject> key_list = GetAllKey();
            if (key_list != null)
            {
                if (key_list != null)
                {
                    foreach (Key element in key_list)
                    {
                        if (element.id == key_id)
                        {
                            key.id = key_id;
                            key.creators_mac_address = element.creators_mac_address;
                            key.sent_mac_address = element.sent_mac_address;
                            key.get_date = element.get_date;
                            key.sent_date = element.sent_date;
                            key.key_seccurity_degree = element.key_seccurity_degree;
                            key.key_value = element.key_value;
                            return key;
                        }
                    }
                }
                return key;
            }
            else
            {
                return null;
            }
        }

        public Key UpdateKey(Key key)
        {
            if (DeleteKey(key.id) == true)
            {
                AddKey(key);
                return key;
            }
            return new Key();
        }
    }
}