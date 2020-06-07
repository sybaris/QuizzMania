using QuizzMania.DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using QuizzMania.DataAccessLayer.Entities;

namespace QuizzMania.DataAccessLayer
{
    public class DBRepository : IRepository
    {
        private readonly QuizzManiaContext _quizzManiaContext;

        public DBRepository(QuizzManiaContext quizzManiaContext)
        {
            _quizzManiaContext = quizzManiaContext;
        }

        public void AddUser(User user)
        {
            _quizzManiaContext.Users.Add(user);
            _quizzManiaContext.SaveChanges();
        }

        public void ClearUsers()
        {
            // Attention aux performances si beaucoup de users : https://stackoverflow.com/questions/15220411/entity-framework-delete-all-rows-in-table
            _quizzManiaContext.Users.RemoveRange(_quizzManiaContext.Users);
            _quizzManiaContext.SaveChanges();
        }

        public User GetUser(string firstname)
        {
            var q = from u in _quizzManiaContext.Users
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
            return _quizzManiaContext.Users;
        }
    }
}
