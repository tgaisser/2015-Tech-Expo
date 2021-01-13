#region

using System;
using Newtonsoft.Json;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Models
{
    public class VisitScan : BindableBase
    {
        #region Member Variables

        private string _location;
        private bool _optedIn;
        private string _registrant;
        private DateTime _participationDateTime;

        #endregion

        #region Properties

        [JsonProperty(PropertyName = "opted_in")]
        public bool OptedIn
        {
            get
            {
                return _optedIn;
            }
            set
            {
                SetProperty(ref _optedIn, value);
            }
        }

        [JsonProperty(PropertyName = "location")]
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                SetProperty(ref _location, value);
            }
        }

        [JsonProperty(PropertyName = "registrant")]
        public string Registrant
        {
            get
            {
                return _registrant;
            }
            set
            {
                SetProperty(ref _registrant, value);
            }
        }

        [JsonProperty(PropertyName = "participation_datetime")]
        public DateTime ParticipationDateTime
        {
            get { return _participationDateTime; }
            set { _participationDateTime = value; }
        }

        #endregion
    }
}