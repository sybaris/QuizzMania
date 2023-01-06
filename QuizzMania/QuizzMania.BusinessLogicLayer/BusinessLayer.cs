using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuizzMania.DataAccessLayer;

namespace QuizzMania.BusinessLogicLayer
{
    public class BusinessLayer : IBusinessLayer
    {
        private readonly IRepository _repository;
        public BusinessLayer(IRepository repository)
        {
            _repository = repository;
        }

        public void AddUser(string firstname)
        {
            _repository.AddUser(new DataAccessLayer.Entities.User() { FirstName = firstname });
        }

        public void ClearUsers()
        {
            _repository.ClearUsers();
        }

        public bool ExistUser(string firstname)
        {
            return _repository.ExistUser(firstname);
        }

        public IEnumerable<UserDto> GetPlayers()
        {
            var users = _repository.GetUsers();
            var q = from user in users
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
            var user = _repository.GetUser(firstname);

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
