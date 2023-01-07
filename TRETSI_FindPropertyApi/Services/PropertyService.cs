using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using TR_ETSI_PropertyFinderApi.DBContext;
using TRETSIPropertyFinderApi.Interfaces;
using TRETSIPropertyFinderApi.Models;

namespace TRETSIPropertyFinderApi.Services
{
    public class PropertyService : IPropertyKind
    {
        private readonly IUserContext _context = null;

        public PropertyService(IUserContext propertycontext)
        {
            _context = propertycontext;

        }

        public async Task<List<City>> GetAllCitiesAsync()
        {

            return await _context.city.Find(s => true).ToListAsync();
        }

        public async Task<List<TypesOfProperties>> GetAllPropertiesAsync()
        {

            return await _context.typeofproperty.Find(s => true).ToListAsync();
        }

        public async Task<City> GetCityByNameAsync(string cityName)
        {
            return await _context.city.Find(s => s.cityName == cityName).FirstOrDefaultAsync();
        }

        public async Task<TypesOfProperties> GetPropertiesDetailsAsync(string? propId)
        {

            return await _context.typeofproperty.Find(s => s.propId == propId).FirstOrDefaultAsync();
            //   var agg = await _context.typeofproperty.Aggregate().Lookup<TypesOfProperties, City, PropertyResult>(_context.city, a => a.FkCityName, a => a.cityName, a => a.propertyList).Match(x => x.v == CityName).ToListAsync();
            // return agg;

        }

        public async Task<TypesOfProperties> GetPropertiesDetailsByCityNameAsync(string cityName)
        {

            return await _context.typeofproperty.Find(s => s.FkCityName == (cityName)).FirstOrDefaultAsync(); 
                 

        }

        public async Task DeleteCityAsync(string id)
        {
            await _context.city.DeleteOneAsync(c => c.cityName == id);
        }
        public async Task DeletePropertyAsync(string id)
        {
            await _context.typeofproperty.DeleteOneAsync(c => c.propId == id);
        }
        public async Task UpdatePropertiesAsync(string id, TypesOfProperties tofProperties)
        {
            await _context.typeofproperty.ReplaceOneAsync(c => c.propId == id, tofProperties);
        }

        public async Task<TypesOfProperties> CreatePropertyAsync(TypesOfProperties _typesOfProperties)
        {
       
            var typesOfProperties = new TypesOfProperties
            {
                description = _typesOfProperties.description,
                title = _typesOfProperties.title,
                propertyType = _typesOfProperties.propertyType,
                postedOn = DateTime.Now,
                propertyCost = _typesOfProperties.propertyCost,
                costTobeDisplayed = _typesOfProperties.costTobeDisplayed,
                availableFrom = _typesOfProperties.availableFrom,
                propImgUrl = _typesOfProperties.propImgUrl,
                FkCityName = _typesOfProperties.FkCityName,
                TimeStamp = DateTime.Now,
                isActiveProperties = _typesOfProperties.isActiveProperties,
              
               // cityName = _typesOfProperties.cityName

        };
            
            await _context.typeofproperty.InsertOneAsync(typesOfProperties);
          
            return typesOfProperties;
        }
    }

        
   
}
