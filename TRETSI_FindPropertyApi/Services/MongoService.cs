using MongoDB.Driver;
using TR_ETSI_PropertyFinderApi.Interfaces;


namespace TR_ETSI_PropertyFinderApi.Services
{
    public class MongoService
    {
        private static MongoClient _client;
        public MongoService(IPropertyFinderDatabaseSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
        }

        public MongoClient GetClient()
        {
            return _client;
        }
    }
}