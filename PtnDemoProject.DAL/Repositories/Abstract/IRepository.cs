using PtnDemoProjectAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProject.DAL.Repositories.Abstract
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <param name="entity">The entity to be created.</param>
        Task CreateAsync(TEntity entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id">The id of the entity to be updated.</param>
        Task DeleteAsync(string id);

        /// <summary>
        /// Retrieves an entity.
        /// </summary>
        /// <param name="id">The id of the entity to be retrieved.</param>
        /// <returns>The entity</returns>
        Task<TEntity> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves entities.
        /// </summary>
        /// <returns>The entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Checks if any entities that match.
        /// </summary>
        /// <param name="expression">Optional filter predicate.</param>
        /// <returns>The match status.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null);
    }
}
