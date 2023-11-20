using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBServer.mongoDb
{
    public class MongoDbConnectionFactory
    {
        private readonly static object _connObj = new object();
        private static MongoEntity _model;

        private static MongoDbConnectionFactory _connFactory;

        /// <summary>
        /// 锁+双IF获取单例
        /// </summary>
        public static MongoDbConnectionFactory ConnFactory
        {
            get
            {
                if (_connFactory == null)
                {
                    lock (_connObj)
                    {
                        if (_connFactory == null)
                        {
                            _connFactory = new MongoDbConnectionFactory();
                        }
                    }
                }
                return _connFactory;
            }
        }

        /// <summary>
        /// MDB链接信息的模型委托
        /// </summary>
        private Action<MongoEntity> _connectionDelegate = null;

        /// <summary>
        /// 添加MDB的链接信息
        /// </summary>
        /// <param name="connectionDelegate"></param>
        /// <returns></returns>
        public MongoDbConnectionFactory AddMongoDbConnectionInfo(Action<MongoEntity> connectionDelegate)
        {
            _connectionDelegate = delegate (MongoEntity x)
            {
                connectionDelegate(x);
                _model = x;
            };
            return ConnFactory;
        }

        public void Build()
        {
            if (_connectionDelegate != null)
            {
                _connectionDelegate.Invoke(new MongoEntity() { });
            }
        }


        public IMongoDatabase GetConnection()
        {
            var _client = new MongoClient(_model.MongoDbConnet);
            var _dataBase = _client.GetDatabase(_model.DataBaseName);

            return _dataBase;
        }

        public IMongoDatabase GetConnection(MongoEntity entity)
        {
            var _client = new MongoClient(entity.MongoDbConnet);
            var _dataBase = _client.GetDatabase(entity.DataBaseName);

            return _dataBase;
        }
    }
}
