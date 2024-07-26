using PtnDemoProjectAPI.Entities.Concrete.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.Entities.Concrete.DbSets
{
    public class Building : BaseEntity
    {
        public int BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
        public string BuildingType { get; set; }
        public string BuildingTypeId { get; set; }
    }
}
