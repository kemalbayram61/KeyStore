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
    public class AuthorityDataAccess : IAuthorityDataAccess
    {
        private string authority_db_path = @"C:\\Users\\Melih\\Desktop\\KeyStore-master\\KeyStore\\KeyStore\\DataAccess\\Database\\DBAuthority.txt";
        public Authority AddAuthority(Authority authority)
        {
            List<PackageObject> authority_list = GetAllAuthority();
            if (authority_list != null)
            {
                foreach (Authority element in authority_list)
                {
                    if (element.id == authority.id)
                    {
                        return new Authority();
                    }
                }
            }

            if (File.Exists(authority_db_path))
            {
                File.Delete(authority_db_path);
            }

            if (!File.Exists(authority_db_path))
            {
                using (StreamWriter sw = File.CreateText(authority_db_path))
                {
                    if (authority_list != null)
                    {
                        foreach (Authority element in authority_list)
                        {
                            sw.WriteLine(element.id.ToString() + ";" + element.authority_type);
                        }
                    }

                    sw.WriteLine(authority.id.ToString() + ";" + authority.authority_type);
                }
            }
            else
            {
                return new Authority();
            }
            return authority;
        }

        public bool DeleteAuthority(int authority_id)
        {
            bool is_element_find = false;

            List<PackageObject> authority_list = GetAllAuthority();
            if (File.Exists(authority_db_path))
            {
                File.Delete(authority_db_path);
            }
            using (StreamWriter sw = File.CreateText(authority_db_path))
            {
                foreach (Authority element in authority_list)
                {
                    if (element.id != authority_id)
                    {
                        sw.WriteLine(element.id + ";" + element.authority_type);
                    }
                    else
                    {
                        is_element_find = true;
                    }
                }
            }

            return is_element_find;
        }

        public List<PackageObject> GetAllAuthority()
        {
            List<PackageObject> authority_list = new List<PackageObject>();
            if (File.Exists(authority_db_path))
            {
                using(StreamReader sr = File.OpenText(authority_db_path))
                {
                    string line = "";
                    string[] line_element;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_element = line.Split(';');
                        Authority authority = new Authority();
                        authority.id = Convert.ToInt32(line_element[0]);
                        authority.authority_type = line_element[1];
                        authority_list.Add(authority);
                    }
                }
                return authority_list;
            }
            else
            {
                return null;
            }
        }

        public Authority GetAuthorityById(int authority_id)
        {
            Authority authority = new Authority();
            List<PackageObject> authority_list = GetAllAuthority();
            if (authority_list != null)
            {
                if (authority_list!=null)
                {
                    foreach (Authority element in authority_list)
                    {
                        if (element.id == authority_id)
                        {
                            authority.id = authority_id;
                            authority.authority_type = element.authority_type;
                            return authority;
                        }
                    }
                }
                return authority;
            }
            else
            {
                return null;
            }
        }

        public Authority UpdateAuthority(Authority authority)
        {
            if (DeleteAuthority(authority.id) == true)
            {
                AddAuthority(authority);
                return authority;
            }
            return new Authority();
        }
    }
}