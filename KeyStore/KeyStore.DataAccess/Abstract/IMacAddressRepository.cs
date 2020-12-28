using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.DataAccess.Abstract
{
    public interface IMacAddressRepository
    {
        List<MacAddress> GetAllMacAddress();

        MacAddress GetMacAddressById(int id);

        bool CreateMacAddress(MacAddress mac_address);

        bool UpdateMacAddress(MacAddress mac_address);

        bool DeleteMacAddress(int id);
    }
}
