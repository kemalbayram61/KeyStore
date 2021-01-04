using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyStore.Entities
{
    public class Image
    {
        public int id;
        public byte[,] image;
        public string user_id;
        public string history_of_use;
    }
}