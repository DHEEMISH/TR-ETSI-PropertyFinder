using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace API
{
    [Serializable]
    public class UserForRegistrationDto
    {

        [BsonId]
        [BsonElement("UserId"), BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Required(ErrorMessage = "Email is required.")]
        [BsonElement("Email"), BsonRepresentation(BsonType.String)]
        public string? Email { get; set; }

        //[BsonElement("img"), BsonRepresentation(BsonType.String)]
        //public string? img { get; set; }   

        //[BsonElement("CreatedBy"), BsonRepresentation(BsonType.String)]
        public string? PreferredLocation { get; set; }


        //[BsonElement("CreatedDate"), BsonRepresentation(BsonType.DateTime)]
        //public DateTime? CreatedOn { get; set; }

        [BsonElement("FirstName"), BsonRepresentation(BsonType.String)]
        public string? FirstName { get; set; }

        [BsonElement("LastName"), BsonRepresentation(BsonType.String)]
        public string? LastName { get; set; }

        // [Required(ErrorMessage = "Password is required")]
        [BsonElement("Password"), BsonRepresentation(BsonType.String)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}