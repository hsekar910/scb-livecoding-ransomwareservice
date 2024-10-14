using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ransomware_webapi.Models;

namespace ransomware_webapi.Service
{
    public class RansomwareService
    {
        private readonly IMongoCollection<Ransomware> _ransomwaresCollection;

        public RansomwareService(
            IOptions<RansomwareDBSettings> ransomwareDBSettings)
        {
            var mongoClient = new MongoClient(
                ransomwareDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ransomwareDBSettings.Value.DatabaseName);

            _ransomwaresCollection = mongoDatabase.GetCollection<Ransomware>(
                ransomwareDBSettings.Value.CollectionName);
        }

        public async Task<List<Ransomware>> GetAsync() =>
            await _ransomwaresCollection.Find(_ => true).ToListAsync();

        public async Task<Ransomware?> GetAsync(string id) =>
            await _ransomwaresCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Ransomware ransomware) =>
            await _ransomwaresCollection.InsertOneAsync(ransomware);

        public async Task UpdateAsync(string id, Ransomware ransomware) =>
            await _ransomwaresCollection.ReplaceOneAsync(x => x.Id == id, ransomware);

        public async Task RemoveAsync(string id) =>
            await _ransomwaresCollection.DeleteOneAsync(x => x.Id == id);
    }
}
