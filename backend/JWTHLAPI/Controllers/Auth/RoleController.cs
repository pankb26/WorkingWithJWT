using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JWTHLAPI.BusinessLayer.Manager.Auth;
using JWTHLAPI.ModelLayer.Common;
using System.Threading.Tasks;

namespace JWTHLAPI.Controllers.Auth
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/auth/roles")]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager _manager;

        public RoleController(RoleManager manager)
        {
            _manager = manager;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _manager.GetAll();
            return Ok(new ApiResponse(true, "Success", data));
        }
    }
}