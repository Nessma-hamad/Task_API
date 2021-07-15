using BL.Interfaces;
using BL.Repository;
using DAL;
using DAL.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class UnitOfWork : IUnitOfWork
    {
        private RestaurantDbContext EC_DbContext { get; set; }
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public UnitOfWork(RestaurantDbContext EC_DbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.EC_DbContext = EC_DbContext;
        }
        public int Commit()
        {
            return EC_DbContext.SaveChanges();
        }
        public void Dispose()
        {
            EC_DbContext.Dispose();
        }

        public AccountRepository account;
        public AccountRepository Account
        {
            get
            {
                if (account == null)
                    account = new AccountRepository(EC_DbContext, _userManager, _roleManager);
                return account;
            }
        }

        public MealRepository meal;
        public MealRepository Meal
        {
            get
            {
                if (meal == null)
                    meal = new MealRepository(EC_DbContext);
                return meal;
            }
        }
       
    }
}
