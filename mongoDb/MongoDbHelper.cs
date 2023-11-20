using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MongoDBServer.mongoDb
{
    public class MongoDbHelper : IMongoDbHelper
    {

        private IMongoDatabase _mdbBase;

        public MongoDbHelper()
        {

            _mdbBase = MongoDbConnectionFactory.ConnFactory.GetConnection();
        }

        public MongoDbHelper(MongoEntity entity)
        {

            _mdbBase = MongoDbConnectionFactory.ConnFactory.GetConnection(entity);
        }

        public Task DropCollectionAsync(string name, CreateCollectionOptions options = null, CancellationToken token = default)
        {

            if (_mdbBase != null)
                return _mdbBase.DropCollectionAsync(name, token);
            else
                throw new MongoException($"_mdbBase is null！");

        }

        public void DropCollection(string name, CreateCollectionOptions options = null, CancellationToken token = default)
        {

            if (_mdbBase != null)
                _mdbBase.DropCollection(name, token);
            else
                throw new MongoException($"_mdbBase is null！");

        }

        public Task CreateCollectionAsync(string name, CreateCollectionOptions options = null, CancellationToken token = default)
        {
            if (_mdbBase != null)
                return _mdbBase.CreateCollectionAsync(name, options, token);
            else
                throw new MongoException($"_mdbBase is null！");
        }

        public void CreateCollection(string name, CreateCollectionOptions options = null)
        {
            if (_mdbBase != null)
                _mdbBase.CreateCollection(name, options);
            else
                throw new MongoException($"_mdbBase is null！");
        }

        public bool IsExists(string name){

            var filter = GetBuilders<BsonDocument>().Where(x => 1==1);
            return _mdbBase.GetCollection<BsonDocument>(name).CountDocuments(filter)>0;
        }

        public Task<IMongoCollection<T>> GetCollectionAsync<T>(string name)
        {
            return Task.FromResult(_mdbBase.GetCollection<T>(name));
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _mdbBase.GetCollection<T>(name);
        }

        public FilterDefinitionBuilder<T> GetBuilders<T>()
        {
            return Builders<T>.Filter;
        }

        public UpdateDefinitionBuilder<T> GetUpdateBuliders<T>()
        {
            return Builders<T>.Update;
        }
    }
}
