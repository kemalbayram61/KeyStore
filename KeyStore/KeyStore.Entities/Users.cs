using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.Entities
{
    public class Users
    {
        public int id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public int authority_id { get; set; }
    }
}
