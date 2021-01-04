using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyStore.Entities
{
    public class Key : PackageObject
    {
        public int id=-1;

        public string creators_mac_address = "..";

        public string sent_mac_address = "..";

        public string get_date = "..";

        public string sent_date = "..";

        public int key_seccurity_degree=-1;

        public byte[] key_value;
    }
}