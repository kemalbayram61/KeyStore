using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.DataAccess.Abstract
{
    public interface ISecurityDegreeRepository
    {
        List<SecurityDegree> GetAllSecurityDegree();

        SecurityDegree GetSecurityDegreeById(int id);

        bool CreateSecurityDegree(SecurityDegree security_degree);

        bool UpdateSecurityDegree(SecurityDegree security_degree);

        bool DeleteSecurityDegree(int id);
    }
}
