using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;

namespace TestProject.Common.DAL.Core
{
    public class InMemoryDbContext<TEntity, IId> : IDbContext<TEntity, IId>
         where TEntity : IEntityBase<IId>
    {
        private IList<TEntity> _items;

        public InMemoryDbContext()
        {
            _items = new List<TEntity>();
        }

        public Task CreateAsync(TEntity entity)
        {
            _items.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(IId id)
        {
            var item = _items.FirstOrDefault(i => i.Id.Equals(id));
            if (item != null)
                _items.Remove(item);
            return Task.CompletedTask;
        }

        public Task EditAsync(TEntity entity)
        {
            var item = _items.FirstOrDefault(i => i.Id.Equals(entity.Id));
            if (item != null)
                item = entity;
            return Task.CompletedTask;
        }

        public Task<TEntity> GetAsync(IId id)
        {
            var item = _items.FirstOrDefault(i => i.Id.Equals(id));
            if (item == null)
                throw new Exception();
            return Task.FromResult(item);
        }

        public Task<IList<TEntity>> GetListAsync()
        {
            return Task.FromResult(_items);
        }
    }
}
