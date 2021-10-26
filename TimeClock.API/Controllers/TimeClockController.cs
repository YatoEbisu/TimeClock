using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TimeClock.API.DTOs;
using TimeClock.API.Interfaces;

namespace TimeClock.API.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class TimeClockController : ControllerBase
    {
        private readonly ITimeClockService _service;
        public TimeClockController(ITimeClockService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _service.Get());
       
        [HttpGet("id:guid")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _service.Get(id));
       
        [HttpPost]
        public async Task<IActionResult> Create(CreateTimeClockDTO timeClockDTO)
        {
            if(timeClockDTO == null) return NotFound();
            await _service.Add(timeClockDTO);
            return Created(nameof(Create), timeClockDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTimeClockDTO timeClockDTO)
        {
            await _service.Update(timeClockDTO);
            return Ok();
        }

        [HttpDelete("id:guid")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.Delete(id);
            return Ok();
        }
    }
}
