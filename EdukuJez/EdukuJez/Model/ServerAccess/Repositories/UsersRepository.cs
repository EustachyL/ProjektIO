using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace EdukuJez.Repositories
{
    public class UsersRepository : ARepository<User>
    {

        public UsersRepository()
        {
            Table = Context.Users;
        }
        public bool CheckLogin(string login, string password)
        {
            return Table.Any(x => x.UserLogin == login && x.UserPassword == password);
        }
        public User GetByLogin(string login)
        {
            return Table.First(x => x.UserLogin == login);
        }

        public bool IsLoginInDatabase(string login)
        {
            return Table.Any(x => x.UserLogin == login);

        }
        public override void Delete(User user)
        {
            if (user == null)
                return;

            var groupUsers = Context.GroupUsers.Where(gu => gu.User.Id == user.Id).ToList();
            Context.GroupUsers.RemoveRange(groupUsers);

            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}