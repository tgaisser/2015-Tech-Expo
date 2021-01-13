#region

using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using com.bluewatertech.common.logging;
using TechExpo.Data;
using TechExpo.Data.Models;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class VendingController : ApiController
    {
        #region Member Variables

        private readonly DataContext _db = new DataContext("TechExpo2014");

        #endregion

        // POST api/vending
        public void Post(string id)
        {
           
            var registrant = _db.Registrants.FirstOrDefault(r => r.Id.ToString()
                                                                  .Equals(id));

            var location = _db.Locations.FirstOrDefault(l => l.LocationName.Equals("Vending"));
            if (location == null)
            {
                location = new Location()
                {
                    ContactFirstName = "Jason",
                    ContactLastName = "Carpenter",
                    ContactCompany = "Bluewater Technologies",
                    ContactEmailAddress = "JCarpenter@bluewatertech.com",
                    LocationName = "Vending",
                    PointsValue = 0
                };

                _db.Locations.Add(location);
            }


            if (registrant == null)
            {
                return;
            }

           // Logger.Instance.LogTrace("Got Vend Request For: {0} {1}", registrant.FirstName, registrant.LastName);

            registrant.HasVended = true;

            var partcipation = new Participation
            {
                Location = location,
                Registrant = registrant,
                ParticipationDateTime = DateTime.Now,
                OptedIn = false
            };

            _db.Participations.Add(partcipation);

            _db.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}