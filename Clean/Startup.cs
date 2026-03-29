using Clean.Constants;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Clean.Startup))]

namespace Clean
{
    public sealed class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            CookieAuthenticationOptions options = new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString(RouteConstants.LoginPath),
                LogoutPath = new PathString(RouteConstants.LogoutPath),
                ExpireTimeSpan = System.TimeSpan.FromMinutes(30),
                SlidingExpiration = true
            };

            app.UseCookieAuthentication(options);
        }
    }
}
