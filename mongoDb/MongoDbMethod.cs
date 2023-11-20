using MongoDB.Driver.Linq;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBServer.mongoDb
{
    public static class MongoDbMethod
    {
        public static async Task<List<T>> GetFindToList<T>(this IMongoCollection<T> _coll, FilterDefinition<T> _filter)
        {
            return await _coll.Find(_filter).ToListAsync();

        }

        public static IMongoQueryable<T> GetQueryable<T>(this IMongoCollection<T> _coll)
        {
            return _coll.AsQueryable();

        }

        public static async Task<List<T>> GetFindToList<T>(this IMongoCollection<T> _coll, Expression<Func<T, bool>> _filter)
        {
            return await _coll.Find(_filter).ToListAsync();

        }

        public static async Task<T> GetRandomEntity<T>(this IMongoCollection<T> _coll)
        {
            var _entity = await _coll.AsQueryable().Sample(1).ToCursor().FirstOrDefaultAsync();
            return _entity;
        }
    }
}
