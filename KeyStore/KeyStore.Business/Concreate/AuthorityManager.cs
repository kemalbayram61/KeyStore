using KeyStore.Business.Abstract;
using KeyStore.DataAccess.Abstract;
using KeyStore.DataAccess.Concreate;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.Business.Concreate
{
    public class AuthorityManager : IAuthorityServices
    {
        private IAuthorityRepository authority_repository;

        public AuthorityManager()
        {
            authority_repository = new AuthorityRepository();
        }

        public bool CreateAuthority(Authority authority)
        {
           return  authority_repository.CreateAuthority(authority);
        }

        public bool DeleteAuthority(int id)
        {
            return authority_repository.DeleteAuthority(id);
        }

        public List<Authority> GetAllAuthority()
        {
            return authority_repository.GetAllAuthority();
        }

        public Authority GetAuthorityById(int id)
        {
            return authority_repository.GetAuthorityById(id);
        }

        public bool UpdateAuthority(Authority authority)
        {
            return authority_repository.UpdateAuthority(authority);
        }
    }
}
