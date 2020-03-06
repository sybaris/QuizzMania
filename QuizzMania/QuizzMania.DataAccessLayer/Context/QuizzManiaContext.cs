using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizzMania.DataAccessLayer.Entities;

namespace QuizzMania.DataAccessLayer.Context
{
    /// <summary>
    /// Dbcontext
    /// </summary>
    public class QuizzManiaContext : DbContext
    {
        /// <summary>
        /// Contructeur
        /// </summary>
        /// <param name="options"></param>
        public QuizzManiaContext(DbContextOptions<QuizzManiaContext> options) : base(options)
        {

        }

        /// <summary>
        /// "Table" users
        /// </summary>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Connection string
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB;Trusted_Connection=True;Initial Catalog=QuizzMania;");
            }
        }

        public void InitDefaultValue()
        {
            Database.EnsureCreated();
            /*Users.RemoveRange(Users);*/

            if (!Users.Any())
            {
                Users.Add(new User() { FirstName = "User1" });
                Users.Add(new User() { FirstName = "User2" });
                Users.Add(new User() { FirstName = "User3" });
                Users.Add(new User() { FirstName = "User4" });
                Users.Add(new User() { FirstName = "User5" });
                Users.Add(new User() { FirstName = "User6" });
                Users.Add(new User() { FirstName = "User7" });
                Users.Add(new User() { FirstName = "User8" });
                Users.Add(new User() { FirstName = "Admin", IsAdmin = true });
            }
            SaveChanges();
        }
    }
}

