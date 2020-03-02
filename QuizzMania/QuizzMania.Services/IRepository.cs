using QuizzMania.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizzMania.Services
{
    public interface IRepository
    {
        UserDto GetUser(string firstname);
        IEnumerable<UserDto> GetPlayers();
        void AddUser(string firstname);
        void ClearUsers();
    }
}
