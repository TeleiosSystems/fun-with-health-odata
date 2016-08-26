using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HTMLFacilityParser
    {
        public List<HealthFacility> GetFacilities(string sourceData)
        {
            var facilities = new List<HealthFacility>();
            //each line goes: name, address, number, type, region
            CQ dom = sourceData;
            CQ trList = dom["tr"];
            foreach (var row in trList)
            {
                var tdList = row.ChildElements;
                if (tdList.Count() == 0) continue;
                var facilty = GetFacility(tdList.ToList());
                if (facilty == null) continue;
                facilities.Add(facilty);
            }
            return facilities;
        }

        public HealthFacility GetFacility(List<IDomElement> tdList)
        {
            var firstTD = tdList[0];
            var nameAndId = GetNameAndID(firstTD);
            if (nameAndId == null) return null;
            var facility = new HealthFacility
            {
                ID = nameAndId.Item1,
                Address = tdList[1].InnerText,
                Name = nameAndId.Item2,
                Phone = tdList[2].InnerText,
                Region = tdList[4].InnerText,
                Type = tdList[3].InnerText
            };
            return facility;
//< tr style = "background-color:White;" >
//< td style = "width:175px;" >< a href = "facility.aspx?id=6" > Aranguez Health Centre</ a ></ td >
//< td style = "width:200px;" > Aranguez Main Road, Aranguez</ td >
//< td style = "width:100px;" > (868) - 638 - 2120 </ td >
//< td style = "width:75px;" > Health Centre </ td >
//< td style = "width:50px;" > NWRHA </ td >              
//</ tr>
        }

        public Tuple<int, string> GetNameAndID(IDomElement domObject)
        {
            IDomObject anchorDom = domObject.FirstChild;
            if (null == anchorDom.Attributes) return null;
            var href = anchorDom.Attributes["href"];
            //<td style="width:175px;"><a href="facility.aspx?id=6">Aranguez Health Centre</a></td>
            var pos = href.LastIndexOf("=");
            var id = int.Parse(href.Substring(pos+1));//very, very specific
            var name = anchorDom.InnerText;

            return new Tuple<int, string>(id, name);
        }
    }
}
