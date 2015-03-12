using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;


[assembly: OwinStartup(typeof(WebEditor.Startup))]
namespace WebEditor
{
     public class Startup
     {
          public void Configuration(IAppBuilder app)
          {
               //app.MapSignalR<BroadcastMessage>("/signalr");

               //app.MapSignalR();

               app.Map("/signalr", map => {

                    /* Allow cross origin request */
                    map.UseCors(CorsOptions.AllowAll);

                    var hubConfiguration = new HubConfiguration {
                         EnableJSONP = true
                    };

                    map.RunSignalR(hubConfiguration);
               });


          }
     }
}