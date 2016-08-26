using DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.OData;

namespace Odata.Controllers
{
    [EnableQuery]
    public class HealthFacilityController: ODataController
    {
        public async Task<IHttpActionResult> Get()
        {
            var client = new HttpClient();
            var tablesPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/HealthFacilitiesList.txt");
            var kmlPath = System.Web.Hosting.HostingEnvironment.MapPath("~/Content/HealthFacilities.kml.txt");

            var tables = File.ReadAllText(tablesPath);
            var kml = File.ReadAllText(kmlPath);
            HealthFacilityDataSource.Instance.Initialize(tables, kml);
            return Ok(HealthFacilityDataSource.Instance.Facilities.AsQueryable());
        }
    }
}