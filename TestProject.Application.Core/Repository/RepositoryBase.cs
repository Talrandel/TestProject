using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;

namespace TestProject.Application.Core.Repository
{
    public abstract class RepositoryBase<TEntity, IId> : IRepositoryBase<TEntity, IId>
        where TEntity : IEntityBase<IId>
    {
        public RepositoryBase(IDbContext<TEntity, IId> context)
        {
            DbContext = context;
        }

        public IDbContext<TEntity, IId> DbContext { get; }

        public virtual async Task CreateAsync(TEntity entity)
        {
            await DbContext.CreateAsync(entity).ConfigureAwait(false);
        }

        public virtual async Task DeleteAsync(IId id)
        {
            await DbContext.DeleteAsync(id).ConfigureAwait(false);
        }

        public virtual async Task EditAsync(TEntity entity)
        {
            await DbContext.CreateAsync(entity).ConfigureAwait(false);
        }

        public virtual async Task<TEntity> GetAsync(IId id)
        {
            return await DbContext.GetAsync(id).ConfigureAwait(false);
        }

        public virtual async Task<IList<TEntity>> GetListAsync()
        {
            return await DbContext.GetListAsync().ConfigureAwait(false);
        }
    }
}