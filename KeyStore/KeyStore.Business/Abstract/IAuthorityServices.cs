using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.Business.Abstract
{
    public interface IAuthorityServices
    {
        List<Authority> GetAllAuthority();

        Authority GetAuthorityById(int id);

        bool CreateAuthority(Authority authority);

        bool UpdateAuthority(Authority authority);

        bool DeleteAuthority(int id);
    }
}
