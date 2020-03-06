using QuizzMania.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizzMania.DataAccessLayer
{
    public interface IRepository
    {
        User GetUser(string firstname);
        IEnumerable<User> GetUsers();
        void AddUser(User user);
        void ClearUsers();
    }
}
