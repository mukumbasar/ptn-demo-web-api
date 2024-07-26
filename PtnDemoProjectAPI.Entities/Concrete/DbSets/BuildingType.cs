using PtnDemoProjectAPI.Entities.Concrete.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.Entities.Concrete.DbSets
{
    public class BuildingType : BaseEntity
    {
        public string Name { get; set; }
    }
}
