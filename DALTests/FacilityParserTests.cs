using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsQuery;

namespace DAL.Tests
{
    [TestClass()]
    public class FacilityParserTests
    {
        public string FacilitiesSource { get; set; }
        public string Facility { get; set; }
        public HTMLFacilityParser Parser { get; set; }
        [TestInitialize]
        public void Init()
        {
            FacilitiesSource = @"<tr style='background-color:#EEEEEE;'>
			<td style='width:175px;'><a href='facility.aspx?id=24'>Arima District Health Facility</a></td><td style='width:200px;'>Queen Mary Avenue, Arima</td><td style='width:100px;'>(868)-667-4714</td><td style='width:75px;'>District Health Facility</td><td style='width:50px;'>NCRHA</td>
		</tr><tr style='background-color:White;'>
			<td style='width:175px;'><a href='facility.aspx?id=28'>Arouca Health Centre</a></td><td style='width:200px;'>Corner George Street and Golden Grove Road, Arouca</td><td style='width:100px;'>(868)-642-1065</td><td style='width:75px;'>Health Centre</td><td style='width:50px;'>NCRHA</td>
		</tr><tr style='background-color:#EEEEEE;'>
			<td style='width:175px;'><a href='facility.aspx?id=7'>Barataria Health Centre</a></td><td style='width:200px;'>Eastern Main Road and Seventh Street, Barataria</td><td style='width:100px;'>(868)-638-2124</td><td style='width:75px;'>Health Centre</td><td style='width:50px;'>NWRHA</td>
		</tr><tr style='background-color:White;'>
			<td style='width:175px;'><a href='facility.aspx?id=94'>Belle Garden Health Centre</a></td><td style='width:200px;'>Belle Garden</td><td style='width:100px;'>(868)-660-5606</td><td style='width:75px;'>Health Centre</td><td style='width:50px;'>TRHA</td>
		</tr><tr style='background-color:#EEEEEE;'>
			<td style='width:175px;'><a href='facility.aspx?id=95'>Bethel Health Centre</a></td><td style='width:200px;'>Bethel</td><td style='width:100px;'>(868)-639-8580</td><td style='width:75px;'>Health Centre</td><td style='width:50px;'>TRHA</td>
		</tr>";
            Facility = @"<tr style='background-color:#EEEEEE;'>
			<td style='width:175px;'><a href='facility.aspx?id=95'>Bethel Health Centre</a></td><td style='width:200px;'>Bethel</td><td style='width:100px;'>(868)-639-8580</td><td style='width:75px;'>Health Centre</td><td style='width:50px;'>TRHA</td>
		</tr>";
            Parser = new HTMLFacilityParser();
        }
        [TestMethod()]
        public void GetFacilitiesTest()
        {
            var list = Parser.GetFacilities(FacilitiesSource);
            Assert.IsTrue(list.Count > 0);
            CollectionAssert.AllItemsAreNotNull(list);
        }

        [TestMethod()]
        public void GetFacilityTest()
        {
            CQ dom = Facility;
            var facility = Parser.GetFacility(dom.Elements.ToList());
            Assert.IsNotNull(facility);

        }

        [TestMethod()]
        public void GetIDTest()
        {
            CQ idElt = "<td style=\"width: 175px; \"><a href=\"facility.aspx? id = 6\">Aranguez Health Centre</a></td>";
            var id = Parser.GetNameAndID((IDomElement)idElt);
            Assert.IsTrue(id.Item1 > 0);
            Assert.IsTrue(id.Item2.Length > 0);
        }
    }
}