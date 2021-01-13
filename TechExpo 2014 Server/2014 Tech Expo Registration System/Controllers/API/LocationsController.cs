#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using com.bluewatertech.common.logging;
using TechExpo.Data;
using TechExpo.Data.Models;
using _2014_Tech_Expo_Registration_System.Models;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class LocationsController : ApiController
    {
        #region Member Variables

        private readonly DataContext _db = new DataContext("TechExpo2014");

        #endregion

        // GET api/Location
        public IEnumerable<string> GetLocations()
        {
            //Logger.Instance.LogTrace("Got Request For Updated Locations");
            var results = _db.Locations.OrderBy(l => l.LocationName)
                             .Select(l => l.LocationName)
                             .ToList();

            //Logger.Instance.LogSuccess("Returned {0} Locations", results.Count());
            return results;
        }

        public Location GetLocation(string locationName)
        {
            //Logger.Instance.LogTrace("Got Request For Updated Location");
            Location results = _db.Locations.FirstOrDefault(x => x.LocationName == locationName);

            //Logger.Instance.LogSuccess("Returned {0} Location Id", results.Id);
            return results;
        }

        public Location GetLocation(int id)
        {
            //Logger.Instance.LogTrace("Got Request For Updated Location");
            Location results = _db.Locations.Find(id);

            //Logger.Instance.LogSuccess("Returned {0} Location Id", results.Id);
            return results;
        }

        public Location RegisterLocation(LocationRegistration location)
        {
            if (location == null) return null;

            Location updateMe = new Location();

            if (location.Id > 0)
            {
                // Update Location
                updateMe = _db.Locations.Find(location.Id);
                mapLocation(location, ref updateMe);
            }
            else
            {
                // add new location
                mapLocation(location, ref updateMe);
                _db.Locations.Add(updateMe);
            }
            _db.SaveChanges();
            return _db.Locations.Find(updateMe.Id);
        }

        private void mapLocation(LocationRegistration locationRegistration, ref Location location)
        {
            if (location == null)
            {
                location = new Location();
            }

            location.PointsValue = locationRegistration.PointsValue;
            location.Id = locationRegistration.Id;
            location.ContactFirstName = locationRegistration.ContactFirstName;
            location.ContactLastName = locationRegistration.ContactLastName;
            location.ContactCompany = locationRegistration.ContactCompany;
            location.ContactEmailAddress = locationRegistration.ContactEmailAddress;
            location.ContactPhoneNumber = locationRegistration.ContactPhoneNumber;
            location.LocationName = locationRegistration.LocationName;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}