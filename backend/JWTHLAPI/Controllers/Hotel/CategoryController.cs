using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using JWTHLAPI.BusinessLayer.Manager.Hotel;
using JWTHLAPI.ModelLayer.DTO.Hotel;
using System.Threading.Tasks;
using JWTHLAPI.Helpers;
using JWTHLAPI.ModelLayer.Common;

namespace JWTHLAPI.Controllers.Hotel
{
    [Authorize]
    [ApiController]
    [Route("api/hotel/category")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryManager _manager;

        public CategoryController(CategoryManager manager)
        {
            _manager = manager;
        }

        [HttpPost("Create")]
        [HasPermission(FormNames.HotelCategory,PermissionType.Create)]
        public async Task<IActionResult> Create(CategoryCreateUpdateRequest request)
            => Ok(await _manager.Create(request));

        [HttpPost("Update")]
        [HasPermission(FormNames.HotelCategory, PermissionType.Update)]

        public async Task<IActionResult> Update(CategoryCreateUpdateRequest request)
            => Ok(await _manager.Update(request));

        [HttpDelete("Delete")]
        [HasPermission(FormNames.HotelCategory, PermissionType.Delete)]

        public async Task<IActionResult> Delete(int id)
            => Ok(await _manager.Delete(id));

        [HttpGet("GetAll")]
        [HasPermission(FormNames.HotelCategory, PermissionType.Read)]
        public async Task<IActionResult> GetAll()
            => Ok(await _manager.GetAll());

        [HttpGet("GetById")]
        [HasPermission(FormNames.HotelCategory, PermissionType.Read)]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _manager.GetById(id));
    }
}