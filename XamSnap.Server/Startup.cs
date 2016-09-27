using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(XamSnap.Server.Startup))]

namespace XamSnap.Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}