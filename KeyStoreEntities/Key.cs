using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KeyStoreEntities
{
    public class Key
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [StringLength(50)]
        public string creators_mac_address { get; set; }

        [StringLength(50)]
        public string sent_mac_address { get; set; }

        [StringLength(50)]
        public string get_date { get; set; }

        [StringLength(50)]
        public string sent_date { get; set; }

        public int key_seccurity_degree { get; set; }

    }
}
