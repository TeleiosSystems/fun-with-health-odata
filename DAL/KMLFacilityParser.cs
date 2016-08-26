using CsQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class KMLFacilityParser
    {
        public List<Tuple<string, Tuple<double, double>>> Locations { get; set; }
        private PlaceComparer Placer { get; set; }

        public KMLFacilityParser(string kmlSource)
        {
            Locations = ExtractLocations(kmlSource);
            Locations = Locations.OrderBy(place => place.Item1).ToList();
            Placer = new PlaceComparer();
        }

        public List<Tuple<string, Tuple<double, double>>> ExtractLocations(string kmlSource)
        {
            List<Tuple<string, Tuple<double, double>>> locs = new List<Tuple<string, Tuple<double, double>>>();

            CQ dom = kmlSource;
            CQ placemarks = dom["Placemark"];
            foreach (var placemark in placemarks)
            {
                var elements = placemark.ChildElements.ToList();
                var name = string.Empty;
                var point = string.Empty;
                foreach (var elt in elements)
                {
                    if (elt.NodeName.ToLower() == "name")
                        name = elt.FirstChild.NodeValue.Trim();//probably needs to be the text node ...
                    else if (elt.NodeName.ToLower() == "point")
                    {
                        point = elt.FirstElementChild.FirstChild.NodeValue;
                    }
                }
               //     elements[0].FirstChild.NodeValue.Trim();
                
                //var point = wrapped["Point"].FirstElement().Cq()["coordinates"].FirstElement().FirstChild.NodeValue;
                
                point = point.Replace(",0.0", "");
                var idx = point.IndexOf(",");
                var lon = double.Parse(point.Substring(0, idx).Trim());
                var lat = double.Parse(point.Substring(idx + 1).Trim());
                var tuple = new Tuple<string, Tuple<double, double>>(name, new Tuple<double, double>(lat, lon));
                locs.Add(tuple);
            }
            return locs;

        }

        public List<HealthFacility> AssociateWith(List<HealthFacility> facilities)
        {
            foreach (var facility in facilities)
            {
                var pos = Locations.BinarySearch(new Tuple<string, Tuple<double, double>>(facility.Name.Trim(), null), Placer);
                if (pos < 0) continue;
                var loc = Locations[pos];
                facility.Latitude = loc.Item2.Item1;
                facility.Longitude = loc.Item2.Item2;
            }
            return facilities;
        }


        class PlaceComparer : Comparer<Tuple<string, Tuple<double, double>>>
        {
            public override int Compare(Tuple<string, Tuple<double, double>> x, Tuple<string, Tuple<double, double>> y)
            {
                return x.Item1.CompareTo(y.Item1);
            }
        }
    }
}
