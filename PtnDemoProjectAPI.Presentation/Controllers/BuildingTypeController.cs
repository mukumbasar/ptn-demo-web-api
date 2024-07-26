using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;
using PtnDemoProjectAPI.BLL.Services.Abstact;

namespace PtnDemoProjectAPI.Presentation.Controllers
{
    [Route("building-types")]
    public class BuildingTypeController : Controller
    {
        private readonly IBuildingTypeService _buildingTypeService;

        public BuildingTypeController(IBuildingTypeService buildingTypeService)
        {
            _buildingTypeService = buildingTypeService;
        }

        /// <summary>
        /// Gets all not built building types.
        /// </summary>
        /// <returns>The result of the attempt</returns>
        [HttpGet("not-built")]
        [Authorize(Roles = "AppUser, Admin")]
        public async Task<IActionResult> GetAllNotBuiltAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _buildingTypeService.GetAllNotBuilt();
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
    }
}
