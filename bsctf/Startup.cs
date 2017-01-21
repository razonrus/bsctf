using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bsctf.Startup))]
namespace bsctf
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
