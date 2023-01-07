using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TR_ETSI_PropertyFinderApi.Models;

namespace TRETSIPropertyFinderApi.Models
{
    public class PropertyResult :TypesOfProperties
    {
        public List<TypesOfProperties> propertyList { get; set; }
 
    }
}
