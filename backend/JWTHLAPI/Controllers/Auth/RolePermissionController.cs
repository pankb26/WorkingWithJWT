using JWTHLAPI.BusinessLayer.Manager.Auth;
using JWTHLAPI.ModelLayer.Common;
using JWTHLAPI.ModelLayer.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JWTHLAPI.Controllers.Auth
{
    [Authorize(Roles = "Admin")]
    [Route("api/auth/role-permission")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly RolePermissionManager _manager;

        public RolePermissionController(RolePermissionManager manager)
        {
            _manager = manager;
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> Assign(RolePermissionCreateUpdateRequest request)
        {
            var result = await _manager.Assign(request);

            if (result == 0)
            {
                return Ok(new ApiResponse(
                    false,
                    "Permission already assigned for this role and form"
                ));
            }

            return Ok(new ApiResponse(
                true,
                "Permission assigned successfully"
            ));
        }

        [HttpGet("GetByRole")]
        public async Task<IActionResult> GetByRole(int roleId)
        {
            return Ok(await _manager.GetByRole(roleId));
        }

        [HttpPost("UpdateRole")]
        public async Task<IActionResult> Update(int id, RolePermissionCreateUpdateRequest request)
        {
            await _manager.Update(id, request);
            return Ok("Permission Updated");
        }

        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> Delete(int id)
        {
            await _manager.Delete(id);
            return Ok("Permission Removed");
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _manager.GetAll());

        }
    }
}