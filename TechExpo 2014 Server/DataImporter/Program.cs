
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.bluewatertech.common.logging;
using FileHelpers;
using TechExpo.Data;
using TechExpo.Data.Models;

namespace DataImporter
{

    class Program
    {

        private static DataContext _dataContext;
        private static bool ConnectToDatabase()
        {
            var returnValue = false;
            Logger.Instance.LogTrace("Connecting To Database...");

            try
            {
                _dataContext = new DataContext();
                returnValue = true;
                Logger.Instance.LogSuccess("Connected");
            }

            catch (Exception ex)
            {
                Logger.Instance.LogFail("Failed To Connect.");
                Logger.Instance.LogFail(ex);
            }
            return returnValue;
        }

        private static void CreateLogger()
        {
            var root = new DirectoryInfo(".");
            var logFile = Path.Combine(root.FullName,
                                       "Log Files",
                                       string.Format("Log_{0}_{1}.txt",
                                                     DateTime.Now.ToShortDateString()
                                                             .Replace("/", "_"),
                                                     DateTime.Now.ToShortTimeString()
                                                             .Replace(":", "_")));

            if (!Directory.Exists(Path.GetDirectoryName(logFile)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logFile));
            }
            Logger.Instance.SetLogFile(logFile);
            Logger.Instance.LogMessage += Instance_LogMessage;
            Logger.Instance.LogTrace("Logging Started: {0} {1}",
                                     DateTime.Now.ToShortDateString(),
                                     DateTime.Now.ToShortTimeString());
        }

        private static void Instance_LogMessage(object sender, Logger.LogMessageEventArgs e)
        {

            if (e.Exception == null)
            {
                Console.WriteLine("[{0}] [{1}]: {2}", e.TimeStamp, e.Level, e.Message);
            }

            else
            {
                Console.WriteLine("[{0}] [{1}]: {2}", e.TimeStamp, e.Level, e.Message);
                Console.WriteLine(e.Exception.Message);
                Console.WriteLine(e.Exception.ToString());
            }
        }

        private static void ImportRegistrants()
        {
            var engine = new FileHelperEngine(typeof(RegistrantCsv));
            var registrants = engine.ReadFile("registrants.csv") as RegistrantCsv[];

            foreach (var registrantCsv in registrants)
            {
                var existingRegistrant =
                    _dataContext.Registrants.FirstOrDefault(
                                                            r =>
                                                            r.EmailAddress.Equals(registrantCsv.Email,
                                                                                  StringComparison
                                                                                      .InvariantCultureIgnoreCase));

                if (existingRegistrant != null)
                {
                    Logger.Instance.LogWarning("Existing Registrant Found, Skipping...");
                    continue;
                }
                var newRegistrant = new Registrant
                {
                    Company = registrantCsv.Company,
                    EmailAddress = registrantCsv.Email,
                    FirstName = registrantCsv.FirstName,
                    JobTitle = registrantCsv.JobTitle,
                    LastName = registrantCsv.LastName,
                    PhoneNumber = registrantCsv.Phone,
                    WebRegistrationDateTime = registrantCsv.OrderDate,
					SalesPerson = string.Format("{0}, {1}", registrantCsv.SalesLastName.Trim(), registrantCsv.SalesFirstName.Trim()),
                    OnsiteCheckInDateTime = null
                };
                Logger.Instance.LogTrace("Adding: {0} {1}", newRegistrant.FirstName, newRegistrant.LastName);
                _dataContext.Registrants.Add(newRegistrant);
            }
            _dataContext.SaveChanges();
        }

        private static void Main(string[] args)
        {
            CreateLogger();
            ConnectToDatabase();
            ImportRegistrants();
            ImportLocations();
        }

        private static void ImportLocations()
        {
            var engine = new FileHelperEngine(typeof(LocationCsv));
            var locations = engine.ReadFile("Tech Expo - Locations.csv") as LocationCsv[];

            foreach (var location in locations)
            {

                var newLocation = new Location()
                {
                    PointsValue = location.PointsValue,
                    ContactFirstName = location.FirstName,
                    ContactLastName = location.LastName,
                    ContactCompany = location.Company,
                    ContactEmailAddress = location.Email,
                    ContactPhoneNumber = location.Phone,
                    LocationName = location.LocationName

                };

                Logger.Instance.LogTrace("Adding: {0}", newLocation.LocationName);
                _dataContext.Locations.Add(newLocation);
            }
            _dataContext.SaveChanges();
        }
    }
}
