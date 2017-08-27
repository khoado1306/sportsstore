using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartup(typeof(WebUI.App_Start.Startup))]

namespace WebUI.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            CookieAuthenticationOptions options = new CookieAuthenticationOptions();
            options.AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie;
            options.LoginPath = new PathString("/Account/Login");
            app.UseCookieAuthentication(options);
        }
    }
}
