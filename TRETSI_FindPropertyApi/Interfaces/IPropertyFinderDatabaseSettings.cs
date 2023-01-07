
using System.Collections.Generic;

namespace TR_ETSI_PropertyFinderApi.Interfaces
{
    public interface IPropertyFinderDatabaseSettings
    {
        string DatabaseName { get; set; }
      
        string AppUserCollectionName { get; set; }
        string PropertiesCollectionName { get; set; }
        string CityCollectionName { get; set; }
        string ConnectionString { get; set; }

    }
}
