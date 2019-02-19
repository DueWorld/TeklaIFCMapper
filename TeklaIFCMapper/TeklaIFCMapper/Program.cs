using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeklaIFCMapper.XbimWrapper;
using Xbim.Ifc;
using Xbim.Ifc2x3.Kernel;
using Xbim.Ifc2x3.ProductExtension;
using Xbim.Ifc2x3.SharedComponentElements;
using Xbim.IO;

namespace TeklaIFCMapper
{
    class Program
    {
        static void Main(string[] args)
        {
            string filepath = "loai.ifc";
            using (var model = IfcStore.Create(Xbim.Common.Step21.XbimSchemaVersion.Ifc2X3, XbimStoreType.InMemoryModel))
            {
                model.SaveAs(string.Format(filepath));
                

               
            }
            using (var model = IfcStore.Open("Nignog.ifc"))
            {



                //string section = "PL150*5";
                //string section2 = "PL200*5";
                //string section3 = "PL180*5";

                //ModelInfo mymodel = new ModelInfo("project1", "site1", "building1", filepath, model);
                //TeklaElement myelement = new TeklaElement(section, mymodel);
                //myelement.Create(model);
                //TeklaElement myelement2 = new TeklaElement(section2, mymodel);
                //myelement2.Create(model);

                //TeklaElement myelement3 = new TeklaElement(section3, mymodel);
                //myelement3.Create(model);

                //ElementAssembly elementAssembly = new ElementAssembly(mymodel);
                //elementAssembly.MyElements.Add(myelement);
                //elementAssembly.MyElements.Add(myelement2);
                //elementAssembly.MyElements.Add(myelement3);

                //elementAssembly.Create(model);
                var list = RevitToTeklaIfc.RetrieveColumns(model);
                var col = list[0];
                double w; double l; double c; double v; double r;
                RevitToTeklaIfc.RetrieveLenghtWidthHeight(col,out w,out l,out c,out v,out r);

                Console.Read();
            }

                string filename = "D:\\iti\\courses\\BIM\\bim I project\\TeklaIFCMapper\\TeklaIFCMapper\\TeklaIFCMapper\\bin\\Debug";
            using (var stepmpdel = IfcStore.Open($"{filename}\\{filepath}"))
            {
                //save as xml
                stepmpdel.SaveAs($"{filename}\\loai.ifcxml");

            }


            }
        }
}
