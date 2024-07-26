using AutoMapper;
using PtnDemoProject.DAL.Repositories.Abstract;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;
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
    public class BuildingService : BaseMongoCrudService<CreateBuildingDto, UpdateBuildingDto, GetBuildingDto, GetAllBuildingDto, Building>, IBuildingService
    {
        public BuildingService(IBuildingRepository buildingRepository, IMapper mapper) : base(buildingRepository, mapper)
        {

        }
    }
}
