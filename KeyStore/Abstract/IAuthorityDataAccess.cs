using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStore.Abstract
{
    interface IAuthorityDataAccess
    {
        Authority AddAuthority(Authority authority);
        Authority GetAuthorityById(int authority_id);
        List<PackageObject> GetAllAuthority();
        Authority UpdateAuthority(Authority authority);
        bool DeleteAuthority(int authority_id);

    }
}
