using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyStore.Entities
{
    public class Key
    {
        public int id=-1;

        public string creators_mac_address = null;

        public string sent_mac_address = null;

        public string get_date = null;

        public string sent_date = null;

        public int key_seccurity_degree=-1;

        public byte[] key_value;
    }
}