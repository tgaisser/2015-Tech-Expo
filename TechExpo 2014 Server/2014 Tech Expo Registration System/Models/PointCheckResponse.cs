#region

using Newtonsoft.Json;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Models
{
    public class PointCheckResponse : BindableBase
    {
        private int _currentPoints;
        private string _currentRank;
        private string _firstName;
        private bool _hasVended;

        private string _lastName;
        private string _nextRank;
        private int _pointsToNextRank;

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

        [JsonProperty(PropertyName = "has_vended")]
        public bool HasVended
        {
            get
            {
                return _hasVended;
            }
            set
            {
                SetProperty(ref _hasVended, value);
            }
        }

        [JsonProperty(PropertyName = "current_points")]
        public int CurrentPoints
        {
            get
            {
                return _currentPoints;
            }
            set
            {
                SetProperty(ref _currentPoints, value);
            }
        }

        [JsonProperty(PropertyName = "points_to_next_rank")]
        public int PointsToNextRank
        {
            get
            {
                return _pointsToNextRank;
            }
            set
            {
                SetProperty(ref _pointsToNextRank, value);
            }
        }

        [JsonProperty(PropertyName = "current_rank")]
        public string CurrentRank
        {
            get
            {
                return _currentRank;
            }
            set
            {
                SetProperty(ref _currentRank, value);
            }
        }

        [JsonProperty(PropertyName = "next_rank")]
        public string NextRank
        {
            get
            {
                return _nextRank;
            }
            set
            {
                SetProperty(ref _nextRank, value);
            }
        }
    }
}