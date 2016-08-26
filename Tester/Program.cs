using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            var sourceData = File.ReadAllText("HealthFacilitiesList.txt");
            var parser = new HTMLFacilityParser();
            var list = parser.GetFacilities(sourceData);
            Console.Read();
        }
    }
}
