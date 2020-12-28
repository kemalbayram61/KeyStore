using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyStore.Abstract
{
    interface IUserDataAccess
    {
        User AddUser(User user);
        List<User> GetAllUser();
        User GetUserById(int user_id);
        bool DeleteUser(int user_id);
        User UpdateUser(User user);

    }
}
