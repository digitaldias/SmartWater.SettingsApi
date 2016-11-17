using Newtonsoft.Json;

namespace SmartWater.Domain.Core.Entities
{
    public class Tenant
    {
        [JsonProperty("id", Order = 10)]
        public string Id { get; set; }

        [JsonProperty("name", Order = 20)]

        public string Name { get; set; }

        [JsonProperty("updatePreference", Order = 30)]

        public string UpdatePreference { get; set; }

        [JsonProperty("area", Order = 40)]
        public Area Area { get; set; }
    }
}
