using Microsoft.VisualStudio.TestTools.UnitTesting;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Tests
{
    [TestClass()]
    public class KMLFacilityParserTests
    {
        public string KMLSource { get; private set; }

        [TestInitialize]
        public void Init()
        {
            KMLSource = @"<?xml version='1.0' encoding='UTF-8'?>
            <kml xmlns='http://www.opengis.net/kml/2.2'>
	            <Document>
		            <name>Health Facilities</name>
		            <Placemark>
			            <name>Woodbrook Health Centre</name>
			            <description><![CDATA[Tragarete Road, Woodbrook<br>(868)-622-2045]]></description>
			            <styleUrl>#icon-22</styleUrl>
			            <Point>
				            <coordinates>-61.51995600000001,10.663007,0.0</coordinates>
			            </Point>
		            </Placemark>
		            <Placemark>
			            <name>Port-of-Spain General Hospital</name>
			            <description><![CDATA[Upper Charlotte Street, Port-of-Spain<br>(868)-623-2951]]></description>
			            <styleUrl>#icon-73</styleUrl>
			            <Point>
				            <coordinates>-61.508196999999996,10.662058,0.0</coordinates>
			            </Point>
		            </Placemark>
		            <Placemark>
			            <name>Oxford Street Enhanced Health Centre</name>
			            <description><![CDATA[Corner Observatory Street and Oxford Street, Port-of-Spain<br>(868)-623-6741]]></description>
			            <styleUrl>#icon-22</styleUrl>
			            <Point>
				            <coordinates>-61.506840000000004,10.659369,0.0</coordinates>
			            </Point>
		            </Placemark>
                </Document>
            </kml>
            ";
        }

        [TestMethod()]
        public void ExtractLocationsTest()
        {
            var parser = new KMLFacilityParser(KMLSource);
            Assert.IsTrue(parser.Locations.Count > 0);
            CollectionAssert.AllItemsAreNotNull(parser.Locations);
        }
    }
}