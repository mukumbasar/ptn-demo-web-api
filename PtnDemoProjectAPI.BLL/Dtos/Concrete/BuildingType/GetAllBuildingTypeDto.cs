using PtnDemoProjectAPI.BLL.Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Dtos.Concrete.BuildingType
{
    public class GetAllBuildingTypeDto : IIdenftifiableDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
