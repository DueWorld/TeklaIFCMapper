using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeklaIFCMapper.XbimWrapper;
using Xbim.Ifc;
using Xbim.Ifc2x3.ProductExtension;
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
                string section = "PL150*5";
            string section2 = "PL200*5";
            string section3 = "PL180*5";

            ModelInfo mymodel = new ModelInfo("project1", "site1", "building1", filepath);
            TeklaElement myelement = new TeklaElement(section,mymodel);
            myelement.Create();
            TeklaElement myelement2 = new TeklaElement(section2,mymodel);
            myelement2.Create();

            TeklaElement myelement3 = new TeklaElement(section3,mymodel);
            myelement3.Create();

            ElementAssembly elementAssembly = new ElementAssembly(mymodel);
            elementAssembly.MyElements.Add(myelement);
            elementAssembly.MyElements.Add(myelement2);
            elementAssembly.MyElements.Add(myelement3);
            
            elementAssembly.Create();

            string filename = "D:\\iti\\courses\\BIM\\bim I project\\TeklaIFCMapper\\TeklaIFCMapper\\TeklaIFCMapper\\bin\\Debug";
            using (var stepmpdel = IfcStore.Open($"{filename}\\{filepath}"))
            {
                //save as xml
                stepmpdel.SaveAs($"{filename}\\loai.ifcxml");

            }

            }
        }
}
