using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalRt2.Startup))]

namespace SignalRt2
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app
                .UseNancy()
                .MapSignalR();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
