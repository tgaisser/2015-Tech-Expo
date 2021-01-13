#region

using Microsoft.Owin;
using Owin;
using _2014_Tech_Expo_Registration_System;

#endregion

[assembly: OwinStartup("HubStartup", typeof (Startup))]

namespace _2014_Tech_Expo_Registration_System
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}