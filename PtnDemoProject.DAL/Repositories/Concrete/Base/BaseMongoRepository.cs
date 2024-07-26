using MongoDB.Bson;
using MongoDB.Driver;
using PtnDemoProject.DAL.Repositories.Abstract;
using PtnDemoProjectAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PtnDemoProject.DAL.Repositories.Concrete.Base
{
    public class BaseMongoRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly IMongoCollection<TEntity> _collection;

        public BaseMongoRepository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        public async Task CreateAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            await _collection.InsertOneAsync(entity);
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        public async Task UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity);
        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id">The id of the entity to be updated.</param>
        public async Task DeleteAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            await _collection.DeleteOneAsync(filter);
        }

        /// <summary>
        /// Retrieves entities.
        /// </summary>
        /// <returns>The entities.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _collection.AsQueryable().ToListAsync();
        }

        /// <summary>
        /// Retrieves an entity.
        /// </summary>
        /// <param name="id">The id of the entity to be retrieved.</param>
        /// <returns>The entity</returns>
        public async Task<TEntity> GetByIdAsync(string id)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Checks if any entities that match.
        /// </summary>
        /// <param name="expression">Optional filter predicate.</param>
        /// <returns>The match status.</returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
        {
            if (expression != null)
            {
                var filter = Builders<TEntity>.Filter.Where(expression);
                return await _collection.Find(filter).AnyAsync();
            }

            return await _collection.Find(Builders<TEntity>.Filter.Empty).AnyAsync();
        }
    }
}
