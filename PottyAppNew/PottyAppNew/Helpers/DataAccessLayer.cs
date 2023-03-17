using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PottyAppNew.Helpers
{
    internal class DataAccessLayer
    {
        public static async Task<IMongoCollection<T>> GetDbCollection<T>(string collectionName)
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://MariaH:CampusNyk33@ac-yldl65y-shard-00-00.imsqhpk.mongodb.net:27017,ac-yldl65y-shard-00-01.imsqhpk.mongodb.net:27017,ac-yldl65y-shard-00-02.imsqhpk.mongodb.net:27017/?ssl=true&replicaSet=atlas-f2gcmf-shard-0&authSource=admin&retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("PottyAppDb");
            var Collection = database.GetCollection<T>($"{collectionName}");
            return await Task.FromResult(Collection);
        }
    }
}
