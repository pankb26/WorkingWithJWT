using JWTHLAPI.DataLayer.Interfaces.Auth;
using JWTHLAPI.ModelLayer.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace JWTHLAPI.Controllers.Auth
{

    [Authorize]
    [ApiController]
    [Route("api/auth/user-permission")]
    public class UserPermissionController : ControllerBase
    {
        private readonly IRolePermissionRepository _repo;

        public UserPermissionController(IRolePermissionRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("myPermissions")]
        public async Task<IActionResult> GetMyPermissions()
        {
            var roleId = int.Parse(User.FindFirst("roleId").Value);

            var data = await _repo.GetByRole(roleId);

            var result = data.Select(x => new
            {
                x.FormName,
                x.CanRead,
                x.CanCreate,
                x.CanUpdate,
                x.CanDelete
            });

            return Ok(new ApiResponse(true, "Success", result));
        }
    }
}
