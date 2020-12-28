using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStore.Abstract
{
    interface IKeyDataAccess
    {
        Key AddKey(Key key);
        List<Key> GetAllKey();
        Key GetKeyById(int key_id);
        bool DeleteKey(int key_id);
        Key UpdateKey(Key key);
    }
}
