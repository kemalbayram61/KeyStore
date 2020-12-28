using KeyStore.DataAccess.Abstract;
using KeyStore.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KeyStore.DataAccess
{
    class UsersDBContext:IUsersRepository
    {
        private string users_db_path = "Database\\DBUsers.txt";
        private bool is_connected_db = false;

        public UsersDBContext()
        {
            if (File.Exists(users_db_path))
            {
                Console.WriteLine("Users Veritabanına Ulaşıldı...");
                is_connected_db = true;
            }
            else
            {
                Console.WriteLine("Users Veritabanı Belirtilen Konumda Bulunamıyor.!");
                is_connected_db = false;
            }
        }

        public bool CreateUsers(Users users)
        {
            if (is_connected_db)
            {
                string[] data = { users.id + ";" + users.name + ";" + users.surname + ";" + users.authority_id };
                File.AppendAllLines(users_db_path, data);
                return true;
            }
            else
            {
                return false;
            }
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
