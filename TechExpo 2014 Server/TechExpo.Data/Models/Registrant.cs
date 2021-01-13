#region

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

#endregion

namespace TechExpo.Data.Models
{
    public class Registrant : BindableBase
    {
        #region Member Variables

        private string _company;
        private string _emailAddress;
        private string _firstName;
        private bool _hasVended;
        private int _id;
        private string _jobTitle;
        private string _lastName;
        private string _nfcId;
        private DateTime? _onsiteCkeckInDateTime;
        private List<Participation> _participations;
        private string _phoneNumber;
        private string _printPassStatus;
        private string _salesPerson;
        private DateTime? _webRegistrationDateTime;

        #endregion

        public Registrant()
        {
            // WebRegistrationDateTime = DateTime.Now;
            OnsiteCheckInDateTime = DateTime.Now;
        }

        #region Properties

        [JsonProperty(PropertyName = "print_pass_status")]
        public string PrintPassStatus
        {
            get
            {
                return _printPassStatus;
            }
            set
            {
                SetProperty(ref _printPassStatus, value);
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


        [JsonProperty(PropertyName = "sales_person")]
        public string SalesPerson
        {
            get
            {
                return _salesPerson;
            }
            set
            {
                SetProperty(ref _salesPerson, value);
            }
        }

        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                SetProperty(ref _phoneNumber, value);
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

        [JsonProperty(PropertyName = "company")]
        public string Company
        {
            get
            {
                return _company;
            }
            set
            {
                SetProperty(ref _company, value);
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

        [JsonProperty(PropertyName = "job_title")]
        public string JobTitle
        {
            get
            {
                return _jobTitle;
            }
            set
            {
                SetProperty(ref _jobTitle, value);
            }
        }

        [JsonProperty(PropertyName = "nfcid")]
        public string NfcId
        {
            get
            {
                return _nfcId;
            }
            set
            {
                SetProperty(ref _nfcId, value);
            }
        }

        [JsonProperty(PropertyName = "web_registration_date_time")]
        public DateTime? WebRegistrationDateTime
        {
            get
            {
                return _webRegistrationDateTime;
            }
            set
            {
                SetProperty(ref _webRegistrationDateTime, value);
            }
        }

        [JsonProperty(PropertyName = "onsite_check_in_date_time")]
        public DateTime? OnsiteCheckInDateTime
        {
            get
            {
                return _onsiteCkeckInDateTime;
            }
            set
            {
                SetProperty(ref _onsiteCkeckInDateTime, value);
            }
        }

        [JsonIgnore]
        public virtual List<Participation> Participations
        {
            get
            {
                return _participations ?? (_participations = new List<Participation>());
            }
            set
            {
                SetProperty(ref _participations, value);
            }
        }

        #endregion
    }
}