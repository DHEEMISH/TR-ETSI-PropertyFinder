using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TRETSIPropertyFinderApi.DTO
{

    [Serializable]

    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        //[BsonId]
        //[BsonElement("Id"), BsonRepresentation(BsonType.ObjectId)]
        //public string? Id { get; set; }
        public string? FirstName { get; set; }
      //  public string? MiddleName { get; set; }
        public string? LastName { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string? ConfirmPassword { get; set; }        
        //public string? CreatedBy { get; set; }        
       // public DateTime? CreatedOn { get; set; }
    }
}
