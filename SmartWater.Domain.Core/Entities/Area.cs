using Newtonsoft.Json;

namespace SmartWater.Domain.Core.Entities
{
    public class Area
    {
        [JsonProperty("tenantId", Order = 10)]
        public string TenantId { get; set; }

        [JsonProperty("midPoint", Order = 20)]
        public Point MidPoint { get; set; }

        [JsonProperty("zoomLevel", Order = 30)]
        public int ZoomLevel { get; set; }
    }
}
