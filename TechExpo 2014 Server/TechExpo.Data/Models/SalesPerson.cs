
#region

using Newtonsoft.Json;

#endregion

namespace TechExpo.Data.Models
{

    public class SalesPerson : BindableBase
    {

        #region Member Variables

        private Carrier _carrier;
        private int _id;
        private string _name;
        private string _phoneNumber;

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

        [JsonProperty(PropertyName = "carrier")]
        public virtual Carrier Carrier
        {
            get
            {
                return _carrier;
            }
            set
            {
                SetProperty(ref _carrier, value);
            }
        }

        #endregion

    }
}