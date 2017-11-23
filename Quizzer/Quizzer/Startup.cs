using Microsoft.Owin;
using Owin;
using Quizzer;

[assembly: OwinStartup(typeof(Startup))]
namespace Quizzer
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