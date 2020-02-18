using System.Linq;
using Microsoft.EntityFrameworkCore;
using QuizzMania.Model;

namespace QuizzMania.Services.Context
{
    /// <summary>
    /// Dbcontext
    /// </summary>
    public class QuizzManiaContext : DbContext
    {
        /// <summary>
        /// DbSet users
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
            using (var context = new QuizzManiaContext())
            {
                context.Database.EnsureCreated();
                /*context.Users.RemoveRange(Users);*/

                if (!context.Users.Any())
                {
                    context.Users.Add(new User() { FirstName = "User1" });
                    context.Users.Add(new User() { FirstName = "User2" });
                    context.Users.Add(new User() { FirstName = "User3" });
                    context.Users.Add(new User() { FirstName = "User4" });
                    context.Users.Add(new User() { FirstName = "User5" });
                    context.Users.Add(new User() { FirstName = "User6" });
                    context.Users.Add(new User() { FirstName = "User7" });
                    context.Users.Add(new User() { FirstName = "User8" });
                    context.Users.Add(new User() { FirstName = "Admin", IsAdmin = true });
                }
                context.SaveChanges();
            }
        }
    }
}
