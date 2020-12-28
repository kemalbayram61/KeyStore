using KeyStore.DataAccess.Abstract;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.DataAccess.Concreate
{
    public class AuthorityRepository : IAuthorityRepository
    {
        private AuthorityDBContext authority_db_context = new AuthorityDBContext();

        public bool CreateAuthority(Authority authority)
        {
            return authority_db_context.CreateAuthority(authority);
        }

        public bool DeleteAuthority(int id)
        {
            return authority_db_context.DeleteAuthority(id);
        }

        public List<Authority> GetAllAuthority()
        {
            return authority_db_context.GetAllAuthority();
        }

        public Authority GetAuthorityById(int id)
        {
            return authority_db_context.GetAuthorityById(id);
        }

        public bool UpdateAuthority(Authority authority)
        {
            return authority_db_context.UpdateAuthority(authority);
        }
    }
}
