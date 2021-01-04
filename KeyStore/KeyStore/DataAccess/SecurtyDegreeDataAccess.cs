using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using KeyStore.Abstract;
using KeyStore.Entities;

namespace KeyStore.DataAccess
{
    [Serializable]
    public class SecurtyDegreeDataAccess : ISecurityDegreeDataAccess
    {
        private string security_degree_db_path = @"C:\\Users\\mhd\\Downloads\\KeyStore-master\\KeyStore-master\\KeyStore\\DataAccess\\Database\\DBSecurityDegree.txt";

        public SecurityDegree AddSecurityDegree(SecurityDegree security_degree)
        {
            List<SecurityDegree> security_degree_list = GetAllSecurityDegree();
            if (security_degree_list != null)
            {
                foreach (SecurityDegree element in security_degree_list)
                {
                    if (element.id == security_degree.id)
                    {
                        return new SecurityDegree();
                    }
                }
            }

            if (File.Exists(security_degree_db_path))
            {
                File.Delete(security_degree_db_path);
            }

            if (!File.Exists(security_degree_db_path))
            {
                using (StreamWriter sw = File.CreateText(security_degree_db_path))
                {
                    if (security_degree_list != null)
                    {
                        foreach (SecurityDegree element in security_degree_list)
                        {
                            sw.WriteLine(element.id.ToString() + ";" + element.degree_name);
                        }
                    }

                    sw.WriteLine(security_degree.id.ToString() + ";" + security_degree.degree_name);
                }
            }
            else
            {
                return new SecurityDegree();
            }
            return security_degree;
        }

        public bool DeleteSecurityDegree(int security_degree_id)
        {
            bool is_element_find = false;

            List<SecurityDegree> security_degree_list = GetAllSecurityDegree();
            if (File.Exists(security_degree_db_path))
            {
                File.Delete(security_degree_db_path);
            }
            using (StreamWriter sw = File.CreateText(security_degree_db_path))
            {
                foreach (SecurityDegree element in security_degree_list)
                {
                    if (element.id != security_degree_id)
                    {
                        sw.WriteLine(element.id.ToString() + ";" + element.degree_name);
                    }
                    else
                    {
                        is_element_find = true;
                    }
                }
            }

            return is_element_find;
        }

        public List<SecurityDegree> GetAllSecurityDegree()
        {
            List<SecurityDegree> security_degree_list = new List<SecurityDegree>();
            if (File.Exists(security_degree_db_path))
            {
                using (StreamReader sr = File.OpenText(security_degree_db_path))
                {
                    string line = "";
                    string[] line_element;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_element = line.Split(';');
                        SecurityDegree security_degree = new SecurityDegree();
                        security_degree.id = Convert.ToInt32(line_element[0]);
                        security_degree.degree_name = line_element[1];
                        security_degree_list.Add(security_degree);
                    }
                }
                return security_degree_list;
            }
            else
            {
                return null;
            }
        }

        public SecurityDegree GetSecurityDegreeById(int security_degree_id)
        {
            SecurityDegree security_degree = new SecurityDegree();
            List<SecurityDegree> security_degree_list = GetAllSecurityDegree();
            if (security_degree_list != null)
            {
                if (security_degree_list != null)
                {
                    foreach (SecurityDegree element in security_degree_list)
                    {
                        if (element.id == security_degree_id)
                        {
                            security_degree.id = security_degree_id;
                            security_degree.degree_name = element.degree_name;
                            return security_degree;
                        }
                    }
                }
                return security_degree;
            }
            else
            {
                return null;
            }
        }

        public SecurityDegree UpdateSecurityDegree(SecurityDegree security_degree)
        {
            if (DeleteSecurityDegree(security_degree.id) == true)
            {
                AddSecurityDegree(security_degree);
                return security_degree;
            }
            return new SecurityDegree();
        }
    }
}