using System;
using System.Collections.Generic;
using System.Text;

namespace Todo.Domain.Interfaces
{
    /// <summary>
    /// Defines the basic operations for a generic repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity the repository is for.</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Gets an entity by its id.
        /// </summary>
        /// <param name="id">The id of the entity to retrieve.</param>
        /// <returns>The entity with the specified id.</returns>
        TEntity GetById(int id);

        /// <summary>
        /// Gets a list of all entities.
        /// </summary>
        /// <returns>A list of all entities.</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Adds an entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes an entity from the repository.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        void Delete(TEntity entity);
    }

}
