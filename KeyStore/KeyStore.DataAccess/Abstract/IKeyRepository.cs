using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.DataAccess.Abstract
{
    public interface IKeyRepository
    {
        List<Key> GetAllKeys();

        Key GetKeyById(int id);

        bool CreateKey(Key key);

        bool UpdateKey(Key key);

        bool DeleteKey(int id);
    }
}
