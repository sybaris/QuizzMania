using QuizzMania.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace QuizzMania.Services
{
    public class MemoryRepository : IRepository
    {
        private readonly List<User> _users = new List<User>();

        public void AddUser(string firstname)
        {
            _users.Add(new User() { FirstName = firstname});
        }

        public void ClearUsers()
        {
            _users.Clear();
        }

        public IEnumerable<UserDto> GetPlayers()
        {
            var q = from user in _users
                    where !user.IsAdmin
                    orderby user.FirstName
                    select new UserDto()
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        Email = user.Email,
                        IsAdmin = user.IsAdmin,
                        LastName = user.LastName
                    };
            var players = q.ToList();
            return players;
        }

        public UserDto GetUser(string firstname)
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
            // TODO JPP : Use Automapper
            UserDto userDto = new UserDto()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                LastName = user.LastName
            };
            return userDto;
        }
    }
}
