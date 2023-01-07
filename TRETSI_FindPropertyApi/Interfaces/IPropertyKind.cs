using System.Collections.Generic;
using System.Threading.Tasks;
using TRETSIPropertyFinderApi.Models;

namespace TRETSIPropertyFinderApi.Interfaces
{
    public interface IPropertyKind
    {
        public Task<List<City>> GetAllCitiesAsync();
        public Task<City> GetCityByNameAsync(string cityname);
        public Task<TypesOfProperties> GetPropertiesDetailsAsync(string id);
        public Task<TypesOfProperties> GetPropertiesDetailsByCityNameAsync(string cityName);  
        public Task<TypesOfProperties> CreatePropertyAsync(TypesOfProperties typeOfProperty);
        public Task DeletePropertyAsync(string userId);
       public Task UpdatePropertiesAsync(string id, TypesOfProperties tofProperties);
    }
    
}
