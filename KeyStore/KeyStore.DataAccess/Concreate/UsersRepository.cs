using System;
using System.Collections.Generic;
using KeyStore.DataAccess.Abstract;
using KeyStore.Entities;

namespace KeyStore.DataAccess.Concreate
{
    class UsersRepository : IUsersRepository
    {
        public bool CreateUsers(Users users)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUsers(int id)
        {
            throw new NotImplementedException();
        }

        public List<Users> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Users GetUsersById(int id)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUsers(Users users)
        {
            throw new NotImplementedException();
        }
    }
}
