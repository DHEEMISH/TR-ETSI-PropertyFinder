
using API;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TR_ETSI_PropertyFinderApi.Models;
using TRETSIPropertyFinderApi.DTO;
using TRETSIPropertyFinderApi.Models;

namespace TR_ETSI_PropertyFinderApi.DBContext
{
    public interface IUserContext
    {
       
      
       IMongoCollection<ApplicationUser> appUser { get; }
      //  IMongoCollection<UserForAuthenticationDto> login { get; }
        IMongoCollection<City> city { get; }
        IMongoCollection<TypesOfProperties> typeofproperty { get; }

    }
}
