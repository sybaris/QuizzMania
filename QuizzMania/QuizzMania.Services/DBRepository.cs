using QuizzMania.Model;
using QuizzMania.Services.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QuizzMania.Services
{
    public class DBRepository : IRepository
    {
        private QuizzManiaContext _quizzManiaContext;

        public DBRepository(QuizzManiaContext quizzManiaContext)
        {
            _quizzManiaContext = quizzManiaContext;
        }

        public IEnumerable<User> GetPlayers()
        {
            var q = from user in _quizzManiaContext.Users
                    where !user.IsAdmin
                    orderby user.FirstName
                    select user;
            //select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            var players = q.ToList();
            return players;
        }

        public User GetUser(string firstname)
        {
            var q = from user in _quizzManiaContext.Users
                    where user.FirstName.ToLower() == firstname.ToLower()
                    select user;
            //select new UsersViewModel() { FirstName = user.FirstName, Id = user.Id };
            if (q.Count() != 1)
                throw new Exception($"L'utilisateur {firstname} n'a pas été trouvé");
            return q.Single();
        }
    }
}
