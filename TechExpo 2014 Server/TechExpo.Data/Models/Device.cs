
#region

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

#endregion

namespace TechExpo.Data.Models
{

    public class Device : BindableBase
    {

        #region Member Variables

        private string _id;
        private Location _location;
        private DateTime? _registeredDateTime;

        #endregion

        #region Properties

        [JsonProperty(PropertyName = "id"), Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id
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

        [JsonProperty(PropertyName = "registered_date_time")]
        public DateTime? RegisteredDateTime
        {
            get
            {
                return _registeredDateTime;
            }
            set
            {
                SetProperty(ref _registeredDateTime, value);
            }
        }

        #endregion

    }
}