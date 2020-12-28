using KeyStore.Abstract;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace KeyStore.DataAccess
{
    public class AuthorityDataAccess : IAuthorityDataAccess
    {
        private string authority_db_path = @"Database\\DBAuthority.txt";
        public Authority AddAuthority(Authority authority)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAuthority(int authority_id)
        {
            throw new NotImplementedException();
        }

        public List<Authority> GetAllAuthority()
        {
            List<Authority> authority_list = new List<Authority>();
            if (File.Exists(authority_db_path))
            {
                string[] authority_lines = File.ReadAllLines(authority_db_path);
                for(int i = 0; i < authority_lines.Length; i++)
                {
                    string[] line_elements = authority_lines[i].Split(';');
                    Authority authority = new Authority();
                    authority.id = Convert.ToInt32(line_elements[0]);
                    authority.authority_type = line_elements[1];
                    authority_list.Add(authority);
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
            List<Authority> authority_list = GetAllAuthority();
            if (authority_list != null)
            {
                if (authority_list.Count > 0)
                {
                    for(int i = 0; i < authority_list.Count; i++)
                    {
                        if (authority_list[i].id == authority_id)
                        {
                            authority.id = authority_id;
                            authority.authority_type = authority_list[i].authority_type;
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
            throw new NotImplementedException();
        }
    }
}