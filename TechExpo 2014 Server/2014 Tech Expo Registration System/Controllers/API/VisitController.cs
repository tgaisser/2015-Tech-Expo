#region

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using com.bluewatertech.common.logging;
using TechExpo.Data;
using TechExpo.Data.Models;
using _2014_Tech_Expo_Registration_System.Models;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class VisitController : ApiController
    {
        private readonly DataContext _db = new DataContext("TechExpo2014");

        // POST api/visit
        public VisitScanResponse Post(VisitScan value)
        {

            //Logger.Instance.LogTrace("Got VisitScan");
            if (value == null)
            {
               // Logger.Instance.LogError("Got Empty VisitScan");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                                            "Empty Registrant"));
            }
            if (string.IsNullOrEmpty(value.Registrant))
            {
               // Logger.Instance.LogError("Got Empty Registrant Id");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                                            "Empty Registrant Id"));
            }
            if (string.IsNullOrEmpty(value.Location))
            {
               // Logger.Instance.LogError("Got Empty Location");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Empty Location"));
            }

            int id;

            if (!int.TryParse(value.Registrant, out id))
            {
                //Logger.Instance.LogError("Got Invalid Registrant");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                                            "Invalid Registrant Id"));
            }

            var registrant = _db.Registrants.FirstOrDefault(r => r.Id == id);

            if (registrant == null)
            {
               // Logger.Instance.LogError("Registrant Not Found");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                                            "Registrant Not Found"));
            }

            var location = _db.Locations.FirstOrDefault(l => l.LocationName.Equals(value.Location));

            if (location == null)
            {
                //Logger.Instance.LogError("Location Not Found");
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest,
                                                                            "Location Not Found"));
            }

            var alreadyVisited = false;
            var lastName = registrant.LastName;
            var pointsAwarded = 0;

            var oldRank = RankUtilities.GetRank(registrant);

            var previousVisitToLocation =
                registrant.Participations.FirstOrDefault(p => p.Location.LocationName.Equals(value.Location));

            if (previousVisitToLocation != null)
            {
                alreadyVisited = true;
                //Update OptIn
                previousVisitToLocation.OptedIn = value.OptedIn;
            }
            else
            {
                DateTime participationTime = DateTime.Now;
                if (value.ParticipationDateTime != null)
                {
                    participationTime = value.ParticipationDateTime;
                }
                previousVisitToLocation = new Participation
                {
                    Location = location,
                    OptedIn = value.OptedIn,
                    ParticipationDateTime = participationTime,
                    Registrant = registrant
                };
                pointsAwarded = location.PointsValue;
                registrant.Participations.Add(previousVisitToLocation);
            }

            _db.SaveChanges();

            var rank = RankUtilities.GetRank(registrant);

            var response = new VisitScanResponse
            {
                AlreadyVisited = alreadyVisited,
                LastName = lastName,
                PointsAwarded = pointsAwarded,
                Rank = rank.ToString(),
                AchievedNewRank = !oldRank.Equals(rank),
                EmailAddress = registrant.EmailAddress,
                FirstName = registrant.FirstName
            };

            return response;
        }


        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}