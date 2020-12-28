using System;
using System.Collections.Generic;
using System.Text;
using KeyStore.Entities;

namespace KeyStore.DataAccess.Abstract
{
    public interface IAuthorityRepository
    {
        List<Authority> GetAllAuthority();

        Authority GetAuthorityById(int id);

        bool CreateAuthority(Authority authority);

        bool UpdateAuthority(Authority authority);

        bool DeleteAuthority(int id);
    }
}
