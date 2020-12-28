using KeyStore.DataAccess.Abstract;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KeyStore.DataAccess
{
    class AuthorityDBContext : IAuthorityRepository
    {
        private string authority_db_path = "Database\\DBAuthority.txt";
        private bool is_connected_db = false;

        public AuthorityDBContext()
        {
            if (File.Exists(authority_db_path))
            {
                Console.WriteLine("Authority Veritabanına Ulaşıldı...");
                is_connected_db = true;
            }
            else
            {
                Console.WriteLine("Authority Veritabanı Belirtilen Konumda Bulunamıyor.!");
                is_connected_db = false;
            }
        }

        public bool CreateAuthority(Authority authority)
        {
            if (is_connected_db && GetAuthorityById(authority.id)==null)
            {
                string[] data = { authority.id.ToString() + ";" + authority.authority_type };
                File.AppendAllLines(authority_db_path, data);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteAuthority(int id)
        {
            if (is_connected_db && GetAuthorityById(id) != null)
            {
                List<Authority> authorities = GetAllAuthority();
                List<Authority> new_authorities = new List<Authority>();
                for (int i = 0; i < authorities.Capacity; i++)
                {
                    if (authorities[i].id != id)
                    {
                        Authority authority = new Authority();
                        authority.id = authorities[i].id;
                        authority.authority_type = authorities[i].authority_type;
                        new_authorities.Add(authority);
                    }
                }
                File.Delete(authority_db_path);
                File.AppendAllLines(authority_db_path, GetAuthoryLines(new_authorities));
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Authority> GetAllAuthority()
        {
            if (is_connected_db)
            {
                List<Authority> authorities = new List<Authority>();
                string[] file_data = File.ReadAllLines(authority_db_path);

                for(int i = 0; i < file_data.Length; i++)
                {
                    Authority authority = new Authority();
                    string[] parse = file_data[i].Split(';');
                    authority.id = Convert.ToInt32(parse[0]);
                    authority.authority_type = parse[1];
                    authorities.Add(authority);
                }
                return authorities;
            }
            else
            {
                return null;
            }
            
        }

        public Authority GetAuthorityById(int id)
        {
            if (is_connected_db)
            {
                Authority authority = new Authority();
                string[] file_data = File.ReadAllLines(authority_db_path);

                for (int i = 0; i < file_data.Length; i++)
                {
                    string[] parse = file_data[i].Split(';');
                    if(id == Convert.ToInt32(parse[0]))
                    {
                        authority.authority_type = parse[1];
                        authority.id = id;
                        return authority;
                    }
                }
            }
            return null;
        }

        private string[] GetAuthoryLines(List<Authority> authory_list)
        {
            string[] authories = new string[authory_list.Capacity];
            for(int i = 0; i < authory_list.Capacity; i++)
            {
                authories[i] = authory_list[i].id.ToString() + ";" + authory_list[i].authority_type;
            }
            return authories;
        }

        public bool UpdateAuthority(Authority authority)
        {
            if (is_connected_db && GetAuthorityById(authority.id)!=null)
            {
                List<Authority> authorities = GetAllAuthority();
                for (int i = 0; i < authorities.Capacity; i++)
                {
                    if(authorities[i].id == authority.id)
                    {
                        authorities[i].authority_type = authority.authority_type;
                        break;
                    }
                }
                File.Delete(authority_db_path);
                File.AppendAllLines(authority_db_path, GetAuthoryLines(authorities));
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
