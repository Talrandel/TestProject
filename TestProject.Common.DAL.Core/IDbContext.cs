using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Common.Entities;

namespace TestProject.Common.DAL.Core
{
    public interface IDbContext<TEntity, IId>
         where TEntity : IEntityBase<IId>
    {
        Task CreateAsync(TEntity entity);

        Task DeleteAsync(IId id);

        Task EditAsync(TEntity entity);

        Task<TEntity> GetAsync(IId id);

        Task<IList<TEntity>> GetListAsync();

#if DEBUG
        Task Clear();
#endif

    }
}