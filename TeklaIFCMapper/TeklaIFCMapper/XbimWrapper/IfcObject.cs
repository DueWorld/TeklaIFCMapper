using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.UtilityResource;

namespace TeklaIFCMapper.XbimWrapper
{
    abstract class MyIfcObject
    {
        public abstract IfcGloballyUniqueId GlobalId { get; set; }

        public abstract IfcOwnerHistory OwnerHistory { get; set; }
        public abstract IfcLabel? Name { get; set; }
        public abstract IfcText? Description { get; set; }
        public abstract ModelInfo ModelInfo { get; set; }
        public abstract string Create(IfcStore model);

    }
}