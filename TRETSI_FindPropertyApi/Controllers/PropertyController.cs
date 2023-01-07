using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TRETSIPropertyFinderApi.Interfaces;
using TRETSIPropertyFinderApi.Models;

namespace TRETSIPropertyFinderApi.Controllers
{
    [Route("api/property")]
    public class PropertyController : Controller
    {

        private readonly IPropertyKind _propService;

        public PropertyController(IPropertyKind propKind)
        {

            _propService = propKind;

        }

        [HttpGet("GetCity")]
        public async Task<ActionResult<IEnumerable<TypesOfProperties>>> GetAllCties()
        {
            var users = await _propService.GetAllCitiesAsync();
            return Ok(users);
        }

        [HttpGet("GetCityByName")]
        public async Task<ActionResult<IEnumerable<TypesOfProperties>>> GetCityByName(string cityName)
        {
            var users = await _propService.GetCityByNameAsync(cityName);
            return Ok(users);

        }

        [HttpGet("GetPropertyDetails")]
        public async Task<ActionResult<IEnumerable<TypesOfProperties>>> GetPropertiesDetails(string? cityName)
            {
                var users = await _propService.GetPropertiesDetailsAsync(cityName);
                return Ok(users);
            }

        [HttpPost("CreateProperty")]
        public async Task<IActionResult> CreateProperty(TypesOfProperties typesOfProperties)
        {
            await _propService.CreatePropertyAsync(typesOfProperties);
            return Ok(typesOfProperties);

        }

        [HttpPut("UpdateProperty")]
        public async Task<IActionResult> UpdateProperties(string id, TypesOfProperties updateproperty)
        {
            var queriedUser = await _propService.GetPropertiesDetailsAsync(id);
            if (queriedUser == null)
            {
                return NotFound();
            }
            await _propService.UpdatePropertiesAsync(id, updateproperty);
            return NoContent();
        }

        //[HttpGet("GetPropertiesById")]
        //public async Task<ActionResult<TypesOfProperties>> GetPropertiesById(string id)
        //{
        //    var property = await _propService.GetPropertiesDetailsAsync(id);
        //    if (property == null)
        //    {
        //        return NotFound();
        //    }
        //    if (property.cityName.Count > 0)
        //    {
        //        var tempList = new List<City>();
        //        foreach (var cityName in property.cityName)
        //        {
        //            var course = await _propService.GetCityByNameAsync(cityName);
        //            if (course != null)
        //                tempList.Add(course);
        //        }
        //        property.cityName = tempList;
        //    }
        //    return Ok(property);
        //}


        [HttpGet("GetPropertiesByCity")]
        public async Task<ActionResult<City>> GetPropertiesByCity(List<string> city)
        {
            var propList = new List<TypesOfProperties>();
            foreach (var cNameList in city)
            {
              
            
                var properties = await _propService.GetPropertiesDetailsByCityNameAsync(cNameList);
                if (properties != null)
                    propList.Add(properties);
          
            }

        return Ok(propList);
           
        }
    }
}
