using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyStore.Entities
{
    public class User : PackageObject
    {
        public int id=-1;

        public string name = "..";

        public string surname = "..";
        public string password = "..";

        public int authority_id = -1;
        public int token = -1;
    }
}