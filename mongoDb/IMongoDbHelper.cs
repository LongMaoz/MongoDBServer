using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
namespace MongoDBServer.mongoDb
{
    public interface IMongoDbHelper
    {
        Task DropCollectionAsync(string name, CreateCollectionOptions options = null, CancellationToken token = default);

        void DropCollection(string name, CreateCollectionOptions options = null, CancellationToken token = default);


        Task CreateCollectionAsync(string name, CreateCollectionOptions options = null, CancellationToken token = default);

        void CreateCollection(string name, CreateCollectionOptions options = null);

        IMongoCollection<T> GetCollection<T>(string name);

        Task<IMongoCollection<T>> GetCollectionAsync<T>(string name);

        FilterDefinitionBuilder<T> GetBuilders<T>();

        bool IsExists(string name);
       

        UpdateDefinitionBuilder<T> GetUpdateBuliders<T>();
    }
}
