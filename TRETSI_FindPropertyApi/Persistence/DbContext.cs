

using API;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TR_ETSI_PropertyFinderApi.Interfaces;
using TR_ETSI_PropertyFinderApi.Models;
using TRETSIPropertyFinderApi.DTO;
using TRETSIPropertyFinderApi.Models;

namespace TR_ETSI_PropertyFinderApi.DBContext
{
    public class DbContext : IUserContext
    {
        
        private static MongoClient _client;
        private readonly IPropertyFinderDatabaseSettings _propfinderDatabaseSettings;
        private  IMongoDatabase _db;
      //  public DbContext<AppUser> Users { get; set; }
        public DbContext(IPropertyFinderDatabaseSettings databaseSettings)
        {
            _client = new MongoClient(databaseSettings.ConnectionString);
            _db = _client.GetDatabase(databaseSettings.DatabaseName);
            _propfinderDatabaseSettings = databaseSettings;
       
        }

        public List<string> GetCollections()
        {
            List<string> collections = new List<string>();
            foreach (BsonDocument collection in _db.ListCollectionsAsync().Result.ToListAsync<BsonDocument>().Result)
            {
                string name = collection["Agents"].AsString;
                collections.Add(name);
            }

            return collections;
        }
    
        public IMongoCollection<ApplicationUser> appUser => _db.GetCollection<ApplicationUser>(_propfinderDatabaseSettings.AppUserCollectionName);
        public IMongoCollection<City> city => _db.GetCollection<City>(_propfinderDatabaseSettings.CityCollectionName);
        public IMongoCollection<TypesOfProperties> typeofproperty => _db.GetCollection<TypesOfProperties>(_propfinderDatabaseSettings.PropertiesCollectionName);
    }
}
