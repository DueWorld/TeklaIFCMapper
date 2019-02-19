using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometricConstraintResource;
using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.ProductExtension;
using Xbim.Ifc2x3.SharedComponentElements;
using Xbim.Ifc2x3.UtilityResource;

namespace TeklaIFCMapper.XbimWrapper
{
    class ElementAssembly : MyIfcObject
    {
        public override IfcGloballyUniqueId GlobalId { get; set; }
        public override IfcOwnerHistory OwnerHistory { get; set; }
        public override IfcLabel? Name { get; set; }
        public override IfcText? Description { get; set; }
        public override ModelInfo ModelInfo { get; set; }
        public List<TeklaElement> MyElements { get; set; }
        public IfcLocalPlacement localPlacement { get; set; }
        public IfcElementAssembly element { get; private set; }
        public ElementAssembly(ModelInfo modelInfo)
        {
            this.ModelInfo = modelInfo;
            MyElements = new List<TeklaElement>();
        }

        public override string Create(IfcStore model)
        {
           
                using (var txn = model.BeginTransaction())
                {
                    //////////////
                    var ElementAssembly = model.Instances.New<IfcElementAssembly>(r => r.GlobalId = Guid.NewGuid().ToString());
                    ElementAssembly.OwnerHistory = OwnerHistory;
                    ElementAssembly.Name = Name;
                    ElementAssembly.ObjectPlacement = localPlacement;
                    var rel = model.Instances.New<IfcRelAggregates>(r => r.GlobalId = Guid.NewGuid().ToString());

                    rel.OwnerHistory = OwnerHistory;

                    rel.RelatingObject = ElementAssembly;

                    foreach (TeklaElement item in MyElements)
                    {

                    IfcObjectDefinition ifcObject = item.item;

                    rel.RelatedObjects.Add(ifcObject);
                    }

                    GlobalId = ElementAssembly.GlobalId.ToString();
                    element = ElementAssembly;

                    ////////////////////////////
                    //var ElementAssembly = model.Instances.New<IfcElementAssembly>(r => r.GlobalId = Guid.NewGuid().ToString());
                    //ElementAssembly.OwnerHistory = OwnerHistory;
                    //ElementAssembly.Name = Name;
                    //ElementAssembly.ObjectPlacement = localPlacement;
                    //IfcRelDecomposes ifcRelDecomposes = model.Instances.New<IfcRelAggregates>(r => r.GlobalId = Guid.NewGuid().ToString());
                    //ifcRelDecomposes.RelatingObject = ElementAssembly;

                    //foreach (TeklaElement item in MyElements)
                    //{
                    //    IfcObjectDefinition ifcObject =item.item;

                    //ifcRelDecomposes.RelatedObjects.Add(ifcObject);
                    //}

                    txn.Commit();
                }
                model.SaveAs(ModelInfo.File);
            
            return GlobalId;
        }
    }
}
