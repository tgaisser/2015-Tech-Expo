
#region

using Newtonsoft.Json;

#endregion

namespace TechExpo.Data.Models
{

    public class Carrier : BindableBase
    {

        #region Member Variables

        private string _email;
        private int _id;
        private string _name;

        #endregion

        #region Properties

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

        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                SetProperty(ref _name, value);
            }
        }

        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                SetProperty(ref _email, value);
            }
        }

        #endregion

    }
}