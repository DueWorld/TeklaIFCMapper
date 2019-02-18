using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.ProductExtension;

namespace TeklaIFCMapper.XbimWrapper
{
    class ModelInfo
    {
        public IfcProject ifcProject { get; set; }
        public IfcSite ifcSite { get; set; }
        public IfcBuilding ifcBuilding { get; set; }
        public string File { get; set; }
        public ModelInfo(IfcProject ifcProject, IfcSite ifcSite, IfcBuilding ifcBuilding)
        {
            this.ifcProject = ifcProject;
            this.ifcSite = ifcSite;
            this.ifcBuilding = ifcBuilding;
        }
        public ModelInfo(string projectname, string sitename, string buildingname, string filepath)
        {
            this.File = filepath;
            using (var model = IfcStore.Open(File))
            {
                using (var txn = model.BeginTransaction())
                {
                    ifcSite = model.Instances.New<IfcSite>(p => p.Name = sitename);
                    ifcProject = model.Instances.New<IfcProject>(p => p.Name = projectname);
                    ifcBuilding = model.Instances.New<IfcBuilding>(p => p.Name = buildingname);
                    ifcProject.Initialize(ProjectUnits.SIUnitsUK);
                    txn.Commit();
                }
                model.SaveAs(File);
            }
        }

    }
}
