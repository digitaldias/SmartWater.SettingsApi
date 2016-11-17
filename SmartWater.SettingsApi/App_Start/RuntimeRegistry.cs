using SmartWater.Data.Fakes;
using SmartWater.Domain.Core.Contracts.Readers;
using SmartWater.Domain.Core.Contracts.Writers;
using SmartWater.SettingsApi.Readers;
using StructureMap;

namespace SmartWater.SettingsApi.App_Start
{
    public class RuntimeRegistry : Registry
    {
        public RuntimeRegistry()
        {
            Scan(x => {
                x.Assembly("SmartWater.Domain.Core");
                x.Assembly("SmartWater.Business.Core");

                x.Assembly("SmartWater.SettingsApi.Domain");
                x.Assembly("SmartWater.SettingsApi.Business");

                x.Assembly("SmartWater.Data.DocDb");
                x.Assembly("SmartWater.Data.Fakes");

                x.WithDefaultConventions();
            });
            For<IConfigurationReader>().Singleton().Use(new ConfigurationReader());

            //TODO: Replace fakes with actuals ASAP
            For<ILogger>().Singleton().Use<FakeLogger>();
        }
    }
}