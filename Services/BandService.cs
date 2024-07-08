using MusicLibrary.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MusicLibrary.Services
{
    public class BandService
    {
        private readonly IMongoCollection<Band> _bands;

        public BandService(IMongoDatabase database)
        {
            _bands = database.GetCollection<Band>("Band");
        }

        public async Task<List<Band>> GetBandsAsync() =>
           await _bands.Find(band => true).ToListAsync();

        public async Task<Band> GetBandByIdAsync(ObjectId id) =>
            await _bands.Find(band => band.Id == id).FirstOrDefaultAsync();

        public async Task CreateBandAsync(Band band) =>
            await _bands.InsertOneAsync(band);

        public async Task UpdateBandAsync(ObjectId id, Band bandIn)
        {
            var updateDefinition = Builders<Band>.Update
                .Set(b => b.Name, bandIn.Name)
                .Set(b => b.Albums, bandIn.Albums);

            await _bands.UpdateOneAsync(band => band.Id == id, updateDefinition);
        }

        public async Task DeleteBandAsync(ObjectId id) =>
            await _bands.DeleteOneAsync(band => band.Id == id);


       

    }

}