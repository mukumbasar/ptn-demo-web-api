using PtnDemoProjectAPI.BLL.Dtos.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Dtos.Concrete.Building
{
    public class GetAllBuildingDto : IIdenftifiableDto
    {
        public string Id { get; set; }
        public int BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
        public string BuildingType { get; set; }
        public string BuildingTypeId { get; set; }
    }
}
