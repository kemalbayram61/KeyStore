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
    public class MacAddressDataAccess : IMacAddressDataAccess
    {
        private string mac_address_db_path = @"C:\\Users\\mhd\\Downloads\\KeyStore-master\\KeyStore-master\\KeyStore\\DataAccess\\Database\\DBMacAddress.txt";

        public int GetLastId()
        {
            List<PackageObject> mac_address_list = GetAllMacAddress();
            if(mac_address_list != null)
            {
                int max_id = -1;
                foreach(MacAddress element in mac_address_list)
                {
                    if (max_id < element.id)
                    {
                        max_id = element.id;
                    }
                }
                return max_id;
            }
            else
            {
                return -1;
            }
        }

        public MacAddress AddMacAddress(MacAddress mac_address)
        {
            List<PackageObject> mac_address_list = GetAllMacAddress();
            if (mac_address_list != null)
            {
                foreach (MacAddress element in mac_address_list)
                {
                    if (element.id == mac_address.id)
                    {
                        return new MacAddress();
                    }
                }
            }

            if (File.Exists(mac_address_db_path))
            {
                File.Delete(mac_address_db_path);
            }

            if (!File.Exists(mac_address_db_path))
            {
                using (StreamWriter sw = File.CreateText(mac_address_db_path))
                {
                    if (mac_address_list != null)
                    {
                        foreach (MacAddress element in mac_address_list)
                        {
                            sw.WriteLine(element.id.ToString() + ";" + element.user_id.ToString() + ";" + element.value);
                        }
                    }

                    sw.WriteLine(mac_address.id.ToString() + ";" + mac_address.user_id.ToString() + ";" + mac_address.value);
                }
            }
            else
            {
                return new MacAddress();
            }
            return mac_address;
        }

        public bool DeleteMacAddress(int mac_address_id)
        {
            bool is_element_find = false;

            List<PackageObject> mac_address_list = GetAllMacAddress();
            if (File.Exists(mac_address_db_path))
            {
                File.Delete(mac_address_db_path);
            }
            using (StreamWriter sw = File.CreateText(mac_address_db_path))
            {
                foreach (MacAddress element in mac_address_list)
                {
                    if (element.id != mac_address_id)
                    {
                        sw.WriteLine(element.id.ToString() + ";" + element.user_id.ToString() + ";" + element.value);
                    }
                    else
                    {
                        is_element_find = true;
                    }
                }
            }

            return is_element_find;
        }

        public List<PackageObject> GetAllMacAddress()
        {
            List<PackageObject> mac_address_list = new List<PackageObject>();
            if (File.Exists(mac_address_db_path))
            {
                using (StreamReader sr = File.OpenText(mac_address_db_path))
                {
                    string line = "";
                    string[] line_element;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_element = line.Split(';');
                        MacAddress mac_address = new MacAddress();
                        mac_address.id = Convert.ToInt32(line_element[0]);
                        mac_address.user_id = Convert.ToInt32(line_element[1]);
                        mac_address.value = line_element[2];
                        mac_address_list.Add(mac_address);
                    }
                }
                return mac_address_list;
            }
            else
            {
                return null;
            }
        }

        public MacAddress GetMacAddressById(int mac_address_id)
        {
            MacAddress mac_address = new MacAddress();
            List<PackageObject> mac_address_list = GetAllMacAddress();
            if (mac_address_list != null)
            {
                if (mac_address_list != null)
                {
                    foreach (MacAddress element in mac_address_list)
                    {
                        if (element.id == mac_address_id)
                        {
                            mac_address.id = mac_address_id;
                            mac_address.user_id = element.user_id;
                            mac_address.value = element.value;
                            return mac_address;
                        }
                    }
                }
                return mac_address;
            }
            else
            {
                return null;
            }
        }

        public MacAddress UpdateMacAddress(MacAddress mac_address)
        {
            if (DeleteMacAddress(mac_address.id) == true)
            {
                AddMacAddress(mac_address);
                return mac_address;
            }
            return new MacAddress();
        }
    }
}