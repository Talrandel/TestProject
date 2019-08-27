//using MongoDB.Bson;
//using MongoDB.Bson.Serialization;
//using MongoDB.Driver;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using TestProject.Common.Entities;

//namespace TestProject.Application.Core.Repository
//{
//    // TODO: переделать в DbContext?
//    public abstract class MongoDbRepositoryBase<TEntity, IId, T> : RepositoryBase<TEntity, IId, T>
//        where TEntity : IEntityBase<IId>
//        where T : IEquatable<T>
//    {
//        protected readonly MongoClient _client;
//        protected readonly IMongoDatabase _database;

//        protected readonly string _databaseName;
//        protected readonly string _collectionName;

//        // TODO: сразу маппить _database.GetCollection<BsonDocument>(_collectionName); на нужный мне тип?
//        // https://metanit.com/nosql/mongodb/4.12.php

//        protected MongoDbRepositoryBase(MongoClient client, string databaseName, string collectionName)
//        {
//            _client = client;
//            _databaseName = databaseName;
//            _collectionName = collectionName;
//            _database = _client.GetDatabase(_databaseName);
//        }

//        public override async Task CreateAsync(TEntity entity)
//        {
//            var collection = _database.GetCollection<BsonDocument>(_collectionName);
//            // TODO: алярм! Проверить работу сериализации
//            var document = entity.ToBsonDocument();
//            await collection.InsertOneAsync(document).ConfigureAwait(false);
//        }

//        public override async Task DeleteAsync(IId id)
//        {
//            var collection = _database.GetCollection<BsonDocument>(_collectionName);
//            var filter = new BsonDocument("id", BsonValue.Create(id));
//            await collection.DeleteOneAsync(filter);
//        }

//        public override async Task EditAsync(TEntity entity)
//        {
//            var collection = _database.GetCollection<BsonDocument>(_collectionName);
//            var filter = new BsonDocument("id", BsonValue.Create(entity.Id));
//            // TODO: алярм! Проверить работу сериализации
//            var document = entity.ToBsonDocument();
//            await collection.ReplaceOneAsync(filter, document).ConfigureAwait(false);
//        }

//        public override async Task<TEntity> GetAsync(IId id)
//        {
//            var collection = _database.GetCollection<BsonDocument>(_collectionName);
//            var filter = new BsonDocument();
//            var rawEntity = await collection.Find(filter).FirstAsync().ConfigureAwait(false);
//            var entity = BsonSerializer.Deserialize<TEntity>(rawEntity);
//            return entity;
//        }

//        public override async Task<IList<TEntity>> GetListAsync()
//        {
//            var collection = _database.GetCollection<BsonDocument>(_collectionName);
//            var filter = new BsonDocument();
//            var rawEntityList = await collection.Find(filter).ToListAsync().ConfigureAwait(false);
//            var entityList = rawEntityList.Select(p => BsonSerializer.Deserialize<TEntity>(p)).ToList();
//            return entityList;
//        }
//    }
//}