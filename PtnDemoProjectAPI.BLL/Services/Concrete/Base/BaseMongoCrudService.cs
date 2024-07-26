using AutoMapper;
using PtnDemoProject.DAL.Repositories.Abstract;
using PtnDemoProjectAPI.BLL.Dtos.Abstract;
using PtnDemoProjectAPI.BLL.Results.Abstract;
using PtnDemoProjectAPI.BLL.Results.Concrete;
using PtnDemoProjectAPI.BLL.Services.Abstact;
using PtnDemoProjectAPI.Entities.Abstract;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PtnDemoProjectAPI.BLL.Services.Concrete.Base
{
    public class BaseMongoCrudService<TCreateDto, TUpdateDto, TGetDto, TGetAllDto, TEntity> : ICrudService<TCreateDto, TUpdateDto, TGetDto, TGetAllDto, TEntity> 
        where TCreateDto : class, INonIdentifiableDto
        where TUpdateDto : class, IIdenftifiableDto
        where TGetDto : class, IIdenftifiableDto
        where TGetAllDto : class, IIdenftifiableDto
        where TEntity : class, IEntity
    {
        protected readonly IRepository<TEntity> _repository;
        protected readonly IMapper _mapper;

        public BaseMongoCrudService(IRepository<TEntity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates an entity.
        /// </summary>
        /// <param name="createDto">The creation data transfer object.</param>
        /// <returns>The result of the attempt</returns>
        public virtual async Task<IResult> CreateAsync(TCreateDto createDto)
        {

            var mappedEntity = _mapper.Map<TEntity>(createDto);
            await _repository.CreateAsync(mappedEntity);

            return new SuccessResult("Creation success.");

        }

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result of the attempt</returns>
        public virtual async Task<IResult> DeleteAsync(string id)
        {
            var result = await _repository.AnyAsync(e => e.Id == id);

            if (result)
            {
                await _repository.DeleteAsync(id);
                return new SuccessResult("Deletion success.");
            }

            return new ErrorResult("No item to delete.");
        }

        /// <summary>
        /// Retrieves entities.
        /// </summary>
        /// <returns>The result of the attempt</returns>
        public virtual async Task<IResult> GetAllAsync()
        {
            var results = await _repository.GetAllAsync();
            var mappedDto = _mapper.Map<IEnumerable<TGetAllDto>>(results);

            if (mappedDto.Any())
            {
                return new SuccessDataResult<IEnumerable<TGetAllDto>>("Retrieval success.", mappedDto);
            }

            return new ErrorResult("No items to retrieve.");
        }

        /// <summary>
        /// Retrieves an entity.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result of the attempt</returns>
        public virtual async Task<IResult> GetByIdAsync(string id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result != null)
            {
                var mappedDto = _mapper.Map<TGetDto>(result);
                return new SuccessDataResult<TGetDto>("Retrieval success.", mappedDto);
            }

            return new ErrorResult("No item to retrieve.");
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="updateDto">The update data transfer object.</param>
        /// <returns>The result of the attempt</returns>
        public virtual async Task<IResult> UpdateAsync(TUpdateDto updateDto)
        {
            var result = await _repository.AnyAsync(e => e.Id == updateDto.Id);

            if (result)
            {
                var mappedEntity = _mapper.Map<TEntity>(updateDto);
                await _repository.UpdateAsync(mappedEntity);

                return new SuccessResult("Update success.");
            }

            return new SuccessResult("No item to update.");
        }
    }
}
