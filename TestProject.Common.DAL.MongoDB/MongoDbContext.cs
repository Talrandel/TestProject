using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.Common.DAL.Core;
using TestProject.Common.Entities;

namespace TestProject.Common.DAL.MongoDB
{
    public class MongoDbContext<TEntity, IId> : IDbContext<TEntity, IId>
         where TEntity : IEntityBase<IId>
    {
        protected readonly IMongoClient _client;
        protected readonly IMongoDatabase _database;

        protected readonly string _collectionName;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(settings.Value.DatabaseName);
            _collectionName = nameof(TEntity);
        }

        public IMongoCollection<TEntity> Entities
        {
            get { return _database.GetCollection<TEntity>(_collectionName); }
        }

        public async Task CreateAsync(TEntity entity)
        {
            await Entities.InsertOneAsync(entity).ConfigureAwait(false);
        }

        public async Task DeleteAsync(IId id)
        {
            var filter = new BsonDocument("id", BsonValue.Create(id));
            await Entities.DeleteOneAsync(filter);
        }

        public async Task EditAsync(TEntity entity)
        {
            var filter = new BsonDocument("id", BsonValue.Create(entity.Id));
            await Entities.ReplaceOneAsync(filter, entity).ConfigureAwait(false);
        }

        public async Task<TEntity> GetAsync(IId id)
        {
            var filter = new BsonDocument("id", BsonValue.Create(id));
            var entity = await Entities.Find(filter).FirstAsync().ConfigureAwait(false);
            return entity;
        }

        public async Task<IList<TEntity>> GetListAsync()
        {
            var filter = new BsonDocument();
            var entityList = await Entities.Find(filter).ToListAsync().ConfigureAwait(false);
            return entityList;
        }

        public async Task Clear()
        {
            var filter = new BsonDocument();
            await Entities.DeleteManyAsync(filter).ConfigureAwait(false);
        }
    }
}