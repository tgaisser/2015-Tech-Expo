#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using com.bluewatertech.common.logging;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class SalesPeopleController : ApiController
    {
        private readonly DataContext _db = new DataContext("TechExpo2014");

        // GET api/salespeople
        public IEnumerable<string> Get()
        {
            //Logger.Instance.LogTrace("Got Request For Updated Sales Person List");
            var results = _db.SalesPeople.OrderBy(s => s.Name)
                             .Select(salesPerson => salesPerson.Name)
                             .ToList();
            results.Insert(0, "<NONE>");

           // Logger.Instance.LogSuccess("Returned {0} Sales People", results.Count());
            return results.ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}