using SmartWater.SettingsApi.App_Start;
using StructureMap;
using System.Web.Http;

namespace SmartWater.SettingsApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static Container IocContainer = new Container(new RuntimeRegistry());

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //TODO: Get this working
            // GlobalConfiguration.Configuration.UseStructureMap<RuntimeRegistry>();
        }

        
    }
}
