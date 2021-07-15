using BL.AppService;
using BL.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealsController : ControllerBase
    {
        private readonly MealAppService _mealAppService;

        public MealsController(MealAppService mealAppService)
        {
            _mealAppService = mealAppService;
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<MealDto>> GetMeals()
        {
            return _mealAppService.GetAllMeals();
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<MealDto> GetMeal(int id)
        {
            var brands = _mealAppService.GetMeal(id);

            if (brands == null)
            {
                return NotFound();
            }

            return brands;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutMeal(int id, MealDto MealDto)
        {
            try
            {
                _mealAppService.UpdateMeal(MealDto, id);

                return Ok(MealDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<MealDto> PostMeal(MealDto MealDto)
        {
            _mealAppService.CreateMeal(MealDto);
            return CreatedAtAction("GetMeals", MealDto);

        }
       // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteMeal(int id)
        {
            _mealAppService.DeleteMeal(id);
            return NoContent();
        }

        private bool MealExists(int id)
        {
            return _mealAppService.CheckMealExists(id);
        }
        

    }
}

