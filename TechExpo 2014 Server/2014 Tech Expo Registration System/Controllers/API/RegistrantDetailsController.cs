#region

using System.Linq;
using System.Web.Http;
using com.bluewatertech.common.logging;
using TechExpo.Data;
using TechExpo.Data.Models;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class RegistrantDetailsController : ApiController
    {
        #region Member Variables

        private readonly DataContext _db = new DataContext("TechExpo2014");

        #endregion

        public Registrant Get(string id)
        {
           // Logger.Instance.LogTrace("Got Request To Lookup User Details: {0}", id);
            var result = _db.Registrants.FirstOrDefault(r => r.Id.ToString()
                                                              .Equals(id));

            if (result == null)
            {
              //  Logger.Instance.LogWarning("Did Not Find Registrant Details For {0}", id);
            }
            else
            {
                
            }

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}