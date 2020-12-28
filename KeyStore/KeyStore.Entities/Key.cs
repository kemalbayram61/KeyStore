using System;

namespace KeyStore.Entities
{
    public class Key
    {
        public int id { get; set; }

        public string creators_mac_address { get; set; }

        public string sent_mac_address { get; set; }

        public string get_date { get; set; }

        public string sent_date { get; set; }

        public int key_seccurity_degree { get; set; }

    }
}
