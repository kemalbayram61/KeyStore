using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStore.Abstract
{
    interface ISecurityDegreeDataAccess
    {
        SecurityDegree AddSecurityDegree(SecurityDegree security_degree);
        List<SecurityDegree> GetAllSecurityDegree();
        SecurityDegree UpdateSecurityDegree(SecurityDegree security_degree);
        SecurityDegree GetSecurityDegreeById(int security_degree_id);
        bool DeleteSecurityDegree(int security_degree_id);
    }
}
