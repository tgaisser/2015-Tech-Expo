#region

using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Hubs
{
    [HubName("activityUpdatesHub")]
    public class ActivityUpdatesHub : Hub
    {
        public void GetCurrentCounts()
        {
            SentActivityUpdate();
        }

        public static void SentActivityUpdate()
        {
            var dataContext = new DataContext();
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ActivityUpdatesHub>();
            var counts = new ActivityCounts
            {
                TotalRegistrants = dataContext.Registrants.Count(),
                CheckedInRegistrants = dataContext.Registrants.Count(r => r.OnsiteCheckInDateTime != null),
                TotalBoothVisits = dataContext.Participations.Count()
            };
            hubContext.Clients.All.activityUpdate(counts);
        }
    }

    internal class ActivityCounts
    {
        [JsonProperty(PropertyName = "total_registrants")]
        public int TotalRegistrants
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "checked_in_registrants")]
        public int CheckedInRegistrants
        {
            get;
            set;
        }

        [JsonProperty(PropertyName = "total_booth_visits")]
        public int TotalBoothVisits
        {
            get;
            set;
        }
    }
}