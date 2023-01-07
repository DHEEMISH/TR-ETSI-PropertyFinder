
using System.Collections.Generic;
using TR_ETSI_PropertyFinderApi.Interfaces;

namespace TR_ETSI_PropertyFinderApi.Models
{
    public class DatabaseSettings : IPropertyFinderDatabaseSettings
    {
        public string DatabaseName { get; set; }
    
        public string ConnectionString { get; set; }
        public string PropertiesCollectionName { get; set; }
        public string CityCollectionName { get; set; }
        public string AppUserCollectionName { get; set; }
    }

 }

