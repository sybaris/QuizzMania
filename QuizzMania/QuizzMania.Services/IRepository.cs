using QuizzMania.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizzMania.Services
{
    public interface IRepository
    {
        User GetUser(string firstname);
        IEnumerable<User> GetPlayers();
    }
}
