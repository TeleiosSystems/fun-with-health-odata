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
    public class HealthFacilityDataSourceTests
    {
        public string FacilitiesSource { get; private set; }
        public string KMLSource { get; private set; }

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

            KMLSource = @"<?xml version='1.0' encoding='UTF-8'?>
            <kml xmlns='http://www.opengis.net/kml/2.2'>
	            <Document>
		            <name>Health Facilities</name>
		            <Placemark>
			<name>Arima District Health Facility</name>
			<styleUrl>#icon-22-nodesc</styleUrl>
			<Point>
				<coordinates>-61.285102,10.636246,0.0</coordinates>
			</Point>
		</Placemark>
        <Placemark>
			        <name>Arouca Health Centre</name>
			        <description><![CDATA[Corner George Street and Golden Grove Road, Arouca<br>(868)-642-1065]]></description>
			        <styleUrl>#icon-22</styleUrl>
			        <Point>
				        <coordinates>-61.338601000000004,10.628891,0.0</coordinates>
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
        public void InitializeTest()
        {
            var htmlParser = new HTMLFacilityParser();
            var facilities = htmlParser.GetFacilities(FacilitiesSource);
            var kmlParser = new KMLFacilityParser(KMLSource);
            kmlParser.AssociateWith(facilities);

            Assert.IsTrue(facilities[0].Latitude > 0);
        }
    }
}