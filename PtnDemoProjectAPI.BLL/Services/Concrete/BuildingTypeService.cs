using AutoMapper;
using PtnDemoProject.DAL.Repositories.Abstract;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.BuildingType;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using PtnDemoProjectAPI.BLL.Results.Concrete;
using PtnDemoProjectAPI.BLL.Services.Abstact;
using PtnDemoProjectAPI.BLL.Services.Concrete.Base;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Services.Concrete
{
    public class BuildingTypeService : BaseMongoCrudService<CreateBuildingTypeDto, UpdateBuildingTypeDto, GetBuildingTypeDto, GetAllBuildingTypeDto, BuildingType>, IBuildingTypeService
    {
        private readonly IBuildingTypeRepository _buildingTypeRepository; 
        private readonly IBuildingRepository _buildingRepository;

        public BuildingTypeService(IBuildingTypeRepository buildingTypeRepository, 
            IBuildingRepository buildingRepository,
            IMapper mapper) 
            : base(buildingTypeRepository, mapper)
        {
            _buildingTypeRepository = buildingTypeRepository;
            _buildingRepository = buildingRepository;
        }

        /// <summary>
        /// Retrieves the building types which are not currently in use by any buildings.
        /// </summary>
        /// <returns>The available building types.</returns>
        public async Task<IResult> GetAllNotBuilt()
        {
            var allBuildings = await _buildingRepository.GetAllAsync();
            var allBuildingTypes = await _buildingTypeRepository.GetAllAsync();

            var inUseBuildingTypeIds = allBuildings.Select(x => x.BuildingTypeId);
            var notBuiltBuildingTypes = allBuildingTypes.Where(x => !inUseBuildingTypeIds.Contains(x.Id)).Select(x => x);

            if (notBuiltBuildingTypes.Any())
            {
                var remainingBuildingTypesDto = _mapper.Map<IEnumerable<GetAllBuildingTypeDto>>(notBuiltBuildingTypes);
                return new SuccessDataResult<IEnumerable<GetAllBuildingTypeDto>>("Retrieval success.", remainingBuildingTypesDto);
            }

            return new ErrorResult("No items to retrieve.");
        }
    }
}
