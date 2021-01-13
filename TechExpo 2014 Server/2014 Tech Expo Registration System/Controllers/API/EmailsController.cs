#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using com.bluewatertech.common.logging;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class EmailsController : ApiController
    {
        private readonly DataContext _db = new DataContext("TechExpo2014");

        // GET api/emails
        public IEnumerable<string> Get()
        {
            //Logger.Instance.LogTrace("Got Request For Updated Registrants List");
            var results = _db.Registrants.OrderBy(s => s.EmailAddress)
                             .Select(emailAddress => emailAddress.EmailAddress)
                             .ToList();

           //Logger.Instance.LogSuccess("Returned {0} Registrants", results.Count());
            return results.ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}