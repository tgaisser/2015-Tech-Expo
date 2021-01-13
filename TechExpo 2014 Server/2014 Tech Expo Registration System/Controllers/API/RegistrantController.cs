#region

using System;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.Http;
using com.bluewatertech.common.logging;
using com.bluewatertech.common.utilities;
using TechExpo.Data;
using TechExpo.Data.Models;

#endregion

namespace _2014_Tech_Expo_Registration_System.Controllers.API
{
    public class RegistrantController : ApiController
    {
        #region Member Variables

        private readonly DataContext _db = new DataContext("TechExpo2014");

        #endregion

        // GET api/registrant/tgaisser@bluewt.com
        public Registrant GetRegistrant(string id)
        {
            Logger.Instance.LogTrace("Got Request To Lookup User: {0}", id);
            var registrant =
                _db.Registrants.FirstOrDefault(
                                               r =>
                                               r.EmailAddress.Equals(id, StringComparison.InvariantCultureIgnoreCase)) ??
                new Registrant();
            registrant.EmailAddress = id;
            
            return registrant;
        }

        // POST api/registrant
        public int PostRegistrant(Registrant value)
        {
            value.PrintPassStatus = "NEEDS PRINTING";

            if (value.Id == 0)
            {
                Logger.Instance.LogTrace("Adding Registrant: {0} {1}", value.FirstName, value.LastName);
                _db.Registrants.Add(value);
            }

            else
            {
                Logger.Instance.LogTrace("Updating Registrant: {0} {1}", value.FirstName, value.LastName);
                _db.Entry(value)
                   .State = EntityState.Modified;
            }
            _db.SaveChanges();

            if (value.SalesPerson != null && value.SalesPerson != "<NONE>")
            {
                SendSms(value);
            }

            return value.Id;
        }

        private static void SendSms(Registrant value)
        {
            new Thread(new ThreadStart(delegate
            {
                Thread.CurrentThread.IsBackground = true;

                try
                {
                    var db = new DataContext("TechExpo2014");

                    var threadSafeRegistrant = value;

                    var emailMessage = new EmailMessage();
                    var salesPerson = db.SalesPeople.FirstOrDefault(
                                                   s =>
                                                   s.Name.Equals(threadSafeRegistrant.SalesPerson,
                                                                 StringComparison.InvariantCultureIgnoreCase));
                    if (salesPerson == null)
                    {
                        return;
                    }

                    emailMessage.Recipients.Add(string.Format(salesPerson.Carrier.Email, salesPerson.PhoneNumber));

                    var message = new StringBuilder(1000);

                    message.AppendLine(string.Format("{0} {1}", threadSafeRegistrant.FirstName, threadSafeRegistrant.LastName));
                    message.AppendLine("The");
                    message.AppendLine(threadSafeRegistrant.JobTitle);
                    message.AppendLine("From");
                    message.AppendLine(threadSafeRegistrant.Company);
                    message.AppendLine("Has Just Checked In!");
                    message.AppendLine(DateTime.Now.ToShortTimeString());

                    emailMessage.Body = message.ToString();
                    string lastError;
                    emailMessage.Send(out lastError);
                    Logger.Instance.LogSuccess("Email Message Sent To: {0}", threadSafeRegistrant.SalesPerson);
                }
                catch (Exception ex)
                {
                    
                   Logger.Instance.LogError("Error Sending Email: {0}", ex.Message);
                }


            })).Start();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
    }
}