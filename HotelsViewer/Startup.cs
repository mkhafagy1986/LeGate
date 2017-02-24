using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelsViewer.Startup))]
namespace HotelsViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
