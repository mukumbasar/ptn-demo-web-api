using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.BuildingType;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Services.Abstact
{
    public interface IBuildingTypeService : ICrudService<CreateBuildingTypeDto, UpdateBuildingTypeDto, GetBuildingTypeDto, GetAllBuildingTypeDto, BuildingType>
    {
        /// <summary>
        /// Retrieves the building types which are not currently in use by any buildings.
        /// </summary>
        /// <returns>The available building types.</returns>
        Task<IResult> GetAllNotBuilt();
    }
}
