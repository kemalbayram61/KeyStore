using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeyStore.DataAccess.Abstract
{
    public interface IUsersRepository
    {
        List<Users> GetAllUsers();

        Users GetUsersById(int id);

        bool CreateUsers(Users users);

        bool UpdateUsers(Users users);

        bool DeleteUsers(int id);
    }
}
