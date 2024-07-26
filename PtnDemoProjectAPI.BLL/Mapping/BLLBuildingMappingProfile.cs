using AutoMapper;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;
using PtnDemoProjectAPI.BLL.Dtos.Concrete.BuildingType;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Mapping
{
    public class BLLBuildingMappingProfile : Profile
    {
        public BLLBuildingMappingProfile() 
        {
            CreateMap<CreateBuildingDto, Building>();
            CreateMap<UpdateBuildingDto, Building>();
            CreateMap<Building, GetBuildingDto>();
            CreateMap<Building, GetAllBuildingDto>();

            CreateMap<CreateBuildingTypeDto, BuildingType>();
            CreateMap<UpdateBuildingTypeDto, BuildingType>();
            CreateMap<BuildingType, GetBuildingTypeDto>();
            CreateMap<BuildingType, GetAllBuildingTypeDto>();
        }
    }
}
