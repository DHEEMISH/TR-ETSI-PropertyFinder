using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;

namespace TRETSIPropertyFinderApi.Models
{
   [Serializable]
 
    public class City
    {
        [BsonId]
        [BsonElement("_cityId"), BsonRepresentation(BsonType.ObjectId)]
        public string? CityId { get; set; }
        public string ? cityName { get; set; }     
        public string? cityImgUrl { get; set; }

      //  public List<string>? propList { get; set; }

      //  public List<TypesOfProperties> propertyList { get; set; }

    }

}
