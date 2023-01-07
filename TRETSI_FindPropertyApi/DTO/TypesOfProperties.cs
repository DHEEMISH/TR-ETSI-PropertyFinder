using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using TR_ETSI_PropertyFinderApi.Interfaces;
using TR_ETSI_PropertyFinderApi.Models;

namespace TRETSIPropertyFinderApi.Models
{
    [Serializable]
    public class TypesOfProperties
    {
        [BsonId]
        [BsonElement("_propId"), BsonRepresentation(BsonType.ObjectId)]
        public string? propId { get; set; }

       // [BsonElement("description"), BsonRepresentation(BsonType.String)]
        public string? description { get; set; }

      //  [BsonElement("title"), BsonRepresentation(BsonType.String)]
        public string? title { get; set; }

      //  [BsonElement("propertyType"), BsonRepresentation(BsonType.String)]
        public string? propertyType { get; set; }

    //    [BsonElement("propertyKind"), BsonRepresentation(BsonType.String)]
        public string? propertyKind { get; set; }

      //  [BsonElement("postedOn"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? postedOn { get; set; }


      //  [BsonElement("propertyCost"), BsonRepresentation(BsonType.Double)]
        public double? propertyCost { get; set; }

       // [BsonElement("costTobeDisplayed"), BsonRepresentation(BsonType.Boolean)]
        public bool? costTobeDisplayed { get; set; }

       // [BsonElement("availableFrom"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? availableFrom { get; set; }

       // [BsonElement("propImgUrl"), BsonRepresentation(BsonType.String)]
        public string? propImgUrl { get; set; }

    //    [BsonElement("fkCityName"), BsonRepresentation(BsonType.String)]

        public string? FkCityName { get; set; }

       // [BsonElement("TimeStamp"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? TimeStamp { get; set; }

       // [BsonElement("isActiveProperties"), BsonRepresentation(BsonType.Boolean)]
        public bool? isActiveProperties { get; set; }

      //  [BsonElement("cityId"), BsonRepresentation(BsonType.Int64)]
      //  public List<string>? cityName { get; set; }
       
   //     public City cityList { get; set; }
        //[BsonElement("ownerContact"), BsonRepresentation(BsonType.Array)]
        //   public  List<User> ownerContact { get; set; }

    }
}
