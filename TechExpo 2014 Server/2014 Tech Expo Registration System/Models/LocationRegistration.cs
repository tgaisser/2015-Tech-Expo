#region
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Models
{

    public class LocationRegistration : BindableBase
    {

        #region Member Variables

        private string _contactCompany;
        private string _contactEmailAddress;
        private string _contactFirstName;
        private string _contactLastName;
        private string _contactPhoneNumber;
        private int _id;
        private string _locationName;
        private int _pointsValue;

        #endregion

        #region Properties

        [JsonProperty(PropertyName = "points_value")]
        public int PointsValue
        {
            get
            {
                return _pointsValue;
            }
            set
            {
                SetProperty(ref _pointsValue, value);
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

        [JsonProperty(PropertyName = "contact_first_name")]
        public string ContactFirstName
        {
            get
            {
                return _contactFirstName;
            }
            set
            {
                SetProperty(ref _contactFirstName, value);
            }
        }

        [JsonProperty(PropertyName = "contact_last_name")]
        public string ContactLastName
        {
            get
            {
                return _contactLastName;
            }
            set
            {
                SetProperty(ref _contactLastName, value);
            }
        }

        [JsonProperty(PropertyName = "contact_company")]
        public string ContactCompany
        {
            get
            {
                return _contactCompany;
            }
            set
            {
                SetProperty(ref _contactCompany, value);
            }
        }

        [JsonProperty(PropertyName = "contact_email_address")]
        public string ContactEmailAddress
        {
            get
            {
                return _contactEmailAddress;
            }
            set
            {
                SetProperty(ref _contactEmailAddress, value);
            }
        }

        [JsonProperty(PropertyName = "contact_phone_number")]
        public string ContactPhoneNumber
        {
            get
            {
                return _contactPhoneNumber;
            }
            set
            {
                SetProperty(ref _contactPhoneNumber, value);
            }
        }

        [JsonProperty(PropertyName = "location_name")]
        public string LocationName
        {
            get
            {
                return _locationName;
            }
            set
            {
                SetProperty(ref _locationName, value);
            }
        }

        #endregion

    }
}