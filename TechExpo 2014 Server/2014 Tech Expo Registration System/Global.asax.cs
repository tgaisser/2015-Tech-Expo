#region

using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using com.bluewatertech.common.logging;
using Microsoft.AspNet.SignalR;
using TechExpo.Data;
using TechExpo.Data.Models;
using _2014_Tech_Expo_Registration_System.Hubs;

#endregion

namespace _2014_Tech_Expo_Registration_System
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : HttpApplication
    {
        private static readonly DataContext DataContext = new DataContext("TechExpo2014");

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapHttpRoute("DefaultApi",
                                "api/{controller}/{id}",
                                new
                                {
                                    id = RouteParameter.Optional
                                });
            routes.MapRoute("Default",
                            "{controller}/{action}/{id}",
                            new
                            {
                                controller = "Home",
                                action = "Index",
                                id = UrlParameter.Optional
                            });
        }

        protected void Application_Start()
        {
            Logger.Instance.LogMessage += Instance_LogMessage;
            var now = DateTime.Now;
            Logger.Instance.LogTrace("Logging Restarted: {0} {1}", now.ToShortDateString(), now.ToShortTimeString());

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        
        }

        private static void Instance_LogMessage(object sender, Logger.LogMessageEventArgs e)
        {
            var logMessage = new LogMessage
            {
                Level = e.Level.ToString(),
                Message = e.Message,
                TimeStamp = e.TimeStamp
            };
            DataContext.LogMessages.Add(logMessage);
            DataContext.SaveChanges();
            var context = GlobalHost.ConnectionManager.GetHubContext<LogMessageUpdatesHub>();
            context.Clients.All.updateLogMessages(logMessage);
        }
    }
}