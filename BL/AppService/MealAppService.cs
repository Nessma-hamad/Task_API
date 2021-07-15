using AutoMapper;
using BL.Bases;
using BL.DTOs;
using BL.Interfaces;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppService
{
    public class MealAppService: BaseAppService
    {
        public MealAppService(IUnitOfWork theUnitOfWork) : base(theUnitOfWork)
        {

        }
        public List<MealDto> GetAllMeals()
        {
            return mapper.Map<List<MealDto>>(TheUnitOfWork.Meal.GetAllMeals());
        }
        public MealDto GetMeal(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            return mapper.Map<MealDto>(TheUnitOfWork.Meal.GetMealById(id));
        }
        public bool CreateMeal(MealDto mealdto)
        {
            if (mealdto == null)

                throw new ArgumentNullException();



            bool result = false;
            var meal = mapper.Map<meal>(mealdto);
            if (TheUnitOfWork.Meal.InsertMeal(meal))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool DeleteMeal(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            bool result = false;
            TheUnitOfWork.Meal.DeleteMeal(id);
            result = TheUnitOfWork.Commit() > new int();
            return result;
        }

        public bool CheckMealExists(int mealId)
        {
            var result = TheUnitOfWork.Meal.CheckMealExists(mealId);

            if (result)
            {
                return true;
            }
            return false;
        }
        public bool UpdateMeal(MealDto mealDto, int id)
        {
            var meal = TheUnitOfWork.Meal.GetMealById(id);
            meal.Name = mealDto.Name;
            TheUnitOfWork.Meal.UpdateMeal(meal);
            TheUnitOfWork.Commit();

            return true;
        }
       
       
    }
}

