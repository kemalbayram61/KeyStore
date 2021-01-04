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
    public class UserDataAccess : IUserDataAccess
    {
        private string user_db_path = @"C:\\Users\\Melih\\Desktop\\KeyStore-master\\KeyStore\\KeyStore\\DataAccess\\Database\\DBUser.txt";

        public int GetRandomForToken(int range = 200)
        {
            Random rnd = new Random();
            return rnd.Next(0, range);
        }

        public int GetUserId(string name, string surname)
        {
            List<PackageObject> user_list = GetAllUser();
            if (user_list != null)
            {
                foreach(User element in user_list)
                {
                    if(element.name == name && element.surname == surname)
                    {
                        return element.id;
                    }
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }

        public int GetLastId()
        {
            int max_id = 0;
            List<PackageObject> user_list = GetAllUser();
            if(user_list != null)
            {
                foreach(User element in user_list)
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

        public User AddUser(User user)
        {
            List<PackageObject> user_list = GetAllUser();
            if (user_list != null)
            {
                foreach (User element in user_list)
                {
                    if (element.id == user.id)
                    {
                        return new User();
                    }
                }
            }

            if (File.Exists(user_db_path))
            {
                File.Delete(user_db_path);
            }

            if (!File.Exists(user_db_path))
            {
                using (StreamWriter sw = File.CreateText(user_db_path))
                {
                    if (user_list != null)
                    {
                        foreach (User element in user_list)
                        {
                            sw.WriteLine(element.id.ToString() + ";" + element.name + ";" + element.surname + ";" + element.password + ";" + element.authority_id.ToString() + ";" + element.token.ToString());
                        }
                    }

                    sw.WriteLine(user.id.ToString() + ";" + user.name + ";" + user.surname + ";" + user.password + ";" + user.authority_id.ToString() + ";" + user.token.ToString());
                }
            }
            else
            {
                return new User();
            }
            return user;
        }

        public bool DeleteUser(int user_id)
        {
            bool is_element_find = false;

            List<PackageObject> user_list = GetAllUser();
            if (File.Exists(user_db_path))
            {
                File.Delete(user_db_path);
            }
            using (StreamWriter sw = File.CreateText(user_db_path))
            {
                foreach (User element in user_list)
                {
                    if (element.id != user_id)
                    {
                        sw.WriteLine(element.id.ToString() + ";" + element.name + ";" + element.surname + ";" + element.password + ";" + element.authority_id.ToString() + ";" + element.token.ToString());
                    }
                    else
                    {
                        is_element_find = true;
                    }
                }
            }

            return is_element_find;
        }

        public bool UserActive(int token)
        {
            List<PackageObject> user_list = GetAllUser();
            if (user_list != null)
            {
                foreach(User element in user_list)
                {
                    if (element.token == token) return true;
                }
            }
            return false;
        }

        public int GetUserAuthorityByToken(int token)
        {
            List<PackageObject> user_list = GetAllUser();
            if (user_list != null)
            {
                foreach (User element in user_list)
                {
                    if (element.token == token) return element.authority_id;
                }
            }
            return -1;
        }

        public User GetUserByToken(int token)
        {
            List<PackageObject> user_list = GetAllUser();
            if (user_list != null)
            {
                foreach (User element in user_list)
                {
                    if (element.token == token) return element;
                }
            }
            return null;
        }

        public List<PackageObject> GetAllUser()
        {
            List<PackageObject> user_list = new List<PackageObject>();
            if (File.Exists(user_db_path))
            {
                using (StreamReader sr = File.OpenText(user_db_path))
                {
                    string line = "";
                    string[] line_element;
                    while ((line = sr.ReadLine()) != null)
                    {
                        line_element = line.Split(';');
                        if (line_element.Length > 1)
                        {
                            User user = new User();
                            user.id = Convert.ToInt32(line_element[0]);
                            user.name = line_element[1];
                            user.surname = line_element[2];
                            user.password = line_element[3];
                            user.authority_id = Convert.ToInt32(line_element[4]);
                            user.token = Convert.ToInt32(line_element[5]);
                            user_list.Add(user);
                        }
                    }
                }
                return user_list;
            }
            else
            {
                return null;
            }
        }

        public User GetUserById(int user_id)
        {
            User user = new User();
            List<PackageObject> user_list = GetAllUser();
            if (user_list != null)
            {
                if (user_list != null)
                {
                    foreach (User element in user_list)
                    {
                        if (element.id == user_id)
                        {
                            user.id = user_id;
                            user.name = element.name;
                            user.surname = element.surname;
                            user.password = element.password;
                            user.authority_id = element.authority_id;
                            user.token = element.token;
                            return user;
                        }
                    }
                }
                return user;
            }
            else
            {
                return null;
            }
        }

        public User UpdateUser(User user)
        {
            if (DeleteUser(user.id) == true)
            {
                AddUser(user);
                return user;
            }
            return new User();
        }
    }
}