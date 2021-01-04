using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyStore.Entities
{
    public class Package
    {
        public int process_code = -1;
        public int user_token = -1;
        public PackageObject package_object = null;
        public List<PackageObject> package_object_list = null;
    }
}