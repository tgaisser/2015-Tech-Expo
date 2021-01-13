
#region

using System;
using Newtonsoft.Json;

#endregion

namespace TechExpo.Data.Models
{

    public class LogMessage : BindableBase
    {

        #region Member Variables

        private int _id;
        private string _level;
        private string _message;
        private DateTime _timeStamp;

        #endregion

        #region Properties

        //[JsonProperty(PropertyName = "id")]
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

        //[JsonProperty(PropertyName = "message")]
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                SetProperty(ref _message, value);
            }
        }

        //[JsonProperty(PropertyName = "level")]
        public string Level
        {
            get
            {
                return _level;
            }
            set
            {
                SetProperty(ref _level, value);
            }
        }

        //[JsonProperty(PropertyName = "time_stamp")]
        public DateTime TimeStamp
        {
            get
            {
                return _timeStamp;
            }
            set
            {
                SetProperty(ref _timeStamp, value);
            }
        }

        #endregion

    }
}