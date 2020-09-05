using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomashkaClientBase.Model
{
    public static class UsersManager
    {
        public static void AddUser(User user)
        {
            using (var context = new ClientBaseContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public static void DeleteUser(User user)
        {
            using (var context = new ClientBaseContext())
            {
                var userEntity = context.Users.First(u => u.Id == user.Id);

                context.Users.Remove(userEntity);
                context.SaveChanges();
            }
        }

        public static void UpdateUser(User user)
        {
            using (var context = new ClientBaseContext())
            {
                var userEntity = context.Users.Find(user.Id);

                context.Entry(userEntity).CurrentValues.SetValues(user);
                context.SaveChanges();
            }
        }

        public static IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> users = null;

            using (var context = new ClientBaseContext())
                users = context.Users.ToArray(); 
            
            return users;
        }

        public static IEnumerable<User> GetUsersByCompany(Company company)
        {
            IEnumerable<User> users = null;

            using (var context = new ClientBaseContext())
                users = context.Users.Where(u => u.CompanyId == company.Id).ToArray();
            
            return users;
        }
    }
}
