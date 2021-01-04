using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStore.Abstract
{
    interface IMacAddressDataAccess
    {
        MacAddress AddMacAddress(MacAddress mac_address);
        MacAddress GetMacAddressById(int mac_address_id);
        List<PackageObject> GetAllMacAddress();
        MacAddress UpdateMacAddress(MacAddress mac_address);
        bool DeleteMacAddress(int mac_address_id);
    }
}
