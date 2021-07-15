using BL.Bases;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository
{
    public class MealRepository : BaseRepository<meal>
    {
        private DbContext EC_DbContext;
        public MealRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        public List<meal> GetAllMeals()
        {
            return GetAll().ToList();
        }

        public bool InsertMeal(meal meal)
        {
            return Insert(meal);
        }
        public void UpdateMeal(meal meal)
        {
            Update(meal);
        }
        public void DeleteMeal(int id)
        {
            Delete(id);
        }
        public bool CheckMealExists(int id)
        {
            return GetAny(b => b.ID == id);
        }
        public meal GetMealById(int id)
        {
            return GetFirstOrDefault(b => b.ID == id);
        }
    }
}

