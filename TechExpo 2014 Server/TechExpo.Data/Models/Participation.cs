
#region

using System;
using Newtonsoft.Json;

#endregion

namespace TechExpo.Data.Models
{

    public class Participation : BindableBase
    {

        #region Member Variables

        private int _id;
        private Location _location;
        private DateTime? _participationDateTime;
        private Registrant _registrant;
        private bool _optedIn;

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
        
        [JsonProperty(PropertyName = "id")]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetProperty(ref _id, value);
            }
        }

        [JsonProperty(PropertyName = "registrant")]
        public virtual Registrant Registrant
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

        [JsonProperty(PropertyName = "location")]
        public virtual Location Location
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

        [JsonProperty(PropertyName = "participation_date_time")]
        public DateTime? ParticipationDateTime
        {
            get
            {
                return _participationDateTime;
            }
            set
            {
                SetProperty(ref _participationDateTime, value);
            }
        }

        #endregion

    }
}