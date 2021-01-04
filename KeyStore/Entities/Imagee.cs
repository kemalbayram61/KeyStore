using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KeyStore.Entities
{
    public class Imagee : PackageObject
    {
        public int id = -1;
        public byte[,] image_values = { { 1, 2, 3 }, { 4, 5, 6 } };
        public int user_id = -1;
    }
}