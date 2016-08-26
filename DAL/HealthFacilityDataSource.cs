using System.Collections.Generic;

namespace DAL
{
    /// <summary>
    /// Guidance via: http://www.odata.org/blog/how-to-use-web-api-odata-to-build-an-odata-v4-service-without-entity-framework/
    /// </summary>
    public class HealthFacilityDataSource
    {
        private static HealthFacilityDataSource instance = null;
        public static HealthFacilityDataSource Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HealthFacilityDataSource();
                }
                return instance;
            }
        }

        public List<HealthFacility> Facilities { get; set; }
        private HealthFacilityDataSource()
        {
        }

        public void Initialize(string facilitiesList, string kmlData)
        {
            //read from .txt
            //read from .kml
            var parser = new HTMLFacilityParser();
            Facilities = parser.GetFacilities(facilitiesList);
            var kmlParser = new KMLFacilityParser(kmlData);
            Facilities = kmlParser.AssociateWith(Facilities);
            //read from services (later)
        }
    }
}
