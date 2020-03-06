using System;
using System.Collections.Generic;
using System.Text;

namespace QuizzMania.BusinessLogicLayer
{
    public interface IBusinessLayer
    {
        UserDto GetUser(string firstname);
        IEnumerable<UserDto> GetPlayers();
        void AddUser(string firstname);
        void ClearUsers();
    }
}
