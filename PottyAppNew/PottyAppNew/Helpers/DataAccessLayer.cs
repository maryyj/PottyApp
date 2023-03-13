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
            
            var settings = MongoClientSettings.FromConnectionString("Connection-String");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("PottyAppDb");
            var Collection = database.GetCollection<T>($"{collectionName}");
            return Collection;
        }
    }
}
