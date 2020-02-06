using System.Linq;
using Microsoft.EntityFrameworkCore;

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

                if (!context.Users.Any())
                {
                    context.Users.Add(new User() { FirstName = "Nicolas" });
                    context.Users.Add(new User() { FirstName = "Thomas" });
                    context.Users.Add(new User() { FirstName = "Sergio" });
                    context.Users.Add(new User() { FirstName = "Bennoit" });
                    context.Users.Add(new User() { FirstName = "Colin" });
                    context.Users.Add(new User() { FirstName = "Anthonie" });
                    context.Users.Add(new User() { FirstName = "Rémi" });
                    context.Users.Add(new User() { FirstName = "Mamadou" });
                    context.Users.Add(new User() { FirstName = "Jean-pierre", IsAdmin = true });
                }
                context.SaveChanges();
            }
        }
    }
}
