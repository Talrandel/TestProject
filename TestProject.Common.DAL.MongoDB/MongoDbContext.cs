using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
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

        protected readonly string _databaseName;
        protected readonly string _collectionName;

        public MongoDbContext(IMongoClient client, string databaseName, string collectionName)
        {
            _client = client;
            _databaseName = databaseName;
            _collectionName = collectionName;
            _database = _client.GetDatabase(_databaseName);
        }

        public async Task CreateAsync(TEntity entity)
        {
            var collection = _database.GetCollection<BsonDocument>(_collectionName);
            // TODO: алярм! Проверить работу сериализации
            var document = entity.ToBsonDocument();
            await collection.InsertOneAsync(document).ConfigureAwait(false);
        }

        public async Task DeleteAsync(IId id)
        {
            var collection = _database.GetCollection<BsonDocument>(_collectionName);
            var filter = new BsonDocument("id", BsonValue.Create(id));
            await collection.DeleteOneAsync(filter);
        }

        public async Task EditAsync(TEntity entity)
        {
            var collection = _database.GetCollection<BsonDocument>(_collectionName);
            var filter = new BsonDocument("id", BsonValue.Create(entity.Id));
            // TODO: алярм! Проверить работу сериализации
            var document = entity.ToBsonDocument();
            await collection.ReplaceOneAsync(filter, document).ConfigureAwait(false);
        }

        public async Task<TEntity> GetAsync(IId id)
        {
            var collection = _database.GetCollection<BsonDocument>(_collectionName);
            var filter = new BsonDocument();
            var rawEntity = await collection.Find(filter).FirstAsync().ConfigureAwait(false);
            var entity = BsonSerializer.Deserialize<TEntity>(rawEntity);
            return entity;
        }

        public async Task<IList<TEntity>> GetListAsync()
        {
            var collection = _database.GetCollection<BsonDocument>(_collectionName);
            var filter = new BsonDocument();
            var rawEntityList = await collection.Find(filter).ToListAsync().ConfigureAwait(false);
            var entityList = rawEntityList.Select(p => BsonSerializer.Deserialize<TEntity>(p)).ToList();
            return entityList;
        }
    }
}