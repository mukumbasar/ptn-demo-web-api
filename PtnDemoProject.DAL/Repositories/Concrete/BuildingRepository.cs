using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using PtnDemoProject.DAL.Constants;
using PtnDemoProject.DAL.Repositories.Abstract;
using PtnDemoProject.DAL.Repositories.Concrete.Base;
using PtnDemoProjectAPI.Entities.Concrete.DbSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProject.DAL.Repositories.Concrete
{
    public class BuildingRepository : BaseMongoRepository<Building>, IBuildingRepository
    {
        public BuildingRepository(IMongoDatabase database) : base(database, MongoCollection.buildingCollectionName)
        {

        }
    }
}
