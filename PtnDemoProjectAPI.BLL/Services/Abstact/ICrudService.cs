using PtnDemoProjectAPI.BLL.Dtos.Abstract;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using PtnDemoProjectAPI.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Services.Abstact
{
    public interface ICrudService<TCreateDto, TUpdateDto, TGetDto, TGetAllDto, TEntityDto>
        where TCreateDto : class, IDto
        where TUpdateDto : class, IDto
        where TGetDto : class, IDto
        where TGetAllDto : class, IDto
    {
        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <param name="createDto">The creation data transfer object.</param>
        /// <returns>The result of the attempt</returns>
        Task<IResult> CreateAsync(TCreateDto createDto);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="updateDto">The update data transfer object.</param>
        /// <returns>The result of the attempt</returns>
        Task<IResult> UpdateAsync(TUpdateDto updateDto);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result of the attempt</returns>
        Task<IResult> DeleteAsync(string id);

        /// <summary>
        /// Retrieves an entity.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result of the attempt</returns>
        Task<IResult> GetByIdAsync(string id);

        /// <summary>
        /// Retrieves entities.
        /// </summary>
        /// <returns>The result of the attempt</returns>
        Task<IResult> GetAllAsync();
    }
}
