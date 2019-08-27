using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;

namespace TestProject.Common.DAL.PostgreSQL
{
    public class PostgresDbContext<TEntity, IId> : IDbContext<TEntity, IId>
         where TEntity : IEntityBase<IId>
    {
        public Task CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IId id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetAsync(IId id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<TEntity>> GetListAsync()
        {
            throw new NotImplementedException();
        }
    }
}