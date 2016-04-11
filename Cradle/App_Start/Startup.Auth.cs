using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Newtonsoft.Json.Linq;
using Owin;
using System.Collections.Generic;
using System.Security.Claims;

namespace Cradle
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
               appId: "1525940844377890",
               appSecret: "9fe0027a6015e6dd72861486bef8f74c");

           GoogleOAuth2AuthenticationOptions options = new GoogleOAuth2AuthenticationOptions()
           {
                ClientId = "382069421278-bbt295b12rtse6il2lstekf314ecvakb.apps.googleusercontent.com",
                ClientSecret = "mAo_Z5jH2C2LUI9m4pXzwPym"
           };


           options.Provider = new GoogleOAuth2AuthenticationProvider()
           {
               OnAuthenticated = async context =>
                   {
                       Dictionary<string,string> values = new Dictionary<string,string>();

                       foreach (var claim in context.User)
                       {
                           if(claim.Key == "emails")
                           {
                               values.Add("email", claim.Value[0]["value"].ToString());
                           }
                           else if (claim.Key == "name")
                           {
                               values.Add("familyName", claim.Value["familyName"].ToString());
                               values.Add("givenName", claim.Value["givenName"].ToString());
                           }

                           foreach(var value in values)
                           {
                               if (!context.Identity.HasClaim(value.Key, value.Value))
                               {
                                   Claim userClaim = new Claim(value.Key, value.Value);
                                   context.Identity.AddClaim(userClaim);
                               }
                           }
                           
                       }
                   }

           };


            app.UseGoogleAuthentication( options
                // clientId: "382069421278-bbt295b12rtse6il2lstekf314ecvakb.apps.googleusercontent.com",
                //clientSecret: "mAo_Z5jH2C2LUI9m4pXzwPym",
                
            );
        }
    }
}