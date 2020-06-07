using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuizzMania.DataAccessLayer.Entities;

namespace QuizzMania.DataAccessLayer
{
    public class MemoryRepository : IRepository
    {
        private readonly List<User> _users = new List<User>();

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void ClearUsers()
        {
            _users.Clear();
        }

        public bool ExistUser(string firstname)
        {
            var q = from u in _users
                    where u.FirstName.ToLower() == firstname.ToLower()
                    select u;
            return q.Count() == 1;
        }

        public User GetUser(string firstname)
        {
            var q = from u in _users
                    where u.FirstName.ToLower() == firstname.ToLower()
                    select u;
            //select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            var userCount = q.Count();
            if (userCount < 1)
                throw new Exception($"L'utilisateur {firstname} n'a pas été trouvé");
            if (userCount > 1)
                throw new Exception($"L'utilisateur {firstname} n'est pas unique");

            User user = q.Single();
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }
}
