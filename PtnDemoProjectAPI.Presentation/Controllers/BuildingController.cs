using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;
using PtnDemoProjectAPI.BLL.Services.Abstact;

namespace PtnDemoProjectAPI.Presentation.Controllers
{
    [Route("buildings")]
    public class BuildingController : Controller
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        /// <summary>
        /// Creates a building.
        /// </summary>
        /// <param name="createBuildingDto">The creation data transfer object.</param>
        /// <returns>The result of the attempt</returns>
        [HttpPost]
        [Authorize(Roles = "AppUser, Admin")]
        public async Task<IActionResult> CreateAsync(CreateBuildingDto createBuildingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _buildingService.CreateAsync(createBuildingDto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Deletes a building.
        /// </summary>
        /// <param name="id">The creation data transfer object.</param>
        /// <returns>The result of the attempt</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "AppUser, Admin")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _buildingService.DeleteAsync(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Gets all the buildings.
        /// </summary>
        /// <returns>The result of the attempt</returns>
        [HttpGet]
        [Authorize(Roles = "AppUser, Admin")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _buildingService.GetAllAsync();
            return result.IsSuccess ? Ok(result) : NotFound(result);
        }
    }
}
