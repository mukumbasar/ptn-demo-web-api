using PtnDemoProjectAPI.BLL.Dtos.Concrete.Building;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Services.Abstact
{
    public interface IBuildingService : ICrudService<CreateBuildingDto, UpdateBuildingDto, GetBuildingDto, GetAllBuildingDto, Building>
    {

    }
}
