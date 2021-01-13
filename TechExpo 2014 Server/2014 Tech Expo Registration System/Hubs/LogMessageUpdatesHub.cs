#region

using System;
using System.Linq;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using TechExpo.Data;

#endregion

namespace _2014_Tech_Expo_Registration_System.Hubs
{
    [HubName("logMessageUpdatesHub")]
    public class LogMessageUpdatesHub : Hub
    {
        #region Member Variables

        private readonly DataContext _dataContext;

        #endregion

        public LogMessageUpdatesHub()
        {
            _dataContext = new DataContext();
        }

        public void GetAllMessages()
        {
            var logTimeSpan = DateTime.Now.AddHours(-6);
            var logMessages = _dataContext.LogMessages.Where(l => l.TimeStamp >= logTimeSpan)
                                          .ToList();
            Clients.All.updateLogMessages(logMessages);
        }

        protected override void Dispose(bool disposing)
        {
            _dataContext.Dispose();
            base.Dispose(disposing);
        }
    }
}