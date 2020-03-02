﻿using QuizzMania.Model;
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

        public void AddUser(string firstname)
        {
            _quizzManiaContext.Users.Add(new User() { FirstName = firstname });
            _quizzManiaContext.SaveChanges();
        }

        public void ClearUsers()
        {
            // Attention aux performances si beaucoup de users : https://stackoverflow.com/questions/15220411/entity-framework-delete-all-rows-in-table
            _quizzManiaContext.Users.RemoveRange(_quizzManiaContext.Users);
            _quizzManiaContext.SaveChanges();
        }

        public IEnumerable<UserDto> GetPlayers()
        {
            var q = from user in _quizzManiaContext.Users
                    where !user.IsAdmin
                    orderby user.FirstName
                    //select user;
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
