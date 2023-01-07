using Microsoft.AspNetCore.Authentication;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TR_ETSI_PropertyFinderApi.Models
{
    [Serializable]
    public class UserForAuthenticationDto
    {
    
     
        [Required(ErrorMessage = "Email is required.")]
        [BsonElement("email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

       [Required(ErrorMessage = "Password is required.")]
       [StringLength(20, MinimumLength = 4)]
       [BsonElement("Password"), BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }

        [BsonElement("UserName"), BsonRepresentation(BsonType.String)]
        public string? UserName { get; set; }
    }
}
