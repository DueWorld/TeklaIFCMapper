using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xbim.Ifc;
using Xbim.Ifc2x3.GeometricConstraintResource;
using Xbim.Ifc2x3.GeometricModelResource;
using Xbim.Ifc2x3.GeometryResource;
using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.ProfileResource;
using Xbim.Ifc2x3.RepresentationResource;
using Xbim.Ifc2x3.SharedBldgElements;

namespace TeklaIFCMapper.XbimWrapper
{
    class RevitToTeklaIfc
    {
        public static List<IfcColumn> RetrieveColumns(IfcStore model)
        {
            List<IfcColumn> colList = new List<IfcColumn>();
            var list = model.Instances.OfType<IfcColumn>();
            colList = list.ToList();
            return colList;

        }
        public static IfcObjectPlacement RetrieveLocalPlacement(IfcStore model, IfcColumn column, out List<IfcLocalPlacement> ifcLocal, out IfcCartesianPoint cartesianPoint)
        {
            var objectplacement = column.ObjectPlacement;
            ifcLocal = objectplacement.ReferencedByPlacements.ToList();
            cartesianPoint = ((IfcAxis2Placement3D)ifcLocal.FirstOrDefault().RelativePlacement).Location;
            
            return objectplacement;
            
        }
        public static void RetrieveLenghtWidthHeight(IfcColumn column, out double width, out double depth , out double Flangethickness, out double webthickness, out double filletRadius)
        {
           
         width=   ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).OverallWidth;
            depth = ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).OverallDepth;
            webthickness  = ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).WebThickness;
            Flangethickness = ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).FlangeThickness;
            filletRadius =(double) ((IfcIShapeProfileDef)column.Representation.Representations.OfType<IfcShapeRepresentation>().FirstOrDefault().Items.OfType<IfcMappedItem>().FirstOrDefault().MappingSource.MappedRepresentation.Items.OfType<IfcExtrudedAreaSolid>().FirstOrDefault().SweptArea).FilletRadius;

            

        }

    }
}
