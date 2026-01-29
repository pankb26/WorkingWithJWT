using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JWTHLAPI.BusinessLayer.Manager.Hotel;
using JWTHLAPI.ModelLayer.DTO.Hotel;
using System.Threading.Tasks;

namespace JWTHLAPI.Controllers.Hotel
{
    [Authorize]
    [ApiController]
    [Route("api/hotel/counter")]
    public class CounterController : ControllerBase
    {
        private readonly CounterManager _manager;

        public CounterController(CounterManager manager)
        {
            _manager = manager;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CounterCreateUpdateRequest request)
            => Ok(await _manager.Create(request));

        [HttpPost("Update")]
        public async Task<IActionResult> Update(CounterCreateUpdateRequest request)
            => Ok(await _manager.Update(request));

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _manager.Delete(id));

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _manager.GetAll());

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _manager.GetById(id));
    }
}