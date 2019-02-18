using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.UtilityResource;

namespace TeklaIFCMapper.XbimWrapper
{
    interface IIfcObject
    {
        
       
        
         IfcGloballyUniqueId GlobalId { get; set; }
        
         IfcOwnerHistory OwnerHistory { get; set; }
         IfcLabel? Name { get; set; }
         IfcText? Description { get; set; }

       
       
    }
}
