using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;

namespace TestProject.Application.Core.Repository
{
    public interface IRepositoryBase<TEntity, IId>
        where TEntity : IEntityBase<IId>
    {
        IDbContext<TEntity, IId> DbContext { get; }

        Task<IList<TEntity>> GetListAsync();

        Task<TEntity> GetAsync(IId id);

        Task CreateAsync(TEntity entity);

        Task EditAsync(TEntity entity);

        Task DeleteAsync(IId id);
    }
}