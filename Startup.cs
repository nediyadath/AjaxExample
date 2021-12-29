using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCAjaxExample.Startup))]
namespace MVCAjaxExample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
