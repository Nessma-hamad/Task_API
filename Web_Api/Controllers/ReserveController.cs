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
    public class ReserveController : ControllerBase
    {
        private readonly ReseveAppService  _reserveAppService;

        public ReserveController(ReseveAppService reserveAppService)
        {
            _reserveAppService = reserveAppService;
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ReserveDto>> GetReserves()
        {
            return _reserveAppService.GetAllReservs();
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<ReserveDto> GetReserve(int id)
        {
            var brands = _reserveAppService.GetReserve(id);

            if (brands == null)
            {
                return NotFound();
            }

            return brands;
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult PutReserve(int id, ReserveDto ReserveDto)
        {
            try
            {
                _reserveAppService.UpdateReserve(ReserveDto, id);

                return Ok(ReserveDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<ReserveDto> PostReserve(ReserveDto ReserveDto)
        {
           if( _reserveAppService.CreateReserve(ReserveDto))
            {
                return Ok();
            }
           else
            {
                return BadRequest();
            }
           

        }
        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteReserve(int id)
        {
            _reserveAppService.DeleteReserve(id);
            return NoContent();
        }

        private bool ReserveExists(int id)
        {
            return _reserveAppService.CheckReserveExists(id);
        }

    }
}
