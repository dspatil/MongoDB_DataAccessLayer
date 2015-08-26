using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace MongoDataAccessAdapter
{
    public static class MongoConfig
    {
        public static MongoDatabase GetDBInstance()
        {
            MongoClient client = new MongoClient(ConfigurationManager.AppSettings["MongoServer"]);
            var server = client.GetServer();
            var db = server.GetDatabase(ConfigurationManager.AppSettings["DBName"]);
            return db;
        }

        public static MongoDB.Driver.MongoCollection<T> GetCollection<T>()
        {
            var db = GetDBInstance();
            string collectionName = typeof(T).Name;
            return db.GetCollection<T>(collectionName);
        }
    }
}



