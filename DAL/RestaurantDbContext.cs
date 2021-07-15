using DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class RestaurantDbContext : IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
            .UseSqlServer(@"Data Source=.;Initial Catalog=Restaurant;Integrated Security=True"
            , options => options.EnableRetryOnFailure());
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public RestaurantDbContext()
        {

        }
        public RestaurantDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<meal> meals { get; set; }
        public DbSet<reserve> reserves { get; set; }
    }
    public class ApplicationUserStore : UserStore<User>
    {
        public ApplicationUserStore() : base(new RestaurantDbContext())
        {

        }
        public ApplicationUserStore(DbContext db) : base(db)
        {

        }
    }
}
