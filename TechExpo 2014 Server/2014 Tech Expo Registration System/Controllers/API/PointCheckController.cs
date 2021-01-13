#region

using System.Linq;
using System.Web.Http;
using com.bluewatertech.common.logging;
using TechExpo.Data;
using _2014_Tech_Expo_Registration_System.Models;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class PointCheckController : ApiController
    {
        #region Member Variables

        private readonly DataContext _db = new DataContext("TechExpo2014");

        #endregion

        // GET api/pointcheck/5
        public PointCheckResponse Get(string id)
        {
            var result = new PointCheckResponse();
            
           // Logger.Instance.LogTrace("Got PointCheckRequest: {0}", id);

            var registrant = _db.Registrants.FirstOrDefault(r => r.Id.ToString()
                                                                  .Equals(id));

            if (registrant == null)
            {
                //Logger.Instance.LogWarning("Did Not Find A Registrant For {0}", id);
                return null;
            }

            var currentRank = RankUtilities.GetRank(registrant);

            result.CurrentPoints = registrant.Participations.Sum(participation => participation.Location.PointsValue);
            result.CurrentRank = currentRank.ToString();
            result.FirstName = registrant.FirstName;
            result.HasVended = registrant.HasVended;
            result.LastName = registrant.LastName;
            result.NextRank = RankUtilities.GetNextRank(currentRank)
                                           .ToString();
            result.PointsToNextRank = RankUtilities.PointsToNextRank(currentRank, result.CurrentPoints);

            return result;
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}