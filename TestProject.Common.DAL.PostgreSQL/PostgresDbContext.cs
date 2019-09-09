using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;

namespace TestProject.Common.DAL.PostgreSQL
{
    // Крайне спорное решение.
    // В реальности две разные субд в рамках одного проекта/монолита вряд ли когда-нибудь будут использоваться. 
    // И подходы у MongoDb и у PostgreSQL различаются. Поэтому в целях написания тестового проекта получается сие.

    public class PostgresDbContext<TEntity, IId> : DbContext, IDbContext<TEntity, IId>
         where TEntity : class, IEntityBase<IId>
    {
        // TODO: логировать работу с бд

        public DbSet<TEntity> Entities { get; set; }

        public async Task CreateAsync(TEntity entity)
        {
            Entities.Add(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(IId id)
        {
            var entityToDelete = await Entities.SingleOrDefaultAsync(e => e.Id.Equals(id)).ConfigureAwait(false);
            Entities.Remove(entityToDelete);
            await SaveChangesAsync();
        }

        public async Task EditAsync(TEntity entity)
        {
            Entities.Update(entity);
            await SaveChangesAsync();
        }

        public async Task<TEntity> GetAsync(IId id)
        {
            return await Entities.SingleOrDefaultAsync(e => e.Id.Equals(id)).ConfigureAwait(false);
        }

        public async Task<IList<TEntity>> GetListAsync()
        {
            return await Entities.ToListAsync().ConfigureAwait(false);
        }

        public async Task Clear()
        {
            foreach (var entity in Entities)
            {
                Entities.Remove(entity);
            }
            await SaveChangesAsync();
        }
    }
}