#region

using Newtonsoft.Json;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Models
{
    public class VisitScanResponse : BindableBase
    {
        #region Member Variables

        private bool _achievedNewRank;
        private bool _alreadyVisited;
        private string _emailAddress;
        private string _firstName;
        private string _lastName;
        private int _pointsAwarded;
        private string _rank;

        #endregion

        #region Properties

        [JsonProperty(PropertyName = "achieved_new_rank")]
        public bool AchievedNewRank
        {
            get
            {
                return _achievedNewRank;
            }
            set
            {
                SetProperty(ref _achievedNewRank, value);
            }
        }


        [JsonProperty(PropertyName = "first_name")]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                SetProperty(ref _firstName, value);
            }
        }


        [JsonProperty(PropertyName = "last_name")]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        [JsonProperty(PropertyName = "points_awarded")]
        public int PointsAwarded
        {
            get
            {
                return _pointsAwarded;
            }
            set
            {
                SetProperty(ref _pointsAwarded, value);
            }
        }

        [JsonProperty(PropertyName = "already_visited")]
        public bool AlreadyVisited
        {
            get
            {
                return _alreadyVisited;
            }
            set
            {
                SetProperty(ref _alreadyVisited, value);
            }
        }

        [JsonProperty(PropertyName = "rank")]
        public string Rank
        {
            get
            {
                return _rank;
            }
            set
            {
                SetProperty(ref _rank, value);
            }
        }


        [JsonProperty(PropertyName = "email_address")]
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                SetProperty(ref _emailAddress, value);
            }
        }

        #endregion
    }
}