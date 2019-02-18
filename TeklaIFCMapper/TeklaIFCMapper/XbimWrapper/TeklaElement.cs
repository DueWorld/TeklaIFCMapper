using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Common;
using Xbim.Common.Step21;
using Xbim.Ifc;
using Xbim.IO;

using Xbim.Ifc2x3.SharedComponentElements;
using Xbim.Ifc2x3.MeasureResource;
using Xbim.Ifc2x3.UtilityResource;
using Xbim.Ifc2x3.GeometricConstraintResource;
using Xbim.Ifc2x3.RepresentationResource;
using Xbim.Ifc2x3.Kernel;

namespace TeklaIFCMapper.XbimWrapper
{
    class TeklaElement : MyIfcObject
    {
        public override IfcGloballyUniqueId GlobalId { get; set; }
        public override IfcOwnerHistory OwnerHistory { get; set; }
        public override IfcLabel? Name { get; set; }
        public override IfcText? Description { get; set; }
        public override ModelInfo ModelInfo { get; set; }
        public string Section { get; set; }
        public IfcLocalPlacement localPlacement { get; set; }
        public IfcProductDefinitionShape ifcProductDefinitionShape { get; set; }
        public IfcDiscreteAccessory item { get; private set; }


        public TeklaElement()
        {

        }

        public TeklaElement(IfcOwnerHistory OwnerHistory, IfcLabel Name, IfcText Description, ModelInfo modelInfo, string section, IfcProductDefinitionShape ifcProductDefinitionShape)
        {
            this.Name = Name;
            this.OwnerHistory = OwnerHistory;
            this.ModelInfo = ModelInfo;
            this.Description = Description;
            this.Section = section;
            this.ifcProductDefinitionShape = ifcProductDefinitionShape;
        }
        public TeklaElement(string section, IfcProductDefinitionShape ifcProductDefinitionShape, ModelInfo modelInfo)
        {
            this.Section = section;
            this.ifcProductDefinitionShape = ifcProductDefinitionShape;
        }
        public TeklaElement(string section, ModelInfo modelInfo)
        {
            this.Section = section;
            this.ModelInfo = modelInfo;
        }
        public override string Create()
        {
            using (var model = IfcStore.Open(ModelInfo.File))
            {
                using (var txn = model.BeginTransaction())
                {
                    var discreteAccessory = model.Instances.New<IfcDiscreteAccessory>(r => r.GlobalId = Guid.NewGuid().ToString());
                    discreteAccessory.OwnerHistory = OwnerHistory;
                    discreteAccessory.Name = Name;
                    discreteAccessory.ObjectType = Section;
                    discreteAccessory.ObjectPlacement = localPlacement;
                    GlobalId = discreteAccessory.GlobalId.ToString();
                    item = discreteAccessory;
                    txn.Commit();
                }
                model.SaveAs(ModelInfo.File);
            }
            return GlobalId;
        }

      
        public IfcDiscreteAccessory GetById(string id)
        {
            using (var model = IfcStore.Open(ModelInfo.File))
            {

                if (id == null)
                {
                    return null;
                }
                else
                {

                    return model.Instances.FirstOrDefault<IfcDiscreteAccessory>(d => d.GlobalId == id);
                }
            }
        }


        public List<IfcDiscreteAccessory> GetAll()
        {
            using (var model = IfcStore.Open(ModelInfo.File))
            {

                return model.Instances.OfType<IfcDiscreteAccessory>().ToList();
            }
        }
    }
}
